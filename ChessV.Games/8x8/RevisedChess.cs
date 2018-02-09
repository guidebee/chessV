
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
	[Game("Revised Chess", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "2009",
		  InventedBy = "Mats Winther",
		  Tags = "Chess Variant",
		  GameDescription1 = "Pawn gains the ability to capture forward on 7th rank",
		  GameDescription2 = "This reduces the number of drawn endgames")]
	public class RevisedChess: Chess
	{
		// *** CONSTRUCTION *** //

		public RevisedChess()
		{
		}


		// *** INITIALIZATION *** //

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	pawn has a foward capturing move on 7th rank
			MoveCapability move = new MoveCapability();
			move.MaxSteps = 1;
			move.MustCapture = true;
			move.CanCapture = true;
			move.Direction = new Direction( 1, 0 );
			move.Condition = location => location.Rank == 6;
			Pawn.AddMoveCapability( move );
		}
		#endregion
	}
}
