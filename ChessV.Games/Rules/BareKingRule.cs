
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
	//**********************************************************************
	//
	//                         BareKingRule
	//
	//    This implements the Shatranj "Bare King" rule.  A player loses 
	//    if he is down to only a king, unless he is able to bare the 
	//    opponent's king on the very next move (in which case it's a draw)

	public class BareKingRule: Rule
	{
		// *** OVERRIDES *** //

		public override MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{
			if( Board.GetPlayerMaterial( move.Player ) == 0 )
			{
				//	The moving player has only a King.  Therefore, if the opponent 
				//	has more than just a King, the game is over unless this move 
				//	captures his last piece (in which case it's a draw.) 
				if( move.PieceCaptured == null ||
					Board.GetPlayerMaterial( move.PieceCaptured.Player ) > 0 )
					//	move is illegal since the moving player has a bare king and this 
					//	move doesn't bare the opponent's king
					return MoveEventResponse.IllegalMove;
			}
			return MoveEventResponse.NotHandled;
		}

		public override MoveEventResponse NoMovesResult( int currentPlayer, int ply )
		{
			if( Board.GetPlayerMaterial( 0 ) == 0 && Board.GetPlayerMaterial( 1 ) == 0 )
				return MoveEventResponse.GameDrawn;
			if( Board.GetPlayerMaterial( currentPlayer ) == 0 )
				return MoveEventResponse.GameLost;
			return MoveEventResponse.NotHandled;
		}

		public override MoveEventResponse TestForWinLossDraw( int currentPlayer, int ply )
		{
			//	Only one thing needs to be changed here.  If there are only 
			//	the two kings left, then the game is a draw.  If we didn't handle  
			//	this here, the bare king rules is in effect, then the 
			//	bare king of the player to move would be considered stalemated, 
			//	because the MoveBeingMade() handler above would rule them all illegal
			if( Board.GetPlayerMaterial( 0 ) == 0 && Board.GetPlayerMaterial( 1 ) == 0 )
				return MoveEventResponse.GameDrawn;
			return MoveEventResponse.NotHandled;
		}
	}
}
