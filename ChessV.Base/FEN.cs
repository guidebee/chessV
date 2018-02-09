
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

namespace ChessV
{
	public class FEN
	{
		// *** CONSTRUCTION *** //

		public FEN( string fenFormat )
		{
			fenFormatParts = GetFenFormatPartsFromDefinition( fenFormat );
			fenParts = null;
		}

		public FEN( string fenFormat, string fen )
		{
			fenFormatParts = GetFenFormatPartsFromDefinition( fenFormat );
			Load( fen );
		}


		// *** PROPERTIES *** //

		public string FormatString
		{
			get
			{
				StringBuilder str = new StringBuilder( 120 );
				for( int x = 0; x < fenFormatParts.Length; x++ )
				{
					if( x > 0 )
						str.Append( ' ' );
					str.Append( '{' );
					str.Append( fenFormatParts[x] );
					str.Append( '}' );
				}
				return str.ToString();
			}
		}


		// *** OPERATIONS *** //

		public void Load( string fen )
		{
			string[] fenSplit = fen.Split( ' ' );
			if( fenSplit.Length != fenFormatParts.Length )
				throw new Exception( "Invalid FEN specified - incorrect number of parts" );
			fenParts = new Dictionary<string, string>();
			for( int x = 0; x < fenSplit.Length; x++ )
				fenParts.Add( fenFormatParts[x], fenSplit[x] );
		}

		public string this[string part]
		{
			get
			{
				if( fenParts == null )
					throw new Exception( "FEN not initialized" );
				if( !fenParts.ContainsKey( part ) )
					throw new Exception( "FEN error: does not contain element '" + part + "'" );
				return fenParts[part];
			}

			set
			{
				if( fenParts == null )
					throw new Exception( "FEN not initialized" );
				fenParts[part] = value;
			}
		}

		public void SetUninitializedDefaults()
		{
			List<string> uninitializedParts = new List<string>();
			foreach( KeyValuePair<string, string> pair in fenParts )
				if( pair.Value == "#default" )
					uninitializedParts.Add( pair.Key );
			foreach( string key in uninitializedParts )
				fenParts[key] = "-";
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder( 120 );
			builder.Append( fenParts[fenFormatParts[0]] );
			for( int x = 1; x < fenFormatParts.Length; x++ )
			{
				builder.Append( ' ' );
				builder.Append( fenParts[fenFormatParts[x]] );
			}
			return builder.ToString();
		}


		// *** HELPER FUNCTIONS *** //

		#region GetFenFormatPartsFromDefinition
		protected string[] GetFenFormatPartsFromDefinition( string fenFormat )
		{
			List<string> fenFormatParts = new List<string>();

			int cursor = 0;
			while( cursor < fenFormat.Length )
			{
				//	skip whitespace
				while( Char.IsWhiteSpace( fenFormat[cursor] ) && cursor < fenFormat.Length )
					cursor++;

				if( cursor < fenFormat.Length )
				{
					//	match "{"
					if( fenFormat[cursor++] != '{' )
						throw new Exception( "Invalid FEN format specifier" );
					//	match part name
					int partNameStart = cursor;
					if( cursor >= fenFormat.Length || !Char.IsLetterOrDigit( fenFormat[cursor++] ) )
						throw new Exception( "Invalid FEN format specifier" );
					while( cursor < fenFormat.Length && fenFormat[cursor++] != '}' )
						;
					if( cursor == fenFormat.Length && fenFormat[cursor - 1] != '}' )
						throw new Exception( "Invalid FEN format specifier - encountered end-of-line when expecting '}'" );
					//	matched "}"
					string partName = fenFormat.Substring( partNameStart, cursor - partNameStart - 1 );
					fenFormatParts.Add( partName );
				}
			}
			string[] returnval = new string[fenFormatParts.Count];
			for( int x = 0; x < fenFormatParts.Count; x++ )
				returnval[x] = fenFormatParts[x];
			return returnval;
		}
		#endregion


		//	*** PROTECTED DATA MEMBERS *** //
		protected string[] fenFormatParts;
		protected Dictionary<string, string> fenParts;
	}
}
