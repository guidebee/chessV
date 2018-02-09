
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
using ChessV.Games.Pieces.Diamond;

namespace ChessV.Games
{
	//**********************************************************************
	//
	//                           DiamondChess
	//
	[Game("Diamond Chess", typeof(Geometry.Rectangular), 8, 8, 
		  XBoardName = "diamond_chess",
		  InventedBy = "J. A. Porterfield Rynd",
		  Invented = "1886",
		  Tags = "Chess Variant,Historic",
		  GameDescription1 = "Chess with the board rotated 45 degrees and a different",
		  GameDescription2 = "opening position.  Only the pawns move differently.")]
	class DiamondChess: ChessV.Games.Abstract.GenericChess
	{
		// *** PIECE TYPES *** //

		public PieceType DiamondPawn;
		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		

		// *** CONSTRUCTION *** //

		public DiamondChess(): 
			base
				( /* num files = */ 8, 
				  /* num ranks = */ 8,
				  /* Symmetry = */ new RotationalSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "krbp4/rqnp4/nbpp4/pppp4/4PPPP/4PPBN/4PNQR/4PBRK";
			PromotionTypes = "QRBN";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			replacePieceType( Pawn, DiamondPawn = new DiamondPawn( "Pawn", "P", 100, 125 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 500, 550 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 325, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 900, 1000 ) );
			pawnType = DiamondPawn;
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** PROMOTION *** //
			List<PieceType> availablePromotionTypes = ParseTypeListFromString( PromotionTypes );
			BasicPromotionRule( DiamondPawn, availablePromotionTypes, loc => loc.Rank == 7 || loc.File == 0 );
		}
		#endregion
	}
}
