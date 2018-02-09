
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
using System.IO;

namespace ChessV.Manager
{
	public class SavedGameReader
	{
		public string GameName { get; private set; }
		public string BaseGameName { get; private set; }
		public Dictionary<string, string> VariableDefinitions { get; private set; }
		public List<string> Moves { get; private set; }

		public SavedGameReader( TextReader inputStream )
		{
			reader = inputStream;
			VariableDefinitions = new Dictionary<string, string>();
			matchGameName();
			int cursor;
			while( matchVariable( out cursor ) ) ;
			if( Moves != null )
			{
				matchEquals( ref cursor );
				matchOpenBrace( ref cursor );
				readMoves( ref cursor );
			}
			else
				//	no moves in saved game, but create empty list anyway
				Moves = new List<string>();
		}

		protected void matchGameName()
		{
			string line;
			//	loop here to skip any blank lines at the beginning
			while( (line = reader.ReadLine()) != null )
			{
				line = line.Trim();
				if( line.Length > 0 )
				{
					//	do we have a base game name?
					if( line.Contains( ":" ) )
					{
						GameName = line.Substring( 0, line.IndexOf( ':' ) );
						BaseGameName = line.Substring( line.IndexOf( ':' ) + 1 );
					}
					else
						GameName = line;
					return;
				}
			}
			throw new Exception( "Unexpected end-of-file reached reading game name" );
		}

		protected bool matchVariable( out int cursor )
		{
			while( (lineBuffer = reader.ReadLine()) != null )
			{
				lineBuffer = lineBuffer.Trim();
				if( lineBuffer.Length > 0 )
				{
					cursor = 0;
					string variable = grabToken( ref cursor );
					if( variable.ToUpper() == "MOVES" )
					{
						//	encountering the "moves" section of the file, we 
						//	are done with variable definitions
						Moves = new List<string>();
						return false;
					}
					if( cursor == lineBuffer.Length )
						throw new Exception( "Unexpected end-of-line encountered" );
					if( lineBuffer[cursor] != '=' )
						throw new Exception( "Unexpected character found; expected =" );
					cursor++;
					//	gobble up any additional whitespace
					while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
						cursor++;
					if( cursor == lineBuffer.Length )
						throw new Exception( "Unexpected end-of-line encountered" );
					//	value of the variable extend to end of (previously trimmed) line
					string value = lineBuffer.Substring( cursor );
					VariableDefinitions.Add( variable, value );
					return true;
				}
			}
			//	End of file and no "Moves" section, so we're done
			cursor = 0;
			return false;
		}

		protected string grabToken( ref int cursor )
		{
			int start = cursor;
			if( (lineBuffer[cursor] < 'a' || lineBuffer[cursor] > 'z') &&
				(lineBuffer[cursor] < 'A' || lineBuffer[cursor] > 'Z') &&
				 lineBuffer[cursor] != '_' )
				throw new Exception( "Unexpected character found parsing token: " + lineBuffer[cursor] );
			while( cursor < lineBuffer.Length && !Char.IsWhiteSpace( lineBuffer[cursor] ) && lineBuffer[cursor] != '=' )
				cursor++;
			int end = cursor - 1;
			//	gobble up any additional whitespace
			while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
				cursor++;
			return lineBuffer.Substring( start, end - start + 1 );
		}

		protected void matchEquals( ref int cursor )
		{
			if( cursor >= lineBuffer.Length )
				throw new Exception( "Unexpected end-of-line encountered" );
			if( lineBuffer[cursor] != '=' )
				throw new Exception( "Unexpected character found; expected =" );
			cursor++;
			//	gobble up any additional whitespace
			while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
				cursor++;
		}

		protected void matchOpenBrace( ref int cursor )
		{
			while( true )
			{
				while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
					cursor++;
				if( cursor < lineBuffer.Length )
				{
					if( lineBuffer[cursor] != '{' )
						throw new Exception( "Unexpected character found; expected {" );
					cursor++;
					return;
				}
				if( (lineBuffer = reader.ReadLine()) == null )
					throw new Exception( "Unexpected end-of-file encountered" );
				cursor = 0;
			}
		}

		protected void readMoves( ref int cursor )
		{
			while( true )
			{
				//	gobble up any additional whitespace
				while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
					cursor++;
				if( cursor < lineBuffer.Length )
				{
					//	grab chars until whitespace, close-brace, or end-of-line
					int start = cursor;
					while( cursor < lineBuffer.Length && !Char.IsWhiteSpace( lineBuffer[cursor] ) && lineBuffer[cursor] != '}' )
						cursor++;
					if( cursor == lineBuffer.Length )
					{
						if( cursor > start + 1 )
							Moves.Add( lineBuffer.Substring( start, cursor - start ) );
						if( (lineBuffer = reader.ReadLine()) == null )
							throw new Exception( "Unexpected end-of-file encountered" );
						cursor = 0;
					}
					else if( lineBuffer[cursor] == '}' )
					{
						if( cursor > start + 1 )
							Moves.Add( lineBuffer.Substring( start, cursor - start ) );
						return;
					}
					else
					{
						Moves.Add( lineBuffer.Substring( start, cursor - start ) );
						//	gobble up any additional whitespace
						while( cursor < lineBuffer.Length && Char.IsWhiteSpace( lineBuffer[cursor] ) )
							cursor++;
						if( cursor == lineBuffer.Length )
						{
							if( (lineBuffer = reader.ReadLine()) == null )
								throw new Exception( "Unexpected end-of-file encountered" );
							cursor = 0;
						}
					}
				}
				else
				{
					if( (lineBuffer = reader.ReadLine()) == null )
						throw new Exception( "Unexpected end-of-file encountered" );
					cursor = 0;
				}
			}
		}

		private TextReader reader;
		private string lineBuffer;
	}
}
