
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
using Microsoft.Win32;

namespace ChessV.GUI
{
	public class Theme
	{
		public ColorScheme ColorScheme { get; set; }
		public PieceSet PieceSet { get; set; }
		public int NSquareColors { get; set; }
		public string CustomThemeName { get; set; }

		public Theme()
		{
			ColorScheme = new ColorScheme();
			PieceSet = PieceSetLibrary.Default;
			NSquareColors = 2;
			CustomThemeName = null;
		}

		public Theme( ColorScheme colorScheme, int nSquareColors )
		{
			ColorScheme = colorScheme;
			PieceSet = PieceSetLibrary.Default;
			NSquareColors = nSquareColors;
			CustomThemeName = null;
		}

		public Theme( ColorScheme colorScheme, PieceSet pieceSet, int nSquareColors, string customThemeName )
		{
			ColorScheme = colorScheme;
			PieceSet = pieceSet;
			NSquareColors = nSquareColors;
			CustomThemeName = customThemeName;
		}

		public void SaveToRegistry( RegistryKey key )
		{
			//  save new color scheme to the game's registry key - either as a named scheme (if it 
			//  is one) or as a list of ad-hoc colors
			if( !ColorScheme.Modified )
			{
				//  save the color scheme name in the game's registry key
				key.SetValue( "ColorScheme", ColorScheme.Name );
				//  since this is associated with a named scheme, delete any values in 
				//  the game's key that provide individual color elements
				string[] valueNames = key.GetValueNames();
				foreach( string valuename in valueNames )
				{
					if( (valuename.Length == 11 && valuename.ToUpper() == "BORDERCOLOR") ||
						(valuename.Length == 13 && valuename.ToUpper() == "BORDERTEXTURE") ||
						(valuename.Length == 9 && valuename.ToUpper() == "TEXTCOLOR") ||
						(valuename.Length == 14 && valuename.ToUpper() == "HIGHLIGHTCOLOR") || 
						(valuename.Length > 11 && valuename.ToUpper().Substring( 0, 11 ) == "SQUARECOLOR") || 
						(valuename.Length > 13 && valuename.ToUpper().Substring( 0, 13 ) == "SQUARETEXTURE") || 
						(valuename.Length > 11 && valuename.ToUpper().Substring( 0, 11 ) == "PLAYERCOLOR") )
						//	remove game-specific ad hoc element
						key.DeleteValue( valuename );
				}
			}
			else
			{
				ColorScheme.WriteToRegistry( key );
				//  since this is an ad-hoc scheme, delete the previous entry to named scheme if any
				key.DeleteValue( "ColorScheme", false );
			}
			//  save the piece set selection to the registry
			key.SetValue( "PieceSet", PieceSet.Name );
			//	save custom theme name (if any)
			if( CustomThemeName != null )
				key.SetValue( "CustomTheme", CustomThemeName );
			else
				if( key.GetValue( "CustomTheme" ) != null )
					key.DeleteValue( "CustomTheme" );
		}
	}
}
