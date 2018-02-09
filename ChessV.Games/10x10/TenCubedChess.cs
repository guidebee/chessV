
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
	[Game("TenCubed Chess", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2005",
		  InventedBy = "David Paolowich",
		  Tags = "Chess Variant" )]
	public class TenCubedChess: GrandChess
	{
		// *** PIECE TYPES *** //

		public PieceType Wizard;
		public PieceType Champion;


		// *** CONSTRUCTION *** //

		public TenCubedChess()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "2cwamwc2/1rnbqkbnr1/pppppppppp/10/10/10/10/PPPPPPPPPP/1RNBQKBNR1/2CWAMWC2";
			PawnMultipleMove.Value = "Grand";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QMA";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Wizard = new Wizard( "Wizard", "W", 460, 460 ) );
			AddPieceType( Champion = new Champion( "Chapion", "C", 475, 475 ) );
			//	Set the name for the Archbishop (called a Cardinal in Grand Chess)
			Archbishop.Name = "Archbishop";
			Archbishop.Notation = "A";
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
