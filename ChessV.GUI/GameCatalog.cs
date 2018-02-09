
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
using ChessV;

namespace ChessV.GUI
{
	class CatalogNode
	{
		public string Name { get; private set; }
		public bool IsAbstract { get; private set; }
		public List<CatalogNode> Children { get; private set; }
		public string SampleGameName { get; private set; }

		public CatalogNode( string name, bool isAbstract )
		{ Name = name; IsAbstract = isAbstract; Children = new List<CatalogNode>(); }

		public CatalogNode( string name, bool isAbstract, string sampleGameName )
		{ Name = name; IsAbstract = isAbstract; Children = new List<CatalogNode>(); SampleGameName = sampleGameName; }
	}

	class GameCatalogNode: CatalogNode
	{
		public ColorScheme DefaultColorScheme { get; set; }

		public GameCatalogNode( string Name ): base( Name, false )
		{
		}
	}

	class GameCatalog
	{
		public CatalogNode Root { get; private set; }

		public GameCatalog()
		{ Root = new CatalogNode( null, true ); }

		public void LoadIndex( TextReader input )
		{
			const int MAX_DEPTH = 48;
			CatalogNode[] nodePointers = new CatalogNode[MAX_DEPTH];
			nodePointers[0] = Root;
			int currentDepth = 1;

			string line = input.ReadLine();
			while( line != null )
			{
				//	determine indent depth
				int depth = 1;
				while( line[depth-1] == '\t' )
					depth++;

				//	read name and modifiers
				string name = line.Substring( depth - 1 );
				string sampleGameName = null;
				bool isAbstract = name[0] == '.';
				if( isAbstract )
				{
					name = name.Substring( 1 );
					if( name.IndexOf( '|' ) > 0 && name.Length > name.IndexOf( '|' ) )
					{
						sampleGameName = name.Substring( name.IndexOf( '|' ) + 1 );
						name = name.Substring( 0, name.IndexOf( '|' ) );
					}
				}

				//	create new catalog node
				CatalogNode newNode;
				if( isAbstract )
					newNode = new CatalogNode( name, true, sampleGameName );
				else
					newNode = new GameCatalogNode( name );

				//	add new node to catalog
				if( depth <= currentDepth + 1 )
				{
					nodePointers[depth - 1].Children.Add( newNode );
					nodePointers[depth] = newNode;
					currentDepth = depth;
				}
				else
					throw new Exception();

				//	read next line
				line = input.ReadLine();
			}
		}
	}
}
