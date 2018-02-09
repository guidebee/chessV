
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
	public class CheckmateRule: Rule
	{
		// *** PROPERTIES *** //

		public MoveEventResponse StalemateResult { get; set; }

		public PieceType RoyalPieceType { get; private set; }


		// *** CONSTRUCTION ** //

		public CheckmateRule( PieceType royalPieceType )
		{
			RoyalPieceType = royalPieceType;
			StalemateResult = MoveEventResponse.GameDrawn;
		}

		public override void Initialize( Game game )
		{
			royalPieces = new Piece[game.NumPlayers];
			base.Initialize( game );
		}


		// *** OVERRIDES *** //

		public override void PositionLoaded( FEN fen )
		{
			//	scan the board for the royal pieces
			for( int player = 0; player < Game.NumPlayers; player++ )
			{
				List<Piece> piecelist = Game.GetPieceList( player );
				foreach( Piece piece in piecelist )
					if( piece.PieceType == RoyalPieceType )
						royalPieces[player] = piece;
			}
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			//	Assert that this move doesn't capture a royal piece, 
			//	otherwise we have a fundamental problem!
			if( move.PieceCaptured != null &&
				(move.PieceCaptured == royalPieces[0] || move.PieceCaptured == royalPieces[1]) )
				throw new Exception( "Fatal error in CheckmateRule - Royal piece captured" );
			//	Make sure that as a result of this move, the moving player's
			//	royal piece isn't attacked.  If it is, this move is illegal.
			Piece royalPiece = royalPieces[move.Player];
			if( royalPiece != null && Game.IsSquareAttacked( royalPiece.Square, move.Player ^ 1 ) )
				return MoveEventResponse.IllegalMove;
			return MoveEventResponse.NotHandled;
		}

		public override MoveEventResponse NoMovesResult( int currentPlayer, int ply )
		{
			Piece royalPiece = royalPieces[currentPlayer];
			//	No moves - if the royal piece is attacked, the game is lost;
			//	Otherwise, return the StalemateResult
			if( Game.IsSquareAttacked( royalPiece.Square, currentPlayer ^ 1 ) )
				return MoveEventResponse.GameLost;
			return StalemateResult;
		}

		public override int PositionalSearchExtension( int currentPlayer, int ply )
		{
			if( Game.IsSquareAttacked( royalPieces[currentPlayer].Square, currentPlayer ^ 1 ) )
				//	king is in check - extend by one ply
				return Game.ONEPLY;
			return 0;
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected Piece[] royalPieces;
	}
}
