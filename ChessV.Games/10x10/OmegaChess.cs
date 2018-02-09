
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
	[Game("Omega Chess", typeof(Geometry.Rectangular), 10, 10, 1, 4,
		  XBoardName = "omega",
		  Invented = "1992",
		  InventedBy = "Daniel MacDonald",
		  Tags = "Chess Variant,Popular")]
	[Appearance(ColorScheme = "Buckingham Green")]
	public class OmegaChess: Abstract.Generic12x12
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Wizard;
		public PieceType Champion;


		// *** CONSTRUCTION *** //

		public OmegaChess():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region CreateBoard
		public override Board CreateBoard( int nPlayers, int nFiles, int nRanks, Symmetry symmetry )
		{
			Board board = new Board( 12, 12 );
			board.SetFileNotation( new char[] { ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', ' ' } );
			board.SetRankNotation( new string[] { " ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " " } );
			return board;
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "w10w/1crnbqkbnrc1/1pppppppppp1/12/12/12/12/12/12/1PPPPPPPPPP1/1CRNBQKBNRC1/W10W";
			PromotionRule.Value = "Custom";
			PromotionTypes = "QRBNCW";
			Castling.Value = "Custom";
			PawnMultipleMove.Value = "@3(2,3)";
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Queen = new Queen( "Queen", "Q", 1025, 1250 ) );
			AddPieceType( Wizard = new Wizard( "Wizard", "W", 360, 360 ) );
			AddPieceType( Champion = new Champion( "Champion", "C", 375, 375 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			// *** BORDER RULE *** //
			AddRule( new Rules.Omega.OmegaChessBorderRule() );

			//	add custom pawn promotion rule
			if( PromotionRule.Value == "Custom" )
			{
				AddRule( new Rules.BasicPromotionRule( Pawn, ParseTypeListFromString( PromotionTypes ), loc => loc.Rank == 10 ) );
			}

			//	add custom castling rule
			if( Castling.Value == "Custom" )
			{
				CastlingRule();
				CastlingMove( 0, "f0", "h0", "i0", "g0", 'K' );
				CastlingMove( 0, "f0", "d0", "b0", "e0", 'Q' );
				CastlingMove( 1, "f9", "h9", "i9", "g9", 'k' );
				CastlingMove( 1, "f9", "d9", "b9", "e9", 'q' );
			}
		}
		#endregion


		// *** CUSTOM APPEARANCE and NOTATION *** //

		#region GetSquareNotation
		public override string GetSquareNotation( int square )
		{
			if( square == 0 )
				return "w1";
			else if( square == 11 )
				return "w4";
			else if( square == 132 )
				return "w2";
			else if( square == 143 )
				return "w3";
			else
				return Board.GetDefaultSquareNotation( square );
		}
		#endregion

		#region NotationToSquare
		public override int NotationToSquare( string notation )
		{
			if( notation == "w1" )
				return 0;
			else if( notation == "w4" )
				return 11;
			else if( notation == "w2" )
				return 132;
			else if( notation == "w3" )
				return 143;
			else
				return Board.DefaultNotationToSquare( notation );
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
			//	finally, turn unreachable border squares to the third color
			if( (location.Rank == 0 && location.File != 0 && location.File != Board.NumFiles - 1) || 
				(location.Rank == Board.NumRanks - 1 && location.File != 0 && location.File != Board.NumFiles - 1) || 
				(location.File == 0 && location.Rank != 0 && location.Rank != Board.NumRanks - 1) || 
				(location.File == Board.NumFiles - 1 && location.Rank != 0 && location.Rank != Board.NumRanks - 1) )
				color = 2;
			return color;
		}
		#endregion
	}
}
