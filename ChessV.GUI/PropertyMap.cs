
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
using System.Text;
using System.IO;
using System.Drawing;

namespace ChessV.GUI
{
	public class PropertyMap
	{
		public Dictionary<string, object> Properties { get; private set; }

		public PropertyMap()
		{
			Properties = new Dictionary<string, object>();
		}

		public PropertyMap( TextReader reader )
		{
			Properties = new Dictionary<string, object>();
			string inputLine;
			while( (inputLine = reader.ReadLine()) != null )
			{
				int cursor = 0;
				string name = matchName( inputLine, ref cursor );
				//	if the name returned is null, then it is a blank line that we will skip
				if( name != null )
				{
					matchEqualsSign( inputLine, ref cursor );
					object value = matchValue( inputLine, ref cursor );
					//	skip whitespace and make sure we are at end-of-line
					while( cursor < inputLine.Length && Char.IsWhiteSpace( inputLine[cursor] ) )
						cursor++;
					if( cursor < inputLine.Length )
						throw new Exceptions.PropertyImportException( "Unexpected trailing characters encountered parsing property" );
					//	add to the property map, upper-casing the name for easier lookup
					Properties.Add( name.ToUpper(), value );
				}
			}
		}

		protected string matchName( string inputLine, ref int cursor )
		{
			//	skip leading whitespace
			while( cursor < inputLine.Length && Char.IsWhiteSpace( inputLine[cursor] ) )
				cursor++;
			if( cursor == inputLine.Length )
				//	blank line - return null
				return null;
			//	scan the actual property name
			int tokenStart = cursor;
			while( cursor < inputLine.Length &&
				   !Char.IsWhiteSpace( inputLine[cursor] ) &&
				   inputLine[cursor] != '=' )
				cursor++;
			if( cursor == inputLine.Length )
				throw new Exceptions.PropertyImportException( "Unexpected end-of-line parsing property" );
			return inputLine.Substring( tokenStart, cursor - tokenStart );
		}

		protected void matchEqualsSign( string inputLine, ref int cursor )
		{
			//	skip leading whitespace
			while( cursor < inputLine.Length && Char.IsWhiteSpace( inputLine[cursor] ) )
				cursor++;
			if( cursor == inputLine.Length )
				throw new Exceptions.PropertyImportException( "Unexpected end-of-line parsing property" );
			if( inputLine[cursor] != '=' )
				throw new Exceptions.PropertyImportException( "Error parsing property - equal-sign expected" );
			cursor++;
			if( cursor == inputLine.Length )
				throw new Exceptions.PropertyImportException( "Unexpected end-of-line parsing property" );
		}

		protected object matchValue( string inputLine, ref int cursor )
		{
			//	skip leading whitespace
			while( cursor < inputLine.Length && Char.IsWhiteSpace( inputLine[cursor] ) )
				cursor++;
			if( cursor == inputLine.Length )
				throw new Exceptions.PropertyImportException( "Unexpected end-of-line parsing property" );
			//	scan the value
			int tokenStart = cursor++;
			if( inputLine[tokenStart] == '@' )
				//	@-sign denotes a hexadecimal color
				return matchColor( inputLine, ref cursor );
			//	TODO: parse other types of values
			return null;
		}

		protected Color matchColor( string inputLine, ref int cursor )
		{
			//	for the color to be valid, we need 8 hexadecimal digits
			if( inputLine.Length < cursor + 8 )
				throw new Exceptions.PropertyImportException( "Unable to parse color value from property" );
			for( int x = 0; x < 8; x++ )
				if( isHexDigit( inputLine[cursor+x] ) )
					throw new Exceptions.PropertyImportException( "Unable to parse color value from property" );
			cursor += 8;
			return Color.FromArgb( Convert.ToInt32( inputLine.Substring( cursor - 8, 8 ), 16 ) );
		}

		protected bool isHexDigit( char ch )
		{
			return ch != 'a' && ch != 'A' &&
				   ch != 'b' && ch != 'B' &&
				   ch != 'c' && ch != 'C' &&
				   ch != 'd' && ch != 'D' &&
				   ch != 'e' && ch != 'E' &&
				   ch != 'f' && ch != 'F' &&
				   !Char.IsDigit( ch );
		}
	}
}
