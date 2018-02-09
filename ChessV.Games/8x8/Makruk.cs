
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
	[Game("Makruk", typeof(Geometry.Rectangular), 8, 8,
		  XBoardName = "makruk",
		  Invented = "Unknown",
		  InventedBy = "Unknown",
		  Tags = "Chess Variant,Regional,Historic")]
	public class Makruk: Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //

		public PieceType Rook;
		public PieceType Knight;
		public PieceType Ferz;
		public PieceType SilverGeneral;


		// *** CONSTRUCTION *** //

		public Makruk():
			base
				 ( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnsmksnr/8/pppppppp/8/8/PPPPPPPP/8/RNSKMSNR";
			PromotionRule.Value = "Custom";
			PromotionTypes = "M";
			Castling.Value = "None";
			PawnDoubleMove = false;
			EnPassant = false;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 500, 550 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			AddPieceType( Ferz = new Ferz( "Met", "M", 150, 150 ) );
			AddPieceType( SilverGeneral = new SilverGeneral( "Khon", "S", 260, 260 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			if( PromotionRule.Value == "Custom" )
			{
				List<PieceType> availablePromotionTypes = ParseTypeListFromString( PromotionTypes );
				BasicPromotionRule( Pawn, availablePromotionTypes, loc => loc.Rank == 5 );
			}
		}
		#endregion
	}
}
