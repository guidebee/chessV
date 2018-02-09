
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
	[Game("Maryland Chess", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "2017",
		  InventedBy = "Greg Strong",
		  Tags = "Chess Variant")]
	public class MarylandChess: Abstract.Generic8x8
	{
		// *** PIECE TYPES *** //

		public PieceType Bowman;
		public PieceType Knight;
		public PieceType Elephant;
		public PieceType GoldGeneral;
		public PieceType SilverGeneral;


		// *** CONSTRUCTION *** //

		public MarylandChess():
			base
				 ( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {castling} {last square} {half-move clock} {turn number}";
			FENStart = "#{Array} w #default #default 0 1";
			Array = "bnekkenb/pppsgppp/3pp3/8/8/3PP3/PPPGSPPP/BNEKKENB";
			PromotionTypes = "BNEGS";
			PawnDoubleMove = false;
			EnPassant = false;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Bowman = new Bowman( "Bowman", "B", 300, 300 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			AddPieceType( Elephant = new ElephantFerz( "Elephant", "E", 275, 275 ) );
			AddPieceType( GoldGeneral = new GoldGeneral( "Gold General", "G", 225, 225 ) );
			AddPieceType( SilverGeneral = new SilverGeneral( "Silver General", "S", 200, 200 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			//	Remove the Checkmate rule
			RemoveRule( typeof(Rules.CheckmateRule) );
			//	Player can win by capturing last king or last pawn.
			//	This is handled by the ExtinctionRule
			AddRule( new Rules.Extinction.ExtinctionRule( "KP" ) );
			//	Add MarylandChessMoveCompletionRule (which will automatically 
			//	replace MoveCompletionDefaultRule since there can be 
			//	only one MoveCompletionRule)
			AddRule( new Rules.MultiMove.MarylandChessMoveCompletionRule( King, Pawn ) );
			//	Game is won if a King reaches the last rank
			AddRule( new Rules.LocationVictoryConditionRule( King, loc => loc.Rank == 7 ) );
		}
		#endregion
	}
}
