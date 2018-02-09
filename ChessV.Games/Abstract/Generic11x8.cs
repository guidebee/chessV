
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
	//                           Generic11x8
	//
	//    The Generic game classes make it easier to specify games by 
	//    providing functionality common to chess variants.  This class 
	//    extends the Generic__x8 class by adding support for a 
	//    variety of different castling rules commonly used on 11x8 board

	[Game("Generic 11x8", typeof( Geometry.Rectangular ), 11, 8,
		  Template = true)]
	public class Generic11x8: Generic__x8
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Castling { get; set; }


		// *** CONSTRUCTION *** //

		public Generic11x8
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
			Castling = new ChoiceVariable( new string[] { "None", "Standard", "Long", "Flexible", "Close-Rook", "Custom" } );
			Castling.Value = "None";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** CASTLING *** //

			if( Castling.Value != "None" && Castling.Value != "Custom" )
			{
				//	find the king's start square, confirm the kings are centered on f1/f8
				GenericPiece WhiteKing = new GenericPiece( 0, castlingType );
				GenericPiece BlackKing = new GenericPiece( 1, castlingType );
				if( StartingPieces["f1"] != WhiteKing || StartingPieces["f8"] != BlackKing )
					throw new Exception( "Can't enable castling rule because King does not start on a supported square" );

				//	STANDARD CASTLING - King slides three squares and corner piece jumps over to adjacent square
				if( Castling.Value == "Standard" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "i1", "k1", "h1", 'K' );
					CastlingMove( 0, "f1", "c1", "a1", "d1", 'A' );
					CastlingMove( 1, "f8", "i8", "k8", "h8", 'k' );
					CastlingMove( 1, "f8", "c8", "a8", "d8", 'a' );
				}
				//	LONG CASTLING - King slides four squares and the corner piece jumps over to adjacent square
				else if( Castling.Value == "Long" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "j1", "k1", "i1", 'K' );
					CastlingMove( 0, "f1", "b1", "a1", "c1", 'A' );
					CastlingMove( 1, "f8", "j8", "k8", "i8", 'k' );
					CastlingMove( 1, "f8", "b8", "a8", "c8", 'a' );
				}
				//	FLEXIBLE CASTLING - King slides two or more squares (but must stop short of the 
				//	corner) and the corner piece jumps over to adjacent square
				else if( Castling.Value == "Flexible" )
				{
					FlexibleCastlingRule();
					FlexibleCastlingMove( 0, "f1", "h1", "k1", 'K' );
					FlexibleCastlingMove( 0, "f1", "d1", "a1", 'A' );
					FlexibleCastlingMove( 1, "f8", "h8", "k8", 'k' );
					FlexibleCastlingMove( 1, "f8", "d8", "a8", 'a' );
				}
				//	CLOSE ROOK CASTLING - Castling pieces are on b1/b8 and j1/j8 rather than in the 
				//	corners.  King slides three squares and castling piece jumps over to adjacent square.
				else if( Castling.Value == "Close-Rook" )
				{
					CastlingRule();
					CastlingMove( 0, "f1", "i1", "j1", "h1", 'J' );
					CastlingMove( 0, "f1", "c1", "b1", "d1", 'B' );
					CastlingMove( 1, "f8", "i8", "j8", "h8", 'j' );
					CastlingMove( 1, "f8", "c8", "b8", "d8", 'b' );
				}
			}
		}
		#endregion
	}
}
