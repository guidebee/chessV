
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
	public class CastlingRule: Rule
	{
		const int MAX_CASTLING_MOVES = 16;

		protected struct CastlingMove
		{
			public int KingFromSquare;
			public int KingToSquare;
			public int OtherFromSquare;
			public int OtherToSquare;
			public int RequiredPriv;
			public char PrivChar;

			public CastlingMove( int kingfrom, int kingto, int otherfrom, int otherto, int priv, char privChar )
			{ KingFromSquare = kingfrom; KingToSquare = kingto; OtherFromSquare = otherfrom; OtherToSquare = otherto; RequiredPriv = priv; PrivChar = privChar; }
		}


		// *** CONSTRUCTION *** //

		public CastlingRule()
		{
			privLookup = new Dictionary<char, int>();
		}

		public CastlingRule( CastlingRule template )
		{
			Initialize( template.Game );
			privLookup = template.privLookup;
			allPrivsPerPlayer = template.allPrivsPerPlayer;
			allPrivString = template.allPrivString;
			nextPriv = template.nextPriv;
			castlingMoves = template.castlingMoves;
			nCastlingMoves = template.nCastlingMoves;
		}


		// *** INITIALIZATION *** //

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			castlingMoves = new CastlingMove[game.NumPlayers, MAX_CASTLING_MOVES];
			nCastlingMoves = new int[game.NumPlayers];
			searchStackPrivs = new int[Game.MAX_DEPTH];
			gameHistoryPrivs = new int[Game.MAX_GAME_LENGTH];
			allPrivsPerPlayer = new int[game.NumPlayers];
			allPrivString = "";
			nextPriv = 1;
			privEraseMap = new int[game.Board.NumSquaresExtended];

			//	Hook up MoveBeingPlayed event handler
			game.MoveBeingPlayed += MovePlayedHandler;
		}

		public override void PostInitialize()
		{
			hashKeyIndex = Game.HashKeys.TakeKeys( nextPriv - 1 );
			hasCheckmateRule = Game.FindRule( typeof( CheckmateRule ) ) != null;
			for( int square = 0; square < Game.Board.NumSquares; square++ )
			{
				int flagvalue = -1;
				for( int player = 0; player < Game.NumPlayers; player++ )
				{
					for( int move = 0; move < nCastlingMoves[player]; move++ )
					{
						if( castlingMoves[player, move].KingFromSquare == square ||
							castlingMoves[player, move].OtherFromSquare == square )
							//	moving from this square erases this castling priv
							flagvalue = flagvalue & ~castlingMoves[player, move].RequiredPriv;
					}
				}
				privEraseMap[square] = flagvalue;
			}
			for( int square = Game.Board.NumSquares; square < Game.Board.NumSquaresExtended; square++ )
				privEraseMap[square] = -1;
		}

		public override void ClearGameState()
		{
			for( int x = 0; x < Game.MAX_DEPTH; x++ )
				searchStackPrivs[x] = 0;
			for( int x = 0; x < Game.MAX_GAME_LENGTH; x++ )
				gameHistoryPrivs[x] = 0;
		}


		// *** OPERATIONS *** //

		public void AddCastlingMove( int player, int kingfrom, int kingto, int otherfrom, int otherto, char privChar )
		{
			int priv = 0;
			if( privLookup.ContainsKey( privChar ) )
				priv = privLookup[privChar];
			else
			{
				priv = nextPriv;
				nextPriv = nextPriv << 1;
				privLookup.Add( privChar, priv );
			}
			if( allPrivString.IndexOf( privChar ) < 0 )
				allPrivString += privChar;
			allPrivsPerPlayer[player] |= priv;
			castlingMoves[player, nCastlingMoves[player]++] = new CastlingMove( kingfrom, kingto, otherfrom, otherto, priv, privChar );
		}


		// *** OVERRIDES *** //

		public override ulong GetPositionHashCode( int ply )
		{
			int castlingPriv = ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber + 1] : searchStackPrivs[ply - 1];
			return HashKeys.Keys[hashKeyIndex + castlingPriv];
		}

		public override void SetDefaultsInFEN( FEN fen )
		{
			if( fen["castling"] == "#default" )
				fen["castling"] = allPrivString;
		}

		public override void PositionLoaded( FEN fen )
		{
			searchStackPrivs[0] = 0;
			string castling = fen["castling"];
			if( castling != "-" )
			{
				foreach( char c in castling )
					if( privLookup.ContainsKey( c ) )
						searchStackPrivs[0] |= privLookup[c];
					else
						throw new Exceptions.FENParseFailureException( "castling", fen["castling"],
							"Invalid character in FEN castling privileges: " + c );
			}
			gameHistoryPrivs[Game.GameMoveNumber] = searchStackPrivs[0];
		}

		public override void SavePositionToFEN( FEN fen )
		{
			int castlingPriv = gameHistoryPrivs[Game.GameMoveNumber];
			string privString = "";
			foreach( var pair in privLookup )
				if( (pair.Value & castlingPriv) != 0 )
					privString += pair.Key;
			if( privString == "" )
				privString = "-";
			fen["castling"] = privString;
		}

		public void MovePlayedHandler( MoveInfo move )
		{
			gameHistoryPrivs[Game.GameMoveNumber] = searchStackPrivs[1];
			searchStackPrivs[0] = searchStackPrivs[1];
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			searchStackPrivs[ply] = 
				(ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber] : searchStackPrivs[ply - 1]) & 
				privEraseMap[move.FromSquare];
			if( ply == 1 )
				gameHistoryPrivs[Game.GameMoveNumber + 1] = searchStackPrivs[1];
			return MoveEventResponse.MoveOk;
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( !capturesOnly )
			{
				int castlingPriv = ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber] : searchStackPrivs[ply - 1];
				for( int x = 0; x < nCastlingMoves[Game.CurrentSide]; x++ )
				{
					if( (castlingMoves[Game.CurrentSide, x].RequiredPriv & castlingPriv) != 0 )
					{
						//	find the left-most square that needs to be free
						int minSquare = castlingMoves[Game.CurrentSide, x].KingFromSquare;
						minSquare = castlingMoves[Game.CurrentSide, x].KingToSquare < minSquare ? castlingMoves[Game.CurrentSide, x].KingToSquare : minSquare;
						minSquare = castlingMoves[Game.CurrentSide, x].OtherFromSquare < minSquare ? castlingMoves[Game.CurrentSide, x].OtherFromSquare : minSquare;
						minSquare = castlingMoves[Game.CurrentSide, x].OtherToSquare < minSquare ? castlingMoves[Game.CurrentSide, x].OtherToSquare : minSquare;
						//	find the right-most square that needs to be free
						int maxSquare = castlingMoves[Game.CurrentSide, x].KingFromSquare;
						maxSquare = castlingMoves[Game.CurrentSide, x].KingToSquare > maxSquare ? castlingMoves[Game.CurrentSide, x].KingToSquare : maxSquare;
						maxSquare = castlingMoves[Game.CurrentSide, x].OtherFromSquare > maxSquare ? castlingMoves[Game.CurrentSide, x].OtherFromSquare : maxSquare;
						maxSquare = castlingMoves[Game.CurrentSide, x].OtherToSquare > maxSquare ? castlingMoves[Game.CurrentSide, x].OtherToSquare : maxSquare;
						//	make sure the squares are empty
						bool squaresEmpty = true;
						for( int file = Board.GetFile( minSquare ); squaresEmpty && file <= Board.GetFile( maxSquare ); file++ )
						{
							int sq = file * Board.NumRanks + Board.GetRank( minSquare );
							if( sq != castlingMoves[Game.CurrentSide, x].KingFromSquare &&
								sq != castlingMoves[Game.CurrentSide, x].OtherFromSquare &&
								Board[sq] != null )
								//	the path is blocked by a piece other than those involved in castling
								squaresEmpty = false;
						}
						if( squaresEmpty )
						{
							//	make sure squares the King is passes through are not attacked
							bool squaresAttacked = false;
							//	if this game doesn't have check/checkmate, we don't care if the squares are attacked
							if( hasCheckmateRule )
							{
								if( castlingMoves[Game.CurrentSide, x].KingFromSquare < castlingMoves[Game.CurrentSide, x].KingToSquare )
								{
									for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										!squaresAttacked && file <= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ); file++ )
									{
										int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										if( Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 ) )
											squaresAttacked = true;
									}
								} else
								{
									for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										!squaresAttacked && file >= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ); file-- )
									{
										int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										if( Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 ) )
											squaresAttacked = true;
									}
								}
							}

							if( !squaresAttacked )
							{
								//	required squares are empty and not attacked so the move is legal - add it
								try
								{
									if( Board[castlingMoves[Game.CurrentSide, x].KingFromSquare] == null )
										throw new Exception( "!" );
									list.BeginMoveAdd( MoveType.Castling, castlingMoves[Game.CurrentSide, x].KingFromSquare,
										castlingMoves[Game.CurrentSide, x].KingToSquare );
								}
								catch( Exception ex )
								{
									throw ex;
								}
								Piece king = list.AddPickup( castlingMoves[Game.CurrentSide, x].KingFromSquare );
								Piece other = list.AddPickup( castlingMoves[Game.CurrentSide, x].OtherFromSquare );
								list.AddDrop( king, castlingMoves[Game.CurrentSide, x].KingToSquare, null );
								list.AddDrop( other, castlingMoves[Game.CurrentSide, x].OtherToSquare, null );
								list.EndMoveAdd( 1000 );
							}
						}
					}
				}
			}
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected CastlingMove[,] castlingMoves;
		protected int[] nCastlingMoves;
		protected int[] searchStackPrivs;
		protected int[] gameHistoryPrivs;
		protected int[] allPrivsPerPlayer;
		protected Dictionary<char, int> privLookup;
		protected int nextPriv;
		protected int[] privEraseMap;
		protected string allPrivString;
		protected bool hasCheckmateRule;
		protected int hashKeyIndex;
	}
}
