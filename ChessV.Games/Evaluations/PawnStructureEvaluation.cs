
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

namespace ChessV.Evaluations
{
    public class PawnStructureEvaluation: Evaluation
    {
		// *** PROPERTIES *** //

		//	Stores the weights for the various adjustments
		public PawnStructureAdjustments Adjustments { get; private set; }
	

		// *** INITIALIZATION *** //

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			player0pawns = new int[Board.MAX_FILES + 2];
			player1pawns = new int[Board.MAX_FILES + 2];
			player0backPawn = new int[Board.MAX_FILES + 2];
			player1backPawn = new int[Board.MAX_FILES + 2];
			pawnHashTable = new PawnHashEntry[1048576]; // 2^20 slots
			Adjustments = new PawnStructureAdjustments();
		}

        public override void PostInitialize()
        {
            base.PostInitialize();

            PieceType[] pieceTypes;
            int pieceTypeCount = game.GetPieceTypes( out pieceTypes );
            for( int nPieceType = 0; nPieceType < pieceTypeCount; nPieceType++ )
                if( pieceTypes[nPieceType] is Pawn )
                    pawnTypeNumber = pieceTypes[nPieceType].TypeNumber;
        }


		// *** EVENT HANDLERS *** //

        public override void AdjustEvaluation( ref int midgameEval, ref int endgameEval )
        {
			// *** CHECK PAWN STRUCTURE HASH TABLE *** //

			int slot = (int) (game.Board.PawnHashCode & 0x00000000000FFFFFUL);
			if( pawnHashTable[slot].HashCode == game.Board.PawnHashCode )
			{
				midgameEval += pawnHashTable[slot].MidgameAdjustment;
				endgameEval += pawnHashTable[slot].EndgameAdjustment;
				return;
			}


            // *** DETERMINE BASIC PAWN INFO *** //

			int midgameAdjustment = 0;
			int endgameAdjustment = 0;

            //  reset information
            for( int file = 0; file < board.NumFiles + 2; file++ )
            {
                player0pawns[file] = 0;
                player1pawns[file] = 0;
                player0backPawn[file] = 128;
                player1backPawn[file] = 0;
            }
            
            //  loop through player 0's pawns
			BitBoard p0pawns = board.GetPieceTypeBitboard( 0, pawnTypeNumber );
            while( p0pawns )
            {
				int square = p0pawns.ExtractLSB();
				int file = board.GetFile( square );
                int rank = board.GetRank( square );
                player0pawns[file+1]++;
                if( rank < player0backPawn[file+1] )
                    player0backPawn[file+1] = rank;
            }

            //  loop through player 1's pawns
			BitBoard p1pawns = board.GetPieceTypeBitboard( 1, pawnTypeNumber );
            while( p1pawns )
            {
				int square = p1pawns.ExtractLSB();
				int file = board.GetFile( square );
                int rank = board.GetRank( square );
                player1pawns[file+1]++;
                if( rank > player1backPawn[file+1] )
                    player1backPawn[file+1] = rank;
            }


            // *** APPLY THIS INFO TO EACH PAWN TO DETERMINE STATUS *** //

            //  lopp through player 0's pawns
			p0pawns = board.GetPieceTypeBitboard( 0, pawnTypeNumber );
            while( p0pawns )
            {
				int square = p0pawns.ExtractLSB();
				int pawnFile = board.GetFile( square );
                int pawnRank = board.GetRank( square );
                bool isolated = false;
                bool backward = false;

                if( player0pawns[pawnFile] == 0 && 
				    player0pawns[pawnFile + 2] == 0 )
                {
                    //	isolated pawn
                    midgameAdjustment -= Adjustments.IsolatedMidgame;
                    endgameAdjustment -= Adjustments.IsolatedEndgame;
                    isolated = true;
                }
                if( player0backPawn[pawnFile] > pawnRank && 
				    player0backPawn[pawnFile + 2] > pawnRank )
                {
                    //	backward pawn
                    midgameAdjustment -= Adjustments.BackwardMidgame;
                    endgameAdjustment -= Adjustments.BackwardEndgame;
                    backward = true;
                }
                if( player1pawns[pawnFile + 1] == 0 )
                {
                    //	penalize weak, exposed pawns
                    if( backward )
                    {
                        midgameAdjustment -= Adjustments.WeakExposedMidgame;
                        endgameAdjustment -= Adjustments.WeakExposedEndgame;
                    }
                    if( isolated )
                    {
                        midgameAdjustment -= Adjustments.WeakExposedMidgame;
                        endgameAdjustment -= Adjustments.WeakExposedEndgame;
                    }
                }
                if( player0backPawn[pawnFile + 1] < pawnRank )
                {
                    //	doubled, trippled, etc.
                    midgameAdjustment -= Adjustments.DoubledMidgame;
                    endgameAdjustment -= Adjustments.DoubledEndgame;
                }
                if( pawnRank >= player1backPawn[pawnFile + 1] &&
				    pawnRank >= player1backPawn[pawnFile] &&
    				pawnRank >= player1backPawn[pawnFile + 2] )
                {
                    //	passed pawn
                    midgameAdjustment += Adjustments.PassedMidgame;
                    endgameAdjustment += Adjustments.PassedEndgame;
                    if( !isolated )
                    {
                        midgameAdjustment += Adjustments.PassedNotIsolatedMidgame;
                        endgameAdjustment += Adjustments.PassedNotIsolatedEndgame;
                    }
                }
            }

            //  loop through player 1's pawns
			p1pawns = board.GetPieceTypeBitboard( 1, pawnTypeNumber );
            while( p1pawns )
            {
				int square = p1pawns.ExtractLSB();
				int pawnFile = board.GetFile( square );
                int pawnRank = board.GetRank( square );

                bool isolated = false;
                bool backward = false;

                if( player1pawns[pawnFile] == 0 && 
				    player1pawns[pawnFile + 2] == 0 )
                {
                    //	isolated pawn
                    midgameAdjustment += Adjustments.IsolatedMidgame;
                    endgameAdjustment += Adjustments.IsolatedEndgame;
                    isolated = true;
                }
                if( player1backPawn[pawnFile] < pawnRank && 
				    player1backPawn[pawnFile + 2] < pawnRank )
                {
                    //	backward pawn
                    midgameAdjustment += Adjustments.BackwardMidgame;
                    endgameAdjustment += Adjustments.BackwardEndgame;
                    backward = true;
                }
                if( player0pawns[pawnFile + 1] == 0 )
                {
                    //	penalize weak, exposed pawns
                    if( backward )
                    {
                        midgameAdjustment += Adjustments.WeakExposedMidgame;
                        endgameAdjustment += Adjustments.WeakExposedEndgame;
                    }
                    if( isolated )
                    {
                        midgameAdjustment += Adjustments.WeakExposedMidgame;
                        endgameAdjustment += Adjustments.WeakExposedEndgame;
                    }
                }
                if( player1backPawn[pawnFile + 1] > pawnRank )
                {
                    //	doubled, trippled, etc.
                    midgameAdjustment += Adjustments.DoubledMidgame;
                    endgameAdjustment += Adjustments.DoubledEndgame;
                }
                if( pawnRank <= player0backPawn[pawnFile + 1] &&
				    pawnRank <= player0backPawn[pawnFile] &&
				    pawnRank <= player0backPawn[pawnFile + 2] )
                {
                    //	passed pawn
                    midgameAdjustment -= Adjustments.PassedMidgame;
                    endgameAdjustment -= Adjustments.PassedEndgame;
                    if( !isolated )
                    {
                        midgameAdjustment -= Adjustments.PassedNotIsolatedMidgame;
                        endgameAdjustment -= Adjustments.PassedNotIsolatedEndgame;
                    }
                }
            }

			//	store this information in the pawn hash table
			pawnHashTable[slot].HashCode = game.Board.PawnHashCode;
			pawnHashTable[slot].MidgameAdjustment = midgameAdjustment;
			pawnHashTable[slot].EndgameAdjustment = endgameAdjustment;

			//	adjust the actual evaluations accordingly
			midgameEval += midgameAdjustment;
			endgameEval += endgameAdjustment;
        }


		// *** PROTECTED DATA MEMBERS *** //

		protected int pawnTypeNumber;
		protected int[] player0pawns;
		protected int[] player1pawns;
		protected int[] player0backPawn;
		protected int[] player1backPawn;
		protected PawnHashEntry[] pawnHashTable;
	}
}
