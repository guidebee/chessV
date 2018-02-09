
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
	public delegate bool CustomMoveGenerationHandler( PieceType pieceType, Piece piece, MoveList moveList, bool capturesOnly );

	public class PieceType: ExObject
	{
		// *** CONSTANTS *** //

		#region Constants
		const int MAX_MOVE_CAPABILITIES = 32;
		#endregion


		// *** PUBLIC PROPERTIES *** //

		#region Public Properties
		public Game Game { get; private set; }
		public Board Board { get; private set; }

		public string InternalName { get; private set; }
		public string Name { get; set; }
		public string Notation { get; set; }
		public List<string> ImagePreferenceList { get; protected set; }
		public string PreferredImage { get; set; }
		public string FallbackImage { get; set; }
		public bool SimpleMoveGeneration { get; protected set; }
		public bool HasMovesWithPaths { get; protected set; }
		public bool HasMovesWithConditionalLocation { get; protected set; }

		public bool Enabled { get; set; }

		public int TypeNumber;
		public int[] AttackRangePerDirection;
		public int[] CannonAttackRangePerDirection;

		public int MidgameValue { get; set; }
		public int EndgameValue { get; set; }

		public int NumSlices { get; private set; }
		public int[] SliceLookup { get; private set; }

		public CustomMoveGenerationHandler CustomMoveGenerator { get; set; }

		public int NumMoveCapabilities
		{ get { return nMoveCapabilities; } }

		//	variables in calculating the piece-square-tables
		public int PSTMidgameInSmallCenter { get; set; }
		public int PSTMidgameInLargeCenter { get; set; }
		public int PSTMidgameSmallCenterAttacks { get; set; }
		public int PSTMidgameLargeCenterAttacks { get; set; }
		public int PSTMidgameForwardness { get; set; }
		public int PSTMidgameGlobalOffset { get; set; }
		public int PSTEndgameInSmallCenter { get; set; }
		public int PSTEndgameInLargeCenter { get; set; }
		public int PSTEndgameSmallCenterAttacks { get; set; }
		public int PSTEndgameLargeCenterAttacks { get; set; }
		public int PSTEndgameForwardness { get; set; }
		public int PSTEndgameGlobalOffset { get; set; }
		public Double AverageMobility { get; set; }
		public Double AverageDirectionsAttacked { get; set; }
		public Double AverageSafeChecks { get; set; }
		#endregion


		public int GetMoveCapabilities( out MoveCapability[] moves )
		{ moves = moveCapabilities; return nMoveCapabilities; }

		protected PieceType( string internalName, string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null )
		{
			InternalName = internalName;
			Name = name;
			Notation = notation;
			ImagePreferenceList = new List<string>();
			ImagePreferenceList.Add( internalName );
			if( internalName != name )
				ImagePreferenceList.Add( name );
			PreferredImage = preferredImageName;
			SimpleMoveGeneration = true;
			HasMovesWithPaths = false;
			HasMovesWithConditionalLocation = false;
			Enabled = true;
			moveCapabilities = new MoveCapability[MAX_MOVE_CAPABILITIES];
			nMoveCapabilities = 0;
			AttackRangePerDirection = new int[Game.MAX_DIRECTIONS];
			CannonAttackRangePerDirection = new int[Game.MAX_DIRECTIONS];
			MidgameValue = midgameValue;
			EndgameValue = endgameValue;
			CustomMoveGenerator = null;

			PSTMidgameInSmallCenter = 3; // 4;
			PSTMidgameInLargeCenter = 3; // 4;
			PSTMidgameSmallCenterAttacks = 1;
			PSTMidgameLargeCenterAttacks = 1; // 2;
			PSTMidgameForwardness = 2;
			PSTMidgameGlobalOffset = -15;
			PSTEndgameInSmallCenter = 3;
			PSTEndgameInLargeCenter = 3;
			PSTEndgameSmallCenterAttacks = 1;
			PSTEndgameLargeCenterAttacks = 1;
			PSTEndgameForwardness = 1;
			PSTEndgameGlobalOffset = -10;
		}

		public static void AddMoves( PieceType type )
		{
		}

		public void Step( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.Step( direction ); }

		public void Slide( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.Slide( direction ); }

		public void Slide( Direction direction, int maxSteps )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.Slide( direction, maxSteps ); }

		public void StepMoveOnly( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.StepMoveOnly( direction ); }

		public void SlideMoveOnly( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.SlideMoveOnly( direction ); }

		public void StepCaptureOnly( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.StepCaptureOnly( direction ); }

		public void SlideCaptureOnly( Direction direction )
		{ moveCapabilities[nMoveCapabilities++] = MoveCapability.SlideCaptureOnly( direction ); }

		public void CannonMove( Direction direction )
		{
			moveCapabilities[nMoveCapabilities++] = MoveCapability.CannonMove( direction );
			SimpleMoveGeneration = false;
		}

		public void RifleCapture( Direction direction, int maxSpaces )
		{
			moveCapabilities[nMoveCapabilities++] = MoveCapability.RifleCapture( direction, maxSpaces );
			SimpleMoveGeneration = false;
		}

		public void AddMoveCapability( MoveCapability moveCapability )
		{ 
			moveCapabilities[nMoveCapabilities++] = moveCapability;
			if( moveCapability.PathInfo != null )
			{
				SimpleMoveGeneration = false;
				HasMovesWithPaths = false;
			}
			if( moveCapability.Condition != null )
			{
				HasMovesWithConditionalLocation = true;
			}
		}

		public virtual void Initialize( Game game )
		{
			Game = game;
			Board = game.Board;

			//	add extra image names to image list
			if( PreferredImage != null )
				ImagePreferenceList.Insert( 0, PreferredImage );
			if( FallbackImage != null )
				ImagePreferenceList.Add( FallbackImage );

			//	find this piece type in the Game's index
			TypeNumber = game.GetPieceTypeNumber( this );

			//	initialize hash keys
			hashKeyIndex = new int[game.NumPlayers];
			pawnHashKeyIndex = new int[game.NumPlayers];
			for( int player = 0; player < game.NumPlayers; player++ )
			{
				hashKeyIndex[player] = game.HashKeys.TakeKeys( game.Board.NumSquaresExtended );
				pawnHashKeyIndex[player] = 0;
			}
			
			//	initialize all move capabilities
			for( int x = 0; x < nMoveCapabilities; x++ )
			{
				moveCapabilities[x].Initialize( game );
				//	find the number of this direction in the Game's index of Directions
				Direction direction = moveCapabilities[x].Direction;
				Direction[] gameDirections;
				int nGameDirections = game.GetDirections( out gameDirections );
				for( int y = 0; y < nGameDirections; y++ )
				{
					Direction gameDirection = gameDirections[y];
					if( direction == gameDirection )
					{
						moveCapabilities[x].NDirection = y;
						break;
					}
				}
				//	if this move has a move path, initialize all the 
				//	direction numbers for the directions of the individual 
				//	steps down the path
				if( moveCapabilities[x].PathInfo != null )
				{
					foreach( List<Direction> dirPath in moveCapabilities[x].PathInfo.PathDirections )
					{
						List<int> path = new List<int>();
						foreach( Direction dir in dirPath )
						{
							for( int y = 0; y < nGameDirections; y++ )
							{
								Direction gameDirection = gameDirections[y];
								if( dir == gameDirection )
								{
									path.Add( y );
									break;
								}
							}
						}
						moveCapabilities[x].PathInfo.PathNDirections.Add( path );
					}
				}
				//	update AttackRangePerDirection
				if( moveCapabilities[x].CanCapture &&
					AttackRangePerDirection[moveCapabilities[x].NDirection] < moveCapabilities[x].MaxSteps )
					AttackRangePerDirection[moveCapabilities[x].NDirection] = moveCapabilities[x].MaxSteps;
				//	update CannonAttackRangePerDirection
				if( (moveCapabilities[x].SpecialAttacks & SpecialAttacks.CannonCapture) != 0 && 
					CannonAttackRangePerDirection[moveCapabilities[x].NDirection] < moveCapabilities[x].MaxSteps )
					CannonAttackRangePerDirection[moveCapabilities[x].NDirection] = moveCapabilities[x].MaxSteps;
			}

			//	Calculate slices.  Slices are sets of squares that cannot be reached from 
			//	other squares by this piece ("colorbound").  For most pieces, the entire 
			//	board is one slice.  For a bishop, the board has two slices.  A dabbabah has four.
			NumSlices = 0;
			SliceLookup = new int[Board.NumSquares];
			for( int square = 0; square < Board.NumSquares; square++ )
				SliceLookup[square] = -1;
			for( int square = 0; square < Board.NumSquares; square++ )
				if( SliceLookup[square] == -1 )
					findSquare( square, NumSlices++ );

			//	Calculate some values that will be later used to build the PST
			pstSmallCenterAttacks = new int[Board.NumSquares];
			pstLargeCenterAttacks = new int[Board.NumSquares];
			bool[] reachableSquares = new bool[Board.NumSquares];
			for( int sq = 0; sq < Board.NumSquares; sq++ )
			{
				pstSmallCenterAttacks[sq] = 0;
				pstLargeCenterAttacks[sq] = 0;

				//	clear out reachableSquares matrix
				for( int y = 0; y < Board.NumSquares; y++ )
					reachableSquares[y] = false;

				GetEmptyBoardMobility( game, 0, sq, reachableSquares );
				for( int y = 0; y < Board.NumSquares; y++ )
					if( reachableSquares[y] )
					{
						pstSmallCenterAttacks[sq] += Board.InSmallCenter( y );
						pstLargeCenterAttacks[sq] += Board.InLargeCenter( y );
					}
			}

			//	Initialize PST
			pstMidgame = new int[Board.NumSquaresExtended];
			pstEndgame = new int[Board.NumSquaresExtended];
			for( int sq = 0; sq < Board.NumSquares; sq++ )
			{
				pstMidgame[sq] = PSTMidgameGlobalOffset +
					PSTMidgameInLargeCenter * Board.InLargeCenter( sq ) +
					PSTMidgameInSmallCenter * Board.InSmallCenter( sq ) +
					PSTMidgameLargeCenterAttacks * pstLargeCenterAttacks[sq] +
					PSTMidgameSmallCenterAttacks * pstSmallCenterAttacks[sq] +
					PSTMidgameForwardness * Board.Forwardness( sq );
				pstEndgame[sq] = PSTMidgameGlobalOffset +
					PSTEndgameInLargeCenter * Board.InLargeCenter( sq ) +
					PSTEndgameInSmallCenter * Board.InSmallCenter( sq ) +
					PSTEndgameLargeCenterAttacks * pstLargeCenterAttacks[sq] +
					PSTEndgameSmallCenterAttacks * pstSmallCenterAttacks[sq] +
					PSTEndgameForwardness * Board.Forwardness( sq );
			}
			for( int sq = Board.NumSquares; sq < Board.NumSquaresExtended; sq++ )
			{
				// TODO: don't hard-code these numbers
				pstMidgame[sq] = 50;
				pstEndgame[sq] = 0;
			}
		}

		public void GetEmptyBoardMobility( Game game, int player, int square, bool[] boardSquares )
		{
			for( int x = 0; x < nMoveCapabilities; x++ )
			{
				MoveCapability move = moveCapabilities[x];
				if( !move.MustCapture && (move.ConditionalBySquare == null || move.ConditionalBySquare[player][square]) )
				{
					int steps = 1;
					int nextSquare = game.Board.NextSquare( game.PlayerDirection( player, move.NDirection ), square );
					while( nextSquare >= 0 && steps <= move.MaxSteps )
					{
						if( steps >= move.MinSteps )
							boardSquares[nextSquare] = true;
						steps++;
						nextSquare = game.Board.NextSquare( game.PlayerDirection( player, move.NDirection ), nextSquare );
					}
				}
			}
		}

		public double CalculateMobilityStatistics( Game game, double density )
		{
			Board board = game.Board;
			double[] mobilityPerSquare = new double[board.NumSquares];
			double[] directionsPerSquare = new double[board.NumSquares];
			double[] safeChecksPerSquare = new double[board.NumSquares];
			for( int square = 0; square < board.NumSquares; square++ )
			{
				int rank = board.GetRank( square );
				int file = board.GetFile( square );
				double mobility = 0.0;
				double directions = 0.0;
				double safeChecks = 0.0;
				for( int x = 0; x < nMoveCapabilities; x++ )
				{
					MoveCapability move = moveCapabilities[x];
					if( !move.MustCapture && (move.ConditionalBySquare == null || move.ConditionalBySquare[0][square]) )
					{
						double directionalMobility = 0.0;
						double currentWeight = 1.0;
						int steps = 1;
						int nextSquare = game.Board.NextSquare( move.NDirection, square );
						if( nextSquare >= 0 )
							directions++;
						while( nextSquare >= 0 && steps <= move.MaxSteps )
						{
							if( steps >= move.MinSteps )
							{
								directionalMobility += currentWeight;
								currentWeight = currentWeight * density;
								//	is this a safe check?
								int currentRank = board.GetRank( nextSquare );
								int currentFile = board.GetFile( nextSquare );
								if( currentFile > file + 1 || currentFile < file - 1 ||
									currentRank > rank + 1 || currentRank < rank - 1 )
									safeChecks++;
							}
							steps++;
							nextSquare = game.Board.NextSquare( move.NDirection, nextSquare );
						}
						mobility += directionalMobility;
					}
				}
				mobilityPerSquare[square] = mobility;
				directionsPerSquare[square] = directions;
				safeChecksPerSquare[square] = safeChecks;
			}
			//	calculate averages
			double totalMobility = 0.0;
			double totalDirections = 0.0;
			double totalSafeChecks = 0.0;
			for( int square = 0; square < board.NumSquares; square++ )
			{
				totalMobility += mobilityPerSquare[square];
				totalDirections += directionsPerSquare[square];
				totalSafeChecks += safeChecksPerSquare[square];
			}
			AverageMobility = totalMobility / board.NumSquares;
			AverageDirectionsAttacked = totalDirections / board.NumSquares;
			AverageSafeChecks = totalSafeChecks / board.NumSquares;
			return AverageMobility;
		}

		public int[] GetMidgamePST()
		{ return pstMidgame; }

		public int[] GetEndgamePST()
		{ return pstEndgame; }

		public int GetMidgamePST( int square )
		{ return pstMidgame[square]; }

		public int GetEndgamePST( int square )
		{ return pstEndgame[square]; }

		public UInt64 GetHashKey( int player, int square )
		{ return HashKeys.Keys[hashKeyIndex[player] + square]; }

		public UInt64 GetPawnHashKey( int player, int square )
		{ return HashKeys.Keys[pawnHashKeyIndex[player] + square]; }

		private void findSquare( int square, int slice )
		{
			SliceLookup[square] = slice;
			for( int x = 0; x < nMoveCapabilities; x++ )
			{
				if( moveCapabilities[x].ConditionalBySquare == null )
				{
					int nextSquare = Game.Board.NextSquare( moveCapabilities[x].NDirection, square );
					if( nextSquare >= 0 && SliceLookup[nextSquare] == -1 )
						findSquare( nextSquare, slice );
				}
			}
		}


		// *** PROTECTED DATA *** //

		#region Protected Data
		protected MoveCapability[] moveCapabilities;
		protected int nMoveCapabilities;
		protected int[] hashKeyIndex;
		protected int[] pawnHashKeyIndex;

		protected int[] pstSmallCenterAttacks;
		protected int[] pstLargeCenterAttacks;

		protected int[] pstMidgame;
		protected int[] pstEndgame;
		#endregion
	}
}
