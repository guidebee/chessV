
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
	[Game("Lemurian Shatranj", typeof(Geometry.Rectangular), 8, 8,
		  InventedBy = "Joe Joyce",
		  Invented = "2006",
		  Tags = "Chess Variant")]
	public class LemurianShatranj: Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //

		public PieceType SlidingGeneral;
		public PieceType BentShaman;
		public PieceType BentHero;
		public PieceType WarElephant;


		// *** CONSTRUCTION *** //

		public LemurianShatranj():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "whsgkshw/pppppppp/8/8/8/8/PPPPPPPP/WHSGKSHW";
			PromotionRule.Value = "Custom";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( SlidingGeneral = new SlidingGeneral( "Sliding General", "G", 1000, 1100 ) );
			AddPieceType( BentShaman = new BentShaman( "Bent Shaman", "S", 600, 650 ) );
			AddPieceType( BentHero = new BentHero( "Bent Hero", "H", 750, 750 ) );
			AddPieceType( WarElephant = new WarElephant( "War Elephant", "W", 475, 475 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			if( PromotionRule.Value == "Custom" )
			{
				List<PieceType> promotionTypes = new List<PieceType>() { SlidingGeneral };
				List<PieceType> promoteByReplacementTypes = new List<PieceType>() { BentShaman, BentHero, WarElephant };
				Rules.ComplexPromotionRule promotionRule = new Rules.ComplexPromotionRule();
				promotionRule.AddPromotionCapability( Pawn, promotionTypes, promoteByReplacementTypes, loc => loc.Rank == 7 ? Rules.PromotionOption.MustPromote : Rules.PromotionOption.CannotPromote );
				AddRule( promotionRule );
				AddRule( new Rules.ColorboundPromotionRestrictionRule() );
			}
		}
		#endregion
	}
}
