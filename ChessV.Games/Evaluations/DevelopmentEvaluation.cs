
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
using ChessV.Games.Rules;

namespace ChessV.Evaluations
{
	public class DevelopmentEvaluation: Evaluation
	{
		// *** PROPERTIES *** //

		public int OpeningProgress { get; private set; }
		public int OpeningTransitionThreshold { get; set; }
		public int OpeningCompleteThreshold { get; set; }
		public int PawnPieceType { get; set; }
		public int PawnMaxRankFirstMove { get; set; }
		public int HeavyPieceThreshold { get; set; }
		public int LeftPawnPenaltyFile { get; set; }
		public int RightPawnPenaltyFile { get; set; }
		public CastlingRule CastlingRule { get; private set; }
		public int[] EvalAdjustment { get; private set; }


		// *** CONSTRUCTION *** //

		public DevelopmentEvaluation()
		{
			OpeningProgress = 0;
			OpeningTransitionThreshold = 0;
			OpeningCompleteThreshold = 0;
			PawnPieceType = -1;
			PawnMaxRankFirstMove = 0;
			HeavyPieceThreshold = 400;
			LeftPawnPenaltyFile = -1;
			RightPawnPenaltyFile = -1;
		}


		// *** INITIALIZATION *** //

        public override void Initialize( Game game )
        {
            base.Initialize( game );
			EvalAdjustment = new int[game.NumPlayers];
			for( int player = 0; player < game.NumPlayers; player++ )
				EvalAdjustment[player] = 0;
		}

		public override void PostInitialize()
		{
			base.PostInitialize();

			//	We will set the values of the properties to what we believe 
			//	are reasonable values based on examination of the Game and 
			//	its PieceTypes.  We will only set these values if they are 
			//	still unset so that individual games can tweek the values if 
			//	they choose without us overwriting them here.

			//	first, determine the total number of pieces on board at game start
			int gameStartPieceCount = 0;
			for( int player = 0; player < game.NumPlayers; player++ )
				gameStartPieceCount += game.StartingPieceCount[player];


			// *** OPENING THRESHOLDS *** //

			//	The values for OpeningTransitionThreshold and 
			//	OpeningCompleteThreshold determine when we start phasing 
			//	out the development bonus/penalty and when it is completely 
			//	phased out, respectively, based on OpeningProgress.
			if( OpeningTransitionThreshold == 0 )
				OpeningTransitionThreshold = gameStartPieceCount / 4 + 1;
			if( OpeningCompleteThreshold == 0 )
				OpeningCompleteThreshold = OpeningTransitionThreshold * 2 - 2;


			// *** PAWN STUFF *** //

			//	Determine if we have a standard pawn in this game, and 
			//	if so, what's the farthest square it can reach on first move.
			if( PawnPieceType == -1 )
			{
				for( int nPieceType = 0; nPieceType < game.NPieceTypes; nPieceType++ )
					if( game.GetPieceType( nPieceType ) is Pawn )
						PawnPieceType = game.GetPieceType( nPieceType ).TypeNumber;
			}
			if( PawnPieceType >= 0 && PawnMaxRankFirstMove == 0 )
			{
				//	first, determine the max steps of the pawn's longest move
				int maxSteps = 0;
				MoveCapability[] moves;
				int nMoves = game.GetPieceType( PawnPieceType ).GetMoveCapabilities( out moves );
				for( int x = 0; x < nMoves; x++ )
					if( moves[x].MaxSteps > maxSteps )
						maxSteps = moves[x].MaxSteps;
				//	now find the farthest rank on which a pawn begins
				int maxRank = 0;
				foreach( KeyValuePair<string, GenericPiece> pair in game.StartingPieces )
				{
					GenericPiece piece = pair.Value;
					if( piece != null && piece.PieceType.TypeNumber == PawnPieceType )
					{
						int rank = game.Board.GetRank( game.Board.PlayerSquare(
							piece.Player, game.NotationToSquare( pair.Key ) ) );
						if( rank > maxRank )
							maxRank = rank;
					}
				}
				//	the max rank a pawn can reach on its first move is 
				//	the sum of the rank on which it begins and its max reach.
				//	NOTE: this calculation isn't perfect if the pawn piece is 
				//	modified in some very unusual way, or different pawns start on 
				//	different ranks.
				PawnMaxRankFirstMove = maxRank + maxSteps;
			}
			if( LeftPawnPenaltyFile == -1 )
				LeftPawnPenaltyFile = 0;
			if( RightPawnPenaltyFile == -1 )
				RightPawnPenaltyFile = game.Board.NumFiles - 1;

			//	find this game's castling rule (if it has one)
			CastlingRule = (CastlingRule) game.FindRule( typeof(CastlingRule), true );
		}


		// *** OVERRIDES *** //

