
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

namespace ChessV.Games.Pieces.Diamond
{
	class DiamondPawn: PieceType
	{
		public DiamondPawn( string name, string notation, int midgameValue, int endgameValue ) :
			base( "Diamond Pawn", name, notation, midgameValue, endgameValue )
		{
			AddMoves( this );

			//	the notion of "forwardness" isn't quite right for 
			//	this game.  will need a way to customize PSTs.
			PSTMidgameForwardness = 25;
			PSTEndgameForwardness = 35;
		}

		public static new void AddMoves( PieceType type )
		{
			type.StepMoveOnly( new Direction( 1, -1 ) );
			type.StepCaptureOnly( new Direction( 1, 0 ) );
			type.StepCaptureOnly( new Direction( 0, -1 ) );
		}
	}
}
