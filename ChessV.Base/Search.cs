
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
using System.IO;
using System.Windows.Forms;

namespace ChessV
{
	public enum NodeType
	{
		PV,
		All,
		Cut
	}

	public partial class Game
	{
		// *********************************** //
		// ***                             *** //
		// ***       INTERNAL ENGINE       *** //
		// ***                             *** //
		// *********************************** //

		int[] razorMargin;

		public List<Movement> Think( TimeControl timeControl, int multiPV = 1 )
		{
			List<Movement> moves;
			thinkStartTime = DateTime.Now;
			this.timeControl = timeControl;
			abortSearch = false;

			//	clear killer moves
			for( int x = 0; x < MAX_DEPTH; x++ )
			{
				killers1[x] = 0;
				killers2[x] = 0;
			}

			//	clear history counters
			for( int p = 0; p < NumPlayers; p++ )
				for( int x = 0; x < NPieceTypes; x++ )
					for( int y = 0; y < Board.NumSquaresExtended; y++ )
					{
						historyCounters[p, x, y] = 0;
						butterflyCounters[p, x, y] = 1;
					}

			//	clear countermoves
			for( int x = 0; x < Board.NumSquaresExtended; x++ )
				for( int y = 0; y < Board.NumSquaresExtended; y++ )
					countermoves[x, y] = 0;

			//	reset statistics
			Statistics.Reset();
			Statistics.SearchStartTime = DateTime.Now;
			Statistics.SearchStartTime = DateTime.Now;

			//	create hashtable (transposition table)
			if( hashtable == null )
			{
				hashtable = new Hashtable();
				hashtable.SetSize( 128 );
			}

			// *** DETERMINE TIME ALLOCATION *** //

			#region Determine Time Allocation
			maxSearchTime = -1;
			absoluteMaxSearchTime = -1;
			exactMaxTime = -1;
			if( timeControl != null )
			{
				if( timeControl.TimePerMove != 0 )
				{
					exactMaxTime = timeControl.TimePerMove;
				}
				else
				{
					if( timeControl.MovesLeft != 0 )
					{
						// *** TOURNAMENT TIME CONTROLS *** //

						if( timeControl.MovesLeft == 1 )
						{
							maxSearchTime = timeControl.ActiveTimeLeft / 2;
							absoluteMaxSearchTime = Math.Min( timeControl.ActiveTimeLeft / 2, timeControl.ActiveTimeLeft - 500 );
						}
						else
						{
							maxSearchTime = timeControl.ActiveTimeLeft / Math.Min( timeControl.MovesLeft, 20 );
							absoluteMaxSearchTime = Math.Min( 4 * timeControl.ActiveTimeLeft / timeControl.MovesLeft, timeControl.ActiveTimeLeft / 3 );
						}
					}
					else
					{
						// *** SUDDEN DEATH TIME CONTROL *** //

						if( timeControl.TimeIncrement > 0 )
						{
							maxSearchTime = timeControl.ActiveTimeLeft / (StartingPieceCount[0] + StartingPieceCount[1] - 2) + timeControl.TimeIncrement;
							absoluteMaxSearchTime = Math.Max( timeControl.ActiveTimeLeft / 6, timeControl.TimeIncrement - 100 );
						}
						else
						{
							maxSearchTime = timeControl.ActiveTimeLeft / (StartingPieceCount[0] + StartingPieceCount[1] - 2);
							absoluteMaxSearchTime = timeControl.ActiveTimeLeft / 8;
						}
					}
				}
			}
			#endregion

			try
			{
				//	Initialize arrays for collecting PV(s)
				PV[] PVs = new PV[multiPV];
				for( int x = 0; x < multiPV; x++ )
				{
					PVs[x] = new PV();
					PVs[x].Initialize();
				}
				int[] pvScores = new int[multiPV];
				Dictionary<UInt32, PV> lookupPVbyExcludedMoves = new Dictionary<uint, PV>();

				//	If we are in multi-pv mode, we will use this movesToExclude list to 
				//	accumulate the moves selected from previous PVs and exclude them from 
				//	consideration in later PVs.
				List<UInt32> movesToExclude = multiPV > 1 ? new List<UInt32>() : null;

				int alpha = -INFINITY;
				int beta = INFINITY;
				int score = -INFINITY;
				int delta = -INFINITY;

				// *** ITERATIVE DEEPENING LOOP *** //
				bool deepen = true;
				for( idepth = ONEPLY; deepen && !abortSearch; idepth += ONEPLY )
				{
					if( movesToExclude != null )
						movesToExclude.Clear();

					lmrHistoryCutoff = 0;
					if( idepth >= 4 * ONEPLY )
					{
						for( int p = 0; p < NumPlayers; p++ )
							for( int x = 0; x < NPieceTypes; x++ )
								for( int y = 0; y < Board.NumSquaresExtended; y++ )
								{
									if( historyCounters[p, x, y] / butterflyCounters[p, x, y] > lmrHistoryCutoff )
										lmrHistoryCutoff = historyCounters[p, x, y] / butterflyCounters[p, x, y];
								}
						lmrHistoryCutoff = lmrHistoryCutoff * 5 / 4;
					}

					// *** MULTI-PV LOOP *** //
					for( int pvIndex = 0; pvIndex < multiPV && !abortSearch; pvIndex++ )
					{
						//	Reset aspiration window size
						if( idepth >= 5 * ONEPLY )
						{
							delta = 35;
							alpha = Math.Max( pvScores[pvIndex] - delta, -INFINITY );
							beta = Math.Min( pvScores[pvIndex] + delta, INFINITY );
						}

						UInt32 exclusionKey = 0;
						if( movesToExclude != null )
						{
							//	Determine key based on moves to exclude
							foreach( UInt32 movehash in movesToExclude )
								exclusionKey = exclusionKey ^ (movehash * 17);
							//	Lookup PV in case we've already run an iteration 
							//	with these particular moves excluded before
							if( lookupPVbyExcludedMoves.ContainsKey( exclusionKey ) )
								lookupPVbyExcludedMoves[exclusionKey].CopyTo( SearchStack[1].PV );
						}

						while( true )
						{
							score = SearchRoot( alpha, beta, idepth, movesToExclude );

							if( abortSearch )
								break;

							if( score <= alpha )
							{
								beta = (alpha + beta) / 2;
								alpha = Math.Max( score - delta, -INFINITY );
							}
							else if( score >= beta )
							{
								alpha = (alpha + beta) / 2;
								beta = Math.Min( score + delta, INFINITY );
							}
							else
								break;

							delta += delta; // delta / 4 + 5;
						}

						pvScores[pvIndex] = score;

						if( !abortSearch )
						{
							//	completed search - update current PV
							SearchStack[1].PV.CopyTo( PVs[pvIndex] );

							if( multiPV > 1 )
							{
								movesToExclude.Add( PVs[pvIndex][1] );
								lookupPVbyExcludedMoves[exclusionKey] = new PV();
								lookupPVbyExcludedMoves[exclusionKey].Initialize();
								PVs[pvIndex].CopyTo( lookupPVbyExcludedMoves[exclusionKey] );
							}

							#region Call ThinkingCallback to update display
							if( ThinkingCallback != null )
							{
								Int64 nodesUsed = Statistics.Nodes;
								TimeSpan timeUsed = DateTime.Now - thinkStartTime;
								StringBuilder pv = new StringBuilder( 80 );
								pv.Append( DescribeMove( searchStack[1].PV[1], MoveNotation.StandardAlbegraic ) );
								for( int x = 2; searchStack[1].PV[x] != 0; x++ )
								{
									pv.Append( " " );
									pv.Append( DescribeMove( searchStack[1].PV[x], MoveNotation.StandardAlbegraic ) );
								}
								Dictionary<string, string> searchinfo = new Dictionary<string, string>();
								searchinfo["Depth"] = (idepth / ONEPLY).ToString();
								searchinfo["Score"] = ((double) score / 100.0).ToString();
								searchinfo["Time"] = timeUsed.Minutes.ToString() + ":" + timeUsed.Seconds.ToString( "D2" );
								searchinfo["Nodes"] = nodesUsed.ToString();
								searchinfo["PV"] = pv.ToString();
								ThinkingCallback( searchinfo );
							}
							#endregion
						}
						else
							//	search aborted due to timeout or node limit reached
							deepen = false;
					}

					#region Determine whether to perform another iteration
					//	handle "Quick Hint" - timeControl is NULL, we search to 4 ply
					if( timeControl == null )
						deepen = idepth <= 3 * ONEPLY;
					else
					{
						deepen = true;
						if( !timeControl.Infinite )
						{
							long timeUsed = (long) (DateTime.Now - thinkStartTime).TotalMilliseconds;
							if( exactMaxTime > 0 )
							{
								if( timeUsed > exactMaxTime * 2 / 3 )
									deepen = false;
							}
							else
							{
								if( timeUsed > maxSearchTime / 3 || timeUsed > absoluteMaxSearchTime )
									deepen = false;
							}
						}
						//	handle fixed depth search
						if( timeControl.PlyLimit > 0 && deepen )
							deepen = idepth < timeControl.PlyLimit * ONEPLY;
						//	handle fixed node search
						else if( timeControl.NodeLimit > 0 && deepen )
							deepen = Statistics.Nodes * 2 < timeControl.NodeLimit;
					}
					#endregion
				}

				hashtable.NextGeneration();

				//	Add all moves for the current player from the PV to a 
				//	Movement list and return.  Normally there will be only one 
				//	move, but there could be more for multi-move variants.
				moves = new List<Movement>();
				for( int x = 1; PVs[0][x] != 0 && Movement.GetPlayerFromHash( PVs[0][x] ) == CurrentSide; x++ )
					moves.Add( new Movement( PVs[0][x] ) );
			}
			catch( Exception ex )
			{
				throw new Exceptions.ChessVException( this, ex );
			}
			return moves;
		}