		public override void MoveBeingMade( MoveInfo move, int ply )
		{
			// *** OPENING PROGRESS *** //

			//	The purpose of this is to advance the OpeningProgress 
			//	appropriately for the given move.  The OpeningProgress is 
			//	the measure by which we estimate how far we've come 
			//	from beginning of game, through opening, and into midgame.
			//	This way we can phase out the development adjustments.

			//	a castling move advances the count by 3
			if( move.MoveType == MoveType.Castling )
				OpeningProgress += 3;
			//	any capture advances the count by 2
			else if( move.MoveType == MoveType.StandardCapture )
				OpeningProgress += 2;
			//	the first (non-capture) move of any piece advances the count 1
			else if( move.PieceMoved != null && move.PieceMoved.MoveCount == 1 )
				OpeningProgress++;
			else
			{
				//	the only other way we will advance the counter by 1 is if
				//	the moving piece is a standard pawn (can't move backwards) 
				//	and the move takes it to a rank farther than it could have 
				//	moved on its first move.
				if( move.PieceMoved != null && move.PieceMoved.TypeNumber == PawnPieceType &&
					game.Board.GetRank( game.Board.PlayerSquare( move.Player, move.ToSquare ) ) > PawnMaxRankFirstMove )
					OpeningProgress++;
			}

			// *** EVALUATION ADJUSTMENT *** //

			//	castling is worth a significant bonus
			if( move.MoveType == MoveType.Castling )
				EvalAdjustment[move.Player] += 50;
			else
			{
				//	The first move of a piece may be given a penalty
				if( move.PieceMoved != null && move.PieceMoved.MoveCount == 1 )
				{
					//	Moving a major piece in the opening is penalized
					if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue > HeavyPieceThreshold + 25 )
					{
						int adj = (int) Math.Log( move.PieceMoved.PieceType.MidgameValue - HeavyPieceThreshold, 1.03 ) - 100;
						EvalAdjustment[move.Player] -= adj;
					}
					//	Pushing an edge pawn is penalized
					else if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue < 150 &&
						(board.GetFile( move.FromSquare ) <= LeftPawnPenaltyFile ||
							board.GetFile( move.FromSquare ) >= RightPawnPenaltyFile) )
						EvalAdjustment[move.Player] -= 50;
				}
				//	Moving a piece twice is penalized
				else if( move.MoveType == MoveType.StandardMove )
				{
					if( move.PieceMoved.PieceType.MidgameValue > 150 )
						EvalAdjustment[move.Player] -= 40;
					else
						EvalAdjustment[move.Player] -= 10;
				}
				//	Moving a king in a non-castling move is penalized 
				//	if this game has a castling rule
				else if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue == 0 && CastlingRule != null )
					EvalAdjustment[move.Player] -= 75;
			}
		}

		public override void MoveBeingUnmade( MoveInfo move, int ply )
		{
			//	This function exactly reverses the operations performed 
			//	in MoveBeingMade when the move was first made.

			// *** OPENING PROGRESS *** //

			//	a castling move advanced the count by 3
			if( move.MoveType == MoveType.Castling )
				OpeningProgress -= 3;
			//	any capture advanced the count by 2
			else if( move.MoveType == MoveType.StandardCapture )
				OpeningProgress -= 2;
			//	the first (non-capture) move of any piece advanced the count 1
			else if( move.PieceMoved != null && move.PieceMoved.MoveCount == 1 )
				OpeningProgress--;
			else
			{
				//	the only other way we would have advanced the counter by 1 is
				//	if the moving piece is a standard pawn (can't move backwards) 
				//	and the move took it to a rank farther than it could have 
				//	moved on its first move.
				if( move.PieceMoved != null && move.PieceMoved.TypeNumber == PawnPieceType &&
					game.Board.GetRank( game.Board.PlayerSquare( move.Player, move.ToSquare ) ) > PawnMaxRankFirstMove )
					OpeningProgress--;
			}

			// *** EVALUATION ADJUSTMENT *** //

			//	castling is worth a significant bonus
			if( move.MoveType == MoveType.Castling )
				EvalAdjustment[move.Player] -= 50;
			else
			{
				//	The first move of a piece may be given a penalty
				if( move.PieceMoved != null && move.PieceMoved.MoveCount == 1 )
				{
					//	Moving a major piece in the opening is penalized
					if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue > HeavyPieceThreshold + 25 )
					{
						int adj = (int) Math.Log( move.PieceMoved.PieceType.MidgameValue - HeavyPieceThreshold, 1.03 ) - 100;
						EvalAdjustment[move.Player] += adj;
					}
					//	Pushing an edge pawn is penalized
					else if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue < 150 &&
						(board.GetFile( move.FromSquare ) == 0 ||
							board.GetFile( move.FromSquare ) == board.NumFiles - 1) )
						EvalAdjustment[move.Player] += 50;
				}
				//	Moving a piece twice is penalized
				else if( move.MoveType == MoveType.StandardMove )
				{
					if( move.PieceMoved.PieceType.MidgameValue > 150 )
						EvalAdjustment[move.Player] += 40;
					else
						EvalAdjustment[move.Player] += 10;
				}
				//	Moving a king in a non-castling move is penalized 
				//	if this game has a castling rule
				else if( move.PieceMoved != null && move.PieceMoved.PieceType.MidgameValue == 0 && CastlingRule != null )
					EvalAdjustment[move.Player] += 75;
			}
		}

		public override void AdjustEvaluation( ref int midgameEval, ref int endgameEval )
		{
			int phase =
				OpeningProgress >= OpeningCompleteThreshold ? 0 :
				(OpeningProgress < OpeningTransitionThreshold ? (OpeningCompleteThreshold - OpeningTransitionThreshold) : 
				(OpeningProgress - OpeningTransitionThreshold));
			midgameEval += (EvalAdjustment[0] - EvalAdjustment[1]) * phase / (OpeningCompleteThreshold - OpeningTransitionThreshold);
		}
	}
}
