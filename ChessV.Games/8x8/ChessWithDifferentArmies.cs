
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
	[Game("Chess with Different Armies", typeof(Geometry.Rectangular), 8, 8, 
		  InventedBy = "Ralph Betza",
		  Invented = "1996",
		  Tags = "Chess Variant,Popular,Different Armies")]
	[Appearance(ColorScheme="Orchid")]
	public class ChessWithDifferentArmies : Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //
		
		//	Fabulous FIDEs
		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;

		//	Colorbound Clobberers
		public PieceType Archbishop;
		public PieceType WarElephant;
		public PieceType Phoenix;
		public PieceType Cleric;

		//	Remarkable Rookies
		public PieceType ShortRook;
		public PieceType Bowman;
		public PieceType Lion;
		public PieceType Chancellor;

		//	Nutty Knights
		public PieceType ChargingRook;
		public PieceType NarrowKnight;
		public PieceType ChargingKnight;
		public PieceType Colonel;


		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable WhiteArmy { get; set; }
		[GameVariable] public ChoiceVariable BlackArmy { get; set; }


		// *** CONSTRUCTION *** //

		public ChessWithDifferentArmies(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "#{BlackArray}/8/8/8/8/#{WhiteArray}";
			Castling.RemoveChoice( "Flexible" );
			WhiteArmy = new ChoiceVariable( new string[] { "Fabulous FIDEs", "Colorbound Clobberers", "Remarkable Rookies", "Nutty Knights" } );
			BlackArmy = new ChoiceVariable( new string[] { "Fabulous FIDEs", "Colorbound Clobberers", "Remarkable Rookies", "Nutty Knights" } );
			PawnDoubleMove = true;
			Castling.Value = "Standard";
		}
		#endregion

		#region SetOtherVariables
		public override void SetOtherVariables()
		{
			base.SetOtherVariables();
			//	Set BlackArmy array
			if( BlackArmy.Value == "Fabulous FIDEs" )
				SetCustomProperty( "BlackArray", "rnbqkbnr/pppppppp" );
			else if( BlackArmy.Value == "Colorbound Clobberers" )
				SetCustomProperty( "BlackArray", "_cl_ph_weak_we_ph_cl/pppppppp" );
			else if( BlackArmy.Value == "Remarkable Rookies" )
				SetCustomProperty( "BlackArray", "_sr_bolckl_bo_sr/pppppppp" );
			else if( BlackArmy.Value == "Nutty Knights" )
				SetCustomProperty( "BlackArray", "_cr_nn_cn_cok_cn_nn_cr/pppppppp" );
			//	Set WhiteArmy array
			if( WhiteArmy.Value == "Fabulous FIDEs" )
				SetCustomProperty( "WhiteArray", "PPPPPPPP/RNBQKBNR" );
			else if( WhiteArmy.Value == "Colorbound Clobberers" )
				SetCustomProperty( "WhiteArray", "PPPPPPPP/_CL_PH_WEAK_WE_PH_CL" );
			else if( WhiteArmy.Value == "Remarkable Rookies" )
				SetCustomProperty( "WhiteArray", "PPPPPPPP/_SR_BOLCKL_BO_SR" );
			else if( WhiteArmy.Value == "Nutty Knights" )
				SetCustomProperty( "WhiteArray", "PPPPPPPP/_CR_NN_CN_COK_CN_NN_CR" );
			//	Set pawn promotion types
			PromotionTypes = "";
			if( BlackArmy.Value == "Fabulous FIDEs" || WhiteArmy.Value == "Fabulous FIDEs" )
				PromotionTypes += "QRBN";
			if( BlackArmy.Value == "Colorbound Clobberers" || WhiteArmy.Value == "Colorbound Clobberers" )
				PromotionTypes += "_CL_PH_WEA";
			if( BlackArmy.Value == "Remarkable Rookies" || WhiteArmy.Value == "Remarkable Rookies" )
				PromotionTypes += "_SR_BOLC";
			if( BlackArmy.Value == "Nutty Knights" || WhiteArmy.Value == "Nutty Knights" )
				PromotionTypes += "_CR_NN_CN_CO";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();

			if( WhiteArmy.Value == "Fabulous FIDEs" || BlackArmy.Value == "Fabulous FIDEs" )
			{
				AddPieceType( Queen = new Queen( "Queen", "Q", 950, 1050 ) );
				AddPieceType( Rook = new Rook( "Rook", "R", 500, 550 ) );
				AddPieceType( Bishop = new Bishop( "Bishop", "B", 325, 350 ) );
				AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			}
			if( WhiteArmy.Value == "Colorbound Clobberers" || BlackArmy.Value == "Colorbound Clobberers" )
			{
				AddPieceType( Archbishop = new Archbishop( "Archbishop", "A", 875, 875 ) );
				AddPieceType( WarElephant = new WarElephant( "War Elephant", "WE", 475, 475 ) );
				AddPieceType( Phoenix = new Phoenix( "Phoenix", "PH", 315, 315 ) );
				AddPieceType( Cleric = new Cleric( "Cleric", "CL", 500, 525 ) );
			}
			if( WhiteArmy.Value == "Remarkable Rookies" || BlackArmy.Value == "Remarkable Rookies" )
			{
				AddPieceType( Chancellor = new Chancellor( "Chancellor", "C", 950, 1000 ) );
				AddPieceType( ShortRook = new ShortRook( "Short Rook", "SR", 400, 425 ) );
				AddPieceType( Bowman = new Bowman( "Bowman", "BO", 310, 320 ) );
				AddPieceType( Lion = new Lion( "Lion", "L", 500, 500 ) );
			}
			if( WhiteArmy.Value == "Nutty Knights" || BlackArmy.Value == "Nutty Knights" )
			{
				AddPieceType( ChargingRook = new ChargingRook( "Charging Rook", "CR", 500, 525 ) );
				AddPieceType( NarrowKnight = new NarrowKnight( "Narrow Knight", "NN", 300, 300 ) );
				AddPieceType( ChargingKnight = new ChargingKnight( "Charging Knight", "CN", 350, 325 ) );
				AddPieceType( Colonel = new Colonel( "Colonel", "CO", 950, 950 ) );
			}
			//	Army adjustment
			if( (WhiteArmy.Value == "Fabulous FIDEs" && BlackArmy.Value == "Remarkable Rookies") ||
				(BlackArmy.Value == "Fabulous FIDEs" && WhiteArmy.Value == "Remarkable Rookies") )
			{
				//	increase the value of the bishops since the Rookies have no piece that moves that way
				Bishop.MidgameValue += 35;
				Bishop.EndgameValue += 35;
			}
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			//	allow base class to handle, but stop it from adding the castling rule!  CwDA needs 
			//	special castling rules becaue of colorbound rook-equivalents in Colorbound Colobberes
			string castlingOption = Castling.Value;
			Castling.Value = "None";
			base.AddRules();
			Castling.Value = castlingOption;

			// *** PAWN PROMOTION *** //
			List<PieceType> availablePromotionTypes = new List<PieceType>();
			if( WhiteArmy.Value == "Fabulous FIDEs" || BlackArmy.Value == "Fabulous FIDEs" )
			{
				availablePromotionTypes.Add( Queen );
				availablePromotionTypes.Add( Rook );
				availablePromotionTypes.Add( Bishop );
				availablePromotionTypes.Add( Knight );
			}
			if( WhiteArmy.Value == "Colorbound Clobberers" || BlackArmy.Value == "Colorbound Clobberers" )
			{
				availablePromotionTypes.Add( Archbishop );
				availablePromotionTypes.Add( WarElephant );
				availablePromotionTypes.Add( Phoenix );
				availablePromotionTypes.Add( Cleric );
			}
			if( WhiteArmy.Value == "Remarkable Rookies" || BlackArmy.Value == "Remarkable Rookies" )
			{
				availablePromotionTypes.Add( ShortRook );
				availablePromotionTypes.Add( Bowman );
				availablePromotionTypes.Add( Lion );
				availablePromotionTypes.Add( Chancellor );
			}
			if( WhiteArmy.Value == "Nutty Knights" || BlackArmy.Value == "Nutty Knights" )
			{
				availablePromotionTypes.Add( ChargingRook );
				availablePromotionTypes.Add( NarrowKnight );
				availablePromotionTypes.Add( ChargingKnight );
				availablePromotionTypes.Add( Colonel );
			}
			BasicPromotionRule( Pawn, availablePromotionTypes, loc => loc.Rank == 7 );

			// *** CASTLING *** //
			if( castlingOption == "Standard" )
			{
				CastlingRule();
				CastlingMove( 0, "e1", "g1", "h1", "f1", 'K' );
				CastlingMove( 1, "e8", "g8", "h8", "f8", 'k' );
				if( WhiteArmy.Value == "Colorbound Clobberers" )
					CastlingMove( 0, "e1", "b1", "a1", "c1", 'Q' );
				else
					CastlingMove( 0, "e1", "c1", "a1", "d1", 'Q' );
				if( BlackArmy.Value == "Colorbound Clobberers" )
					CastlingMove( 1, "e8", "b8", "a8", "c8", 'q' );
				else
					CastlingMove( 1, "e8", "c8", "a8", "d8", 'q' );
			}
		}
		#endregion
	}
}
