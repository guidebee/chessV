
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
	[Game("File Sharing Chess", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "2016",
		  InventedBy = "Jeffrey T. Kubach",
		  Tags = "Chess Variant",
		  GameDescription1 = "To reduce draws and open up closed positions",
		  GameDescription2 = "you can swap directly opposed pawns, subject to restrictions")]
	public class FileSharingChess: Chess
	{
		// *** INITIALIZATION *** //

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	add the PawnSwapRule
			AddRule( new Rules.FileSharing.PawnSwapRule( Pawn ) );
		}
		#endregion
	}
}
