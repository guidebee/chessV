
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

namespace ChessV.Evaluations
{
	public class ColorbindingEvaluation: Evaluation
	{
		// *** CONSTRUCTION *** //

		public ColorbindingEvaluation()
		{
		}


		// *** INITIALIZATION *** //

		public override void Initialize( Game game )
		{
			//	for now, we only support colorbound pieces that 
			//	see exactly half the board (two slices)
			numSlices = 2;
			slicesFound = new bool[2];

			//	go through all piece types and find those that 
			//	have two slices and add them to the list
			PieceType[] pieceTypes;
			int nPieceTypes = game.GetPieceTypes( out pieceTypes );
			colorboundPieceTypes = new List<PieceType>();
			for( int x = 0; x < nPieceTypes; x++ )
				if( pieceTypes[x].NumSlices == 2 )
					colorboundPieceTypes.Add( pieceTypes[x] );
		}


		// *** OVERRIDES *** //

		public override void AdjustEvaluation( ref int midgameEval, ref int endgameEval )
		{
			//	check for player 0
			slicesFound[0] = false;
			slicesFound[1] = false;
			foreach( PieceType pieceType in colorboundPieceTypes )
			{
				BitBoard pieces = board.GetPieceTypeBitboard( 0, pieceType.TypeNumber );
				while( pieces )
				{
					int square = pieces.ExtractLSB();
					slicesFound[board[square].PieceType.SliceLookup[square]] = true;
				}
			}
			if( slicesFound[0] && slicesFound[1] )
			{
				midgameEval += 50;
				endgameEval += 50;
			}
			//	check for player 1
			slicesFound[0] = false;
			slicesFound[1] = false;
			foreach( PieceType pieceType in colorboundPieceTypes )
			{
				BitBoard pieces = board.GetPieceTypeBitboard( 1, pieceType.TypeNumber );
				while( pieces )
				{
					int square = pieces.ExtractLSB();
					slicesFound[board[square].PieceType.SliceLookup[square]] = true;
				}
			}
			if( slicesFound[0] && slicesFound[1] )
			{
				midgameEval -= 50;
				endgameEval -= 50;
			}
		}


		// *** PROTECTED DATA *** //

		protected List<PieceType> colorboundPieceTypes;
		protected int numSlices;
		protected bool[] slicesFound;
	}
}
