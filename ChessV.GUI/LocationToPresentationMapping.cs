
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

namespace ChessV.GUI
{
	public class LocationToPresentationMapping
	{
		public Board Board { get; protected set; }
		public Theme Theme { get; protected set; }
		public int BorderSize { get; protected set; }
		public int SquareSize { get; protected set; }
		public int TotalWidth { get; protected set; }
		public int TotalHeight { get; protected set; }

		public LocationToPresentationMapping( Board board, Theme theme, int borderSize, int squareSize )
		{
			Board = board;
			Theme = theme;
			BorderSize = borderSize;
			SquareSize = squareSize;

			TotalWidth = 2 * borderSize + Board.NumFiles * squareSize + 1;
			TotalHeight = 2 * borderSize + Board.NumRanks * squareSize + 1;
		}

		public virtual Rectangle MapLocation( Location location, bool rotateBoard = false )
		{
			if( rotateBoard )
				return new Rectangle( BorderSize + (Board.NumFiles - location.File - 1) * SquareSize,
					BorderSize + location.Rank * SquareSize,
					SquareSize, SquareSize );
			else
				return new Rectangle( BorderSize + location.File * SquareSize,
					BorderSize + (Board.NumRanks - location.Rank - 1) * SquareSize,
					SquareSize, SquareSize );
		}

		public virtual Location MapCoordinate( Point coordinate, bool rotateBoard )
		{
			for( int square = 0; square < Board.NumSquaresExtended; square++ )
				if( MapLocation( Board.SquareToLocation( square ), rotateBoard ).Contains( coordinate.X, coordinate.Y ) )
					return Board.SquareToLocation( square );
			return Location.NullLocation;
		}
	}
}
