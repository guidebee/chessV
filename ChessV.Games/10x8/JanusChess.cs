
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
	//                         JanusChess
	//
	[Game("Janus Chess", typeof(Geometry.Rectangular), 10, 8,
		  XBoardName = "janus",
		  Invented = "1978",
		  InventedBy = "Werner Schöndorf",
		  Tags = "Chess Variant,Popular")]
	public class JanusChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public JanusChess()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rjnbkqbnjr/pppppppppp/10/10/10/10/PPPPPPPPPP/RJNBKQBNJR";
			PawnDoubleMove = true;
			EnPassant = true;
			PromotionTypes = "QJRBN";
			Castling.Value = "Long";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			//	The Chancellor is not used in this game
			Chancellor.Enabled = false;
		}
		#endregion

		#region ChangePieceNames
		public override void ChangePieceNames()
		{
			Archbishop.Name = "Janus";
			Archbishop.Notation = "J";
		}
		#endregion
	}
}
