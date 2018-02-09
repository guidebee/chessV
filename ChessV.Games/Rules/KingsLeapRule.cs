
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
	public class KingsLeapRule: Rule
	{
		// *** CONSTRUCTION *** //

		public KingsLeapRule( int king1square, int king2square )
		{
			privChar = new char[] { 'K', 'k' };
			kingSquare = new int[] { king1square, king2square };
			directions = new List<int>();
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			hashKeyIndex = Game.HashKeys.TakeKeys( 4 );
			gameHistory = new int[Game.MAX_GAME_LENGTH];
			privs = new int[Game.MAX_DEPTH];
			game.MovePlayed += MovePlayedHandler;
		}

		public override void ClearGameState()
		{
			for( int x = 0; x < Game.MAX_DEPTH; x++ )
				privs[x] = 0;
			for( int x = 0; x < Game.MAX_GAME_LENGTH; x++ )
				gameHistory[x] = 0;
		}


		// *** OVERRIDES *** //

		public override void PostInitialize()
		{
			Direction[] allDirections = new Direction[Game.NDirections];
			int ndirections = Game.GetDirections( out allDirections );
			for( int x = 0; x < ndirections; x++ )
			{
				Direction dir = allDirections[x];
				if( dir.FileOffset ==  0 && dir.RankOffset ==  2 || 
					dir.FileOffset ==  0 && dir.RankOffset == -2 || 
					dir.FileOffset ==  2 && dir.RankOffset ==  0 || 
					dir.FileOffset == -2 && dir.RankOffset ==  0 )
 					directions.Add( x );
			}
		}

		public override ulong GetPositionHashCode( int ply )
		{
			int priv = ply == 1 ? gameHistory[Game.GameMoveNumber + 1] : privs[ply - 1];
			return HashKeys.Keys[hashKeyIndex + priv];
			
		}

		void MovePlayedHandler( MoveInfo move )
		{
			gameHistory[Game.GameMoveNumber] = privs[1];
			privs[0] = privs[1];
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( !capturesOnly )
			{
				int priv = ply == 1 ? gameHistory[Game.GameMoveNumber] : privs[ply - 1];
				if( Game.CurrentSide == 0 && (priv & 1) == 1 )
				{
					foreach( int direction in directions )
					{
						int square = Board.NextSquare( direction, kingSquare[0] );
						if( square >= 0 && Board[square] == null )
						{
							list.BeginMoveAdd( MoveType.StandardMove, kingSquare[0], square );
							Piece king = list.AddPickup( kingSquare[0] );
							list.AddDrop( king, square );
							list.EndMoveAdd( 500 );
						}
					}
				}
				else if( Game.CurrentSide == 1 && (priv & 2) == 2 )
				{
					foreach( int direction in directions )
					{
						int square = Board.NextSquare( direction, kingSquare[1] );
						if( square >= 0 && Board[square] == null )
						{
							list.BeginMoveAdd( MoveType.StandardMove, kingSquare[1], square );
							Piece king = list.AddPickup( kingSquare[1] );
							list.AddDrop( king, square );
							list.EndMoveAdd( 500 );
						}
					}
				}
			}
		}

		public override void PositionLoaded( FEN fen )
		{
			privs[0] = 0;
			foreach( char c in fen["kings-leap"] )
				if( c == 'K' )
					privs[0] |= 1;
				else if( c == 'k' )
					privs[0] |= 2;
				else
					throw new Exception( "Unrecognized character in FEN kings-leap section - " + c );
			gameHistory[Game.GameMoveNumber] = privs[0];
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			privs[ply] = (ply == 1 ? gameHistory[Game.GameMoveNumber] : privs[ply - 1]);
			if( ply == 1 )
				gameHistory[Game.GameMoveNumber + 1] = privs[1];
			if( move.FromSquare == kingSquare[0] )
				privs[ply] = privs[ply] & 2;
			else if( move.FromSquare == kingSquare[1] )
				privs[ply] = privs[ply] & 1;
			return MoveEventResponse.MoveOk;
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected char[] privChar;
		protected int[] kingSquare;
		protected int[] privs;
		protected int[] gameHistory;
		protected List<int> directions;
		protected int hashKeyIndex;
	}
}
