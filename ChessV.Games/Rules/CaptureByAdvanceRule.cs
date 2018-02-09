
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

namespace ChessV.Games.Rules
{
	public class CaptureByAdvanceRule: Rule
	{
		// *** PROPERTIES *** //

		public PieceType PieceType { get; private set; }


		// *** CONSTRUCTION *** //

		public CaptureByAdvanceRule( PieceType pieceType )
		{
			PieceType = pieceType;
		}


		// *** EVENT HANDLERS *** //

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			if( Board[from].PieceType == PieceType )
			{
				int direction = Board.DirectionLookup( from, to );
				if( direction >= 0 )
				{
					int nextStep = Board.NextSquare( direction, to );
					if( nextStep >= 0 && nextStep < Board.NumSquares )
					{
						Piece pieceOnNextSquare = Board[nextStep];
						Piece pieceOnLandingSquare = Board[to];
						if( pieceOnNextSquare != null && pieceOnNextSquare.Player != Board[from].Player )
						{
							//	we can capture by advance
							moves.BeginMoveAdd( pieceOnLandingSquare == null ? MoveType.BaroqueCapture : MoveType.ExtraCapture, from, to, nextStep );
							Piece pieceBeingMoved = moves.AddPickup( from );
							Piece pieceBeingCaptured = moves.AddPickup( nextStep );
							if( pieceOnLandingSquare != null )
								moves.AddPickup( to );
							moves.AddDrop( pieceBeingMoved, to );
							moves.EndMoveAdd( pieceOnLandingSquare == null 
								? (3000 + pieceBeingCaptured.PieceType.MidgameValue - (pieceBeingMoved.PieceType.MidgameValue / 16))
								: (4000 + pieceBeingCaptured.PieceType.MidgameValue + pieceOnLandingSquare.PieceType.MidgameValue - (pieceBeingMoved.PieceType.MidgameValue / 16)) );
							return MoveEventResponse.Handled;
						}
					}
				}
			}
			return base.MoveBeingGenerated( moves, from, to, type );
		}

		public override bool IsSquareAttacked( int square, int side )
		{
			//	Find all directions that our capture-by-advancing piece travels
			MoveCapability[] moveCapabilities;
			int nMoves = PieceType.GetMoveCapabilities( out moveCapabilities );
			//	Find all pieces that might pose this threat, then check them all
			BitBoard attackers = Board.GetPieceTypeBitboard( side, PieceType.TypeNumber );
			while( attackers )
			{
				int pieceSquare = attackers.ExtractLSB();
				for( int x = 0; x < nMoves; x++ )
				{
					int dir = moveCapabilities[x].NDirection;
					int nextSquare = Board.NextSquare( side, dir, pieceSquare );
					int step = 1;
					while( nextSquare >= 0 && step <= moveCapabilities[x].MaxSteps )
					{
						if( nextSquare == square )
						{
							if( step > 1 )
								return true;
							nextSquare = -1;
						}
						else if( Board[nextSquare] != null )
							nextSquare = -1;
						else
						{
							nextSquare = Game.Board.NextSquare( side, dir, nextSquare );
							step++;
						}
					}
				}
			}

			return false;
		}
	}
}
