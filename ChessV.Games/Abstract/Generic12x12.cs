
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
	//                           Generic12x12
	//
	//    The Generic game classes make it easier to specify games by 
	//    providing functionality common to chess variants.  This class 
	//    extends the Generic__x12 class by adding support for a 
	//    variety of different castling rules commonly used on 12x12 board

	[Game("Generic 12x12", typeof(Geometry.Rectangular), 12, 12,
		  Template=true)]
	public class Generic12x12: Generic__x12
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Castling { get; set; }


		// *** CONSTRUCTION *** //

		public Generic12x12
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
			Castling = new ChoiceVariable( new string[] { "None", "Flexible", "Custom" } );
			Castling.Value = "None";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** CASTLING *** //

			#region Castling
			if( Castling.Value != "None" && Castling.Value != "Custom" )
			{
				//	find the king's start square (must be f1 or g1)
				GenericPiece WhiteKing = new GenericPiece( 0, castlingType );
				GenericPiece BlackKing = new GenericPiece( 1, castlingType );
				string kingSquare;
				if( StartingPieces["f1"] == WhiteKing )
					kingSquare = "f1";
				else if( StartingPieces["g1"] == WhiteKing )
					kingSquare = "g1";
				else
					throw new Exception( "Can't enable castling rule because King does not start on a supported square" );

				//	FLEXIBLE CASTLING - King slides two or more squares (but must stop short of the 
				//	corner) and the corner piece jumps over to adjacent square
				if( Castling.Value == "Flexible" )
				{
					FlexibleCastlingRule();
					if( kingSquare == "g1" )
					{
						FlexibleCastlingMove( 0, "g1", "i1", "l1", 'K' );
						FlexibleCastlingMove( 0, "g1", "e1", "a1", 'Q' );
						FlexibleCastlingMove( 1, "g12", "i12", "l12", 'k' );
						FlexibleCastlingMove( 1, "g12", "e12", "a12", 'q' );
					}
					else
					{
						FlexibleCastlingMove( 0, "e1", "c1", "a1", 'A' );
						FlexibleCastlingMove( 0, "e1", "g1", "l1", 'L' );
						FlexibleCastlingMove( 1, "e12", "c12", "a12", 'a' );
						FlexibleCastlingMove( 1, "e12", "g12", "l12", 'l' );
					}
				}
			}
			#endregion
		}
		#endregion
	}
}
