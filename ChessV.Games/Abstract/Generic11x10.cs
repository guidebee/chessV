
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

namespace ChessV.Games.Abstract
{
	//**********************************************************************
	//
	//                           Generic11x10
	//
	//    The Generic game classes make it easier to specify games by 
	//    providing functionality common to chess variants.  This class 
	//    extends the Generic__x10 class by adding support for castling 
	//    on an 11x10 board
	[Game("Generic 11x10", typeof(Geometry.Rectangular), 11, 10, 
		  Template = true)]
	public class Generic11x10: Generic__x10
    {
		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Castling { get; set; }


		// *** CONSTRUCTION *** //

		public Generic11x10
			( Symmetry symmetry ): 
				base
					( /* num files = */ 11, 
					  /* symmetry = */ symmetry )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Castling = new ChoiceVariable( new string[] { "None", "Standard", "Long", "Flexible", "Close-Rook", "Wildebeest", "Custom" } );
			Castling.Value = "None";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
            base.AddRules();

			// *** CASTLING *** //
			if( Castling.Value != "None" )
			{
				//	find the king's start square (must be f1)
				GenericPiece WhiteKing = new GenericPiece( 0, castlingType );
				GenericPiece BlackKing = new GenericPiece( 1, castlingType );
				if( StartingPieces["f1"] != WhiteKing || StartingPieces["f10"] != BlackKing )
					throw new Exception( "Can't enable castling rule because King does not start on a supported square" );

				//	FLEXIBLE Castilng - King starts in center and can slide 2, 3 or 4 squares 
				//	toward the castling piece on the corner square which hops to the other side
				//	STANDARD CASTLING - King slides three squares and corner piece jumps over to adjacent square
				if( Castling.Value == "Standard" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "i1", "k1", "h1", 'K' );
					CastlingMove( 0, "f1", "c1", "a1", "d1", 'A' );
					CastlingMove( 1, "f10", "i10", "k10", "h10", 'k' );
					CastlingMove( 1, "f10", "c10", "a10", "d10", 'a' );
				}
				//	LONG CASTLING - King slides four squares and the corner piece jumps over to adjacent square
				else if( Castling.Value == "Long" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "j1", "k1", "i1", 'K' );
					CastlingMove( 0, "f1", "b1", "a1", "c1", 'A' );
					CastlingMove( 1, "f10", "j10", "k10", "i10", 'k' );
					CastlingMove( 1, "f10", "b10", "a10", "c10", 'a' );
				}
				else if( Castling.Value == "Flexible" )
				{
					FlexibleCastlingRule();
					FlexibleCastlingMove( 0, "f1", "h1", "k1", 'K' );
					FlexibleCastlingMove( 0, "f1", "d1", "a1", 'A' );
					FlexibleCastlingMove( 1, "f10", "h10", "k10", 'k' );
					FlexibleCastlingMove( 1, "f10", "d10", "a10", 'a' );
				}
				//	CLOSE ROOK CASTLING - Castling pieces are on b1/b10 and j1/j10 rather than in the 
				//	corners.  King slides three squares and castling piece jumps over to adjacent square.
				else if( Castling.Value == "Close-Rook" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "i1", "j1", "h1", 'J' );
					CastlingMove( 0, "f1", "c1", "b1", "d1", 'B' );
					CastlingMove( 1, "f10", "i10", "j10", "h10", 'j' );
					CastlingMove( 1, "f10", "c10", "b10", "d10", 'b' );
				}
				//	WILDEBEEST Castling - the castling rule from Wildebeest Chess.  The King slides 
				//	one to four spaces toward the rook and the rook jumps over to the adjacent square
				else if( Castling.Value == "Wildebeest" )
				{
					FlexibleCastlingRule();
					FlexibleCastlingMove( 0, "f1", "g1", "k1", 'K' );
					FlexibleCastlingMove( 0, "f1", "e1", "a1", 'A' );
					FlexibleCastlingMove( 1, "f10", "g10", "k10", 'k' );
					FlexibleCastlingMove( 1, "f10", "e10", "a10", 'a' );
				}
			}
		}
		#endregion
    }
}
