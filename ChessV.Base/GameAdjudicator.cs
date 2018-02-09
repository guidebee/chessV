
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG
  
  THIS FILE DERIVED FROM CUTE CHESS BY ILARI PIHLAJISTO AND ARTO JONSSON

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
	public class GameAdjudicator
	{
		// *** CONSTRUCTION *** //

		public GameAdjudicator()
		{
			//	all game adjudication disabled by default
			drawMoveNum = 0;
			drawMoveCount = 0;
			drawScore = 0;
			drawScoreCount = 0;
			resignMoveCount = 0;
			resignScore = 0;
			tbEnabled = false;
			resignScoreCount = new int[2];
			resignScoreCount[0] = 0;
			resignScoreCount[1] = 0;
		}

		//	Sets the draw adjudication threshold for a game
		public void SetDrawThreshold
			( int moveNumber,           //	minimum number of moves played
			  int moveCount,            //	number of moves score must be maintained
			  int score )               //	maximum difference from zero to be a draw
		{
			//Q_ASSERT( moveNumber >= 0 );
			//Q_ASSERT( moveCount >= 0 );

			drawMoveNum = moveNumber;
			drawMoveCount = moveCount;
			drawScore = score;
			drawScoreCount = 0;
		}

		//	Sets the resign adjudication threshold for a game
		public void SetResignThreshold
			( int moveCount,            //	number of moves below threshold
			  int score )               //	the threshold below zero for forfeit
		{
			//Q_ASSERT( moveCount >= 0 );

			resignMoveCount = moveCount;
			resignScore = score;
			resignScoreCount[0] = 0;
			resignScoreCount[1] = 0;
		}

		//	Enable or disable Tablebase adjudication -- NOT IMPLEMENTED YET
		public void SetTablebaseAdjudication( bool enable )
		{
			tbEnabled = enable;
		}

		//	Adds a new move evaluation to the adjudicator.
		//	The Result property can be checked after calling this function
		//	to determine if the game should be adjudicated.
		public void AddEval
			( Game game,				//	game state following move
			  MoveEvaluation eval )		//	the evaluation of the move
		{
			int side = game.CurrentSide ^ 1;

			//	Tablebase adjudication - NOT IMPLEMENTED YET
			if( tbEnabled )
			{
				//m_result = board->tablebaseResult();
				//if (!m_result.isNone())
				//	return;
			}

			//	Moves forced by the user (e.g., from opening book or played by user)
			if( eval.Depth <= 0 )
			{
				drawScoreCount = 0;
				resignScoreCount[side] = 0;
				return;
			}

			//	Draw adjudication
			if( drawMoveNum > 0 )
			{
				if( (eval.Score >= 0 ? eval.Score : -eval.Score) <= drawScore )
					drawScoreCount++;
				else
					drawScoreCount = 0;
				if( game.GameMoveNumber / 2 >= drawMoveNum && drawScoreCount >= drawMoveCount * 2 )
				{
					Result = new Result( ResultType.Adjudication, -1 );
					return;
				}
			}

			//	Resign adjudication
			if( resignMoveCount > 0 )
			{
				
				if( eval.Score <= resignScore )
					resignScoreCount[side]++;
				else
					resignScoreCount[side] = 0;

				if( resignScoreCount[side] >= resignMoveCount )
					Result = new Result( ResultType.Adjudication, side ^ 1 );
			}
		}


		//	Adjucation result - the expected result of the game, 
		//	or null if the game can't be adjudicated yet
		public Result Result { get; private set; }

	
		// *** PRIVATE DATA MEMBERS *** //

		private int drawMoveNum;
		private int drawMoveCount;
		private int drawScore;
		private int drawScoreCount;
		private int resignMoveCount;
		private int resignScore;
		private int[] resignScoreCount;
		private bool tbEnabled;
	}
}
