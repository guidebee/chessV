
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
using System.Windows.Forms;

namespace ChessV
{
	public class InternalEngine: Player
	{
		public InternalEngine( IDebugMessageLog messageLog, TimerFactory timerFactory ):
			base( messageLog, timerFactory )
		{
			Name = "ChessV";
			State = PlayerState.Idle;
			moveDelayTimer = timerFactory.NewTimer();
			moveDelayTimer.Interval = 50;
			moveDelayTimer.Tick += onTimerTick;
		}

		public override bool IsHuman
		{ get { return false; } }

		public override bool SupportsVariant( string variant )
		{ return true; }

		public override void MakeMove( List<Movement> move )
		{ }

		public override void StopThinking()
		{
			Game.AbortSearch();
		}

		protected override void startThinking()
		{
			moves = Game.Think( TimeControl );
			moveDelayTimer.Start();
		}

		protected override void startGame()
		{ }

		protected void onTimerTick( object sender, System.EventArgs e )
		{
			moveDelayTimer.Stop();
			emitMove( moves );
		}

		protected Timer moveDelayTimer;
		protected List<Movement> moves;
	}
}
