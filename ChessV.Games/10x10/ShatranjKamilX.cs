
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
	[Game("Shatranj Kamil X", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2007",
		  InventedBy = "David Paulowich",
		  Tags = "Chess Variant")]
	public class ShatranjKamilX: Abstract.Generic10x10
	{
		// *** PIECE TYPES *** //

		public PieceType Rook;
		public PieceType Knight;
		public PieceType Ferz;
		public PieceType SilverGeneral;
		public PieceType Cannon;
		public PieceType Elephant;
		public PieceType WarElephant;


		// *** CONSTRUCTION *** //

		public ShatranjKamilX():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "c3ke3c/1rnsefsnr1/pppppppppp/10/10/10/10/PPPPPPPPPP/1RNSEFSNR1/C3KE3C";
			PromotionRule.Value = "Custom";
			StalemateResult.Value = "Loss";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Ferz = new Ferz( "Ferz", "F", 150, 150 ) );
			AddPieceType( SilverGeneral = new SilverGeneral( "Silver General", "S", 175, 175 ) );
			AddPieceType( Cannon = new Cannon( "Cannon", "C", 400, 275 ) );
			AddPieceType( Elephant = new ChainedPadwar( "Elephant", "E", 150, 200, "Elephant" ) );
			AddPieceType( WarElephant = new FreePadwar( "War Elephant", "W", 300, 350, "ElephantFerz2" ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			//	Add promotions for Pawn and Ferz
			if( PromotionRule.Value == "Custom" )
			{
				AddRule( new Rules.BasicPromotionRule( Pawn, new List<PieceType> { WarElephant }, Loc => Loc.Rank == 9 ) );
				AddRule( new Rules.BasicPromotionRule( Ferz, new List<PieceType> { WarElephant }, Loc => Loc.Rank == 9 ) );
			}
		}
		#endregion
	}
}
