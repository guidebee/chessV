
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
	[Game("Falcon Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "1992",
		  InventedBy = "George Duke",
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme = "Surrealistic Summer")]
	public class FalconChess: Abstract.Generic10x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Falcon;


		// *** CONSTRUCTION *** //

		public FalconChess() :
			base
				 ( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbfqkfbnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNBFQKFBNR";
			PawnDoubleMove = true;
			EnPassant = true;
			Castling.Value = "Flexible";
			PromotionRule.Value = "Standard";
			PromotionTypes = "RBNF";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1100, 1150 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 600 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 350, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 285, 285 ) );
			AddPieceType( Falcon = new Falcon( "Falcon", "F", 600, 650 ) );
		}
		#endregion
	}
}
