
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
	[Game("Capablanca Shatranj", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2006",
		  InventedBy = "Christine Bagley-Jones", 
		  Tags = "Chess Variant, Capablanca Variant")]
	public class CapablancaShatranj: CapablancaChess
	{
		// *** PIECE TYPES *** //

		PieceType Minister;
		PieceType HighPriestess;


		// *** CONSTRUCTION *** //

		public CapablancaShatranj()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbmqkhbnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNBMQKHBNR";
			PromotionTypes = "MH";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Minister = new Minister( "Minister", "M", 600, 600, "Knight Wazir Dabbabah" ) );
			AddPieceType( HighPriestess = new HighPriestess( "High Priestess", "H", 625, 625, "ElephantKnight" ) );
			Archbishop.Enabled = false;
			Chancellor.Enabled = false;
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
		}
		#endregion
	}
}
