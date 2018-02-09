
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

namespace ChessV.Boards
{
	//**********************************************************************
	//
	//                            TwoBoards
	//
	//    This class derives from the Board class and implements two  
	//    side-by-side boards of the given size for games such as Alice 
	//    Chess.  The constructor is passed the number of ranks and files 
	//    of one board and the files are doubled for the second board. 
	//    The two boards aren't "connected" - the direction movement 
	//    matricies don't allow movement from one board to another.  Any 
	//    movement between boards is handled by the Game (for example, 
	//    by adding the AliceRule.)

	public class TwoBoards: Board
	{
		public TwoBoards( int nFiles, int nRanks ):
			//	The base class is called with only the number of ranks and 
			//	files for the first board, so that the direction movement 
			//	matricies will be initialized correctly for the first board 
			//	and not reach the second.  We'll copy them to the second 
			//	board later.  We do tell it the total number of squares though
			base( nFiles, nRanks, nRanks * nFiles * 2 )
		{
			//	handle naming of files of second board (upper case of first board)
			for( int file = 0; file < nFiles; file++ )
			{
				fileNotations[nFiles + file] = (char) ('A' + file);
			}

			//	initialize other info for second board
			for( int square = nFiles * nRanks; square < 2 * nFiles * nRanks; square++ )
			{
				//	file/rank lookup for second board
				fileBySquare[square] = square / NumRanks;
				rankBySquare[square] = square % NumRanks;

				//	Piece-Square-Table values can be initialized by copying from first board
				pstInSmallCenter[square] = pstInSmallCenter[square - NumSquares];
				pstInLargeCenter[square] = pstInLargeCenter[square - NumSquares];
				pstInSmallCenter[square] = pstInSmallCenter[square - NumSquares];

				//	initialize distances by copying from first board
				for( int square2 = nFiles * nRanks; square2 < 2 * nFiles * nRanks; square2++ )
					distances[square, square2] = distances[square - NumSquares, square2 - NumSquares];
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			
			//	Extend "nextStep" direction attack matrix by copying from first board
			for( int x = 0; x < NumberOfDirections; x++ )
				for( int y = 0; y < NumSquares; y++ )
					nextStep[x, y + NumSquares] = nextStep[x, y] == -1 ? -1 : nextStep[x, y] + NumSquares;

			//	Extend "flipSquare" matrix by copying from first board
			for( int player = 0; player < Game.NumPlayers; player++ )
				for( int square = 0; square < NumSquares; square++ )
					flipSquare[player, square + NumSquares] = flipSquare[player, square] + NumSquares;

			//	Now set the number of files and squares to properly reflect 
			//	the total across both boards
			NumFiles = NumFiles * 2;
			NumSquares = NumRanks * NumFiles;
		}
	}
}
