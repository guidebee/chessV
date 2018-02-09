
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

namespace ChessV.Games.Rules.MultiMove
{
	public class MarseillaisMoveCompletionRule: MoveCompletionRule
	{
		// *** PROPERTIES *** //

		public PieceType RoyalPieceType { get; private set; }


		// *** CONSTRUCTION *** //

		public MarseillaisMoveCompletionRule()
		{
			currentState = 1;
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			hashKeyIndex = game.HashKeys.TakeKeys( 4 );
			searchStateHistory = new int[Game.MAX_GAME_LENGTH + Game.MAX_DEPTH];
			searchStateHistoryIndex = 0;
			searchStateHistory[searchStateHistoryIndex++] = currentState;
			
			//	Determine royal piece type (if any)
			CheckmateRule checkmateRule = (CheckmateRule) game.FindRule( typeof(CheckmateRule), true );
			if( checkmateRule != null )
			{
				RoyalPieceType = checkmateRule.RoyalPieceType;
				royalPieces = new Piece[game.NumPlayers];
			}
		}


		// *** OVERRIDES *** //

		public override int TurnNumber
		{
			get { return turnNumber; }
		}

		public override void PositionLoaded( FEN fen )
		{
			//	read the turn number from the FEN
			if( !Int32.TryParse( fen["turn number"], out turnNumber ) )
				throw new Exception( "FEN parse error - invalid turn number specified: '" + fen["turn number"] + "'" );
			//	read the current player from the FEN
			string currentPlayerNotation = fen["current player"];
			bool notationFound = false;
			for( int x = 0; x < stateNotations.Length; x++ )
				if( currentPlayerNotation == stateNotations[x] )
				{
					currentState = x;
					Game.CurrentSide = currentState / 2;
					notationFound = true;
				}
			if( !notationFound )
				throw new Exception( "FEN parse error - invalid current player specified: '" + currentPlayerNotation + "'" );
			if( royalPieces != null )
			{
				//	scan the board for the royal pieces
				for( int player = 0; player < Game.NumPlayers; player++ )
				{
					List<Piece> piecelist = Game.GetPieceList( player );
					foreach( Piece piece in piecelist )
						if( piece.PieceType == RoyalPieceType )
							royalPieces[player] = piece;
				}
			}
		}

		public override void SavePositionToFEN( FEN fen )
		{
			fen["turn number"] = turnNumber.ToString();
			fen["current player"] = stateNotations[currentState];
		}

		public override ulong GetPositionHashCode( int ply )
		{
			return HashKeys.Keys[hashKeyIndex + currentState];
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			int currentSide = currentState / 2;
			//	Calculate what will be the next state under normal circumstances
			nextState = (currentState + 1) % 4;
			//	Make sure this is correct - if the other side is in 
			//	check, and the same side is still to move, we'll need 
			//	to skip a turn.
			if( nextState / 2 == currentSide && royalPieces != null )
			{
				Piece royalPiece = royalPieces[currentSide ^ 1];
				if( Game.IsSquareAttacked( royalPiece.Square, currentSide ) )
					//	The first move checked the opponent's royal piece, so 
					//	the player forfeits his second move.  Advance the state again.
					nextState = (nextState + 1) % 4;
			}
			return MoveEventResponse.MoveOk;
		}

		public override int GetNextSide()
		{
			return nextState / 2;
		}

		public override void CompleteMove( MoveInfo move, int ply )
		{
			searchStateHistory[searchStateHistoryIndex++] = currentState;
			currentState = nextState;
			if( currentState == 0 )
				turnNumber++;
			Game.CurrentSide = currentState / 2;
		}

		public override void UndoingMove()
		{
			int previousState = currentState;
			currentState = searchStateHistory[--searchStateHistoryIndex];
			if( currentState > previousState )
				turnNumber--;
			Game.CurrentSide = currentState / 2;
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected Piece[] royalPieces;
		protected int currentState;
		protected int nextState;
		protected int hashKeyIndex;
		protected int[] searchStateHistory;
		protected int searchStateHistoryIndex;
		protected int turnNumber;

		protected string[] stateNotations = new string[] { "w2", "w", "b2", "b" };
	}
}