		public int SearchRoot( int alpha, int beta, int depth, List<UInt32> movesToExclude = null )
		{
			//	track counts of moves executed:
			int moveNumber = 0; // count of all moves
			int normalMoveCount = 0; // count of non-captures

			//	positional extension (typically check extension)
			int extension = getExtension( 1 );
			if( depth < ONEPLY )
				depth += extension;

			//	track number of nodes per move for better move ordering
			Dictionary<UInt32, int> nodesPerMove = new Dictionary<UInt32, int>();

			//	no need to regenerate moves at the root - just restart the move selection
			moveLists[1].Restart( movesToExclude != null ? 0 : searchStack[1].PV[1] );

			// *** MOVE LOOP *** //
			int movingSide = CurrentSide;
			int originalAlpha = alpha;
			while( !abortSearch && moveLists[1].MakeNextMove() )
			{
				Statistics.Nodes++;
				MoveInfo currentMove = moveLists[1].CurrentMove;
				//	Check to see if this is an excluded move.  If it is, just 
				//	undo and continue.  The excluded moves are used for multi-pv
				if( movesToExclude != null && movesToExclude.Contains( currentMove ) )
				{
					moveLists[1].UnmakeMove();
					nodesPerMove.Add( currentMove.Hash, 0 );
					continue;
				}
				//	Store current node count so we can determine how many total 
				//	nodes were used during consideration of this move.  We will 
				//	use the info for better move ordering on the next iteration.
				long startNodeCount = Statistics.Nodes;
				searchPath[1] = currentMove;
				if( (currentMove.MoveType & MoveType.CaptureProperty) == 0 )
					normalMoveCount++;
				int score;
				if( moveNumber < 1 )
				{
					if( CurrentSide != movingSide )
						score = -SearchPV( -beta, -alpha, depth - ONEPLY, 2 );
					else
						score = SearchPV( alpha, beta, depth - ONEPLY, 2 );
				}
				else
				{
					//	Late Move Reductions
					int reduction = depth >= 2*ONEPLY && moveNumber > 5 && normalMoveCount > 1 && extension == 0 ?
						(Math.Min( depth/2, moveNumber ) /* * 2 / 3 */ + (moveNumber / 10) + 1) : 0;
					if( CurrentSide != movingSide )
						score = -Search( -alpha, depth - ONEPLY - reduction, 2, true, NodeType.Cut );
					else
						score = Search( beta, depth - ONEPLY - reduction, 2, true, NodeType.Cut );
					if( reduction > 0 && score > alpha )
						//	re-search without reduction
						if( CurrentSide != movingSide )
							score = -Search( -alpha, depth - ONEPLY, 2, true, NodeType.Cut );
						else
							score = Search( beta, depth - ONEPLY, 2, true, NodeType.Cut );
					if( score > alpha )
						//	we found a new best move and have a new PV
						if( CurrentSide != movingSide )
							score = -SearchPV( -beta, -alpha, depth - ONEPLY, 2 );
						else
							score = SearchPV( alpha, beta, depth - ONEPLY, 2 );
				}
				moveLists[1].UnmakeMove();

				//	did we run out of time?
				if( abortSearch )
					break;

				//	store the number of nodes considered for this move
				nodesPerMove.Add( currentMove.Hash, (int) ((Statistics.Nodes - startNodeCount) / depth) );

				//	if this move is better than alpha, we have a new PV
				if( score > alpha )
				{
					alpha = score;
					updatePV( 1 );
				}

				moveNumber++;
			}

			//	update move evaluations for better ordering on the next iteration
			if( !abortSearch && depth >= 3 * ONEPLY && (movesToExclude == null || movesToExclude.Count == 0) )
				moveLists[1].ReorderMoves( nodesPerMove );
			
			return alpha;
		}

