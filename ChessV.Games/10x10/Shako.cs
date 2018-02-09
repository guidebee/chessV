
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
	//                             Shako
	//
	//    This class implements Shako by Jean-Louis Cazaux.  This is an 
	//    East-meets-West game that maintains the features of Chess while 
	//    adding the Cannon from Chinese Chess (Xiangqi) and a slightly 
	//    stronger Elephant.  The King can castle, but is on the second 
	//    rank, so the castling rule in Shako is custom and cannot be 
	//    altered by the Castling game variable.

	[Game("Shako", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "1997",
		  InventedBy = "Jean-Louis Cazaux",
	      Tags = "Chess Variant",
		  GameDescription1 = "East-meets-West game that maintains the features of Chess",
		  GameDescription2 = "while adding the cannon from Xiangqi and a stronger elephant")]
	public class Shako: Abstract.Generic10x10
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Elephant;
		public PieceType Cannon;


		// *** CONSTRUCTION *** //

		public Shako(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "c8c/ernbqkbnre/pppppppppp/10/10/10/10/PPPPPPPPPP/ERNBQKBNRE/C8C";
			PawnMultipleMove.Value = "Grand";
			Castling.Value = "2R Close-Rook";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QRBNEC";
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 1025, 1250 ) );
			AddPieceType( Cannon = new Cannon( "Cannon", "C", 400, 275 ) );
			Elephant = new Elephant( "Elephant", "E", 225, 225 );
			Elephant.Step( new Direction(  1,  1 ) );
			Elephant.Step( new Direction( -1,  1 ) );
			Elephant.Step( new Direction(  1, -1 ) );
			Elephant.Step( new Direction( -1, -1 ) );
			AddPieceType( Elephant );
		}
		#endregion
	}
}
