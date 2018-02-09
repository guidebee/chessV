
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG

This file is part of ChessV.  ChessV is free software; you can redistribute
it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, 
or (at your option) any later version.

ChessV is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or 
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for 
more details; the file 'COPYING' contains the License text, but if for
some reason you need a copy, please visit <http://www.gnu.org/licenses/>.

****************************************************************************/

using System;
using System.Collections.Generic;

namespace ChessV.Games.Rules
{
	public class EnPassantRule: Rule
	{
		// *** PROPERTIES *** //

		public PieceType PawnType { get; private set; }
		public int NDirection { get; private set; }


		// *** CONSTRUCTION *** //

		public EnPassantRule( PieceType pawnType, int nDirection )
		{
			PawnType = pawnType;
			NDirection = nDirection;
		}

		public EnPassantRule( EnPassantRule templateRule )
		{
			PawnType = templateRule.PawnType;
			NDirection = templateRule.NDirection;
			Initialize( templateRule.Game );
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			hashKeyIndex = game.HashKeys.TakeKeys( game.Board.NumSquaresExtended );
			epSquares = new int[Game.MAX_DEPTH];
			for( int x = 0; x < Game.MAX_DEPTH; x++ )
				epSquares[x] = 0;
			gameHistory = new int[Game.MAX_GAME_LENGTH];
			gameHistory[0] = 0;
			List<int> attackdirs = new List<int>();
			MoveCapability[] moveCapabilities;
			int nMoveCapabilities = PawnType.GetMoveCapabilities( out moveCapabilities );
			for( int nMoveCap = 0; nMoveCap < nMoveCapabilities; nMoveCap++ )
			{
				MoveCapability move = moveCapabilities[nMoveCap];
				if( move.CanCapture )
					attackdirs.Add( move.NDirection );
			}
			nAttackDirections = attackdirs.Count;
			attackDirections = new int[game.NumPlayers, nAttackDirections];
			for( int ndir = 0; ndir < nAttackDirections; ndir++ )
				for( int player = 0; player < game.NumPlayers; player++ )
					attackDirections[player, ndir] = game.PlayerDirection( player, attackdirs[ndir] );

			//	Hook up MoveBeingPlayed event handler
			game.MoveBeingPlayed += MoveBeingPlayedHandler;
		}

		public override void ClearGameState()
		{
			for( int x = 0; x < Game.MAX_DEPTH; x++ )
				epSquares[x] = 0;
			for( int x = 0; x < Game.MAX_GAME_LENGTH; x++ )
				gameHistory[x] = 0;
		}


		// *** OVERRIDES *** //

		public override ulong GetPositionHashCode( int ply )
		{
			int epSquare = ply == 1 ? gameHistory[Game.GameMoveNumber] : epSquares[ply - 1];
			return HashKeys.Keys[hashKeyIndex + epSquare];
		}

		public override void PositionLoaded( FEN fen )
		{
			string ep = fen["en-passant"];
			if( ep == "-" )
				epSquares[0] = -1;
			else
				epSquares[0] = Game.NotationToSquare( ep );
		}

		public override void SavePositionToFEN( FEN fen )
		{
			int epSquare = (Game.GameMoveNumber == 0 ? 0 : gameHistory[Game.GameMoveNumber-1]);
			if( epSquare > 0 )
				fen["en-passant"] = Game.GetSquareNotation( epSquare );
			else
				fen["en-passant"] = "-";
		}

		public override void SetDefaultsInFEN( FEN fen )
		{
			if( fen["en-passant"] == "#default" )
				fen["en-passant"] = "-";
		}

		public void MoveBeingPlayedHandler( MoveInfo move )
		{
			gameHistory[Game.GameMoveNumber] = epSquares[1];
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			epSquares[ply] = 0;
			if( ply == 1 )
				gameHistory[Game.GameMoveNumber] = 0;
			//	if the current side to move is also the next side to move then 
			//	we return here - en passant is not possible so we would not 
			//	want to set the ep square
			if( Game.CurrentSide == Game.NextSide )
				return MoveEventResponse.NotHandled;
			if( move.PieceMoved != null && move.PieceMoved.PieceType == PawnType )
				if( Board.GetDistance( move.FromSquare, move.ToSquare ) > 1 )
				{
					//	check to see if there are any pawn attackers - even if a pawn makes a 
					//	multi-step move, we don't set the e.p. square unless there is a pawn 
					//	that can actually take it.  this is an important consideration because of 
					//	the hashing/matching of board positions.  we don't want an identical board 
					//	position to be considered different just because a pawn made a multi-step 
					//	move - the fact that a multi-step move was made is only significant because 
					//	of the availability of an e.p. capture
					int epsquare = Board.NextSquare( Game.PlayerDirection( move.Player, NDirection ), move.FromSquare );

					//	we loop here to accomodate large-board games where a pawn can make a move 
					//	of more than two steps and still be captured e.p. on any square passed over
					while( Board[epsquare] == null )
					{
						for( int ndir = 0; ndir < nAttackDirections; ndir++ )
						{
							int nextSquare = Board.NextSquare( attackDirections[move.Player, ndir], epsquare );
							if( nextSquare >= 0 )
							{
								Piece piece = Board[nextSquare];
								if( piece != null && piece.PieceType == PawnType && piece.Player != move.Player )
								{
									//	en passant capture is possible
									epSquares[ply] = epsquare;
									if( ply == 1 )
										gameHistory[Game.GameMoveNumber] = epsquare;
									return MoveEventResponse.MoveOk;
								}
							}
						}
						epsquare = Board.NextSquare( Game.PlayerDirection( move.Player, NDirection ), epsquare );
					}
				}
			return MoveEventResponse.NotHandled;
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			int epSquare = ply == 1 
				? (Game.GameMoveNumber == 0 ? 0 : gameHistory[Game.GameMoveNumber-1])
				: epSquares[ply-1];
			if( epSquare > 0 )
			{
				int nd = Game.PlayerDirection( Game.CurrentSide ^ 1, NDirection );
				int sq = Board.NextSquare( nd, epSquare );
				Piece pawn = Board[sq];
				for( int ndir = 0; ndir < nAttackDirections; ndir++ )
				{
					int nextSquare = Board.NextSquare( attackDirections[Game.CurrentSide ^ 1, ndir], epSquare );
					if( nextSquare >= 0 )
					{
						Piece piece = Board[nextSquare];
						if( piece != null && piece.PieceType == PawnType && piece.Player == Game.CurrentSide )
						{
							//	find square of pawn being captured.  we may have to make several 
							//	steps for large-board games where pawns make more than two steps 
							//	and can still be captured e.p.
							int captureSquare = Board.NextSquare( nd, epSquare );
							while( Board[captureSquare] == null )
								captureSquare = Board.NextSquare( nd, captureSquare );

							//	this piece can capture en passant 
							list.BeginMoveAdd( MoveType.EnPassant, nextSquare, epSquare );
							list.AddPickup( nextSquare );
							list.AddPickup( captureSquare );
							list.AddDrop( piece, epSquare, null );
							list.EndMoveAdd( 120 );
						}
					}
				}
			}
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected int[] epSquares;
		protected int[] gameHistory;
		protected int[,] attackDirections;
		protected int nAttackDirections;
		protected int hashKeyIndex;
	}
}
