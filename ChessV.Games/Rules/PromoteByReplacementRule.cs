
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
	public class PromoteByReplacementRule: Rule
	{
		protected PieceType promotingType;
		protected int promotingTypeNumber;
		protected OptionalPromotionLocationDelegate condition;

		public PromoteByReplacementRule( PieceType promotingType, OptionalPromotionLocationDelegate conditionDelegate )
		{ this.promotingType = promotingType; condition = conditionDelegate; }

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			promotingTypeNumber = game.GetPieceTypeNumber( promotingType );
		}

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			Piece movingPiece = Board[from];
			if( movingPiece.TypeNumber == promotingTypeNumber )
			{
				Location loc = Board.SquareToLocation( Board.PlayerSquare( movingPiece.Player, to ) );
				PromotionOption option = condition( loc );
				if( option != PromotionOption.CannotPromote )
				{
					//	enemy piece being captured (if any)
					Piece capturedEnemyPiece = Board[to];

					//	if promotion is optional, add move without promotion
					if( option == PromotionOption.CanPromote )
					{
						if( capturedEnemyPiece == null )
							moves.AddMove( from, to, true );
						else
							moves.AddCapture( from, to, true );
					}

					List<int> pieceTypesFound = new List<int>();
					List<Piece> capturedPieces = Game.GetCapturedPieceList( movingPiece.Player );
					foreach( Piece capturedFriendlyPiece in capturedPieces )
					{
						if( capturedFriendlyPiece.TypeNumber != movingPiece.TypeNumber &&
							!pieceTypesFound.Contains( capturedFriendlyPiece.TypeNumber ) )
						{
							if( capturedEnemyPiece == null )
							{
								moves.BeginMoveAdd( MoveType.MoveReplace, from, to, capturedFriendlyPiece.TypeNumber );
								moves.AddPickup( from );
								moves.AddDrop( capturedFriendlyPiece, to );
								moves.EndMoveAdd( 5000 + capturedFriendlyPiece.PieceType.MidgameValue );
							}
							else
							{
								moves.BeginMoveAdd( MoveType.CaptureReplace, from, to, capturedFriendlyPiece.TypeNumber );
								moves.AddPickup( from );
								moves.AddPickup( to );
								moves.AddDrop( capturedFriendlyPiece, to );
								moves.EndMoveAdd( 5000 + capturedFriendlyPiece.PieceType.MidgameValue + capturedEnemyPiece.PieceType.MidgameValue );
							}
							pieceTypesFound.Add( capturedFriendlyPiece.TypeNumber );
						}
					}
					return MoveEventResponse.Handled;
				}
			}
			return MoveEventResponse.NotHandled;
		}
	}
}
