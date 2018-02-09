
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
	public delegate bool ConditionalLocationDelegate( Location loc );

	public class BasicPromotionRule: Rule
	{
		public BasicPromotionRule
			( PieceType promotingType, 
			  List<PieceType> availablePromotionTypes, 
			  ConditionalLocationDelegate destLocationConditionDelegate,
			  ConditionalLocationDelegate origLocationConditionDelegate = null )
		{ 
			this.promotingType = promotingType; 
			promotionTypes = availablePromotionTypes; 
			destLocationCondition = destLocationConditionDelegate;
			origLocationCondition = origLocationConditionDelegate;
		}

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
				//	check the from-square condition (if any, usually there isn't)
				if( origLocationCondition != null )
				{
					Location fromLocation = Board.SquareToLocation( Board.PlayerSquare( movingPiece.Player, from ) );
					if( !origLocationCondition( fromLocation ) )
						return MoveEventResponse.NotHandled;
				}

				//	check the to-square condition
				Location toLocation = Board.SquareToLocation( Board.PlayerSquare( movingPiece.Player, to ) );
				if( destLocationCondition( toLocation ) )
				{
					//	this is a promotion; add one move for each promotion type
					Piece capturedPiece = Board[to];
					if( capturedPiece == null )
					{
						foreach( PieceType promoteTo in promotionTypes )
						{
							moves.BeginMoveAdd( MoveType.MoveWithPromotion, from, to );
							moves.AddPickup( from );
							moves.AddDrop( movingPiece, to, promoteTo );
							moves.EndMoveAdd( 5000 + promoteTo.MidgameValue );
						}
					}
					else
					{
						foreach( PieceType promoteTo in promotionTypes )
						{
							moves.BeginMoveAdd( MoveType.CaptureWithPromotion, from, to );
							moves.AddPickup( from );
							moves.AddPickup( to );
							moves.AddDrop( movingPiece, to, promoteTo );
							moves.EndMoveAdd( 5000 + promoteTo.MidgameValue + capturedPiece.PieceType.MidgameValue );
						}
					}
					return MoveEventResponse.Handled;
				}
			}
			return MoveEventResponse.NotHandled;
		}

		protected PieceType promotingType;
		protected int promotingTypeNumber;
		protected List<PieceType> promotionTypes;
		protected ConditionalLocationDelegate origLocationCondition;
		protected ConditionalLocationDelegate destLocationCondition;
	}
}