		public int SearchPV( int alpha, int beta, int depth, int ply )
		{
			//	test for end-of-game
			MoveEventResponse response = TestForWinLossDraw( CurrentSide );
			if( response != MoveEventResponse.NotHandled )
			{
				if( response == MoveEventResponse.GameDrawn )
					return 0;
				if( response == MoveEventResponse.GameWon )
					return INFINITY - ply;
				if( response == MoveEventResponse.GameLost )
					return -INFINITY + ply;
			}

			//	bookkeeping, time check, etc., every 1024 nodes
			if( Statistics.Nodes % 1024 == 0 )
			{
				doBookkeeping();
				if( abortSearch )
					return 0;
			}

			//	positional extension (typically check extension only)
			int extension = getExtension( ply );
			if( depth < ONEPLY || ply < idepth * 2 )
				depth += extension;

			//	enter q-search when out of depth
			if( depth < ONEPLY )
				return QSearch( alpha, beta, 0, ply );

			searchStack[ply].PV[ply] = 0;
			searchStack[ply + 1].PV[ply] = 0;

			// *** TRANSPOSITION TABLE CHECK *** //
			TTHashEntry hash = new TTHashEntry();
			UInt32 hashtableMove = 0;
			if( hashtable.Lookup( GetPositionHashCode( ply ), ref hash ) )
			{
				//	If this position has been stored in the hashtable with a 
				//	preferred move, we'll go ahead and try that move first.
				//	Regardless of the depth associated with the stored hash, 
				//	this is still the best information available about best move.
				hashtableMove = hash.MoveHash;
				//	At a PV node, besides potentially offering a move to be 
				//	searched first, the hashtable entry is only good if the 
				//	stored hash is of type Exact and the stored depth is 
				//	greater that or equal to what we need.  (In practice, 
				//	this almost never happens.)
				if( hash.Depth >= depth && hash.Type == TTHashEntry.HashType.Exact )
				{
					if( hash.Score >= beta )
						saveKiller( ply, hash.MoveHash );
					return scoreFromHashtable( hash.Score, ply );
				}
			}

			// *** INTERNAL ITERATIVE DEEPENING *** //
			bool useIID =
				/* we have no move from the hashtable ... */ Movement.GetMoveTypeFromHash( hashtableMove ) == MoveType.Invalid &&
				/* ... and we have at least 5 ply remaining */ depth >= 5*ONEPLY &&
				/* ... and we are not in check */ extension == 0;
			if( useIID )
			{
				SearchPV( alpha, beta, depth - 2*ONEPLY, ply );
				hashtableMove = SearchStack[ply].PV[ply];
			}

			//	generate moves
			generateMoves( CurrentSide, ply, hashtableMove );

			// *** MOVE LOOP *** //
			int score = -INFINITY;
			int bestScore = -INFINITY;
			int originalAlpha = alpha;
			int moveNumber = 0;
			int normalMoveCount = 0; // non-captures
			int movingSide = CurrentSide;
			while( alpha < beta && moveLists[ply].MakeNextMove() )
			{
				Statistics.Nodes++;
				MoveInfo currentMove = moveLists[ply].CurrentMove;
				searchPath[ply] = currentMove;
				if( currentMove.MoveType == MoveType.StandardMove )
					normalMoveCount++;

				if( moveNumber == 0 )
				{
					//	FIRST MOVE - SEARCH AT FULL WIDTH  //

					if( CurrentSide != movingSide )
						score = -SearchPV( -beta, -alpha, depth - ONEPLY, ply + 1 );
					else
						score = SearchPV( alpha, beta, depth - ONEPLY, ply + 1 );
					//	undo the move
					moveLists[ply].UnmakeMove();
				}
				else
				{
					//	ALL MOVES AFTER FIRST ARE SEARCHED WITH ZERO WIDTH  //

					//	Late Move Reductions (LMR) - calculate reduction (if any)
					bool reduce =
						/* leave at least 1 full ply and ... */ depth >= 2 * ONEPLY &&
						/* ... we have searched at least 5 moves */ moveNumber > 5 &&
						/* ... we have searched at least 1 normal move */ normalMoveCount > 1 &&
						/* ... we are not in check (or other extension) */ extension == 0 &&
						/* ... this is not a killer move */ currentMove != killers1[ply] && currentMove != killers2[ply] &&
						/* ... this is not the recorded counter-move */ currentMove != countermoves[searchPath[ply - 1].FromSquare, searchPath[ply - 1].ToSquare] &&
						/* ... move is not too successful historically: */
						historyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare] /
							Math.Max((int) butterflyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare], 1) < lmrHistoryCutoff;

					int reduction = reduce
						? (Math.Min( depth / 2, moveNumber ) * 2 / 3 + (moveNumber / 10) + 1)
						: 0;

					int reducedDepth = reduction > 0 ? Math.Max( depth - ONEPLY - reduction, ONEPLY ) : depth - ONEPLY;
					//	After the first, all moves are searched with zero-width.  The idea 
					//	here is that we only need to prove that they are worse than the move 
					//	we already searched.  If we are incorrect about this, we'll need 
					//	to repeat the search with a full window.
					if( CurrentSide != movingSide )
						score = -Search( -alpha, reducedDepth, ply + 1, true, NodeType.Cut );
					else
						score = Search( beta, reducedDepth, ply + 1, true, NodeType.Cut );

					if( reduction > 0 && score > alpha )
						//	The move appears to be better than the one we already searched, but 
						//	we're not sure becuase we reduced.  Re-search without reduction.
						if( CurrentSide != movingSide )
							score = -Search( -alpha, depth - ONEPLY, ply + 1, true, NodeType.Cut );
						else
							score = Search( beta, depth - ONEPLY, ply + 1, true, NodeType.Cut );

					if( score > alpha && score < beta )
						//	fail high!  Our zero-window search was incorrect - the fact that the 
						//	value of alpha has increased means the first move was not the best, 
						//	we have a new PV, and need to re-search the move with full alpha-beta.
						if( CurrentSide != movingSide )
							score = -SearchPV( -beta, -alpha, depth - ONEPLY, ply + 1 );
						else
							score = SearchPV( alpha, beta, depth - ONEPLY, ply + 1 );

					if( currentMove.MoveType == MoveType.StandardMove && depth > 2*ONEPLY && score < beta )
						butterflyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare] += (ushort) (depth / ONEPLY);

