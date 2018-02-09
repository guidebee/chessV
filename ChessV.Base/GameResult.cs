
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

namespace ChessV
{
	public enum ResultType
	{
		//	Win by any means
		Win,

		//	Draw by any means
		Draw,

		//	Loser resigns
		Resignation,

		//	A player's time flag falls
		Timeout,

		//	Adjudication by the GUI
		Adjudication,

		//	Loser tries to make an illegal move
		IllegalMove,

		//	Loser disconnects or terminates (if it's an engine)
		Disconnection,

		//	Loser's connection stalls (doesn't respond to ping)
		StalledConnection,

		//	Both players agree to a result
		Agreement,

		//	No result yet
		NoResult,

		//	Result error, caused by an invalid result string
		ResultError
	}

	public class Result
	{
		// *** CONSTRUCTION *** //

		public Result
			( ResultType type = ResultType.NoResult,
			  int winner = -1,
			  string description = null )
		{
			Type = type;
			this.winner = winner;
			this.description = description;
		}

		//	Creates a new result from a string
		public Result( string str )
		{
			if( str.Length >= 3 && str.Substring( 0, 3 ) == "1-0" )
			{
				Type = ResultType.Win;
				winner = 0;
			}
			else if( str.Length >= 3 && str.Substring( 0, 3 ) == "0-1" )
			{
				Type = ResultType.Win;
				winner = 1;
			}
			else if( str.Length >= 7 && str.Substring( 0, 7 ) == "1/2-1/2" )
				Type = ResultType.Draw;
			else if( str.Length >= 1 && str[0] == '*' )
				Type = ResultType.NoResult;
			int start = str.IndexOf( '{' );
			int end = str.LastIndexOf( '}' );
			if( start != -1 && end != -1 )
				description = str.Substring( start + 1, end - start - 1 );
		}


		// *** OPERATORS *** //

		//	Returns true if r1 and r2 are the same result
		public static bool operator ==( Result r1, Result r2 )
		{
			//	if both are null, or both are same instance, return true
			if( System.Object.ReferenceEquals( r1, r2 ) )
				return true;

			//	if one is null, but not both, return false
			if( (object) r1 == null || (object) r2 == null )
				return false;

			return 
				r1.Type == r2.Type &&
				r1.Winner == r2.Winner;
		}

		//	Returns true if r1 and r2 are not the same result
		public static bool operator !=( Result r1, Result r2 )
		{
			// If both are null, or both are same instance, return false
			if( System.Object.ReferenceEquals( r1, r2 ) )
				return false;

			// If one is null, but not both, return true
			if( (object) r1 == null || (object) r2 == null )
				return true;

			return
				r1.Type != r2.Type ||
				r1.Winner != r2.Winner;
		}

		//	Returns true if other is the same result
		public bool Equals( Result other )
		{ 
			return 
				other != null && 
				Type == other.Type && 
				Winner == other.Winner;
		}

		//	Returns true if obj is the same result
		public override bool Equals( object obj )
		{
			if( obj is Result )
				return Equals( (Result) obj );
			return false;
		}

		public override int GetHashCode()
		{
			return (((int) Type) + 2 * 3) + Winner;
		}


		// *** PROPERTIES *** //

		//	Returns true if the result is NoResult
		public bool IsNone
		{ get { return Type == ResultType.NoResult; } }

		//	Returns true if the result is Draw
		public bool IsDraw
		{ 
			get 
			{ 
				return 
					winner == -1 && 
					Type != ResultType.NoResult && 
					Type != ResultType.ResultError;
			}
		}

		//	Returns the winning player number, or -1 if there's no winner
		public int Winner
		{
			get
			{
				return winner;
			}
		}

		//	Returns the losing side, or NoSide if there's no loser
		public int Loser
		{
			get
			{
				if( winner == -1 )
					return -1;
				return 1 ^ winner;
			}
		}

		//	Returns the type of the result
		public ResultType Type { get; private set; }

		//	Returns the result description
		public string Description
		{ 
			get 
			{ 
				string w = winner == 0 ? "white" : (winner == 1 ? "black" : null);
				string l = winner == 0 ? "black" : (winner == 1 ? "white" : null);
				string str = "";

				if( Type == ResultType.Resignation )
					str = string.Format( "{0} resigns", l );
				else if( Type == ResultType.Timeout )
				{
					if( l == null )
						str = "Draw by timeout";
					else
						str = string.Format( "{0} loses on time", l );
				}
				else if( Type == ResultType.Adjudication )
				{
					if( w == null )
						str = "Draw by adjudication";
					else
						str = string.Format( "{0} wins by adjudication", w );
				}
				else if( Type == ResultType.IllegalMove )
					str = string.Format( "{0} makes an illegal move", l );
				else if( Type == ResultType.Disconnection )
				{
					if( l == null )
						str = "Draw by disconnection";
					else
						str = string.Format( "{0} disconnects", l );
				}
				else if( Type == ResultType.StalledConnection )
				{
					if( l == null )
						str = "Draw by stalled connection";
					else
						str = string.Format( "{0}'s connection stalls", l );
				}
				else if( Type == ResultType.Agreement )
				{
					if( w == null )
						str = "Draw by agreement";
					else
						str = string.Format( "{0} wins by agreement", w );
				}
				else if( Type == ResultType.NoResult )
					str = "No result";
				else if( Type == ResultType.ResultError )
					str = "Result error";

				if( description == null )
				{
					if( Type == ResultType.Win )
						str = string.Format( "{0} wins", w );
					else if( Type == ResultType.Draw )
						str = "Drawn game";
				}
				else
				{
					if( str != null )
						str += ": ";
					str += description;
				}

				//Q_ASSERT(!str.isEmpty());
				str = char.ToUpper( str[0] ) + str.Substring( 1 );
				return str;
			}
		}

		//	Returns the short string representation of the result - 
		//	can be "1-0", "0-1", "1/2-1/2", or "*"
		public string ShortString
		{
			get
			{
				if( Type == ResultType.NoResult || Type == ResultType.ResultError )
					return "*";
				if( winner == 0 )
					return "1-0";
				if( winner == 1 )
					return "0-1";
				return "1/2-1/2";
			}
		}

		//	Returns the verbose string representation of the result
		//	Uses the format "result {description}" e.g., "1-0 {White mates}"
		public string VerboseString
		{
			get
			{
				return ShortString + " {" + Description + "}";
			}
		}


		// *** PRIVATE DATA MEMBERS *** //

		private int winner;
		private string description;
	}
}
