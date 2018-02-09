
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
	//                      UnicornGreatChess
	//
	//    This class implements David Paulowich's Unicorn Great Chess.
	//    It adds two new types, the Lion (Betza's HFD) and the Unicorn 
	//    (Bishop+Knightrider) on a 10x10 board.

	[Game("Unicorn Great Chess", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2002",
		  InventedBy = "David Paulowich", 
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme="Lesotho", NumberOfColors=3)]
	public class UnicornGreatChess: Abstract.Generic10x10
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Unicorn;
		public PieceType Chancellor;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Lion;


		// *** CONSTRUCTION *** //

		public UnicornGreatChess(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}
		

		// *** INITIALIZATION *** //
		
		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			NumberOfSquareColors = 3;
			Array = "crnbukbnrq/ppppllpppp/4pp4/10/10/10/10/4PP4/PPPPLLPPPP/CRNBUKBNRQ";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QCU";
			EnPassant = true;
			PawnMultipleMove.Value = "Unicorn";
			Castling.Value = "Close-Rook";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1025, 1250 ) );
			AddPieceType( Unicorn = new Unicorn( "Unicorn", "U", 1050, 1125 ) );
			AddPieceType( Chancellor = new Chancellor( "Chancellor", "C", 925, 1050 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Lion = new Lion( "Lion", "L", 400, 400 ) );
		}
		#endregion
	}
}
