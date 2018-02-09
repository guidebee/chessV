
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
	public class Move50Rule: Rule
	{
		protected int[] gameHistoryCounter;
		protected int[] searchStackCounter;
		protected List<PieceType> types;
		protected int requiredDirection;
		public int HalfMoveCounterThreshold { get; set; }

		public Move50Rule( PieceType pawnType )
		{
			types = new List<PieceType>() { pawnType };
			HalfMoveCounterThreshold = 100;
			requiredDirection = -1;
		}

		public Move50Rule( List<PieceType> pawnTypes )
		{
			types = pawnTypes;
			requiredDirection = -1;
		}

		public void SetRequiredDirection( Direction direction )
		{
			requiredDirection = Game.GetDirectionNumber( direction );
		}

		void MovePlayedHandler( MoveInfo move )
		{
			gameHistoryCounter[Game.GameMoveNumber] = searchStackCounter[1];
			searchStackCounter[0] = searchStackCounter[1];
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			gameHistoryCounter = new int[Game.MAX_GAME_LENGTH];
			searchStackCounter = new int[Game.MAX_DEPTH];
			game.MovePlayed += MovePlayedHandler;
		}

		public override void ClearGameState()
		{
			for( int x = 0; x < Game.MAX_DEPTH; x++ )
				searchStackCounter[x] = 0;
			for( int x = 0; x < Game.MAX_GAME_LENGTH; x++ )
				gameHistoryCounter[x] = 0;
		}

		public override void PositionLoaded( FEN fen )
		{
			try
			{
				searchStackCounter[0] = Convert.ToInt32( fen["half-move clock"] );
				gameHistoryCounter[0] = searchStackCounter[0];
			}
			catch( Exception ex )
			{
				throw new Exceptions.FENParseFailureException( "half-move clock", fen["half-move clock"],
					"Cannot parse the half-move count from FEN", ex );
			}
		}

		public override void SavePositionToFEN( FEN fen )
		{
			fen["half-move clock"] = gameHistoryCounter[Game.GameMoveNumber].ToString();
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			searchStackCounter[ply] = 
				(ply == 1 ? gameHistoryCounter[Game.GameMoveNumber] : searchStackCounter[ply - 1]) + 1;
			bool resetCounter = // we reset the half-move counter if ...
				/* a piece is captured */ move.PieceCaptured != null || 
				/* or move is a promotion */ (move.MoveType & MoveType.PromotionProperty) != null || 
				/* or piece moved is of correct type AND ... */ (move.PieceMoved != null && types.Contains( move.PieceMoved.PieceType ) && 
					/* required move direction is not set OR */ (requiredDirection == -1 || 
					/* move is in required direction */ Game.DirectionLookup( move ) == requiredDirection));
			if( resetCounter )
				searchStackCounter[ply] = 0;
			if( ply == 1 )
				gameHistoryCounter[Game.GameMoveNumber + 1] = searchStackCounter[1];
			return MoveEventResponse.MoveOk;
		}

		public override MoveEventResponse TestForWinLossDraw( int currentPlayer, int ply )
		{
			if( (ply == 1 && gameHistoryCounter[Game.GameMoveNumber] >= HalfMoveCounterThreshold) ||
				searchStackCounter[ply] >= HalfMoveCounterThreshold )
				return MoveEventResponse.GameDrawn;
			return MoveEventResponse.NotHandled;
		}
	}
}
