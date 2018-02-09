
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
	//**********************************************************************
	//
	//                   PieceLocationRestrictionRule
	//
	//    This rule can be used to prevent pieces from moving to areas 
	//    of the boad.  For example, in Xiangqi, the king cannot leave 
	//    the castle, or in Eurasian Chess, the king cannot cross the river.

	public class PieceLocationRestrictionRule: Rule
	{
		public PieceLocationRestrictionRule( PieceType pieceType, ConditionalLocationDelegate conditionDelegate )
		{ this.pieceType = pieceType; pieceTypeNumber = pieceType.TypeNumber; condition = conditionDelegate; }

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			if( Board[from].PieceType == pieceType && condition( Board.SquareToLocation( Board.PlayerSquare( Board[from].Player, to ) ) ) )
				return MoveEventResponse.IllegalMove;
			return MoveEventResponse.NotHandled;
		}

		protected PieceType pieceType;
		protected int pieceTypeNumber;
		protected ConditionalLocationDelegate condition;
	}
}
