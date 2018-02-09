
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
using System.Drawing;
using ChessV;
using ChessV.Boards;
using ChessV.GUI.Attributes;

namespace ChessV.GUI.BoardPresentations
{
	[PresentsBoard(typeof(TwoBoards))]
	public class TwoBoardsPresentation: BoardPresentation
	{
		public TwoBoardsPresentation( Board board, Theme theme, bool smallPreview = false ) :
			base( board, theme, smallPreview )
		{ }
		
		protected override LocationToPresentationMapping CreateMapping( int borderSize, int squareSize )
		{ return new TwoBoardsLocationToPresentationMapping( Board, Theme, borderSize, squareSize ); }

		public class TwoBoardsLocationToPresentationMapping: LocationToPresentationMapping
		{
			public TwoBoardsLocationToPresentationMapping( Board board, Theme theme, int borderSize, int squareSize ) :
				base( board, theme, borderSize, squareSize )
			{
				TotalWidth = 3 * borderSize + Board.NumFiles * squareSize + 1;
			}

			public override Rectangle MapLocation( Location location, bool rotateBoard = false )
			{
				int nFilesPerBoard = Board.NumFiles / 2;
				if( rotateBoard )
				{
					if( location.File >= nFilesPerBoard )
						return new Rectangle( (BorderSize * 2) + (nFilesPerBoard * SquareSize) + (nFilesPerBoard - (location.File - nFilesPerBoard) - 1) * SquareSize,
							BorderSize + location.Rank * SquareSize,
							SquareSize, SquareSize );
					else
						return new Rectangle( BorderSize + (nFilesPerBoard - location.File - 1) * SquareSize,
							BorderSize + location.Rank * SquareSize,
							SquareSize, SquareSize );
				}
				else
					return new Rectangle( BorderSize + location.File * SquareSize + (location.File >= nFilesPerBoard ? BorderSize : 0),
						BorderSize + (nFilesPerBoard - location.Rank - 1) * SquareSize,
						SquareSize, SquareSize );
			}
		}
	}
}
