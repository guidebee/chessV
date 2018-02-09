
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
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace ChessV.GUI
{
	public static class TextureLibrary
	{
		public static void Initialize()
		{
			textures = new Dictionary<string, Texture>();
			//	Textures sets aren't stored in the Registry - we just check the Graphics\Textures 
			//	subdirectiry on each run of the program to see what's available.
			string rootTextureDir = Path.Combine( "Graphics", "Textures" );
			if( Directory.Exists( rootTextureDir ) )
			{
				string[] textureDirs = Directory.GetDirectories( rootTextureDir );
				foreach( string textureDir in textureDirs )
				{
					//	The directory must have at least an image1.png, a sample_image.png,
					//	and a properties.txt  to be considered a valid texture.
					if( File.Exists( textureDir + Path.DirectorySeparatorChar + "image1.png" ) &&
						File.Exists( textureDir + Path.DirectorySeparatorChar + "sample_image.png" ) &&
						File.Exists( textureDir + Path.DirectorySeparatorChar + "properties.txt" ) )
					{
						string textureName = textureDir.Substring( 18 );
						textures.Add( textureName, new Texture( textureName, textureDir ) );
					}
				}
			}
		}

		public static Texture Lookup( string name )
		{
			if( textures.ContainsKey( name ) )
				return textures[name];
			return null;
		}

		public static bool Contains( string name )
		{
			return textures.ContainsKey( name );
		}

		public static void AddTexture( string name, Texture texture )
		{
			textures.Add( name, texture );
		}

		public static Dictionary<string, Texture> Textures
		{
			get
			{ return textures; }
		}


		//	A lookup table of all the different textures discovered in the Graphics\Textures folder
		private static Dictionary<string, Texture> textures;
	}
}
