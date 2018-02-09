
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
	//                            Brouhaha
	//
	//    This class implements Greg Strong's Brouhaha, which adds extra 
	//    pieces to the standard game.  Each side gets two Scouts which 
	//    have similar value to Knight or Bishop and two Clerics which 
	//    have similar value to Rooks (but are color-bound.)  This adds 
	//    a lot more potential for uneven trades leading to battles between
	//    different armies.  The extra pieces start on border squares that 
	//    are special.  As soon as they are vacated, they disappear so that 
	//    the board becomes a standard 8x8 game once all pieces are
	//    developed.  A piece may only move to a border square to capture 
	//    a piece located there.

	[Game("Brouhaha", typeof(Geometry.Rectangular), 8, 8, 1, 8, 
		  InventedBy = "Greg Strong",
		  Invented = "2006",
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme = "Buckingham Green")]
	public class Brouhaha: Abstract.GenericChess
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Cleric;
		public PieceType Scout;


		// *** CONSTRUCTION *** //

		public Brouhaha():
			base
				( /* num files = */ 10, 
				  /* num ranks = */ 10,
				  /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region CreateBoard
		public override Board CreateBoard( int nPlayers, int nFiles, int nRanks, Symmetry symmetry )
		{
			Board board = new Board( 10, 10 );
			board.SetFileNotation( new char[] { 'x', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'z' } );
			board.SetRankNotation( new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" } );
			return board;
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {castling} {en-passant} {half-move clock} {turn number}";
			FENStart = "#{Array} w #default #default 0 1";
			Array = "c3ss3c/1rnbqkbnr1/1pppppppp1/10/10/10/10/1PPPPPPPP1/1RNBQKBNR1/C3SS3C";
			PromotionTypes = "QRCBNS";
			PromotionRule.Value = "Custom";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 500, 550 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 325, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 325, 325 ) );
			AddPieceType( Scout = new Scout( "Scout", "S", 300, 300 ) );
			AddPieceType( Cleric = new Cleric( "Cleric", "C", 475, 500 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 900, 1000 ) );

			// *** CLERIC FIRST MOVE *** //
			MoveCapability clericMove = new MoveCapability();
			clericMove.MaxSteps = 1;
			clericMove.Direction = new Direction( 1, 3 );
			clericMove.Condition = location => location.Rank == 0 && (location.File == 0 || location.File == 9);
			Cleric.AddMoveCapability( clericMove );
			clericMove = new MoveCapability();
			clericMove.MaxSteps = 1;
			clericMove.Direction = new Direction( 1, -3 );
			clericMove.Condition = location => location.Rank == 0 && (location.File == 0 || location.File == 9);
			Cleric.AddMoveCapability( clericMove );
			clericMove = new MoveCapability();
			clericMove.MaxSteps = 1;
			clericMove.Direction = new Direction( 3, 1 );
			clericMove.Condition = location => location.Rank == 0 && (location.File == 0 || location.File == 9);
			Cleric.AddMoveCapability( clericMove );
			clericMove = new MoveCapability();
			clericMove.MaxSteps = 1;
			clericMove.Direction = new Direction( 3, -1 );
			clericMove.Condition = location => location.Rank == 0 && (location.File == 0 || location.File == 9);
			Cleric.AddMoveCapability( clericMove );

			// *** SCOUT FIRST MOVE *** //
			MoveCapability scoutMove = new MoveCapability();
			scoutMove.MaxSteps = 1;
			scoutMove.Direction = new Direction( 1, 2 );
			scoutMove.Condition = location => location.Rank == 0 && (location.File == 4 || location.File == 5);
			Scout.AddMoveCapability( scoutMove );
			scoutMove = new MoveCapability();
			scoutMove.MaxSteps = 1;
			scoutMove.Direction = new Direction( 1, -2 );
			scoutMove.Condition = location => location.Rank == 0 && (location.File == 4 || location.File == 5);
			Scout.AddMoveCapability( scoutMove );
			scoutMove = new MoveCapability();
			scoutMove.MaxSteps = 1;
			scoutMove.Direction = new Direction( 2, 1 );
			scoutMove.Condition = location => location.Rank == 0 && (location.File == 4 || location.File == 5);
			Scout.AddMoveCapability( scoutMove );
			scoutMove = new MoveCapability();
			scoutMove.MaxSteps = 1;
			scoutMove.Direction = new Direction( 2, -1 );
			scoutMove.Condition = location => location.Rank == 0 && (location.File == 4 || location.File == 5);
			Scout.AddMoveCapability( scoutMove );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** BORDER RULE *** //
			AddRule( new Rules.Brouhaha.BrouhahaBorderRule() );

			// *** PAWN DOUBLE MOVE *** //
			MoveCapability doubleMove = new MoveCapability();
			doubleMove.MinSteps = 2;
			doubleMove.MaxSteps = 2;
			doubleMove.MustCapture = false;
			doubleMove.CanCapture = false;
			doubleMove.Direction = new Direction( 1, 0 );
			doubleMove.Condition = location => location.Rank == 2;
			Pawn.AddMoveCapability( doubleMove );

			// *** EN-PASSANT *** //
			EnPassantRule( Pawn, new Direction( 1, 0 ) );

			// *** CASTLING *** //
			CastlingRule();
			CastlingMove( 0, "e1", "g1", "h1", "f1", 'K' );
			CastlingMove( 0, "e1", "c1", "a1", "d1", 'Q' );
			CastlingMove( 1, "e8", "g8", "h8", "f8", 'k' );
			CastlingMove( 1, "e8", "c8", "a8", "d8", 'q' );

			// *** PAWN PROMOTION *** //
			if( PromotionRule.Value == "Custom" )
			{
				List<PieceType> availablePromotionTypes = ParseTypeListFromString( PromotionTypes );
				BasicPromotionRule( Pawn, availablePromotionTypes, loc => loc.Rank == 8 );
			}
		}
		#endregion

		#region GetSquareColor
		public override int GetSquareColor( Location location, int nColors )
		{
			//	calculate standard coloring for two-color boards
			int color = (Math.Max( location.Rank, 0 ) + Math.Max( location.File, 0 )) % 2;
			//	now, ensure light color is on bottom right by inverting if even number of files
			if( Board.NumFiles % 2 == 0 )
				color = color ^ 1;
			//	finally, turn unoccupied border squares to the third color
			if( location.Rank == 0 || location.Rank == Board.NumRanks - 1 ||
				location.File == 0 || location.File == Board.NumFiles - 1 )
				if( Board[Board.LocationToSquare( location )] == null )
					color = 2;
			return color;
		}
		#endregion
	}
}
