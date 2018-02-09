
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
	[Game("Mecklenbeck Chess", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "1973",
		  InventedBy = "Bernd Eickenscheidt;B. Schwarzkopf",
		  Tags = "Chess Variant",
		  GameDescription1 = "Pawns may promote upon reaching the 6th rank",
		  GameDescription2 = "Pawns must promote upon reaching the last rank")]
	public class MecklenbeckChess: Chess
	{
		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			PromotionRule.Value = "Custom";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** PAWN PROMOTION *** //
			if( PromotionRule.Value == "Custom" )
			{
				Rules.ComplexPromotionRule promotionRule = new Rules.ComplexPromotionRule();
				List<PieceType> availablePromotionTypes = ParseTypeListFromString( PromotionTypes );
				promotionRule.AddPromotionCapability( Pawn, availablePromotionTypes, null,
					loc => loc.Rank == Board.NumRanks - 1 ? Rules.PromotionOption.MustPromote :
						(loc.Rank >= Board.NumRanks - 3 ? Rules.PromotionOption.CanPromote : Rules.PromotionOption.CannotPromote) );
				AddRule( promotionRule );
			}
		}
		#endregion
	}
}
