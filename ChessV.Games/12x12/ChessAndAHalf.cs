
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
	[Game("Chess and a Half", typeof(Geometry.Rectangular), 12, 12,
		  Invented = "2017",
		  InventedBy = "Nicolino Will",
		  Tags = "Chess Variant")]
	public class ChessAndAHalf: Abstract.Generic12x12
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Guard;
		public PieceType Cat;
		public PieceType StarCat;
		public PieceType SpeedyKnight;
		public PieceType EquesRex;


		// *** CONSTRUCTION *** //

		public ChessAndAHalf():
			base
				 ( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbsccqkcscbnr/pppgppppgppp/12/12/12/12/12/12/12/12/PPPGPPPPGPPP/RNBSCCQKCSCBNR";
			PawnMultipleMove.Value = "@2(2,3,4)";
			Castling.Value = "Flexible";
			PromotionTypes = "QRBNCG";
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();

			//	basic types:
			AddPieceType( Rook = new Rook( "Rook", "R", 750, 850 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 450 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 1300, 1400 ) );
			AddPieceType( Guard = new General( "Guard", "G", 300, 325 ) );
			AddPieceType( Cat = new JumpingGeneral( "Cat", "C", 600, 600, "Cat" ) );
			AddPieceType( SpeedyKnight = new Knightrider( "Speedy Knight", "SN", 550, 550, "Knightrider" ) );
			AddPieceType( EquesRex = new Centaur( "Eques Rex", "E", 650, 750 ) );

			//	Star Cat (augmented Jumping General)
			AddPieceType( StarCat = new JumpingGeneral( "Star Cat", "SC", 1100, 1100, "Star Cat" ) );
			StarCat.Step( new Direction(  3,  0 ) );
			StarCat.Step( new Direction( -3,  0 ) );
			StarCat.Step( new Direction(  0,  3 ) );
			StarCat.Step( new Direction(  0, -3 ) );
			StarCat.Step( new Direction(  3,  3 ) );
			StarCat.Step( new Direction(  3, -3 ) );
			StarCat.Step( new Direction( -3,  3 ) );
			StarCat.Step( new Direction( -3, -3 ) );

		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	Promotion Rules
			AddRule( new Rules.BasicPromotionRule( Guard, new List<PieceType> { EquesRex }, loc => loc.Rank == 11, loc => loc.Rank != 11 ) );
			AddRule( new Rules.BasicPromotionRule( Cat, new List<PieceType> { StarCat }, loc => loc.Rank == 11, loc => loc.Rank != 11 ) );
			AddRule( new Rules.BasicPromotionRule( Knight, new List<PieceType> { SpeedyKnight }, loc => loc.Rank == 11 ) );

			//	Cat and Star Cat Powers
			AddRule( new Rules.OptionalCaptureByOvertakeRule( new List<PieceType> { Cat, StarCat } ) );

			//	Pawn Enhancement
			MoveCapability pawnLeftMove = new MoveCapability();
			pawnLeftMove.MinSteps = 1;
			pawnLeftMove.MaxSteps = 1;
			pawnLeftMove.MustCapture = false;
			pawnLeftMove.CanCapture = false;
			pawnLeftMove.Direction = new Direction( 0, -1 );
			pawnLeftMove.Condition = location => location.Rank >= 6;
			Pawn.AddMoveCapability( pawnLeftMove );
			MoveCapability pawnRightMove = new MoveCapability();
			pawnRightMove.MinSteps = 1;
			pawnRightMove.MaxSteps = 1;
			pawnRightMove.MustCapture = false;
			pawnRightMove.CanCapture = false;
			pawnRightMove.Direction = new Direction( 0, 1 );
			pawnRightMove.Condition = location => location.Rank >= 6;
			Pawn.AddMoveCapability( pawnRightMove );

			//	Reconfigure the 50-move rule
			Rules.Move50Rule rule = (Rules.Move50Rule) FindRule( typeof(Rules.Move50Rule) );
			rule.HalfMoveCounterThreshold = 160;
			rule.SetRequiredDirection( new Direction( 1, 0 ) );
		}
		#endregion
	}
}
