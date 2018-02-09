
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
	public class LocationVictoryConditionRule: Rule
	{
		public LocationVictoryConditionRule( PieceType pieceType, ConditionalLocationDelegate victoryLocationDelegate )
		{
			types = new List<PieceType>() { pieceType };
			victoryLocation = victoryLocationDelegate;
			lastDestinationSquare = -1;
		}

		public LocationVictoryConditionRule( List<PieceType> pieceTypes, ConditionalLocationDelegate victoryLocationDelegate )
		{
			types = pieceTypes;
			victoryLocation = victoryLocationDelegate;
			lastDestinationSquare = -1;
		}

		public override MoveEventResponse TestForWinLossDraw( int currentPlayer, int ply )
		{
			if( lastDestinationSquare >= 0 &&
				types.Contains( Board[lastDestinationSquare].PieceType ) &&
				victoryLocation( Board.SquareToLocation( Board.PlayerSquare( Board[lastDestinationSquare].Player, lastDestinationSquare ) ) ) )
				return currentPlayer == Board[lastDestinationSquare].Player ? MoveEventResponse.GameWon : MoveEventResponse.GameLost;
			return MoveEventResponse.NotHandled;
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			lastDestinationSquare = move.ToSquare;
			return MoveEventResponse.NotHandled;
		}

		protected List<PieceType> types;
		protected ConditionalLocationDelegate victoryLocation;
		protected int lastDestinationSquare;
	}
}
