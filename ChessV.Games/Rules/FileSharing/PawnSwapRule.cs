
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

namespace ChessV.Games.Rules.FileSharing
{
	public class PawnSwapRule: Rule
	{
		// *** PROPERTIES *** //

		public PieceType PawnType { get; private set; }


		// *** CONSTRUCTION *** //

		public PawnSwapRule( PieceType pawnType )
		{
			PawnType = pawnType;
		}


		// *** OVERRIDES *** //

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( !capturesOnly )
			{
				BitBoard pawns = Board.GetPieceTypeBitboard( Game.CurrentSide, PawnType.TypeNumber );
				while( pawns )
				{
					int pawnSquare = pawns.ExtractLSB();
					int nextSquare = Board.NextSquare( PredefinedDirections.N + Game.CurrentSide, pawnSquare );
					if( nextSquare >= 0 && Board[nextSquare] != null && 
						Board[nextSquare].TypeNumber == PawnType.TypeNumber && 
						Board[nextSquare].Player != Game.CurrentSide )
					{
						Piece enemyPawn = Board[nextSquare];
						//	we can only perform the swap if the enemyPawn is not 
						//	currently attacking any piece of the current side
						int attackSquare1 = Board.NextSquare( PredefinedDirections.E, pawnSquare );
						int attackSquare2 = Board.NextSquare( PredefinedDirections.W, pawnSquare );
						if( (attackSquare1 < 0 || Board[attackSquare1] == null || Board[attackSquare1].Player != Game.CurrentSide) &&
							(attackSquare2 < 0 || Board[attackSquare2] == null || Board[attackSquare2].Player != Game.CurrentSide) )
						{
							//	swap move is legal - add it now
							list.BeginMoveAdd( MoveType.Swap, pawnSquare, nextSquare );
							Piece myPawn = list.AddPickup( pawnSquare );
							enemyPawn = list.AddPickup( nextSquare );
							list.AddDrop( myPawn, nextSquare );
							list.AddDrop( enemyPawn, pawnSquare );
							list.EndMoveAdd( 25 );
						}
					}
				}
			}
		}
	}
}
