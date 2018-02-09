
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
	public class FlexibleCastlingRule: CastlingRule
	{
		// *** CONSTRUCTION *** //

		public FlexibleCastlingRule() { }
		public FlexibleCastlingRule( CastlingRule templateRule ) : base( templateRule ) { }


		public override void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{
			if( !capturesOnly )
			{
				int castlingPriv = ply == 1 ? gameHistoryPrivs[Game.GameMoveNumber] : searchStackPrivs[ply - 1];
				for( int x = 0; x < nCastlingMoves[Game.CurrentSide]; x++ )
				{
					if( (castlingMoves[Game.CurrentSide, x].RequiredPriv & castlingPriv) != 0 )
					{
						if( castlingMoves[Game.CurrentSide, x].KingFromSquare < castlingMoves[Game.CurrentSide, x].KingToSquare )
						{
							bool squaresEmpty = true;
							for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare ) + 1;
								squaresEmpty && (file <= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ) ||
								file <= Board.GetFile( castlingMoves[Game.CurrentSide, x].OtherFromSquare )); file++ )
							{
								int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
								if( sq != castlingMoves[Game.CurrentSide, x].OtherFromSquare && Board[sq] != null )
									squaresEmpty = false;
							}
							if( squaresEmpty )
							{
								bool squaresAttacked = false;
								for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									!squaresAttacked && file <= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ); file++ )
								{
									int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									if( Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 ) )
										squaresAttacked = true;
								}
								if( !squaresAttacked )
								{
									list.BeginMoveAdd( MoveType.Castling, castlingMoves[Game.CurrentSide, x].KingFromSquare,
										castlingMoves[Game.CurrentSide, x].KingToSquare );
									Piece king = list.AddPickup( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									Piece other = list.AddPickup( castlingMoves[Game.CurrentSide, x].OtherFromSquare );
									list.AddDrop( king, castlingMoves[Game.CurrentSide, x].KingToSquare, null );
									list.AddDrop( other, castlingMoves[Game.CurrentSide, x].KingToSquare - Board.NumRanks, null );
									list.EndMoveAdd( 1000 );

									for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ) + 1;
										!squaresAttacked && file < Board.GetFile( castlingMoves[Game.CurrentSide, x].OtherFromSquare ) + 
										castlingMoves[Game.CurrentSide, x].OtherToSquare; file++ )
									{
										int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										squaresAttacked = Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 );
										if( !squaresAttacked )
										{
											list.BeginMoveAdd( MoveType.Castling, castlingMoves[Game.CurrentSide, x].KingFromSquare, sq );
											king = list.AddPickup( castlingMoves[Game.CurrentSide, x].KingFromSquare );
											other = list.AddPickup( castlingMoves[Game.CurrentSide, x].OtherFromSquare );
											list.AddDrop( king, sq, null );
											list.AddDrop( other, sq - Board.NumRanks, null );
											list.EndMoveAdd( 1000 );
										}
									}
								}
							}
						}
						else
						{
							bool squaresEmpty = true;
							for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare ) - 1;
								squaresEmpty && (file >= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ) ||
								file >= Board.GetFile( castlingMoves[Game.CurrentSide, x].OtherFromSquare )); file-- )
							{
								int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
								if( sq != castlingMoves[Game.CurrentSide, x].OtherFromSquare && Board[sq] != null )
									squaresEmpty = false;
							}
							if( squaresEmpty )
							{
								bool squaresAttacked = false;
								for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									!squaresAttacked && file >= Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ); file-- )
								{
									int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									if( Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 ) )
										squaresAttacked = true;
								}
								if( !squaresAttacked )
								{
									list.BeginMoveAdd( MoveType.Castling, castlingMoves[Game.CurrentSide, x].KingFromSquare,
										castlingMoves[Game.CurrentSide, x].KingToSquare );
									Piece king = list.AddPickup( castlingMoves[Game.CurrentSide, x].KingFromSquare );
									Piece other = list.AddPickup( castlingMoves[Game.CurrentSide, x].OtherFromSquare );
									list.AddDrop( king, castlingMoves[Game.CurrentSide, x].KingToSquare, null );
									list.AddDrop( other, castlingMoves[Game.CurrentSide, x].KingToSquare + Board.NumRanks, null );
									list.EndMoveAdd( 1000 );

									for( int file = Board.GetFile( castlingMoves[Game.CurrentSide, x].KingToSquare ) - 1;
										!squaresAttacked && file > Board.GetFile( castlingMoves[Game.CurrentSide, x].OtherFromSquare ) -
										castlingMoves[Game.CurrentSide, x].OtherToSquare; file-- )
									{
										int sq = file * Board.NumRanks + Board.GetRank( castlingMoves[Game.CurrentSide, x].KingFromSquare );
										squaresAttacked = Game.IsSquareAttacked( sq, Game.CurrentSide ^ 1 );
										if( !squaresAttacked )
										{
											list.BeginMoveAdd( MoveType.Castling, castlingMoves[Game.CurrentSide, x].KingFromSquare, sq );
											king = list.AddPickup( castlingMoves[Game.CurrentSide, x].KingFromSquare );
											other = list.AddPickup( castlingMoves[Game.CurrentSide, x].OtherFromSquare );
											list.AddDrop( king, sq, null );
											list.AddDrop( other, sq + Board.NumRanks, null );
											list.EndMoveAdd( 1000 );
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
