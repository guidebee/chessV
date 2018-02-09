
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

namespace ChessV
{
	public class MoveCompletionDefaultRule: MoveCompletionRule
	{
		public override int TurnNumber
		{
			get { return turnNumber; }
		}

		public override ulong GetPositionHashCode( int ply )
		{
			UInt64 u = (UInt64) (-((Int64) Game.CurrentSide));
			return (UInt64) (-((Int64) Game.CurrentSide));
		}

		public override void CompleteMove( MoveInfo move, int ply )
		{
			Game.CurrentSide = Game.CurrentSide ^ 1;
			if( Game.CurrentSide == 0 )
				turnNumber++;
		}

		public override void UndoingMove()
		{
			Game.CurrentSide = Game.CurrentSide ^ 1;
			if( Game.CurrentSide == 1 )
				turnNumber--;
		}

		public override int GetNextSide()
		{
			return Game.CurrentSide ^ 1;
		}

		public override void PositionLoaded( FEN fen )
		{
			//	read the turn number from the FEN
			if( !Int32.TryParse( fen["turn number"], out turnNumber ) )
				throw new Exception( "FEN parse error - invalid turn number specified: '" + fen["turn number"] + "'" );
			//	read the current player from the FEN
			string currentPlayerNotation = fen["current player"];
			if( currentPlayerNotation == "w" )
				Game.CurrentSide = 0;
			else if( currentPlayerNotation == "b" )
				Game.CurrentSide = 1;
			else
				throw new Exception( "FEN parse error - invalid current player specified: '" + currentPlayerNotation + "'" );
		}

		public override void SavePositionToFEN( FEN fen )
		{
			fen["turn number"] = turnNumber.ToString();
			fen["current player"] = Game.CurrentSide == 0 ? "w" : "b";
		}

		protected int turnNumber;
	}
}
