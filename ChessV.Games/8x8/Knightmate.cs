
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
	//**********************************************************************
	//
	//                             Knightmate
	//

	[Game("Knightmate", typeof(Geometry.Rectangular), 8, 8, 
		  XBoardName = "knightmate",
		  InventedBy = "Bruce Zimov",
		  Invented = "1972",
		  Tags = "Chess Variant,Popular",
		  GameDescription1 = "Player has two Kings where the Knights usually are",
		  GameDescription2 = "and a royal Knight where the King usually is")]
	[Appearance(ColorScheme="Cinnamon")]
	public class Knightmate: Chess
	{
		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rkbqnbkr/pppppppp/8/8/8/8/PPPPPPPP/RKBQNBKR";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			castlingType = Knight;
			Knight.MidgameValue = Knight.EndgameValue = 0;
			King.MidgameValue = King.EndgameValue = 325;

			//	fix the King's piece-square-tables to more 
			//	reasonable settings since it is not royal
			King.PSTMidgameInSmallCenter = 4;
			King.PSTMidgameInLargeCenter = 4;
			King.PSTMidgameSmallCenterAttacks = 1;
			King.PSTMidgameLargeCenterAttacks = 1;
			King.PSTMidgameForwardness = 2;
			King.PSTEndgameInSmallCenter = 3;
			King.PSTEndgameInLargeCenter = 3;
			King.PSTEndgameSmallCenterAttacks = 1;
			King.PSTEndgameLargeCenterAttacks = 1;
			King.PSTEndgameForwardness = 1;
			//	keep the Knight out of the center in the 
			//	midgame since it is royal
			Knight.PSTMidgameInSmallCenter = 0;
			Knight.PSTMidgameInLargeCenter = 0;
			Knight.PSTMidgameSmallCenterAttacks = 0;
			Knight.PSTMidgameLargeCenterAttacks = 0;
			Knight.PSTMidgameForwardness = -15;
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			//	get rid of the Checkmate Rule
			RemoveRule( typeof(Rules.CheckmateRule) );
			//	add the new Checkmate rule
			AddRule( new Rules.CheckmateRule( Knight ) );
		}
		#endregion
	}
}
