
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
	//********************************************************************
	//
	//                        NewChancellorChess
	//
	[Game("New Chancellor Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "1997",
		  InventedBy = "David Paulowich",
		  Tags = "Chess Variant")]
	public class NewChancellorChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public NewChancellorChess()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "crnbqkbnrc/pppppppppp/10/10/10/10/PPPPPPPPPP/CRNBQKBNRC";
			PawnDoubleMove = true;
			EnPassant = true;
			Castling.Value = "Close-Rook";
			PromotionTypes = "QCRBN";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			//	The Archbishop is not used in this game
			Archbishop.Enabled = false;
		}
		#endregion
	}
}
