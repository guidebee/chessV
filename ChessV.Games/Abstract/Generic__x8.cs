
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
	//                           Generic__x8
	//
	//    The Generic game classes make it easier to specify games by 
	//    providing functionality common to chess variants.  This class 
	//    is for chess variants on boards with 8 ranks and is used as a 
	//    base class for other generic boards such as 8x8 and 10x8.
	//
	//    It derives from the GenericChess class which provides the 
	//    rules for a game with Pawns and a Royal King, as well as the 
	//    50-move and draw-by-repetition rules.
	//
	//    This class adds optional support for the two-move initial pawn 
	//    move and En Passant.

	public class Generic__x8: GenericChess
	{
        // *** CONSTRUCTION *** //

        public Generic__x8
			( int nFiles,               // number of files on main part of board
			  Symmetry symmetry ):       // symmetry determining board mirroring/rotation
				base( nFiles, /* num ranks = */ 8, symmetry )
        {
        }


		// *** GAME VARIABLES *** //

		[GameVariable] public bool PawnDoubleMove { get; set; }
		[GameVariable] public bool EnPassant { get; set; }


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			PawnDoubleMove = false;
			EnPassant = false;
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** PAWN DOUBLE MOVE *** //
			if( PawnDoubleMove )
			{
				MoveCapability doubleMove = new MoveCapability();
				doubleMove.MinSteps = 2;
				doubleMove.MaxSteps = 2;
				doubleMove.MustCapture = false;
				doubleMove.CanCapture = false;
				doubleMove.Direction = new Direction( 1, 0 );
				doubleMove.Condition = location => location.Rank == 1;
				Pawn.AddMoveCapability( doubleMove );
			}

			// *** EN-PASSANT *** //
			if( EnPassant )
				EnPassantRule( Pawn, new Direction( 1, 0 ) );
		}
		#endregion
	}
}
