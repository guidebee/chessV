
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

namespace ChessV.Games
{
	//**********************************************************************
	//
	//                        WildebeestChess
	//
	//    This class implements R. Wayne Schmittberger's Wildebeest Chess.
	//    The intention was to balance the number of leaping piece types 
	//    with the number of riding piece types.  Thus was added the Camel 
	//    and the Wildebeest, a Knight+Camel compound.

	[Game("Wildebeest Chess", typeof(Geometry.Rectangular), 11, 10,
		  XBoardName = "wildebeest",
		  Invented = "1987",
		  InventedBy = "R. Wayne Schmittberger",
		  Tags = "Chess Variant,Popular")]
	[Appearance(ColorScheme = "Lemon Cappuccino")]
	public class WildebeestChess: Abstract.Generic11x10
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Camel;
		public PieceType Wildebeest;


		// *** CONSTRUCTION *** //

		public WildebeestChess(): 
			base
				( /* symmetry = */ new RotationalSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnccwkqbbnr/ppppppppppp/11/11/11/11/11/11/PPPPPPPPPPP/RNBBQKWCCNR";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QW";
			PawnMultipleMove.Value = "Wildebeest";
			Castling.Value = "Wildebeest";
			EnPassant = true;
		}                    
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1025, 1250 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Camel = new Camel( "Camel", "C", 250, 250 ) );
			AddPieceType( Wildebeest = new Wildebeest( "Wildebeest", "W", 675, 675 ) );
		}
		#endregion
	}
}
