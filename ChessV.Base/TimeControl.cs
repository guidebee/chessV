
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
	public class TimeControl
	{
		// *** CONSTRUCTION *** //

		//	Creates a new time control with invalid default settings
		public TimeControl()
		{
			Infinite = false;
			MovesPerTC = 0;
			TimePerTC = 0;
			TimeIncrement = 0;
			TimePerMove = 0;
			TimeLeft = 0;
			MovesLeft = 0;
			PlyLimit = 0;
			NodeLimit = 0;
			ExpiryMargin = 0;
			lastMoveTime = 0;
			expired = false;
		}

		//	Creates a new TimeControl from a string
		//	
		//	str must either be "inf" for infinite time, or it can use
		//	the format: movesPerTc/timePerTc+timeIncrement
		//	 - timePerTc is time in seconds if it's a single value.
		//	   It can also use the form minutes:seconds.
		//	 - if movesPerTc is 0, it should be left out, and the slash
		//	   character isn't needed.
		//	 - timeIncrement is the time increment per move in seconds.
		//	   If it's 0, it should be left out along with the plus sign.
		//
		//	Example 1 (40 moves in 120 seconds):
		//	 TimeControl("40/120");
		//
		//	Example 2 (same as example 1, 40 moves in 2 minutes):
		//	 TimeControl("40/2:0");
		//	
		//	Example 3 (whole game in 2.5 minutes plus 5 sec increment):
		//	 TimeControl("2:30+5");
		//	
		//	Example 4 (infinite thinking time):
		//	 TimeControl("inf");
		public TimeControl( string str )
		{
			Infinite = false;
			MovesPerTC = 0;
			TimePerTC = 0;
			TimeIncrement = 0;
			TimePerMove = 0;
			TimeLeft = 0;
			MovesLeft = 0;
			PlyLimit = 0;
			NodeLimit = 0;
			ExpiryMargin = 0;
			lastMoveTime = 0;
			expired = false;

			if( str == "inf" )
			{
				Infinite = true;
				return;
			}

			string[] list = str.Split( '+' );

			//	increment
			if( list.Length == 2 )
			{
				int inc = (int) (Convert.ToDouble( list[1] ) * 1000);
				if( inc >= 0 )
					TimeIncrement = inc;
			}

			list = list[0].Split( '/' );
			string strTime;

			//	moves per tc
			if( list.Length == 2 )
			{
				int nmoves = Convert.ToInt32( list[0] );
				if( nmoves >= 0 )
					MovesPerTC = nmoves;
				strTime = list[1];
			}
			else
				strTime = list[0];

			//	time per tc
			int ms = 0;
			list = strTime.Split( ':' );
			if( list.Length == 2 )
				ms = (int) (Convert.ToDouble( list[0] ) * 60000 + Convert.ToDouble( list[1] ) * 1000);
			else
				ms = (int) (Convert.ToDouble( list[0] ) * 1000);

			if( ms > 0 )
				TimePerTC = ms;
		}

		//	Creates a new TimeControl that has the exact same values as this one
		public TimeControl Clone()
		{
			TimeControl clone = new TimeControl();
			clone.Infinite = Infinite;
			clone.MovesPerTC = MovesPerTC;
			clone.TimePerTC = TimePerTC;
			clone.TimeIncrement = TimeIncrement;
			clone.TimePerMove = TimePerMove;
			clone.TimeLeft = TimeLeft;
			clone.MovesLeft = MovesLeft;
			clone.PlyLimit = PlyLimit;
			clone.NodeLimit = NodeLimit;
			clone.ExpiryMargin = ExpiryMargin;
			clone.lastMoveTime = lastMoveTime;
			clone.expired = expired;
			return clone;
		}


		// *** OPERATORS *** //

		//	Returns true if other is the same as this time control.
		//	The state of a game (eg. time left, used time, the expiry flag)
		//	and the expiry margin are ignored.
		public static bool operator ==( TimeControl tc1, TimeControl tc2 )
		{
			if( Object.ReferenceEquals( tc1, tc2 ) )
				return true;
			if( ((Object) tc1) == null || ((Object) tc2) == null )
				return false;

			return
				tc1.MovesPerTC == tc2.MovesPerTC &&
				tc1.TimePerTC == tc2.TimePerTC &&
				tc1.TimePerMove == tc2.TimePerMove &&
				tc1.TimeIncrement == tc2.TimeIncrement &&
				tc1.PlyLimit == tc2.PlyLimit &&
				tc1.NodeLimit == tc2.NodeLimit &&
				tc1.Infinite == tc2.Infinite;
		}

		public static bool operator !=( TimeControl tc1, TimeControl tc2 )
		{
			if( Object.ReferenceEquals( tc1, tc2 ) )
				return false;
			if( ((Object) tc1) == null || ((Object) tc2) == null )
				return true;

			return
				tc1.MovesPerTC != tc2.MovesPerTC || 
				tc1.TimePerTC != tc2.TimePerTC || 
				tc1.TimePerMove != tc2.TimePerMove || 
				tc1.TimeIncrement != tc2.TimeIncrement || 
				tc1.PlyLimit != tc2.PlyLimit || 
				tc1.NodeLimit != tc2.NodeLimit || 
				tc1.Infinite != tc2.Infinite;
		}

		public bool Equals( TimeControl other )
		{
			return
				MovesPerTC == other.MovesPerTC &&
				TimePerTC == other.TimePerTC &&
				TimePerMove == other.TimePerMove &&
				TimeIncrement == other.TimeIncrement &&
				PlyLimit == other.PlyLimit &&
				NodeLimit == other.NodeLimit &&
				Infinite == other.Infinite;
		}

		public override bool Equals( object obj )
		{
			if( obj is TimeControl )
				return Equals( (TimeControl) obj );
			return false;
		}

		public override int GetHashCode()
		{
			return (int)
				((uint) (MovesPerTC << 1) |
				 (uint) (TimePerTC * 3) |
				 (uint) ((TimePerMove << 2) * 7) |
				 (uint) ((TimeIncrement << 5) * 9) |
				 (uint) ((PlyLimit << 7) * 5) |
				 (uint) (NodeLimit << 6) |
				 (uint) (Infinite ? 82371 : 0));
		}

		//	Returns true if the time control is valid
		public bool IsValid
		{
			get
			{
				if( MovesPerTC < 0 || 
					TimePerTC < 0 || 
					TimePerMove < 0 || 
					TimeIncrement < 0 || 
					PlyLimit < 0 ||
					NodeLimit < 0 || 
					ExpiryMargin < 0 ||
					(TimePerTC == TimePerMove && !Infinite) )
					return false;
				return true;
			}
		}

		//	Returns the time control string in PGN format
		public override string ToString()
		{
			if( !IsValid )
				return "";

			if( Infinite )
				return "inf";

			if( TimePerMove != 0 )
				return string.Format( "{0}/move", (double) TimePerMove / 1000.0 );

			string str = "";
			if( MovesPerTC > 0 )
				str += MovesPerTC.ToString() + "/";
			str += ((double) TimePerTC / 1000.0).ToString();

			if( TimeIncrement > 0 )
				str += "+" + ((double) TimeIncrement / 1000.0).ToString();
			return str;
		}

		//	Returns a verbose description of the time control
		public string ToVerboseString()
		{
			if( !IsValid )
				return "";

			string str;

			if( Infinite )
				str = "infinite time";
			else if( TimePerMove != 0 )
				str = string.Format( "{0} per move", timeString( TimePerMove ) );
			else if( MovesPerTC != 0 )
				str = string.Format( "{0} moves in {1}", MovesPerTC.ToString(), timeString( TimePerTC ) );
			else
				str = timeString( TimePerTC );

			if( TimePerTC != 0 && TimeIncrement != 0 )
				str += string.Format( ", {0} increment", timeString( TimeIncrement ) );
			if( NodeLimit != 0 )
				str += string.Format( ", {0} nodes", nodeString( NodeLimit ) );
			if( PlyLimit != 0 )
				str += string.Format( ", {0} plies", PlyLimit.ToString() );
			if( ExpiryMargin != 0 )
				str += string.Format( ", {0} msec margin", ExpiryMargin.ToString() );

			return str;
		}

		//	Initializes the time control ready for a new game
		public void Initialize()
		{
			expired = false;
			lastMoveTime = 0;

			if( TimePerTC != 0 )
			{
				TimeLeft = TimePerTC;
				MovesLeft = MovesPerTC;
			}
			else if( TimePerMove != 0 )
				TimeLeft = TimePerMove;
		}

		//	Returns true if the time control is infinite
		public bool Infinite { get; set; }

		//	Returns the time per time control,
		//	or 0 if there's no specified total time
		public long TimePerTC { get; set; }

		//	Returns the number of moves per time control,
		//	or 0 if the whole game is played in timePerTc() time
		public int MovesPerTC { get; set; }

		//	Returns the time increment per move
		public long TimeIncrement { get; set; }

		//	Returns the time per move - 
		//	the player will think of each move this long at most.
		//	Returns 0 if there's no specified total time.
		public long TimePerMove { get; set; }

		//	Returns the time left in the time control
		public long TimeLeft { get; set; }

		//	Returns the number of full moves left in the time control,
		//	or 0 if the number of moves is not specified.
		public int MovesLeft { get; set; }

		//	Returns the maximum search depth in plies
		public int PlyLimit { get; set; }

		//	Returns the node limit for each move
		public long NodeLimit { get; set; }

		//	Returns the expiry margin - 
		//	Expiry margin is the amount of time a player can go over
		//	the time limit without losing on time (default is 0.)
		public int ExpiryMargin { get; set; }


		//	Starts the timer
		public void StartTimer()
		{
			time = DateTime.Now;
		}
		
		//	Update the time control with the elapsed time
		public void Update()
		{
			lastMoveTime = (long) (DateTime.Now - time).TotalMilliseconds;

			if( !Infinite && lastMoveTime > TimeLeft + ExpiryMargin )
				expired = true;

			if( TimePerMove != 0 )
				TimeLeft = TimePerMove;
			else if( !Infinite )
			{
				TimeLeft = TimeLeft + TimeIncrement - lastMoveTime;
		
				if( MovesPerTC > 0 )
				{
					MovesLeft--;
			
					// Restart the time control
					if( MovesLeft == 0 )
					{
						MovesLeft = MovesPerTC;
						TimeLeft = TimePerTC + TimeLeft;
					}
				}
			}
		}

		//	Returns the last elapsed move time
		public long LastMoveTime
		{ get { return lastMoveTime; } }

		//	Returns true if the allotted time has expired
		public bool Expired
		{ get { return expired; } }

		//	Returns the time left in an active clock.
		//
		//	The TimeControl object doesn't know whether the clock is
		//	active or not. It's recommended to check the player's
		//	state first to verify that it's in the thinking state.
		public long ActiveTimeLeft
		{ get { return TimeLeft - (long) (DateTime.Now - time).TotalMilliseconds; } }


		protected static string timeString( long ms )
		{
			if( ms == 0 || ms % 60000 != 0 )
				return string.Format( "{0} sec", (double) ms / 1000.0 );
			if( ms % 3600000 != 0 )
				return string.Format( "{0} min", ms / 60000 );
			return string.Format( "{0} h", ms / 3600000 );
		}

		protected static string nodeString( long nodes )
		{
			if( nodes == 0 || nodes % 1000 != 0 )
				return nodes.ToString();
			else if( nodes % 1000000 != 0 )
				return string.Format( "{0} k", nodes / 1000 );
			return string.Format( "{0} M", nodes / 1000000 );
		}


		// *** PRIVATE DATA MEMBERS *** //

		private long lastMoveTime;
		private bool expired;
		private DateTime time;
	}
}
