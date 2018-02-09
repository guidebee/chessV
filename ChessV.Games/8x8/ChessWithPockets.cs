
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
using ChessV;

namespace ChessV.Games
{
	[Game("Pocket Knight", typeof(Geometry.Rectangular), 8, 8, 
		  XBoardName = "pocketknight",
		  Invented = "Circa 1900",
		  InventedBy = "Unknown", 
		  Tags = "Chess Variant,Popular", 
		  Definitions = "PocketPieces=Nn")]
	[Appearance(ColorScheme = "Norwegian Wood")]
	public class ChessWithPockets: Chess
	{
		// *** GAME VARIABLES *** //

		//	stores the notation of the pieces players have in pocket at start of game
		[GameVariable] public string PocketPieces { get; set; }


		// *** CONSTRUCTION *** //

		public ChessWithPockets()
		{
		}


		// *** INITIALIZATION *** //

		#region CreateBoard
		public override Board CreateBoard( int nPlayers, int nFiles, int nRanks, Symmetry symmetry )
		{ return new ChessV.Boards.BoardWithPockets( nFiles, nRanks, 2 ); }
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {pieces in hand} {castling} {en-passant} {half-move clock} {turn number}";
			FENStart = "#{Array} w #{PocketPieces} KQkq - 0 1";
			Array = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			AddRule( new Rules.Pocket.PocketDropRule() );
		}
		#endregion
	}
}
