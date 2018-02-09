
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

namespace ChessV.Games.Rules.Brouhaha
{
	public class BrouhahaBorderRule: Rule
	{
		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			//	we are only concerned about moves to the border squares
			Location toLocation = Board.SquareToLocation( to );
			if( toLocation.File == 0 || toLocation.File == Board.NumFiles - 1 ||
				toLocation.Rank == 0 || toLocation.Rank == Board.NumRanks - 1 )
			{
				//	 move to border square - is this square occupied?
				if( Board[to] == null )
					//	not a capture, so the move is illegal
					return MoveEventResponse.IllegalMove;
			}
			return MoveEventResponse.NotHandled;
		}
	}
}
