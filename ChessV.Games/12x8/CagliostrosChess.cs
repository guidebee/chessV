
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
	[Game("Cagliostro's Chess", typeof(Geometry.Rectangular), 12, 8,
		  Invented = "1970s",
		  InventedBy = "Savio Cagliostro",
		  Tags = "Chess Variant")]
	public class CagliostrosChess: Abstract.Generic12x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Archbishop;
		public PieceType Chancellor;
		public PieceType Amazon;


		// *** CONSTRUCTION *** //

		public CagliostrosChess():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbacqkgabnr/pppppppppppp/12/12/12/12/PPPPPPPPPPPP/RNBACQKGABNR";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QCAGRNB";
			Castling.Value = "4-4";
			PawnDoubleMove = true;
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1000, 1150 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 350, 400 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Archbishop = new Archbishop( "Archbishop", "A", 900, 1000 ) );
			AddPieceType( Chancellor = new Chancellor( "Chancellor", "C", 975, 1100 ) );
			AddPieceType( Amazon = new Amazon( "General", "G", 1300, 1200 ) );
		}
		#endregion
	}
}

