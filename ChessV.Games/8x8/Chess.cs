
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
	//                              Chess
	//
	//    This class implements the classic game of Chess.  Most of the 
	//    functionality, however, are in the generic game classes from 
	//    which it is derived as they are intended to provide functionality
	//    common to chess variants.

	[Game("Chess", typeof(Geometry.Rectangular), 8, 8,
		  XBoardName = "normal",
		  InventedBy = "Unknown", 
		  Invented = "circa 8th century", 
		  Tags = "Chess Variant,Historic,Regional,Popular", 
		  GameDescription1 = "Exact origins unknown", 
		  GameDescription2 = "Generally believed to be from India around the 6th century AD")]
	[Appearance(ColorScheme = "Luna Decorabat")]
	public class Chess: Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;


		// *** CONSTRUCTION *** //

		public Chess(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}
		

		// *** INITIALIZATION *** //
		
		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
			PawnDoubleMove = true;
			EnPassant = true;
			Castling.Value = "Standard";
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
			AddPieceType( Queen = new Queen( "Queen", "Q", 950, 1000 ) );
		}
		#endregion
	}
}
