
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
	//                      Double Chess 16x8
	//

	[Game("Double Chess (16 x 8)", typeof(Geometry.Rectangular), 16, 8,
		  Invented = "1996",
		  InventedBy = "David Short",
		  Tags = "Chess Variant",
		  GameDescription1 = "Chess played with two boards put together and two sets of pieces",
		  GameDescription2 = "except that the second King is replaced with a third Queen")]
	[Appearance(ColorScheme = "Cape Cod")]
	public class DoubleChess16x8: Abstract.Generic__x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;


		// *** CONSTRUCTION *** //
		public DoubleChess16x8():
			base
				( /* num files = */ 16, 
		          /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbqrnbqkbnrqbnr/pppppppppppppppp/16/16/16/16/PPPPPPPPPPPPPPPP/RNBQRNBQKBNRQBNR";
			PawnDoubleMove = true;
			EnPassant = true;
			PromotionRule.Value = "Standard";
			PromotionTypes = "QRBN";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** CASTLING *** //
			CastlingRule();
			//	White inside castling moves
			CastlingMove( 0, "i1", "k1", "l1", "j1", 'L' );
			CastlingMove( 0, "i1", "g1", "e1", "h1", 'E' );
			//	White outside castling moves
			CastlingMove( 0, "i1", "m1", "p1", "l1", 'P' );
			CastlingMove( 0, "i1", "e1", "a1", "f1", 'A' );
			//	Black inside castling moves
			CastlingMove( 1, "i8", "k8", "l8", "j8", 'l' );
			CastlingMove( 1, "i8", "g8", "e8", "h8", 'e' );
			//	Black outside castling moves
			CastlingMove( 1, "i8", "m8", "p8", "l8", 'p' );
			CastlingMove( 1, "i8", "e8", "a8", "f8", 'a' );
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 600, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 325, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 275, 275 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 1050, 1150 ) );
		}
		#endregion
	}
}
