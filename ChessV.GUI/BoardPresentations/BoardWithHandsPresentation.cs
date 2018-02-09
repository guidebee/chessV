
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

namespace ChessV.GUI.BoardPresentations
{
	public class BoardWithHandsLocationToPresentationMapping: LocationToPresentationMapping
	{
		public BoardWithHandsLocationToPresentationMapping( Board board, Theme theme, int borderSize, int squareSize ):
			base( board, theme, borderSize, squareSize )
		{
			TotalWidth += borderSize + squareSize;
		}

		public override Rectangle MapLocation( Location location, bool rotateBoard = false )
		{
			if( location.File == -1 )
			{
				if( location.Rank == 0 )
					return new Rectangle( BorderSize * 2 + Board.NumFiles * SquareSize,
						BorderSize + (Board.NumRanks - 1) * SquareSize, SquareSize, SquareSize );
				else if( location.Rank == 1 )
					return new Rectangle( BorderSize * 2 + Board.NumFiles * SquareSize,
						BorderSize, SquareSize, SquareSize );
				else
					throw new Exception( "not implemented" );
			}
			return new Rectangle( BorderSize + location.File * SquareSize,
				BorderSize + (Board.NumRanks - location.Rank - 1) * SquareSize,
				SquareSize, SquareSize );
		}
	}

	public class BoardWithHandsPresetation : BoardPresentation
	{
		public BoardWithHandsPresetation( Board board, Theme theme )
			: base( board, theme )
		{ }

		protected override LocationToPresentationMapping CreateMapping( int borderSize, int squareSize )
		{ return new BoardWithHandsLocationToPresentationMapping( Board, Theme, borderSize, squareSize ); }
	}
}
