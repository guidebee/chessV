
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

namespace ChessV
{
	public class Rule

		/*  The Rule class encapsulates a (potentially) configurable "rule" that can be 
		    plugged into a Game.  By separating the rules for a game into these plugable 
		    modules, it makes it much easier to define new games.  Most Chess variants 
		    are built with combinations of the same basic rules.  And, if a Game has a 
		    brand new rule, implementing it in this way allows it to be reused in new 
		    games.  This is why the architecture is designed such that the Game class 
		    doesn't have the ability to override move generation directly.  */

	{
		// *** PROPERTIES *** //

		public Board Board { get; protected set; }
		public Game Game { get; protected set; }


		// *** CONSTRUCTION *** //

		public Rule()
		{ }


		// *** OVERRIDABLE VIRTUAL FUNCTIONS *** //

		public virtual void Initialize( Game game )
		{ this.Game = game; this.Board = game.Board; }

		public virtual void PostInitialize()
		{ }

		public virtual void ClearGameState()
		{ }

		public virtual void PositionLoaded( FEN fen )
		{ }

		public virtual void SetDefaultsInFEN( FEN fen )
		{ }

		public virtual void SavePositionToFEN( FEN fen )
		{ }

		public virtual UInt64 GetPositionHashCode( int ply )
		{ return 0; }

		public virtual MoveEventResponse MoveBeingGenerated( MoveList moves, int from, int to, MoveType type )
		{ return MoveEventResponse.NotHandled; }

		public virtual MoveEventResponse MoveBeingMade( MoveInfo move, int ply )
		{ return MoveEventResponse.NotHandled; }

		public virtual MoveEventResponse MoveMade( MoveInfo move, int ply )
		{ return MoveEventResponse.NotHandled; }

		public virtual MoveEventResponse MoveBeingUnmade( MoveInfo move, int ply )
		{ return MoveEventResponse.NotHandled; }

		public virtual bool IsSquareAttacked( int square, int side )
		{ return false; }

		public virtual MoveEventResponse TestForWinLossDraw( int currentPlayer, int ply )
		{ return MoveEventResponse.NotHandled; }

		public virtual MoveEventResponse NoMovesResult( int currentPlayer, int ply )
		{ return MoveEventResponse.NotHandled; }

		public virtual void GenerateSpecialMoves( MoveList list, bool capturesOnly, int ply )
		{ }

		public virtual MoveEventResponse DescribeMove( MoveInfo move, MoveNotation format, ref string description )
		{ return MoveEventResponse.NotHandled; }

		public virtual int PositionalSearchExtension( int currentPlayer, int ply )
		{ return 0; }
	}
}
