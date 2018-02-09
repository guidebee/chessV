
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
using System.Drawing;
using Microsoft.Win32;

namespace ChessV.GUI
{
	public class ColorScheme
	{
		public string Name { get; set; }
		public Color BorderColor { get; set; }
		public Color TextColor { get; set; }
		public Color HighlightColor { get; set; }
		public Texture BorderTexture { get; set; }
		public Dictionary<int, Texture> SquareTextures { get; set; }
		public Dictionary<int, Color> SquareColors { get; set; }
		public Dictionary<int, Color> PlayerColors { get; set; }
		public int NumberOfColors { get; set; }
		public bool Modified { get; set; }

		public ColorScheme()
		{
			SquareColors = new Dictionary<int, Color>();
			PlayerColors = new Dictionary<int, Color>();
			Modified = false;
		}

		public ColorScheme( RegistryKey key ): this()
		{
			Name = key.Name.Substring( key.Name.LastIndexOf( '\\' ) + 1 );

			//	Border - may be texture or static color
			object borderTextureObject = key.GetValue( "BorderTexture" );
			if( borderTextureObject != null )
			{
				if( TextureLibrary.Contains( (string) borderTextureObject ) )
					BorderTexture = TextureLibrary.Lookup( (string) borderTextureObject );
				else
					throw new Exception( "Unknown texture specified in color scheme: " + (string) borderTextureObject );
			}
			else
				BorderColor = Color.FromArgb( Convert.ToInt32( key.GetValue( "BorderColor" ) ) );
			HighlightColor = Color.FromArgb( Convert.ToInt32( key.GetValue( "HighlightColor" ) ) );
			TextColor = Color.FromArgb( Convert.ToInt32( key.GetValue( "TextColor" ) ) );
			int nSquareColors = 0;
			object value;
			//	repeat for each square color until we run out of specified colors/textures
			while( true )
			{
				//	first see if a texture is defined for this square color
				string valuename = "SquareTexture" + (nSquareColors + 1).ToString();
				value = key.GetValue( valuename );
				if( value != null )
				{
					if( TextureLibrary.Contains( (string) value ) )
					{
						if( SquareTextures == null )
							SquareTextures = new Dictionary<int, Texture>();
						Texture texture = TextureLibrary.Lookup( (string) value );
						SquareTextures.Add( nSquareColors, texture );
						SquareColors.Add( nSquareColors, texture.SubstituteColor );
						nSquareColors++;
					}
					else
						throw new Exception( "Unknown texture specified in color scheme: " + valuename );
				}
				else
				{
					//	if no texture check for a specific color
					valuename = "SquareColor" + (nSquareColors + 1).ToString();
					value = key.GetValue( valuename );
					if( value != null )
					{
						SquareColors.Add( nSquareColors, Color.FromArgb( Convert.ToInt32( value ) ) );
						nSquareColors++;
					}
					else
						//	out of square colors/textures
						break;
				}
			}
			NumberOfColors = nSquareColors;
			int nPlayerColors = 0;
			string playerColorValueName = "PlayerColor" + (nPlayerColors + 1).ToString();
			while( (value = key.GetValue( playerColorValueName )) != null )
			{
				PlayerColors.Add( nPlayerColors, Color.FromArgb( Convert.ToInt32( value ) ) );
				nPlayerColors++;
				playerColorValueName = "PlayerColor" + (nPlayerColors + 1).ToString();
			}
			Modified = false;
		}

		public ColorScheme
			( Color borderColor,
			  Color highlightColor,
			  Color textColor,
			  Color squareColor1,
			  Color squareColor2,
			  Color squareColor3,
			  Color playerColor1,
			  Color playerColor2 )
		{
			BorderColor = borderColor;
			HighlightColor = highlightColor;
			TextColor = textColor;
			SquareColors = new Dictionary<int, Color>();
			SquareColors[0] = squareColor1;
			SquareColors[1] = squareColor2;
			SquareColors[2] = squareColor3;
			NumberOfColors = 3;
			PlayerColors = new Dictionary<int, Color>();
			PlayerColors[0] = playerColor1;
			PlayerColors[1] = playerColor2;
			Modified = true;
		}

		public ColorScheme Clone()
		{
			ColorScheme clone = new ColorScheme();
			clone.Name = Name;
			clone.BorderColor = BorderColor;
			clone.HighlightColor = HighlightColor;
			clone.TextColor = TextColor;
			foreach( KeyValuePair<int, Color> squareColor in SquareColors )
				clone.SquareColors.Add( squareColor.Key, squareColor.Value );
			foreach( KeyValuePair<int, Color> playerColor in PlayerColors )
				clone.PlayerColors.Add( playerColor.Key, playerColor.Value );
			if( SquareTextures != null )
			{
				clone.SquareTextures = new Dictionary<int, Texture>();
				foreach( KeyValuePair<int, Texture> squareTexture in SquareTextures )
					clone.SquareTextures.Add( squareTexture.Key, squareTexture.Value );
			}
			clone.NumberOfColors = NumberOfColors;
			clone.Modified = Modified;
			return clone;
		}

		public void WriteToRegistry( RegistryKey key )
		{
			Modified = false;
			//	save the standard color values that all schemes have
			key.SetValue( "BorderColor", unchecked((Int32) BorderColor.ToArgb()), RegistryValueKind.DWord );
			key.SetValue( "HighlightColor", unchecked((Int32) HighlightColor.ToArgb()), RegistryValueKind.DWord );
			key.SetValue( "TextColor", unchecked((Int32) TextColor.ToArgb()), RegistryValueKind.DWord );
			//	save out all square colors/textures counting the number of 
			//	square colors in the scheme as we go
			int nSquareColors = 0;
			while( true )
			{
				if( SquareTextures != null && SquareTextures.ContainsKey( nSquareColors ) )
					key.SetValue( "SquareTexture" + (nSquareColors + 1).ToString(), SquareTextures[nSquareColors].Name, RegistryValueKind.String );
				else if( SquareColors.ContainsKey( nSquareColors ) )
				{
					key.SetValue( "SquareColor" + (nSquareColors + 1).ToString(), unchecked( (Int32) SquareColors[nSquareColors].ToArgb() ), RegistryValueKind.DWord );
					//	if there's a specified texture already, remove it or else 
					//	it will override the color when we load this scheme again
					if( key.GetValue( "SquareTexture" + (nSquareColors + 1).ToString() ) != null )
						key.DeleteValue( "SquareTexture" + (nSquareColors + 1).ToString() );
				}
				else
					break;
				nSquareColors++;
			}
			//	clear out any additional colors/textures specified in 
			//	the registry that we no longer have
			while( true )
			{
				bool found = false;
				int extraColorCount = nSquareColors + 1;
				if( key.GetValue( "SquareTexture" + extraColorCount.ToString() ) != null )
				{
					key.DeleteValue( "SquareTexture" + extraColorCount.ToString() );
					found = true;
				}
				if( key.GetValue( "SquareColor" + extraColorCount.ToString() ) != null )
				{
					key.DeleteValue( "SquareColor" + extraColorCount.ToString() );
					found = true;
				}
				extraColorCount++;
				if( !found )
					break;
			}
			//	write out the player colors
			foreach( KeyValuePair<int, Color> pair in PlayerColors )
				key.SetValue( "PlayerColor" + (pair.Key + 1).ToString(), unchecked( (Int32) pair.Value.ToArgb() ), RegistryValueKind.DWord );
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