					//	undo the move
					moveLists[ply].UnmakeMove();
				}

				if( abortSearch )
					//	we ran out of time and can't trust the results of this search
					return 0;

				if( score > bestScore )
				{
					bestScore = score;
					if( score > alpha )
					{
						alpha = score;
						updatePV( ply );

						if( score >= beta )
						{
							//	update killer moves
							saveKiller( ply, ref currentMove );
							//	update history counters
							if( depth > 2*ONEPLY )
								updateHistoryCounters( depth, ref currentMove );
							//	update countermove
							if( ply > 1 )
								countermoves[searchPath[ply - 1].FromSquare, searchPath[ply - 1].ToSquare] = currentMove.Hash;
						}
					}
				}

				moveNumber++;
			}

			if( moveNumber == 0 )
			{
				return -INFINITY + ply;
				//	TODO: checkmate or stalemate
			}

			if( bestScore < originalAlpha )
				hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( bestScore, ply ), depth, 0, TTHashEntry.HashType.UpperBound );
			else if( bestScore >= beta )
				hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( bestScore, ply ), depth, searchStack[ply].PV[ply], TTHashEntry.HashType.LowerBound );
			else
				hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( bestScore, ply ), depth, searchStack[ply].PV[ply], TTHashEntry.HashType.Exact );

			return bestScore;
		}

		public int Search( int beta, int depth, int ply, bool tryNullMove, NodeType nodeType )
		{
			//	test for end-of-game
			MoveEventResponse response = TestForWinLossDraw( CurrentSide );
			if( response != MoveEventResponse.NotHandled )
			{
				if( response == MoveEventResponse.GameDrawn )
					return 0;
				if( response == MoveEventResponse.GameWon )
					return INFINITY - ply;
				if( response == MoveEventResponse.GameLost )
					return -INFINITY + ply;
			}

			//	bookkeeping, time check, etc., every 1024 nodes
			if( Statistics.Nodes % 1024 == 0 )
			{
				doBookkeeping();
				if( abortSearch )
					return 0;
			}

			//	positional extension (typically check extension only)
			int extension = 0;
			if( depth < ONEPLY || ply < idepth * 2 )
			{
				extension = getExtension( ply );
				depth += extension;
			}

			//	enter q-search when out of depth
			if( depth < ONEPLY )
				return QSearch( beta - 1, beta, 0, ply );

			searchStack[ply].PV[ply] = 0;
			searchStack[ply + 1].PV[ply] = 0;

			// *** TRANSPOSITION TABLE CHECK *** //
			TTHashEntry hash = new TTHashEntry();
			UInt32 hashtableMove = 0;
			if( hashtable.Lookup( GetPositionHashCode( ply ), ref hash ) )
			{
				hashtableMove = hash.MoveHash;
				if( (hash.Depth >= depth ||
					 hash.Score >= Math.Max( INFINITY - 100, beta ) ||
					 hash.Score < Math.Min( -INFINITY + 100, beta )) &&
					((hash.Type == TTHashEntry.HashType.LowerBound && hash.Score >= beta) ||
					 (hash.Type == TTHashEntry.HashType.UpperBound && hash.Score < beta)) )
				{
					if( hash.Score >= beta )
						saveKiller( ply, hash.MoveHash );
					return scoreFromHashtable( hash.Score, ply );
				}
			}

			int eval = Evaluate();

			// *** RAZORING *** //
			if( depth < 4*ONEPLY && 
				hashtableMove == 0 && 
				extension == 0 && 
				eval + razorMargin[depth/ONEPLY] <= beta - 1 )
			{
				if( depth <= ONEPLY )
					return QSearch( beta - 1, beta, 0, ply );

				int rAlpha = beta - razorMargin[depth/ONEPLY] - 1;
				int val = QSearch( rAlpha, rAlpha + 1, 0, ply );
				if( val <= rAlpha )
					return val;
			}

			// *** NULL MOVE *** //
			
			//	we will disable the null move if we are too close to the end 
			//	to prevent serious mistakes in zugzuang positions, or if we 
			//	are extending (meaning we're probably in check)
			tryNullMove &= extension == 0 &&
				Board.GetPlayerPieceBitboard( 0 ).BitCount >= 3 &&
				Board.GetPlayerPieceBitboard( 1 ).BitCount >= 3 &&
				Board.GetPlayerPieceBitboard( 0 ).BitCount + Board.GetPlayerPieceBitboard( 1 ).BitCount >= 8 &&
				Board.GetEndgameMaterialEval( 0 ) >= 400 &&
				Board.GetEndgameMaterialEval( 1 ) >= 400 &&
				Board.GetEndgameMaterialEval( 0 ) + Board.GetEndgameMaterialEval( 1 ) >= 1200;
			//	determine size of reduction depending on remaining depth
			int nullReduction = (depth/ONEPLY >= 6 ? 3 : 2) * ONEPLY;
			//	if we have the depth remaining and are close enough to beta, make the null move
			if( tryNullMove && depth >= nullReduction + ONEPLY && 
				Board.GetMidgameMaterialEval() + (nodeType == NodeType.All ? 350 : 450) > beta )
			{
				int nullMoveSide = CurrentSide;
				//	make the null move
				moveLists[ply].MakeNullMove();
				searchPath[ply] = new Movement( 0, 0, 0, MoveType.NullMove );
				//	search with reduced depth
				int nullScore;
				if( CurrentSide != nullMoveSide )
					nullScore = -Search( -(beta - 1), depth - ONEPLY - nullReduction, ply + 1, false, nodeType );
				else
					//	this alternative exists for games with multiple moves such as Marseillais Chess
					nullScore = Search( beta, depth - ONEPLY - nullReduction, ply + 1, false, nodeType );
				//	take back the null move
				moveLists[ply].UnmakeNullMove();
				//	If null score exceeded beta, we'll quit searching and just return 
				//	beta.  The thinking is that if the current position is so strong that 
				//	we can do nothing and it is still good enough to generate a beta 
				//	cutoff, then there is no point in searching because surely there is 
				//	some move that is better than doing nothing which would also generate 
				//	beta cut-off.  In zugzuang positions, this assumption is incorrect!
				if( nullScore >= beta )
					return beta;
			}

			int score = -INFINITY;
			int bestScore = -INFINITY;
			int moveNumber = 0; // count all moves
			int normalMoveCount = 0; // count non-captures

			//	Generate moves
			generateMoves( CurrentSide, ply, hashtableMove );

			//	Futilty Pruning
			bool tryFutility = 
				/* at a frontier or pre-frontier node and */ depth - ONEPLY < 3*ONEPLY && 
				/* not in check (or other extension) */ extension == 0;

			// *** MOVE LOOP *** //
			int movingSide = CurrentSide;
			while( score < beta && moveLists[ply].MakeNextMove() )
			{
				Statistics.Nodes++;
				MoveInfo currentMove = moveLists[ply].CurrentMove;
				searchPath[ply] = currentMove;
				if( currentMove.MoveType == MoveType.StandardMove )
					normalMoveCount++;

				// *** REDUCTIONS *** //

				//	Fuility Pruning
				if( /* our pre-conditions are satisified and ... */ tryFutility && 
					/* ... we've searched at least one move */ moveNumber > 1 && 
					/* ... this is a 'normal' move (not capture or promotion) */ currentMove.MoveType == MoveType.StandardMove && 
					/* ... move leaves us far enough below beta */
					eval + Board.CalculateStandardMovePST( currentMove.FromSquare, currentMove.ToSquare ) + 
					(depth < 2*ONEPLY ? (currentMove.PieceMoved.PieceType is Pawn ? 40 : 20) : 300) < beta )
				{
					//	If we are at a frontier node, we'll go ahead and do an actual eval on the new position
					//	to ensure it is below beta.  (Our evaluation function is remarkably fast, since it doesn't
					//	consider very much.)  At a pre-frontier node, we are happy since we are so far below beta.
					int newEval = (depth < 2 * ONEPLY ? Evaluate() : beta-1);
					if( /* we are still far enough below beta... */ newEval < beta && 
						/* and this move doesn't give check (or other extension) */ getExtension( ply + 1 ) == 0 )
					{
						//	this node is pruned
						moveLists[ply].UnmakeMove();
						continue;
					}
				}

				//	Late Move Reductions
				bool reduce = 
					/* leave at least 1 full ply and ... */ depth >= 2*ONEPLY && 
					/* ... we have searched at least 5 moves */ moveNumber > 5 && 
					/* ... we have searched at least 1 normal (non-capture) move */ normalMoveCount > 1 &&
					/* ... we are not in check (or other extension) */ extension == 0 && 
					/* ... this is not a killer move */ currentMove != killers1[ply] && currentMove != killers2[ply] && 
					/* ... this is not the recorded counter-move */ currentMove != countermoves[searchPath[ply-1].FromSquare, searchPath[ply-1].ToSquare] && 
					/* ... this move is not too successful historically ... */
					historyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare] /
						Math.Max((int) butterflyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare], 1) < lmrHistoryCutoff;
				
				int reduction = !reduce ? 0 : 
					//	calculate size of reduction
					(Math.Min( depth/2, moveNumber ) * 2/3 + (moveNumber / 10) + (nodeType == NodeType.Cut ? 3 : 1));

				//	calculate new depth (making sure we never reduce so much that we don't leave on full ply
				int reducedDepth = reduction > 0 ? Math.Max( depth - ONEPLY - reduction, ONEPLY ) : depth - ONEPLY;


				// *** DEEPEN SEARCH RECURSIVELY *** //

				if( CurrentSide != movingSide )
					score = -Search( -(beta - 1), reducedDepth, ply + 1, true, nodeType == NodeType.Cut ? NodeType.All : NodeType.Cut );
				else
					//	this alternative exists for games with multiple moves such as Marseillais Chess
					score = Search( beta, reducedDepth, ply + 1, true, nodeType == NodeType.Cut ? NodeType.All : NodeType.Cut );

				//	if we reduced and failed high ...
				if( reduction > 0 && score >= beta )
					//	re-search without reduction
					if( CurrentSide != movingSide )
						score = -Search( -(beta - 1), depth - ONEPLY, ply + 1, true, nodeType == NodeType.Cut ? NodeType.All : NodeType.Cut );
					else
						score = Search( beta, depth - ONEPLY, ply + 1, true, nodeType == NodeType.Cut ? NodeType.All : NodeType.Cut );

				if( currentMove.MoveType == MoveType.StandardMove && depth > 2*ONEPLY && score < beta )
					//	update butterfly counter of unsuccessful moves
					butterflyCounters[currentMove.Player, currentMove.PieceMoved.TypeNumber, currentMove.ToSquare] += (ushort) (depth / ONEPLY);

				//	Undo the move
				moveLists[ply].UnmakeMove();

				if( abortSearch )
					//	we ran out of time and can't trust the results of this search
					return 0;

				if( score > bestScore )
				{
					bestScore = score;
					if( score >= beta )
					{
						//	update killer moves
						saveKiller( ply, ref currentMove );
						//	update history counters
						if( depth > 2*ONEPLY )
							updateHistoryCounters( depth, ref currentMove );
						//	update countermove
						if( ply > 1 )
							countermoves[searchPath[ply-1].FromSquare, searchPath[ply-1].ToSquare] = currentMove.Hash;
						//	update PV
						updatePV( ply );
						//	store lower bound in hash table
						hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( score, ply ), depth, currentMove.Hash, TTHashEntry.HashType.LowerBound );
					}
					else
						//	store uppder bound in hash table
						hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( score, ply ), depth, 0, TTHashEntry.HashType.UpperBound );
				}

				moveNumber++;
			}

			if( moveNumber == 0 )
			{
				//	If we found no legal moves, call NoMovesResult which will 
				//	in turn call the NoMovesResult for each rule object until 
				//	one of them returns a result (typically the CheckmateRule.)
				MoveEventResponse result = NoMovesResult( CurrentSide );
				if( result != MoveEventResponse.NotHandled )
				{
					if( result == MoveEventResponse.GameDrawn )
						return 0;
					if( result == MoveEventResponse.GameWon )
						return INFINITY - ply;
					if( result == MoveEventResponse.GameLost )
						return -INFINITY + ply;
				}
			}

			return bestScore;
		}

		public int QSearch( int alpha, int beta, int depth, int ply )
		{
			//	test for end-of-game
			MoveEventResponse response = TestForWinLossDraw( CurrentSide );
			if( response != MoveEventResponse.NotHandled )
			{
				if( response == MoveEventResponse.GameDrawn )
					return 0;
				if( response == MoveEventResponse.GameWon )
					return INFINITY - ply;
				if( response == MoveEventResponse.GameLost )
					return -INFINITY + ply;
			}

			searchStack[ply].PV[ply] = 0;
			searchStack[ply + 1].PV[ply] = 0;

			// *** TRANSPOSITION TABLE CHECK *** //
			TTHashEntry hash = new TTHashEntry();
			UInt32 hashtableMove = 0;
			if( hashtable.Lookup( GetPositionHashCode( ply ), ref hash ) )
			{
				hashtableMove = hash.MoveHash;
				if( (hash.Type == TTHashEntry.HashType.LowerBound && hash.Score >= beta) ||
					(hash.Type == TTHashEntry.HashType.UpperBound && hash.Score < beta) )
				{
					return scoreFromHashtable( hash.Score, ply );
				}
			}

			//	bookkeeping, time check, etc., every 1024 nodes
			if( Statistics.Nodes % 1024 == 0 )
			{
				doBookkeeping();
				if( abortSearch )
					return 0;
			}

			int eval = Evaluate();
			int score = eval;

			//	stand pat?
			if( score >= beta )
				return score;

			int bestScore = score;
			if( bestScore > alpha )
				alpha = bestScore;

			//	Generate moves
			generateMoves( CurrentSide, ply, 0, true );

			// *** MOVE LOOP *** //
			int movingSide = CurrentSide;
			while( alpha < beta && moveLists[ply].MakeNextMove( /* delta pruning threshold: */ alpha - eval - 50 ) )
			{
				Statistics.Nodes++;
				Statistics.QNodes++;
				MoveInfo currentMove = moveLists[ply].CurrentMove;
				searchPath[ply] = currentMove;

				if( CurrentSide != movingSide )
					score = -QSearch( -beta, -alpha, depth - ONEPLY, ply + 1 );
				else
					score = QSearch( alpha, beta, depth - ONEPLY, ply + 1 );
				moveLists[ply].UnmakeMove();

				if( abortSearch )
					//	we ran out of time and can't trust the results of this search
					return 0;

				if( score > bestScore )
				{
					bestScore = score;
					if( score > alpha )
					{
						alpha = score;
						updatePV( ply );
					}
				}
			}

			//	Update Transposition Table
			if( alpha - beta != 1 )
			{
				if( bestScore < beta )
				{
					if( bestScore > eval )
						hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( score, ply ), 0, 0, TTHashEntry.HashType.UpperBound );
				}
				else
					hashtable.Store( GetPositionHashCode( ply ), scoreToHashtable( score, ply ), 0, 0, TTHashEntry.HashType.LowerBound );
			}

			return bestScore;
		}

		protected int scoreFromHashtable( int score, int ply )
		{
			if( score >= INFINITY - 100 )
				return score - ply;
			if( score <= -INFINITY + 100 )
				return score + ply;
			return score;
		}

		protected int scoreToHashtable( int score, int ply )
		{
			if( score >= INFINITY - 100 )
				return score + ply;
			if( score <= -INFINITY + 100 )
				return score - ply;
			return score;
		}

		protected int getExtension( int ply )
		{
			int extension = 0;
			foreach( Rule rule in rulesHandlingSearchExtensions )
				extension += rule.PositionalSearchExtension( CurrentSide, ply );
			return extension > ONEPLY ? ONEPLY : extension;
		}

		protected void saveKiller( int ply, ref MoveInfo move )
		{
			if( move.MoveType == MoveType.StandardMove )
			{
				if( move.Hash != killers1[ply] )
				{
					killers2[ply] = killers1[ply];
					killers1[ply] = move.Hash;
				}
			}
		}

		protected void saveKiller( int ply, UInt32 movehash )
		{
			if( Movement.GetMoveTypeFromHash( movehash ) == MoveType.StandardMove )
			{
				if( movehash != killers1[ply] )
				{
					killers2[ply] = killers1[ply];
					killers1[ply] = movehash;
				}
			}
		}

		protected void updateHistoryCounters( int depth, ref MoveInfo move )
		{
			if( move.MoveType == MoveType.StandardMove )
				historyCounters[move.PieceMoved.Player, move.PieceMoved.TypeNumber, move.ToSquare] += 
					(UInt32) (((depth / ONEPLY) + 2) * ((depth / ONEPLY) + 1));
		}

		protected void perft( int ply, int depth, PerftResults results, StreamWriter log )
		{
			if( depth == 0 )
			{
				results.Nodes++;

				if( log != null )
				{
					StringBuilder sbr = new StringBuilder( 64 );
					sbr.Append( DescribeMove( moveLists[1].CurrentMove, MoveNotation.StandardAlbegraic ) );
					for( int p = 2; p < ply; p++ )
					{
						sbr.Append( ' ' );
						sbr.Append( DescribeMove( moveLists[p].CurrentMove, MoveNotation.StandardAlbegraic ) );
					}
					log.WriteLine( sbr );
				}

				return;
			}

			generateMoves( CurrentSide, ply, 0 );
			while( moveLists[ply].MakeNextMove() )
			{
				MoveInfo currentMove = moveLists[ply].CurrentMove;
				Board.Validate();
				if( depth == 1 )
				{

					if( currentMove.MoveType == MoveType.StandardCapture )
						results.Captures++;
					else if( currentMove.MoveType == MoveType.Castling )
						results.Castles++;
					else if( currentMove.MoveType == MoveType.EnPassant )
					{
						results.Captures++;
						results.EnPassants++;
					}
				}
				perft( ply + 1, depth - 1, results, log );
				moveLists[ply].UnmakeMove();
				Board.Validate();
			}
		}

		protected void updatePV( int ply )
		{
			int p;
			SearchStack[ply].PV[ply] = moveLists[ply].CurrentMove;
			for( p = ply + 1; searchStack[ply + 1].PV[p] != 0; p++ )
				searchStack[ply].PV[p] = searchStack[ply + 1].PV[p];
			searchStack[ply].PV[p] = searchStack[ply + 1].PV[p];
		}

		protected void doBookkeeping()
		{
			//	handle Windows events so the GUI doesn't stall
			Application.DoEvents();

			//	perform time check
			if( timeControl != null && !timeControl.Infinite )
			{
				long timeUsed = (long) (DateTime.Now - thinkStartTime).TotalMilliseconds;
				if( (absoluteMaxSearchTime > 0 && timeUsed > absoluteMaxSearchTime) ||
					(exactMaxTime > 0 && timeUsed > exactMaxTime) ||
					(timeControl.NodeLimit > 0 && Statistics.Nodes > timeControl.NodeLimit) )
					//	we must about the search
					abortSearch = true;
			}
		}

		public void AbortSearch()
		{
			abortSearch = true;
		}

		public PerftResults Perft( int depth, StreamWriter log = null )
		{
			PerftResults results = new PerftResults();
			perft( 1, depth, results, log );
			return results;
		}

		// *** EVAULATION *** //

		public int Evaluate()
		{
			//  basic material + piece-square-tables
			int midgameEval = Board.GetMidgameMaterialEval( 0 ) - Board.GetMidgameMaterialEval( 1 );
			int endgameEval = Board.GetEndgameMaterialEval( 0 ) - Board.GetEndgameMaterialEval( 1 );

			//  call all game-specific evaluation functions
			foreach( Evaluation evaluation in evaluations )
				evaluation.AdjustEvaluation( ref midgameEval, ref endgameEval );

			//  scale result based on midgame -> endgame progress and return result
			int materialEval = Board.GetPlayerMaterial( 0 ) + Board.GetPlayerMaterial( 1 );
			int phase =
                materialEval >= MidgameMaterialThreshold ? 128 :
				(materialEval <= EndgameMaterialThreshold ? 0 :
				(((materialEval - EndgameMaterialThreshold) * 128) / (MidgameMaterialThreshold - EndgameMaterialThreshold)));
			int eval = sign[CurrentSide] * ((midgameEval * phase) + (endgameEval * (128 - phase))) / 128;

			//	round the eval to the nearest 4 (essentially reducing the resolution from a 
			//	hundredth of a pawn to a quarter of a pawn.)
			eval = ((eval & 2) << 1) + (eval & ~3);
			return eval;
		}
	}
}
