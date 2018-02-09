
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
	public class MarylandChessMoveCompletionRule: MoveCompletionRule
	{
		public enum State
		{
			WhiteAny = 0,
			WhiteCapture = 1,
			WhitePawnMove = 2,
			WhiteKingMove = 3,
			WhiteOtherMove = 4,
			BlackAny = 5,
			BlackCapture = 6,
			BlackPawnMove = 7,
			BlackKingMove = 8,
			BlackOtherMove = 9
		}

		public const int StateAny = 0;
		public const int StateCapture = 1;
		public const int StatePawnMove = 2;
		public const int StateKingMove = 3;
		public const int StateOtherMove = 4;


		// *** CONSTRUCTION *** //

		public MarylandChessMoveCompletionRule( PieceType kingType, PieceType pawnType )
		{
			this.kingType = kingType;
			this.pawnType = pawnType;
			currentState = 0;
			lastSquare = -1;
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			hashKeyIndex = game.HashKeys.TakeKeys( 10 );
			searchStateHistory = new int[Game.MAX_GAME_LENGTH + Game.MAX_DEPTH];
			lastMovedSquareHistory = new int[Game.MAX_GAME_LENGTH + Game.MAX_DEPTH];
			historyIndex = 0;
			searchStateHistory[historyIndex++] = currentState;
		}


		// *** OVERRIDES *** //

		public override int TurnNumber
		{
			get { return turnNumber; }
		}

		public override void PositionLoaded( FEN fen )
		{
			base.PositionLoaded( fen );
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
					Game.CurrentSide = currentState / 5;
					notationFound = true;
				}
			if( !notationFound )
				throw new Exception( "FEN parse error - invalid current player specified: '" + currentPlayerNotation + "'" );
			//	read last square
			string lastSquareNotation = fen["last square"];
			if( lastSquareNotation == "-" )
				lastSquare = -1;
			else
				if( !Int32.TryParse( fen["last square"], out lastSquare ) || lastSquare < -1 || lastSquare >= Board.NumSquares )
					throw new Exception( "FEN parse error - invalid last square specified: '" + fen["last square"] + "'" );

		}

		public override void SavePositionToFEN( FEN fen )
		{
			fen["turn number"] = turnNumber.ToString();
			fen["current player"] = stateNotations[currentState];
			fen["last square"] = lastSquare.ToString();
		}

		public override ulong GetPositionHashCode( int ply )
		{
			return HashKeys.Keys[hashKeyIndex + currentState];
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( currentState % 5 != StateAny && !capturesOnly )
			{
				//	add move allowing player to Pass
				list.BeginMoveAdd( MoveType.Pass, -1, -1 );
				list.EndMoveAdd( 0 );
			}
		}

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			//	can't move the same piece twice, unless it is a King, 
			//	in which case, lastSquare will be -1
			if( lastSquare >= 0 && from == lastSquare )
				return MoveEventResponse.IllegalMove;
			//	if the current state is StateCapture, 
			//	anything that is not a capture is illegal
			if( currentState % 5 == StateCapture && (type & MoveType.CaptureProperty) == 0 )
				return MoveEventResponse.IllegalMove;
			//	if the current state is StateKingMove, any move that is 
			//	a capture or not a king move is illegal
			else if( currentState % 5 == StateKingMove && (Board[from].PieceType != kingType || (type & MoveType.CaptureProperty) != 0) )
				return MoveEventResponse.IllegalMove;
			//	if the current state is StatePawnMove, any move that is a 
			//	capture or not a pawn move is illegal
			else if( currentState % 5 == StatePawnMove && (Board[from].PieceType != pawnType || (type & MoveType.CaptureProperty) != 0) )
				return MoveEventResponse.IllegalMove;
			//	if the current state is StateMoveOther, move is only legal if 
			//	if it is not a capture, pawn, or king move
			else if( currentState % 5 == StateOtherMove && 
				((type & MoveType.CaptureProperty) != 0 || Board[from].PieceType == kingType || Board[from].PieceType == pawnType) )
				return MoveEventResponse.IllegalMove;
			//	if we are here then the move is legal, at least as far 
			//	as we are concerned.  other rules may still declare it illegal.
			return MoveEventResponse.NotHandled;
		}

		public override int GetNextSide()
		{
			return (currentState / 5) ^ 1;
		}

		public override void CompleteMove( MoveInfo move, int ply )
		{
			//	store the last-moved square and current state to the 
			//	history arrays so we can unmake the move later
			lastMovedSquareHistory[historyIndex] = lastSquare;
			searchStateHistory[historyIndex++] = currentState;
			//	update the last moved square
			if( currentState % 5 != StateAny && move.PieceMoved != null && move.PieceMoved.PieceType != kingType )
				lastSquare = move.ToSquare;
			else
				lastSquare = -1;
			//	update the currentState
			if( currentState % 5 != StateAny )
				currentState = (currentState / 5) * 5;
			else
			{
				int nextState;
				if( (move.MoveType & MoveType.CaptureProperty) != 0 )
					nextState = StateCapture;
				else if( move.PieceMoved != null && move.PieceMoved.PieceType == kingType )
					nextState = StateKingMove;
				else if( move.PieceMoved != null && move.PieceMoved.PieceType == pawnType )
					nextState = StatePawnMove;
				else
					nextState = StateOtherMove;
				currentState = (((currentState / 5) * 5) + nextState + 5) % 10;
			}
			//	update the turn number
			if( currentState >= 1 && currentState <= 4 )
				turnNumber++;
			//	set the Game's side to move
			Game.CurrentSide = currentState / 5;
		}

		public override void UndoingMove()
		{
			int previousState = currentState;
			currentState = searchStateHistory[--historyIndex];
			lastSquare = lastMovedSquareHistory[historyIndex];
			if( previousState < 5 && currentState >= 5 )
				turnNumber--;
			Game.CurrentSide = currentState / 5;
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected PieceType kingType;
		protected PieceType pawnType;
		protected int currentState;
		protected int lastSquare;
		protected int hashKeyIndex;
		protected int[] searchStateHistory;
		protected int[] lastMovedSquareHistory;
		protected int historyIndex;
		protected int turnNumber;

		protected string[] stateNotations = new string[] { "w", "wc", "wp", "wk", "wo", "b", "bc", "bp", "bk", "bo" };
	}
}
