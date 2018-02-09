
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
	[Game("Viking Chess", typeof(Geometry.Rectangular), 12, 7,
		  Invented = "2002",
		  InventedBy = "Tomas Forsman",
		  Tags = "Chess Variant")]
	public class VikingChess: Abstract.GenericChess
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;


		// *** CONSTRUCTION *** //

		public VikingChess() :
			base
				( /* num files = */ 12,
				  /* num ranks = */ 7,
				  /* symmetry = */ new NoSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "12/12/12/12/1PPPP2pppp1/RPNNPPppnnpr/RBKQBPpbqkbr";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QRNB";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 500, 550 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 325, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 900, 1000 ) );
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
