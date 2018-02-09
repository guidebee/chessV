
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
using ChessV.Games;

namespace ChessV.Games.Rules
{
	public class OptionalCaptureByOvertakeRule: Rule
	{
		protected List<PieceType> pieceTypes;

		public OptionalCaptureByOvertakeRule( List<PieceType> affectedPieceTypes )
		{
			pieceTypes = affectedPieceTypes;
		}

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			if( pieceTypes.Contains( Board[from].PieceType ) )
			{
				//	follow the steps from the from-square to the to-square, checking 
				//	for enemy pieces that can be optinally captured.
				int movingPlayerNumber = Board[from].Player;
				int direction = Board.DirectionLookup( from, to );
				int bitPatternOfPotentialVictims = 0;
				int nSteps = 1;
				int nextSquare = Board.NextSquare( direction, from );
				while( nextSquare != to && nSteps <= 8 )
				{
					if( Board[nextSquare] != null && Board[nextSquare].Player != movingPlayerNumber )
						//	this square contains a piece we can capture.
						//	set the appropriate bit indexed by number of steps
						bitPatternOfPotentialVictims |= 1 << (nSteps - 1);
					//	advance to the next square
					nextSquare = Board.NextSquare( direction, nextSquare );
					nSteps++;
				}
				//	if any bits are set, add additional capture moves for 
				//	every possible combination of captures
				if( bitPatternOfPotentialVictims != 0 )
					generatePermutations( moves, from, to, bitPatternOfPotentialVictims, 0 );
			}
			//	Always return NotHandled, even if we did because we still want 
			//	normal move generation to take place so that pieces that can 
			//	capture by overtake can still move when they are not doing so.
			return MoveEventResponse.NotHandled;
		}

		protected void generatePermutations( MoveList moves, int from, int to, int pendingBits, int activeBits )
		{
			if( pendingBits == 0 )
			{
				//	here is where we generate a move, capturing whichever pieces
				//	are indicated by activeBits
				if( activeBits != 0 )
				{
					//	Determine whether activeBits has only one bit set.
					//	If it has only one, the type of generated move will 
					//	be MoveType.ExtraCapture (a move type inherrently 
					//	understood by the framework.)  If more than one 
					//	bit is set, we have multiple indirect captures and 
					//	we will need to use a custom MoveType
					MoveType moveType = (activeBits & (activeBits - 1)) == 0
						? MoveType.ExtraCapture
						: MoveType.CustomMove;
					//	Calculate the evaluation for the move (we will add to 
					//	it as we pick up other pieces
					int eval = 2000 + (Board[to] != null ? Board[to].MidgameValue : 0);
					//	Add the move
					moves.BeginMoveAdd( moveType, from, to, activeBits );
					Piece movingPiece = moves.AddPickup( from );
					if( Board[to] != null )
						moves.AddPickup( to );
					//	Step through the intervening squares picking up 
					//	the appropriate pieces
					int direction = Board.DirectionLookup( from, to );
					int nSteps = 1;
					int nextSquare = Board.NextSquare( direction, from );
					while( nextSquare != to && activeBits != 0 )
					{
						if( (activeBits & (1 << (nSteps - 1))) != 0 )
						{
							moves.AddPickup( nextSquare );
							if( moveType == MoveType.ExtraCapture )
								moves.SetMoveTag( nextSquare );
							eval += Board[nextSquare].MidgameValue;
							activeBits ^= 1 << (nSteps - 1);
						}
						nextSquare = Board.NextSquare( direction, nextSquare );
						nSteps++;
					}
					moves.AddDrop( movingPiece, to );
					moves.EndMoveAdd( eval );
				}
			}
			else
			{
				//	get the next bit from pendingBits and recursively call this 
				//	function both with and without that bit in activeBits
				int newPendingBits = pendingBits ^ (pendingBits & -pendingBits);
				int currentBit = pendingBits ^ newPendingBits;
				generatePermutations( moves, from, to, newPendingBits, activeBits );
				generatePermutations( moves, from, to, newPendingBits, activeBits | currentBit );
			}
		}

		public override MoveEventResponse DescribeMove( MoveInfo move, MoveNotation format, ref string description )
		{
			if( format == MoveNotation.MoveSelectionText && move.MoveType == MoveType.CustomMove )
			{
				//	describe the square on which extra captures are occuring
				List<string> captureSquares = new List<string>();
				int direction = Board.DirectionLookup( move.FromSquare, move.ToSquare );
				int nSteps = 1;
				int activeBits = move.Tag;
				int nextSquare = Board.NextSquare( direction, move.FromSquare );
				while( nextSquare != move.ToSquare && activeBits != 0 )
				{
					if( (activeBits & (1 << (nSteps - 1))) != 0 )
						captureSquares.Add( Game.GetSquareNotation( nextSquare ) );
					nextSquare = Board.NextSquare( direction, nextSquare );
					nSteps++;
				}
				description = "Capture pieces on " + captureSquares[0];
				for( int x = 1; x < captureSquares.Count; x++ )
					if( x < captureSquares.Count - 1 )
						description += ", " + captureSquares[x];
					else
						description += " and " + captureSquares[x];
				return MoveEventResponse.Handled;
			}
			return MoveEventResponse.NotHandled;
		}
	}
}
