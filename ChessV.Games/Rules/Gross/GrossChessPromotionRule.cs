
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
using ChessV.Games;

namespace ChessV.Games.Rules.Gross
{
	public class GrossChessPromotionRule: PromoteByReplacementRule
	{
		public GrossChessPromotionRule( PieceType promotingType, OptionalPromotionLocationDelegate conditionDelegate ):
			base( promotingType, conditionDelegate )
		{
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			GrossChess grossChessGame = (GrossChess) game;

			//	add extra captured pieces for promotion reserves
			for( int player = 0; player < 2; player++ )
			{
				game.AddPiece( new Piece( game, player, grossChessGame.Queen, -1 ) );
				game.AddPiece( new Piece( game, player, grossChessGame.Queen, -1 ) );
				for( int x = 0; x < 4; x++ )
				{
					game.AddPiece( new Piece( game, player, grossChessGame.Rook, -1 ) );
					game.AddPiece( new Piece( game, player, grossChessGame.Bishop, -1 ) );
					game.AddPiece( new Piece( game, player, grossChessGame.Knight, -1 ) );
				}
			}
		}

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			GrossChess grossChessGame = (GrossChess) Game;
			if( move.MoveType == MoveType.CaptureReplace ||
				move.MoveType == MoveType.MoveReplace )
			{
				//	if the promotion is on the 10th rank, then it must be to 
				//	Bishop, Knight, Vao, or Wizard
				if( (Board.GetRank( move.ToSquare ) == 9 || Board.GetRank( move.ToSquare ) == 2) &&
					move.PromotionType != grossChessGame.Bishop.TypeNumber &&
					move.PromotionType != grossChessGame.Knight.TypeNumber &&
					move.PromotionType != grossChessGame.Wizard.TypeNumber &&
					move.PromotionType != grossChessGame.Vao.TypeNumber )
					return MoveEventResponse.IllegalMove;
				else
				{
					//	if the promotion is on the 11th rank, then it 
					//	cannot be Archbishop, Marshall, or Queen
					if( (Board.GetRank( move.ToSquare ) == 10 || Board.GetRank( move.ToSquare ) == 1) &&
						(move.PromotionType == grossChessGame.Queen.TypeNumber ||
						 move.PromotionType == grossChessGame.Archbishop.TypeNumber ||
						 move.PromotionType == grossChessGame.Chancellor.TypeNumber) )
						return MoveEventResponse.IllegalMove;
				}
			}
			return base.MoveBeingMade( move, ply );
		}
	}
}
