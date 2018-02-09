
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
	//                           ExtinctionChess
	//

	[Game("Extinction Chess", typeof(Geometry.Rectangular), 8, 8,
		  InventedBy = "R. Wayne Schmittberger",
		  Invented = "1985",
		  Tags = "Chess Variant,Popular",
		  GameDescription1 = "A derivative of standard Chess where the objective",
		  GameDescription2 = "is to capture all the pieces of any one type",
		  Definitions = "ExtinctionTypes=KQRNBP;PromotionTypes=KQRNB")]
	[Game("Kinglet Chess", typeof(Geometry.Rectangular), 8, 8,
		  InventedBy = "V. R. Parton",
		  Invented = "1953",
		  Tags = "Chess Variant",
		  GameDescription1 = "A derivative of standard Chess where the objective",
		  GameDescription2 = "is to capture all a player's pawns",
		  Definitions = "ExtinctionTypes=P;PromotionTypes=K")]
	public class ExtinctionChess: Chess
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public string ExtinctionTypes { get; set; }


		// *** INITIALIZATION *** //

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			//	Replace the Checkmate Rule with the Extinction Rule
			ReplaceRule( FindRule( typeof(Rules.CheckmateRule), true ), new Rules.Extinction.ExtinctionRule( ExtinctionTypes ) );
		}
		#endregion
	}
}
