
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
	//                           Generic12x8
	//
	//    The Generic game classes make it easier to specify games by 
	//    providing functionality common to chess variants.  This class 
	//    extends the Generic__x8 class by adding support for a 
	//    variety of different castling rules used on 12x8 board

	[Game("Generic 12x8", typeof(Geometry.Rectangular), 12, 8,
		  Template=true)]
	public class Generic12x8: Generic__x8
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Castling { get; set; }


		// *** CONSTRUCTION *** //

		public Generic12x8
			( Symmetry symmetry ):
				base
					( /* num files = */ 12,
					  /* symmetry = */ symmetry )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Castling = new ChoiceVariable( new string[] { "None", "3-3", "3-4", "4-4", "Close Rook 3-3", "Close Rook 3-4", "Close Rook 4-4", "Flexible", "Custom" } );
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
				//	Adding castling rule is somewhat complicated because there are a number of different forms 
				//	of castilng and we have to accomodate the King on either f1 or g1.  On 12x8 we always use 
				//	shredder notation for castling though.

				//	find the king's start square (must be f1 or g1)
				GenericPiece WhiteKing = new GenericPiece( 0, castlingType );
				GenericPiece BlackKing = new GenericPiece( 1, castlingType );
				string whiteKingSquare = null;
				string blackKingSquare = null;
				if( StartingPieces["f1"] == WhiteKing )
					whiteKingSquare = "f1";
				if( StartingPieces["g1"] == WhiteKing )
					whiteKingSquare = "g1";
				if( StartingPieces["f8"] == BlackKing )
					blackKingSquare = "f8";
				if( StartingPieces["g8"] == BlackKing )
					blackKingSquare = "g8";
				if( whiteKingSquare == null || blackKingSquare == null )
					throw new Exception( "Can't enable castling rule because King does not start on a supported square" );

				//	3-3 CASTLING - King slides three squares and corner piece jumps over to adjacent square
				if( Castling.Value == "3-3" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "j1", "l1", "i1", 'L' );
						CastlingMove( 0, "g1", "d1", "a1", "e1", 'A' );
					}
					else
					{
						CastlingMove( 0, "f1", "c1", "a1", "d1", 'A' );
						CastlingMove( 0, "f1", "i1", "l1", "h1", 'L' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "j8", "l8", "i8", 'l' );
						CastlingMove( 1, "g8", "d8", "a8", "e8", 'a' );
					}
					else
					{
						CastlingMove( 1, "f8", "c8", "a8", "d8", 'a' );
						CastlingMove( 1, "f8", "i8", "l8", "h8", 'l' );
					}
				}
				//	3-4 CASTLING - King slides three squares to the closer corner and four squares 
				//	to the farther and corner piece jumps over to adjacent square
				if( Castling.Value == "3-4" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "j1", "l1", "i1", 'L' );
						CastlingMove( 0, "g1", "c1", "a1", "d1", 'A' );
					}
					else
					{
						CastlingMove( 0, "f1", "c1", "a1", "d1", 'A' );
						CastlingMove( 0, "f1", "j1", "l1", "i1", 'L' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "j8", "l8", "i8", 'l' );
						CastlingMove( 1, "g8", "c8", "a8", "d8", 'a' );
					}
					else
					{
						CastlingMove( 1, "f8", "c8", "a8", "d8", 'a' );
						CastlingMove( 1, "f8", "j8", "l8", "i8", 'l' );
					}
				}
				//	4-4 CASTLING - King slides four squares and corner piece jumps over to adjacent square
				else if( Castling.Value == "4-4" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "k1", "l1", "j1", 'L' );
						CastlingMove( 0, "g1", "c1", "a1", "d1", 'A' );
					}
					else
					{
						CastlingMove( 0, "f1", "b1", "a1", "c1", 'A' );
						CastlingMove( 0, "f1", "j1", "l1", "i1", 'L' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "k8", "l8", "j8", 'l' );
						CastlingMove( 1, "g8", "c8", "a8", "d8", 'a' );
					}
					else
					{
						CastlingMove( 1, "f8", "b8", "a8", "c8", 'a' );
						CastlingMove( 1, "f8", "j8", "l8", "i8", 'l' );
					}
				}
				//	CLOSE ROOK 3-3 CASTLING - King slides three squares and piece one square in 
				//	from the corner jumps over to adjacent square
				if( Castling.Value == "Close Rook 3-3" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "j1", "k1", "i1", 'K' );
						CastlingMove( 0, "g1", "d1", "b1", "e1", 'B' );
					}
					else
					{
						CastlingMove( 0, "f1", "c1", "b1", "d1", 'B' );
						CastlingMove( 0, "f1", "i1", "k1", "h1", 'K' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "j8", "k8", "i8", 'k' );
						CastlingMove( 1, "g8", "d8", "b8", "e8", 'b' );
					}
					else
					{
						CastlingMove( 1, "f8", "c8", "b8", "d8", 'b' );
						CastlingMove( 1, "f8", "i8", "k8", "h8", 'k' );
					}
				}
				//	CLOSE ROOK 3-4 CASTLING - King slides three squares to the closer corner and 
				//	four squares to the farther and piece one square in from the corner 
				//	jumps over to adjacent square
				if( Castling.Value == "Close Rook 3-4" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "j1", "k1", "i1", 'K' );
						CastlingMove( 0, "g1", "c1", "b1", "d1", 'B' );
					}
					else
					{
						CastlingMove( 0, "f1", "c1", "b1", "d1", 'B' );
						CastlingMove( 0, "f1", "j1", "k1", "i1", 'K' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "j8", "k8", "i8", 'k' );
						CastlingMove( 1, "g8", "c8", "b8", "d8", 'b' );
					}
					else
					{
						CastlingMove( 1, "f8", "c8", "b8", "d8", 'b' );
						CastlingMove( 1, "f8", "j8", "k8", "i8", 'k' );
					}
				}
				//	CLOSE ROOK 4-4 CASTLING - King slides four squares and the piece on square in 
				//	from the corner jumps over to adjacent square
				else if( Castling.Value == "Close Rook 4-4" )
				{
					CastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "k1", "k1", "j1", 'K' );
						CastlingMove( 0, "g1", "c1", "b1", "d1", 'B' );
					}
					else
					{
						CastlingMove( 0, "f1", "b1", "b1", "c1", 'B' );
						CastlingMove( 0, "f1", "j1", "k1", "i1", 'K' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "k8", "k8", "j8", 'k' );
						CastlingMove( 1, "g8", "c8", "b8", "d8", 'b' );
					}
					else
					{
						CastlingMove( 1, "f8", "b8", "b8", "c8", 'b' );
						CastlingMove( 1, "f8", "j8", "k8", "i8", 'k' );
					}
				}
				//	FLEXIBLE CASTLING - King slides two or more squares towards the corner 
				//	and the corner piece leaps to the square immediately to the other side
				else if( Castling.Value == "Flexible" )
				{
					FlexibleCastlingRule();
					if( whiteKingSquare == "g1" )
					{
						CastlingMove( 0, "g1", "i1", "l1", "i1", 'L' );
						CastlingMove( 0, "g1", "e1", "a1", "e1", 'A' );
					}
					else
					{
						CastlingMove( 0, "f1", "d1", "a1", "d1", 'A' );
						CastlingMove( 0, "f1", "h1", "l1", "h1", 'L' );
					}
					if( blackKingSquare == "g8" )
					{
						CastlingMove( 1, "g8", "i8", "l8", "i8", 'l' );
						CastlingMove( 1, "g8", "e8", "a8", "e8", 'a' );
					}
					else
					{
						CastlingMove( 1, "f8", "d8", "a8", "d8", 'a' );
						CastlingMove( 1, "f8", "h8", "l8", "h8", 'l' );
					}
				}
			}
		}
		#endregion
	}
}
