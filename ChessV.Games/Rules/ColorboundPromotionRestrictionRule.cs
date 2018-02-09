
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
	public class ColorboundPromotionRestrictionRule: Rule
	{
		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			if( (move.MoveType & MoveType.PromotionProperty) != 0 || (move.MoveType & MoveType.Replace) != 0 )
			{
				Piece piece = Board[move.ToSquare];
				if( piece.PieceType.NumSlices > 1 )
				{
					BitBoard pieces = Board.GetPieceTypeBitboard( piece.Player, piece.TypeNumber );
					while( pieces )
					{
						int sq = pieces.ExtractLSB();
						if( sq != move.ToSquare && piece.PieceType.SliceLookup[sq] == piece.PieceType.SliceLookup[move.ToSquare] )
							return MoveEventResponse.IllegalMove;
					}
				}
			}
			return MoveEventResponse.NotHandled;
		}
	}
}
