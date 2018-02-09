
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
	public abstract class MoveCompletionRule: Rule

		/*	The MoveCompletionRule class allows definition of a special kind 
			of rule.  One difference from other Rule types is that a Game
			always has exactly one MoveCompletionRule - typically the default 
			one provided by the Game class which you typically don't need to 
			mess with unless you are implementing a game with an unusual turn 
			order (other than white-black-white-black) such as a double-move
			variant.  Implementing a custom MoveCompletionRule is how 
			multi-move variants are accomplished.  */

	{
		public abstract int TurnNumber { get; }

		public abstract void CompleteMove( MoveInfo move, int ply );

		public abstract void UndoingMove();

		public abstract int GetNextSide();
	}
}
