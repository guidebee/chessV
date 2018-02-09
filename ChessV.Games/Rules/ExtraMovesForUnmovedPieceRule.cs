
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
	public class ExtraMovesForUnmovedPieceRule: Rule
	{
		public const int MAX_EXTRA_MOVES = 16;

		// *** PROPERTIES *** //

		public PieceType PieceType { get; private set; }
		public string FENSegmentName { get; private set; }


		// *** CONSTRUCTION *** //

		public ExtraMovesForUnmovedPieceRule( PieceType pieceType, string fenSegmentName )
		{
			PieceType = pieceType;
			FENSegmentName = fenSegmentName;
			extraMoves = new MoveCapability[MAX_EXTRA_MOVES];
			privLookup = new Dictionary<char, int>();
			privToSquareLookup = new Dictionary<int, int>();
		}


		// *** INITIALIZATION *** //

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			searchStackPrivs = new int[Game.MAX_DEPTH];
			gameHistoryPrivs = new int[Game.MAX_GAME_LENGTH];
			allPrivsPerPlayer = new int[game.NumPlayers];
			allPrivString = "";
			nextPriv = 1;
	
			//	Initialize privEraseMap
			privEraseMap = new int[Board.NumSquaresExtended];
			for( int x = 0; x < Board.NumSquaresExtended; x++ )
				privEraseMap[x] = -1;

			//	Hook up MovePlayed event handler
			Game.MovePlayed += MovePlayedHandler;

			//	Find all the starting locations of the pieces of this 
			//	type and record the required information so that we can 
			//	track when they move and record the extra-move-rights
			var pieces = Game.StartingPieces;
			foreach( var pair in pieces )
			{
				if( pair.Value != null && pair.Value.PieceType == PieceType )
				{
					int player = pair.Value.Player;
					int square = Board.DefaultNotationToSquare( pair.Key );
					int file = Board.SquareToLocation( square ).File;
					string fileNotation = Board.GetFileNotation( file );
					if( fileNotation.Length > 1 )
						throw new Exception( "ExtraMovesForUnmovedPieceRule exception: File notation must be single character" );
					int priv = nextPriv;
					nextPriv = nextPriv << 1;
					privEraseMap[square] = -1 & ~priv;
					char privChar = player == 0 ? fileNotation.ToUpper()[0] : fileNotation.ToLower()[0];
					if( privLookup.ContainsKey( privChar ) )
						throw new Exception( "ExtraMovesForUnmovedPieceRule exception: multiple pieces on the same file not supported" );
					privLookup.Add( privChar, priv );
					privToSquareLookup.Add( priv, square );
					allPrivsPerPlayer[player] |= priv;
					allPrivString = allPrivString + privChar;
				}
			}
		}

		public override void PostInitialize()
		{
			base.PostInitialize();

			//	Initialize direction numbers - the directions were not assigned 
			//	numbers previously, so we must do this now.  
			for( int x = 0; x < nExtraMoves; x++ )
				extraMoves[x].NDirection = Game.GetDirectionNumber( extraMoves[x].Direction );
		}

		public void AddMove( MoveCapability move )
		{
			extraMoves[nExtraMoves++] = move;
		}


		// *** EVENT HANDLERS *** //

		public override void SetDefaultsInFEN( FEN fen )
		{
			if( fen[FENSegmentName] == "#default" )
				fen[FENSegmentName] = allPrivString;
		}

		public override void PositionLoaded( FEN fen )
		{
			searchStackPrivs[0] = 0;
			foreach( char c in fen[FENSegmentName] )
				if( privLookup.ContainsKey( c ) )
					searchStackPrivs[0] |= privLookup[c];
				else
					throw new Exceptions.FENParseFailureException( FENSegmentName, fen[FENSegmentName],
						"Invalid character in FEN " + FENSegmentName + " privileges: " + c );
			gameHistoryPrivs[Game.GameMoveNumber] = searchStackPrivs[0];
		}

		public void MovePlayedHandler( MoveInfo move )
		{
			gameHistoryPrivs[Game.GameMoveNumber] = searchStackPrivs[1];
			searchStackPrivs[0] = searchStackPrivs[1];
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			searchStackPrivs[ply] =
				(ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber] : searchStackPrivs[ply - 1]) &
				privEraseMap[move.FromSquare];
			return MoveEventResponse.MoveOk;
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			UInt64 privs = (UInt64) ((ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber] : searchStackPrivs[ply - 1]) & allPrivsPerPlayer[Game.CurrentSide]);
			while( privs != 0 )
			{
				//	extract one priv bit from privs
				int priv = 1 << index64[(int) ((((UInt64) privs & (UInt64) (-((Int64) privs))) * debruijn64) >> 58)];
				privs = privs & (UInt64) ((Int64) privs - 1);
				//	find the piece associated with this priv
				int square = privToSquareLookup[priv];
				Piece piece = Board[square];
				//	generate moves
				for( int x = 0; x < nExtraMoves; x++ )
					piece.GenerateMovesForCapability( Game.SimpleMoveGeneration, ref extraMoves[x], list, capturesOnly );
			}
		}


		// *** INTERNAL DATA *** //

		protected MoveCapability[] extraMoves;
		protected int nExtraMoves;
		protected int[] searchStackPrivs;
		protected int[] gameHistoryPrivs;
		protected int[] allPrivsPerPlayer;
		protected Dictionary<char, int> privLookup;
		protected Dictionary<int, int> privToSquareLookup;
		protected int nextPriv;
		protected int[] privEraseMap;
		protected string allPrivString;

		private static int[] index64 = {
			63,  0, 58,  1, 59, 47, 53,  2,
			60, 39, 48, 27, 54, 33, 42,  3,
			61, 51, 37, 40, 49, 18, 28, 20,
			55, 30, 34, 11, 43, 14, 22,  4,
			62, 57, 46, 52, 38, 26, 32, 41,
			50, 36, 17, 19, 29, 10, 13, 21,
			56, 45, 25, 31, 35, 16,  9, 12,
			44, 24, 15,  8, 23,  7,  6,  5
		};

		private const UInt64 debruijn64 = 0x07EDD5E59A4E28C2UL;
	}
}
