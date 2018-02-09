
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
	public class PromotionCapability
	{
		public int PromotingTypeNumber;
		public PieceType PromotingType;
		public List<PieceType> PromotionTypes;
		public List<PieceType> ReplacementPromotionTypes;
		public OptionalPromotionLocationDelegate ConditionDelegate;
	}

	public class ComplexPromotionRule: Rule
	{
		public ComplexPromotionRule()
		{
			promotionCapabilities = new List<PromotionCapability>();
			typesUsed = new bool[Game.MAX_PIECE_TYPES];
		}

		public void AddPromotionCapability
			( PieceType promotingType,
			  List<PieceType> promotionTypes,
			  List<PieceType> replacementPromotionTypes,
			  OptionalPromotionLocationDelegate conditionDeletage )
		{
			PromotionCapability newCapability = new PromotionCapability();
			newCapability.PromotingType = promotingType;
			newCapability.PromotionTypes = promotionTypes;
			newCapability.ReplacementPromotionTypes = replacementPromotionTypes;
			newCapability.ConditionDelegate = conditionDeletage;
			promotionCapabilities.Add( newCapability );
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			foreach( PromotionCapability promotion in promotionCapabilities )
				promotion.PromotingTypeNumber = game.GetPieceTypeNumber( promotion.PromotingType );
		}

		public override MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{
			Piece movingPiece = Board[from];
			foreach( PromotionCapability promotion in promotionCapabilities )
			{
				if( movingPiece.TypeNumber == promotion.PromotingTypeNumber )
				{
					Location loc = Board.SquareToLocation( Board.PlayerSquare( movingPiece.Player, to ) );
					PromotionOption option = promotion.ConditionDelegate( loc );
					if( option != PromotionOption.CannotPromote )
					{
						for( int x = 0; x < Game.MAX_PIECE_TYPES; x++ )
							typesUsed[x] = false;

						//	enemy piece being captured (if any)
						Piece capturedPiece = Board[to];

						//	if promotion is optional, add move without promotion
						if( option == PromotionOption.CanPromote )
						{
							if( capturedPiece == null )
								moves.AddMove( from, to, true );
							else
								moves.AddCapture( from, to, true );
						}

						//	handle traditional promotion
						if( promotion.PromotionTypes != null )
						{
							if( capturedPiece == null )
							{
								foreach( PieceType promoteTo in promotion.PromotionTypes )
								{
									moves.BeginMoveAdd( MoveType.MoveWithPromotion, from, to );
									moves.AddPickup( from );
									moves.AddDrop( movingPiece, to, promoteTo );
									moves.EndMoveAdd( 5000 + promoteTo.MidgameValue );
									typesUsed[promoteTo.TypeNumber] = true;
								}
							}
							else
							{
								foreach( PieceType promoteTo in promotion.PromotionTypes )
								{
									moves.BeginMoveAdd( MoveType.CaptureWithPromotion, from, to );
									moves.AddPickup( from );
									moves.AddPickup( to );
									moves.AddDrop( movingPiece, to, promoteTo );
									moves.EndMoveAdd( 5000 + promoteTo.MidgameValue + capturedPiece.PieceType.MidgameValue );
									typesUsed[promoteTo.TypeNumber] = true;
								}
							}
						}

						//	handle promotion by replacement
						if( promotion.ReplacementPromotionTypes != null )
						{
							List<Piece> capturedPieces = Game.GetCapturedPieceList( movingPiece.Player );
							foreach( Piece capturedFriendlyPiece in capturedPieces )
							{
								if( capturedFriendlyPiece.TypeNumber != movingPiece.TypeNumber &&
									!typesUsed[capturedFriendlyPiece.TypeNumber] && 
									promotion.ReplacementPromotionTypes.Contains( capturedFriendlyPiece.PieceType ) )
								{
									if( capturedPiece == null )
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
										moves.EndMoveAdd( 5000 + capturedFriendlyPiece.PieceType.MidgameValue + capturedPiece.PieceType.MidgameValue );
									}
									typesUsed[capturedFriendlyPiece.TypeNumber] = true;
								}
							}
						}
						return MoveEventResponse.Handled;
					}
				}
			}
			return MoveEventResponse.NotHandled;
		}

		private List<PromotionCapability> promotionCapabilities;
		private bool[] typesUsed;
	}
}
