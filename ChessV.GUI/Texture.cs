
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
	public class Texture
	{
		public string Name { get; private set; }
		public int NumberOfImages { get; private set; }
		public Dictionary<int, Image> Images { get; private set; }
		public Image LargeImage { get; private set; }
		public Color SubstituteColor { get; set; }

		public Texture( string name )
		{
			Name = name;
			NumberOfImages = 0;
			Images = new Dictionary<int, Image>();
		}

		public Texture( string name, string textureDir )
		{ 
			Name = name; 
			NumberOfImages = 0;
			Images = new Dictionary<int, Image>();

			int imageNumber = 1;
			while( File.Exists( textureDir + Path.DirectorySeparatorChar + "image" + imageNumber.ToString() + ".png" ) )
			{
				Image image = new Bitmap( textureDir + Path.DirectorySeparatorChar + "image" + imageNumber.ToString() + ".png" );
				AddImage( image );
				imageNumber++;
			}

			//	Check for large image
			if( File.Exists( textureDir + Path.DirectorySeparatorChar + "sample_image.png" ) )
				LargeImage = new Bitmap( textureDir + Path.DirectorySeparatorChar + "sample_image.png" );

			//	Read the properties.txt file to get teh substitute color
			TextReader reader = new StreamReader( textureDir + Path.DirectorySeparatorChar + "properties.txt" );
			PropertyMap properties = new PropertyMap( reader );
			reader.Close();
			if( !properties.Properties.ContainsKey( "COLOR" ) )
				throw new Exception( "Could not load texture - Color not specified in properties.txt" );
			object colorObject = properties.Properties["COLOR"];
			if( !(colorObject is Color) )
				throw new Exception( "Could not load texture - Color property specified in properties.txt not a valid color" );
			SubstituteColor = (Color) colorObject;
		}

		public void AddImage( Image image )
		{
			Images.Add( NumberOfImages++, image ); 
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
