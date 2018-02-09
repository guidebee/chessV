
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
	public class MoveCapability: ExObject
	{
		// *** PROPERTIES *** //

		#region Properties
		//	The direction of the movement
		public Direction Direction { get; set; }

		//	The minimum number of steps that can be made
		public int MinSteps { get; set; }

		//	The maximum number of steps that can be made
		public int MaxSteps { get; set; }

		//	Can an enemy piece be captured with this move?
		public bool CanCapture { get; set; }

		//	Can the move only be made if an enemy piece is captured?
		public bool MustCapture { get; set; }

		//	The direction number of the direction of movement
		//	(all directions used in a given game are given unique 
		//	numbers for efficiency reasons.)
		public int NDirection { get; set; }

		//	If not null, the move can only be performed if the moving 
		//	piece is on a square with a true value in the array 
		//	ConditionalBySquare[playerNumber,squareNumber]
		//public bool[,] ConditionalBySquare { get; set; }
		public BitBoard[] ConditionalBySquare { get; set; }

		//	The ConditionalBySquare is automatically initialized when 
		//	a Condition is set to a ConditionDelegate, which is a 
		//	lambda function determining if a move is allowed for the 
		//	given location (translated appropriately for each player 
		//	based on the Symmetry of the Game.)
		public delegate bool ConditionDelegate( Location location );
		public ConditionDelegate Condition { get; set; }

		//	Records any special attack types (such as Cannon-moves)
		public SpecialAttacks SpecialAttacks { get; set; }

		//	PathInfo is set to a MovePathInfo object only for the 
		//	case of lame leapers such as the Horse in Xiangqi or 
		//	multi-path pieces such as the Falcon in Falcon Chess
		public MovePathInfo PathInfo { get; set; }
		#endregion


		// *** CONSTRUCTION *** //

		#region Construction
		public MoveCapability()
		{
			MinSteps = 1;
			MaxSteps = 9999;
			CanCapture = true;
			MustCapture = false;
		}

		public MoveCapability( Direction dir, int maxSteps = 9999, int minSteps = 1, bool canCapture = true, bool mustCapture = false )
		{
			Direction = dir;
			MaxSteps = maxSteps;
			MinSteps = minSteps;
			CanCapture = canCapture;
			MustCapture = mustCapture;
		}
		#endregion


		// *** INITIALIZATION *** //

		#region Initialization
		public void Initialize( Game game )
		{
			if( Condition != null && ConditionalBySquare == null )
			{
				ConditionalBySquare = new BitBoard[game.NumPlayers];
				for( int player = 0; player < game.NumPlayers; player++ )
				{
					ConditionalBySquare[player].SetAll();
					for( int sq = 0; sq < game.Board.NumSquares; sq++ )
					{
						Location location = game.Board.SquareToLocation( sq );
						location = game.Symmetry.Translate( player, location );
						if( !Condition( location ) )
							ConditionalBySquare[player].ClearBit( sq );
					}
				}
			}
		}
		#endregion


		public static MoveCapability Step( Direction direction )
		{
			return new MoveCapability( direction, 1 );
		}

		public static MoveCapability Slide( Direction direction )
		{
			return new MoveCapability( direction );
		}

		public static MoveCapability Slide( Direction direction, int maxSteps )
		{
			return new MoveCapability( direction, maxSteps );
		}

		public static MoveCapability StepMoveOnly( Direction direction )
		{
			return new MoveCapability( direction, 1, 1, false );
		}

		public static MoveCapability SlideMoveOnly( Direction direction )
		{
			return new MoveCapability( direction, 9999, 1, false );
		}

		public static MoveCapability StepCaptureOnly( Direction direction )
		{
			return new MoveCapability( direction, 1, 1, true, true );
		}

		public static MoveCapability SlideCaptureOnly( Direction direction )
		{
			return new MoveCapability( direction, 9999, 1, true, true );
		}

		public static MoveCapability CannonMove( Direction direction, int maxSteps = 9999 )
		{
			MoveCapability move = new MoveCapability( direction, maxSteps, 1, false, false );
			move.SpecialAttacks = SpecialAttacks.CannonCapture;
			return move;
		}

		public static MoveCapability RifleCapture( Direction direction, int maxSpaces )
		{
			MoveCapability move = new MoveCapability( direction, maxSpaces, 1, true, true );
			move.SpecialAttacks = SpecialAttacks.CannonCapture;
			return move;
		}
	}
}
