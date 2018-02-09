
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
using ChessV.Games.Rules;
using ChessV.Games.Rules.Odyssey;

namespace ChessV.Games
{
	[Game("Odyssey", typeof(Geometry.Rectangular), 12, 12,
		  Invented = "2016",
		  InventedBy = "Greg Strong",
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme="Sublimation")]
	public class Odyssey: Abstract.Generic12x12
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType DragonKing;
		public PieceType DragonHorse;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Camel;
		public PieceType GoldGeneral;
		public PieceType SilverGeneral;
		public PieceType KnightGeneral;
		public PieceType CamelGeneral;
		public PieceType General;
		public PieceType VerticalMover;
		public PieceType SideMover;
		public PieceType Assassin;
		

		// *** CONSTRUCTION *** //

		public Odyssey():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "dk10dk/rcdhb1kq1bdhcr/smvmnsggga_ngggsgnvmsm/pppppppppppp/12/12/12/12/PPPPPPPPPPPP/SMVMNSGGG_NGAGGSGNVMSM/RCDHB1QK1BDHCR/DK10DK";
			PawnMultipleMove.Value = "@4(2)";
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1250, 1350 ) );
			AddPieceType( DragonKing = new DragonKing( "Dragon King", "DK", 850, 950 ) );
			AddPieceType( DragonHorse = new DragonHorse( "Dragon Horse", "DH", 750, 800 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 650, 750 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 500, 550 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Camel = new Camel( "Camel", "C", 250, 250 ) );
			AddPieceType( GoldGeneral = new GoldGeneral( "Gold General", "GG", 250, 250 ) );
			AddPieceType( SilverGeneral = new SilverGeneral( "Silver General", "SG", 225, 225 ) );
			AddPieceType( KnightGeneral = new Centaur( "Knight General", "_NG", 800, 800 ) );
			AddPieceType( CamelGeneral = new CamelGeneral( "Camel General", "_CG", 775, 775 ) );
			AddPieceType( General = new General( "Full General", "FG", 350, 350 ) );
			AddPieceType( VerticalMover = new VerticalMoverGeneral( "Vertical Mover", "VM", 600, 650 ) );
			AddPieceType( SideMover = new SideMoverGeneral( "Side Mover", "SM", 600, 600 ) );
			AddPieceType( Assassin = new SquirrelGeneral( "Assassin", "A", 1400, 1400, "Assassin" ) );
			//	augment the Assassin with the rifle capture move
			Assassin.RifleCapture( new Direction(  0,  1 ), 1 );
			Assassin.RifleCapture( new Direction(  0, -1 ), 1 ); 
			Assassin.RifleCapture( new Direction(  1,  0 ), 1 );
			Assassin.RifleCapture( new Direction( -1,  0 ), 1 );
			Assassin.RifleCapture( new Direction(  1,  1 ), 1 );
			Assassin.RifleCapture( new Direction(  1, -1 ), 1 );
			Assassin.RifleCapture( new Direction( -1,  1 ), 1 );
			Assassin.RifleCapture( new Direction( -1, -1 ), 1 );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	add rule restricting Executioner trades
			AddRule( new AssassinTradeRestrictionRule( Assassin ) );

			//	add promotion rules
			List<PieceType> promotionType = new List<PieceType> { General };
			AddRule( new BasicPromotionRule( Pawn, promotionType, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( SilverGeneral, promotionType, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( GoldGeneral, promotionType, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( Knight, new List<PieceType> { KnightGeneral }, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( Camel, new List<PieceType> { CamelGeneral }, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( Rook, new List<PieceType> { DragonKing }, loc => loc.Rank >= 8 ) );
			AddRule( new BasicPromotionRule( Bishop, new List<PieceType> { DragonHorse }, loc => loc.Rank >= 8 ) );
		}
		#endregion
	}
}
