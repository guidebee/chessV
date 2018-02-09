
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

namespace ChessV
{
	public class BoardMoveStack
	{
		public Board Board { get; private set; }

		public int MoveCount
		{ get { return moves.Count; } }

		public MoveInfo GetMove( int movenum )
		{
			return moves[movenum];
		}

		public BoardMoveStack( Board board )
		{
			Board = board;

			pickups = new List<Pickup>();
			drops = new List<Drop>();
			moves = new List<MoveInfo>();
		}

		public void MakingMove( MoveList movelist, MoveInfo moveinfo )
		{
			MoveInfo newmove = moveinfo;
			movelist.CopyMoveToGameHistory( pickups, drops, moveinfo );
			newmove.PickupCursor = pickups.Count;
			newmove.DropCursor = drops.Count;
			moves.Add( newmove );
		}

		public void UnmakeMove()
		{

			if( moves.Count > 0 )
			{
				Board.Game.MoveBeingUnmade( moves[moves.Count - 1] );

				int pickupCursor = 0;
				int dropCursor = 0;
				if( moves.Count > 1 )
				{
					pickupCursor = moves[moves.Count - 2].PickupCursor;
					dropCursor = moves[moves.Count - 2].DropCursor;
				}
				//	undo all drops
				for( int x = drops.Count; x > dropCursor; x-- )
					UndoDrop();
				//	undo all pickups
				for( int x = pickups.Count; x > pickupCursor; x-- )
					UndoPickup();
				moves.RemoveAt( moves.Count - 1 );
			}
		}

		protected void UndoPickup()
		{
			Board.SetSquare( pickups[pickups.Count-1].Piece, pickups[pickups.Count-1].Square );
			pickups.RemoveAt( pickups.Count - 1 );
		}

		protected void UndoDrop()
		{
			Board.ClearSquare( drops[drops.Count-1].Square );
			drops[drops.Count-1].Piece.MoveCount--;
			if( drops[drops.Count - 1].NewType != null )
			{
				PieceType oldType = drops[drops.Count-1].NewType;
				drops[drops.Count-1].Piece.PieceType = oldType;
				drops[drops.Count-1].Piece.TypeNumber = oldType.TypeNumber;
			}
			drops.RemoveAt( drops.Count - 1 );
		}

		protected List<Pickup> pickups;
		protected List<Drop> drops;
		protected List<MoveInfo> moves;
	}
}
