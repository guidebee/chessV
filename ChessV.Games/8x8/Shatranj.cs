
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
	[Game("Shatranj", typeof(Geometry.Rectangular), 8, 8, 
		  XBoardName = "shatranj",
		  Invented = "circa 7th century",
		  InventedBy = "Unknown", 
		  Tags = "Chess Variant,Historic,Popular")]
	[Appearance(NumberOfColors=1)]
	public class Shatranj: Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //

		public PieceType Rook;
		public PieceType Elephant;
		public PieceType Knight;
		public PieceType General;
		

		// *** CONSTRUCTION *** //
		
		public Shatranj(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}
		

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			NumberOfSquareColors = 1;
			Array = "rnekgenr/pppppppp/8/8/8/8/PPPPPPPP/RNEKGENR";
			PromotionRule.Value = "Standard";
			PromotionTypes = "G";
			BareKing = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 600, 600 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 285, 285 ) );
			AddPieceType( Elephant = new Elephant( "Elephant", "E", 150, 150 ) );
			AddPieceType( General = new Ferz( "General", "G", 175, 175 ) );
		}
		#endregion
	}
}
