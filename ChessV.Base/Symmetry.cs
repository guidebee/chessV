
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
using System.Text;

namespace ChessV
{
	public abstract class Symmetry
	{
		public Board Board { get; set; }

		protected Symmetry() { }

		public abstract Direction Translate( int player, Direction direction );

		public abstract Location Translate( int player, Location location );
	}

	public class NoSymmetry: Symmetry
	{
		public NoSymmetry() : base() { }

		public override Direction Translate( int player, Direction direction )
		{ return direction; }

		public override Location Translate( int player, Location location )
		{ return location; }
	}

	public class MirrorSymmetry: Symmetry
	{
		public MirrorSymmetry(): base() { }

		public override Direction Translate( int player, Direction direction )
		{
			return new Direction( player == 0 ? direction.RankOffset : -direction.RankOffset, direction.FileOffset );
		}

		public override Location Translate( int player, Location location )
		{
			return new Location( player == 0 ? location.Rank : Board.NumRanks - location.Rank - 1, location.File );
		}
	}

	public class RotationalSymmetry: Symmetry
	{
		public RotationalSymmetry(): base() { }

		public override Direction Translate( int player, Direction direction )
		{
			return new Direction( player == 0 ? direction.RankOffset : -direction.RankOffset,
								  player == 0 ? direction.FileOffset : -direction.FileOffset );
		}

		public override Location Translate( int player, Location location )
		{
			return new Location( player == 0 ? location.Rank : Board.NumRanks - location.Rank - 1,
								 player == 0 ? location.File : Board.NumFiles - location.File - 1 );
		}
	}
}
