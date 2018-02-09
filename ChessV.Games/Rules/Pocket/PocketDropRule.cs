
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

namespace ChessV.Games.Rules.Pocket
{
	public class PocketDropRule: Rule
	{
		protected int[] pocketSquares;

		public PocketDropRule()
		{
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			pocketSquares = new int[game.NumPlayers];
			for( int player = 0; player < game.NumPlayers; player++ )
				pocketSquares[player] = Board.LocationToSquare( new Location( player, -1 ) );
		}

		public override void PositionLoaded( FEN fen )
		{
			foreach( char c in fen["pieces in hand"] )
			{
				if( c != '-' && c != '@' )
				{
					PieceType type = Game.GetTypeByNotation( c.ToString() );
					int player = Char.IsUpper( c ) ? 0 : 1;
					Location loc = new Location( player, -1 );
					Piece piece = new Piece( Game, player, type, loc );
					Board.Game.AddPiece( piece );
				}
			}
		}

		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( !capturesOnly )
			{
				int pocketSquare = pocketSquares[Game.CurrentSide];
				Piece pieceInPocket = Board[pocketSquare];
				if( pieceInPocket != null )
				{
					for( int square = 0; square < Board.NumSquares; square++ )
					{
						if( Board[square] == null )
						{
							list.BeginMoveAdd( MoveType.Drop, pocketSquare, square );
							Piece piece = list.AddPickup( pocketSquare );
							list.AddDrop( piece, square, pieceInPocket.PieceType );
							list.EndMoveAdd( piece.PieceType.GetMidgamePST( square ) - 10 );
						}
					}
				}
			}
		}
	}
}
