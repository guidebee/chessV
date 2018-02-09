
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
	//                          FischerRandomChess
	//

	[Game("Fischer Random Chess", typeof(Geometry.Rectangular), 8, 8, 
		  XBoardName = "fischerandom",
		  InventedBy = "Bobby Fischer",
		  Invented = "1996",
		  Tags = "Chess Variant,Popular,Random Array",
		  GameDescription1 = "A derivative of standard Chess where the opening position is",
		  GameDescription2 = "randomized to eliminate the memorization of openings",
		  Definitions="Castling=FRC")]
	[Game("Chess480", typeof(Geometry.Rectangular), 8, 8, 
		  InventedBy = "John Kipling Lewis",
		  Invented = "2005",
		  Tags = "Chess Variant,Random Array",
		  GameDescription1 = "A derivative of Fischer Random Chess with alternate castling rules.",
		  GameDescription2 = "The initial array is randomized to eliminate the memorization of openings",
		  Definitions="Castling=Chess480")]
	class FischerRandomChess: Chess
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public IntVariable PositionNumber { get; set; }


		// *** CONSTRUCTION *** //
		public FischerRandomChess()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			PositionNumber = new IntVariable( 1, 960 );
			Castling.Choices.Add( "FRC" );
			Castling.Choices.Add( "Chess480" );
		}
		#endregion

		#region LookupGameVariable
		public override object LookupGameVariable( string variableName )
		{
			if( variableName.ToUpper() == "ARRAY" )
			{
				//	if the position is unassigned, just show the pawns
				if( PositionNumber.Value == null )
					return "8/pppppppp/8/8/8/8/PPPPPPPP/8";
				//	build black back rank part of the array
				char[] pieces = new char[8] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
				int position = (int) PositionNumber.Value - 1;
				int lightBishop = position % 4;
				position = position / 4;
				int darkBishop = position % 4;
				position = position / 4;
				pieces[lightBishop * 2 + 1] = 'b';
				pieces[darkBishop * 2] = 'b';
				int queenPosition = position % 6;
				position = position / 6;
				for( int x = 0; x < 8; x++ )
					if( pieces[x] == ' ' )
						if( queenPosition-- == 0 )
							pieces[x] = 'q';
				for( int x = 0, y = 0; y < 5; x++ )
					if( pieces[x] == ' ' )
						pieces[x] = kingRookKnight[position, y++];
				string pieceString = new string( pieces );
				//	build full array
				return pieceString + "/pppppppp/8/8/8/8/PPPPPPPP/" + pieceString.ToUpper();
			}
			return base.LookupGameVariable( variableName );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			//	if we are using FRC type castling, temporarily set Castling 
			//	type to "None" when calling base class because they don't 
			//	support this and we will handle later
			string castlingValue = null;
			if( Castling.Value == "FRC" || Castling.Value == "Chess480" )
			{
				castlingValue = Castling.Value;
				Castling.Value = "None";
			}
			base.AddRules();

			// *** CASTLING *** //
			if( castlingValue != null )
			{
				//	restore castling settings now that base class has been called
				Castling.Value = castlingValue;

				//	find the starting squares for the Kings and Rooks
				GenericPiece WhiteKing = new GenericPiece( 0, King );
				GenericPiece WhiteRook = new GenericPiece( 0, Rook );
				string leftRook = null;
				string king = null;
				string rightRook = null;
				for( int x = 0; x < 8; x++ )
				{
					string square = Convert.ToChar( 'a' + x ) + "1";
					if( StartingPieces[square] == WhiteKing )
						king = square;
					else if( StartingPieces[square] == WhiteRook )
					{
						if( leftRook == null )
							leftRook = square;
						else
							rightRook = square;
					}
				}

				if( leftRook == null || rightRook == null )
					return;

				//	add castling rules
				if( Castling.Value == "FRC" )
				{
					CastlingRule();
					CastlingMove( 0, king, "c1", leftRook, "d1", Char.ToUpper( leftRook[0] ) );
					CastlingMove( 0, king, "g1", rightRook, "f1", Char.ToUpper( rightRook[0] ) );
					leftRook = leftRook[0] + "8";
					king = king[0] + "8";
					rightRook = rightRook[0] + "8";
					CastlingMove( 1, king, "c8", leftRook, "d8", leftRook[0] );
					CastlingMove( 1, king, "g8", rightRook, "f8", rightRook[0] );
				}
				else if( Castling.Value == "Chess480" )
				{
					CastlingRule();
					if( king == "b1" )
					{
						CastlingMove( 0, "b1", "a1", "a1", "b1", 'A' );
						CastlingMove( 1, "b8", "a8", "a8", "b8", 'a' );
					}
					else
					{
						CastlingMove( 0, king, Convert.ToChar( king[0] - 2 ) + "1", leftRook, Convert.ToChar( king[0] - 1 ) + "1", Char.ToUpper( leftRook[0] ) );
						CastlingMove( 1, king[0] + "8", Convert.ToChar( king[0] - 2 ) + "8", leftRook[0] + "8", Convert.ToChar( king[0] - 1 ) + "8", leftRook[0] );
					}
					if( king == "g1" )
					{
						CastlingMove( 0, "g1", "h1", "h1", "g1", 'H' );
						CastlingMove( 1, "g8", "h8", "h8", "g8", 'h' );
					}
					else
					{
						CastlingMove( 0, king, Convert.ToChar( king[0] + 2 ) + "1", rightRook, Convert.ToChar( king[0] + 1 ) + "1", Char.ToUpper( rightRook[0] ) );
						CastlingMove( 1, king[0] + "8", Convert.ToChar( king[0] + 2 ) + "8", rightRook[0] + "8", Convert.ToChar( king[0] + 1 ) + "8", rightRook[0] );
					}
				}
			}
		}
		#endregion

		char[,] kingRookKnight = new char[,]
			{ { 'n', 'n', 'r', 'k', 'r' }, 
			  { 'n', 'r', 'n', 'k', 'r' }, 
			  { 'n', 'r', 'k', 'n', 'r' }, 
			  { 'n', 'r', 'k', 'r', 'n' }, 
			  { 'r', 'n', 'n', 'k', 'r' }, 
			  { 'r', 'n', 'k', 'n', 'r' }, 
			  { 'r', 'n', 'k', 'r', 'n' }, 
			  { 'r', 'k', 'n', 'n', 'r' }, 
			  { 'r', 'k', 'n', 'r', 'n' }, 
			  { 'r', 'k', 'r', 'n', 'n' } };
	}
}
