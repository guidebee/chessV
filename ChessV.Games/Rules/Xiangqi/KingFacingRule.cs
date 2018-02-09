
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

namespace ChessV.Games.Rules.Xiangqi
{
	//**********************************************************************
	//
	//                        KingFacingRule
	//
	//    This rule makes it illegal for the two kings to face each other 
	//    on an open rank, file, or diagonal unless a piece is between 
	//    them.  This is used in both Xiangqi and Eurasian Chess.
	//
	//    NOTE: In Eurasian Chess it is not possible for the kings to 
	//    be on the same rank since the Kings cannot cross the river, 
	//    and in Xiangqi the kings can't be on the same diagonal either.
	//    We include this anyway for purposes of making the rule more 
	//    generic and reusable.

	public class KingFacingRule: Rule
	{
		protected int kingType;

		public KingFacingRule( PieceType kingType )
		{
			this.kingType = kingType.TypeNumber;
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			//	Make sure the kings aren't facing each other across an open 
			//	rank, file or diagonal.  If they are, the move is illegal.
			int k0square = Board.GetPieceTypeBitboard( 0, kingType ).GetLSB();
			int k1square = Board.GetPieceTypeBitboard( 1, kingType ).GetLSB();
			if( k0square == -1 || k1square == -1 )
				throw new Exception( "King captured.  This should not be possible and indicates a problem with a rule implementation." );
			Location k0location = Board.SquareToLocation( k0square );
			Location k1location = Board.SquareToLocation( k1square );
			//	Are the kings on the same file?
			if( k0location.File == k1location.File )
			{
				//	Are the spaces between them all open?
				for( int rank = Math.Min( k0location.Rank + 1, k1location.Rank + 1 ); rank < Math.Max( k0location.Rank, k1location.Rank ); rank++ )
					if( Board[Board.LocationToSquare( rank, k0location.File )] != null )
						//	There's a piece between them so it's ok
						return MoveEventResponse.NotHandled;
				//	Kings are facing each other on an open file
				//	therefore the move is illegal
				return MoveEventResponse.IllegalMove;
			}
			//	Are the kings on the same rank?
			if( k0location.Rank == k1location.Rank )
			{
				//	Are the spaces between them all open?
				for( int file = Math.Min( k0location.File + 1, k1location.File + 1 ); file < Math.Max( k0location.File, k1location.File ); file++ )
					if( Board[Board.LocationToSquare( k0location.Rank, file )] != null )
						//	There's a piece between them so it's ok
						return MoveEventResponse.NotHandled;
				//	Kings are facing each other on an open file
				//	therefore the move is illegal
				return MoveEventResponse.IllegalMove;
			}
			//	Are they on the same diagonal?
			if( Math.Abs( k0location.Rank - k1location.Rank ) == Math.Abs( k0location.File - k1location.File ) )
			{
				//	Are the spaces between them all open?
				int r1 = k0location.Rank < k1location.Rank ? k0location.Rank : k1location.Rank;
				int f1 = k0location.Rank < k1location.Rank ? k0location.File : k1location.File;
				int r2 = k0location.Rank < k1location.Rank ? k1location.Rank : k0location.Rank;
				int f2 = k0location.Rank < k1location.Rank ? k1location.File : k0location.File;
				int fileoffset = k0location.File < k1location.File ? 1 : -1;
				r1++;
				f1 += fileoffset;
				while( r1 < r2 )
				{
					if( Board[Board.LocationToSquare( r1, f1 )] != null )
						//	Kings are blocked from each other, so it's ok
						return MoveEventResponse.NotHandled;
					r1++;
					f1 += fileoffset;
				}
				//	Kings are facing each other on open diagonal
				return MoveEventResponse.IllegalMove;
			}
			//	The kings are not on the same rank, file, or diagonal
			return MoveEventResponse.NotHandled;
		}
	}
}
