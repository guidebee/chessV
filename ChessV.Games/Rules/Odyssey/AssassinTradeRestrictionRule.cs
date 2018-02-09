
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
using ChessV.Games;

namespace ChessV.Games.Rules.Odyssey
{
	public class AssassinTradeRestrictionRule: Rule
	{
		protected PieceType executionerType;

		public AssassinTradeRestrictionRule( PieceType executionerType )
		{
			this.executionerType = executionerType;
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			if( move.MoveType == MoveType.StandardCapture && 
				move.PieceMoved.PieceType == executionerType && 
				move.PieceCaptured.PieceType == executionerType )
			{
				//	declare this move illegal if and only if:
				//	  (A) the capturing piece is moving more than one space, and 
				//	  (B) the captured piece is protected
				if( Board.SquareToLocation( move.FromSquare ).Rank - Board.SquareToLocation( move.ToSquare ).Rank > 1 ||
					Board.SquareToLocation( move.ToSquare ).Rank - Board.SquareToLocation( move.FromSquare ).Rank > 1 ||
					Board.SquareToLocation( move.FromSquare ).File - Board.SquareToLocation( move.ToSquare ).File > 1 ||
					Board.SquareToLocation( move.ToSquare ).File - Board.SquareToLocation( move.FromSquare ).File > 1 )
				{
					//	The move is greater than one space.
					//	Is the victim protected?
					if( Game.IsSquareAttacked( move.ToSquare, move.PieceCaptured.Player ) )
						//	the piece is protected, so this move is illegal
						return MoveEventResponse.IllegalMove;
				}
			}
			return base.MoveBeingMade( move, ply );
		}
	}
}