
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

namespace ChessV.Games.Rules.MultiKing
{
	public class RelativeRoyaltyCheckmateRule: Rule
	{
		// *** PROPERTIES *** //

		public MoveEventResponse StalemateResult { get; set; }

		public PieceType RoyalPieceType { get; private set; }


		// *** CONSTRUCTION ** //

		public RelativeRoyaltyCheckmateRule( PieceType royalPieceType )
		{
			RoyalPieceType = royalPieceType;
			StalemateResult = MoveEventResponse.GameDrawn;
		}


		// *** OVERRIDES *** //

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			//	Find the royal piece for the current player
			int royalSquare = FindRoyalPieceSquare( move.Player );
			//	Make sure that as a result of this move, the moving player's
			//	royal piece isn't attacked.  If it is, this move is illegal.
			if( Game.IsSquareAttacked( royalSquare, move.Player ^ 1 ) )
				return MoveEventResponse.IllegalMove;
			return MoveEventResponse.NotHandled;
		}

		public override MoveEventResponse NoMovesResult( int currentPlayer, int ply )
		{
			//	Find the royal piece for the current player
			int royalSquare = FindRoyalPieceSquare( currentPlayer );
			//	No moves - if the royal piece is attacked, the game is lost;
			//	Otherwise, return the StalemateResult
			if( Game.IsSquareAttacked( royalSquare, currentPlayer ^ 1 ) )
				return MoveEventResponse.GameLost;
			return StalemateResult;
		}

		public override int PositionalSearchExtension( int currentPlayer, int ply )
		{
			int royalSquare = FindRoyalPieceSquare( currentPlayer );
			if( Game.IsSquareAttacked( royalSquare, currentPlayer ^ 1 ) )
				//	king is in check - extend by one ply
				return Game.ONEPLY;
			return 0;
		}


		// *** HELPER FUNCTIONS *** //

		protected int FindRoyalPieceSquare( int player )
		{
			BitBoard kings = Board.GetPieceTypeBitboard( player, RoyalPieceType.TypeNumber );
			int royalSquare = kings.ExtractLSB();
			int royalSquareRelative = Board.PlayerSquare( player, royalSquare );
			while( kings )
			{
				int square = kings.ExtractLSB();
				if( Board.PlayerSquare( player, square ) < royalSquareRelative )
				{
					royalSquare = square;
					royalSquareRelative = Board.PlayerSquare( player, square );
				}
			}
			return royalSquare;
		}
	}
}
