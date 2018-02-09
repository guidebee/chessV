
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
	public class DoubleMoveCompletionRule: MoveCompletionRule
	{
		// *** CONSTRUCTION *** //

		public DoubleMoveCompletionRule()
		{
			currentState = 1;
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			hashKeyIndex = game.HashKeys.TakeKeys( 4 );
		}


		// *** OVERRIDES ** //

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

		public override void CompleteMove( MoveInfo move, int ply )
		{
			currentState = (currentState + 1) % 4;
			if( currentState == 0 )
				turnNumber++;
			Game.CurrentSide = currentState / 2;
		}

		public override void UndoingMove()
		{
			currentState = (currentState + 3) % 4;
			if( currentState == 3 )
				turnNumber--;
			Game.CurrentSide = currentState / 2;
		}

		public override int GetNextSide()
		{
			return ((currentState + 1) % 4) / 2;
		}


		// *** PROTECTED DATA *** //

		protected int currentState;
		protected int hashKeyIndex;
		protected int turnNumber;

		protected string[] stateNotations = new string[] { "w2", "w", "b2", "b" };
	}
}
