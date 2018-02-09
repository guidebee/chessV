
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
	[Game("Emperor's Game", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "1840",
		  InventedBy = "L. Tressan",
		  Tags = "Chess Variant,Historic",
		  GameDescription1 = "Historic large-board variant from 19th century Germany",
		  GameDescription2 = "Invented by L. Tressan in 1840")]
	[Appearance(ColorScheme = "Brushed Steel")]
	public class EmperorsGame: Abstract.Generic10x10
	{
		// *** PIECE TYPES *** //

		public PieceType General;
		public PieceType Queen;
		public PieceType Adjutant;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;


		// *** CONSTRUCTION *** //

		public EmperorsGame(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}
		

		// *** INITIALIZATION *** //
		
		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbgqkabnr/pppppppppp/10/10/10/10/10/10/PPPPPPPPPP/RNBGQKABNR";
			EnPassant = true;
			PawnMultipleMove.Value = "Triple";
			PromotionRule.Value = "Replacement";
			Castling.Value = "Long";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( General = new Amazon( "General", "G", 1350, 1500 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 1025, 1250 ) );
			AddPieceType( Adjutant = new Archbishop( "Adjutant", "A", 725, 800 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
		}
		#endregion
	}
}
