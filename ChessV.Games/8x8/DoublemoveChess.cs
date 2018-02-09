
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
	[Game("Doublemove Chess", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "1957",
		  InventedBy = "Fred Galvin",
		  Tags = "Chess Variant",
		  GameDescription1 = "Except for white's first move, players make two",
		  GameDescription2 = "moves per turn.  Win by capturing the king.")]
	public class DoublemoveChess: Chess
	{
		// *** CONSTRUCTION *** //

		public DoublemoveChess()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	No check, checkmate, or stalemate
			RemoveRule( typeof(Rules.CheckmateRule) );

			//	No en passant
			RemoveRule( typeof(Rules.EnPassantRule) );

			//	Victory by capturing the king
			AddRule( new Rules.Extinction.ExtinctionRule( "K" ) );

			//	Add DoubleMoveCompletionRule (which will automatically 
			//	replace MoveCompletionDefaultRule since there can be 
			//	only one MoveCompletionRule)
			AddRule( new Rules.MultiMove.DoubleMoveCompletionRule() );
		}
		#endregion
	}
}
