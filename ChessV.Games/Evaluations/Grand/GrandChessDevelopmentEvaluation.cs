
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
using ChessV.Games.Rules;

namespace ChessV.Evaluations.Grand
{
	public class GrandChessDevelopmentEvaluation: DevelopmentEvaluation
	{
		// *** PROPERTIES *** //

		public int RookPieceType { get; set; }


		// *** INITIALIZATION *** //

		public override void Initialize( Game game )
		{
			base.Initialize( game );
			RookPieceType = -1;
		}

		public override void PostInitialize()
		{
			if( LeftPawnPenaltyFile == -1 )
				LeftPawnPenaltyFile = 0;
			if( RightPawnPenaltyFile == -1 )
				RightPawnPenaltyFile = game.Board.NumFiles - 1;

			base.PostInitialize();

			//	Find the rook type
			if( RookPieceType == -1 )
			{
				for( int nPieceType = 0; nPieceType < game.NPieceTypes; nPieceType++ )
					if( game.GetPieceType( nPieceType ) is Rook )
						RookPieceType = game.GetPieceType( nPieceType ).TypeNumber;
			}
		}


		// *** OVERRIDES *** //

		public override void AdjustEvaluation( ref int midgameEval, ref int endgameEval )
		{
			int phase =
				OpeningProgress >= OpeningCompleteThreshold ? 0 :
				(OpeningProgress < OpeningTransitionThreshold ? (OpeningCompleteThreshold - OpeningTransitionThreshold) :
				(OpeningProgress - OpeningTransitionThreshold));
			if( phase > 0 && RookPieceType >= 0 )
			{
				//	Player 0 - Bonus for keeping the rooks on the bank rank 
				//	and not moving a piece in between them
				int p0bonus = 0;
				BitBoard p0rooks = game.Board.GetPieceTypeBitboard( 0, RookPieceType );
				if( p0rooks.BitCount >= 2 )
				{
					int rook1square = p0rooks.ExtractLSB();
					int rook2square = p0rooks.ExtractLSB();
					if( game.Board.GetRank( rook1square ) == 0 && game.Board.GetRank( rook2square ) == 0 )
					{
						bool blocked = false;
						int file1 = Math.Min( game.Board.GetFile( rook1square ), game.Board.GetFile( rook2square ) );
						int file2 = Math.Max( game.Board.GetFile( rook1square ), game.Board.GetFile( rook2square ) );
						for( int file = file1 + 1; file < file2; file++ )
							if( game.Board[game.Board.LocationToSquare( 0, file )] != null )
								blocked = true;
						if( !blocked )
							p0bonus = 60;
					}
				}
				//	Player 1 - Bonus for keeping the rooks on the bank rank 
				//	and not moving a piece in between them
				int p1bonus = 0;
				BitBoard p1rooks = game.Board.GetPieceTypeBitboard( 1, RookPieceType );
				if( p1rooks.BitCount >= 2 )
				{
					int rook1square = p1rooks.ExtractLSB();
					int rook2square = p1rooks.ExtractLSB();
					if( game.Board.GetRank( rook1square ) == game.Board.NumRanks - 1 && 
						game.Board.GetRank( rook2square ) == game.Board.NumRanks - 1 )
					{
						bool blocked = false;
						int file1 = Math.Min( game.Board.GetFile( rook1square ), game.Board.GetFile( rook2square ) );
						int file2 = Math.Max( game.Board.GetFile( rook1square ), game.Board.GetFile( rook2square ) );
						for( int file = file1 + 1; file < file2; file++ )
							if( game.Board[game.Board.LocationToSquare( game.Board.NumRanks - 1, file )] != null )
								blocked = true;
						if( !blocked )
							p1bonus = 60;
					}
				}

				midgameEval += (EvalAdjustment[0] + p0bonus - EvalAdjustment[1] - p1bonus) *phase / (OpeningCompleteThreshold - OpeningTransitionThreshold);
			}
		}
	}
}
