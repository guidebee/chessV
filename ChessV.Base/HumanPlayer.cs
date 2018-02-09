
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG
  
  THIS FILE DERIVED FROM CUTE CHESS BY ILARI PIHLAJISTO AND ARTO JONSSON

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
	public delegate void WokeUpEventHandler();

	public class HumanPlayer: Player
	{
		// *** CONSTRUCTION *** //

		public HumanPlayer( IDebugMessageLog messageLog, TimerFactory timerFactory ):
			base( messageLog, timerFactory )
		{
			State = PlayerState.Idle;
			Name = "Human";
		}


		// Inherted from Player
		public override void EndGame( Result result )
		{
			//Q_ASSERT(m_bufferMove.isNull());

			base.EndGame( result );
			State = PlayerState.Idle;
		}

		public override void MakeMove( List<Movement> move )
		{
			//Q_ASSERT( m_bufferMove.isNull() );
		}

		public override bool SupportsVariant( string variant )
		{ return true; }

		public override bool  IsHuman
		{ get { return true; } }

		public void OnHumanMove( List<Movement> move, int side )
			/*	Plays move as the human player's move if side is this player's 
				side and the move is legal.  Otherwise it does nothing.  
				If the player is in PlayerState Thinking it plays the move 
				immediately.  If in an Observing state, it saves the move 
				for later, raises the wokeUp event, and plays the move when 
				it gets its turn.  */
		{
			if( side != Side )
				return;

			//Q_ASSERT(m_bufferMove.isNull());
			if( State != PlayerState.Thinking )
			{
				if( State == PlayerState.Observing )
					bufferMove = move;

				WokeUp(); // raise wokeUp event
				return;
			}

			// Q_ASSERT(board()->isLegalMove(tmp));

			emitMove( move );
		}

		public override void Go( Player sender )
		{
			base.Go( sender );

		}

		public override void StopThinking()
		{		
		}


		//	This event is raised when the player receives a user-made move
		//	when in Observing state.  Normally connected to Match.Resume() 
		//	to resume paused game when the user makes a move.
		public WokeUpEventHandler WokeUp;
		
	
		// Inherited from Player
		protected override void startGame()
		{
			//Q_ASSERT( m_bufferMove.isNull() );
		}

		protected override void startThinking()
		{
			if( bufferMove == null )
				return;

			List<Movement> move = bufferMove;
			bufferMove = null;

			// TODO if (board()->isLegalMove(move))
				emitMove( move );
		}


		private List<Movement> bufferMove;
	}
}
