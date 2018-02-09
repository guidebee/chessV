
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
using System.Drawing;
using ChessV;
using Microsoft.Win32;

namespace ChessV.GUI
{
	public static class ThemeFactory
	{
		public static void Initialize()
		{
		}

		public static Theme CreateTheme( Game game )
		{
			//	Load theme from RegistryKey if it exists.  This will restore 
			//	the theme used last time this game was played
			RegistryKey gameKey = getKeyForGame( game, false );
			if( gameKey != null )
			{
				game.RegistryKey = gameKey;
				return loadThemeFromRegistry( game, gameKey );
			}

			//	Defaults
			ColorScheme colorScheme = ColorSchemeLibrary.Default;
			PieceSet pieceSet = PieceSetLibrary.Default;
			int numColors = 2;

			//	Find AppearanceAttribute and update defaults (if it exists)
			object[] attrs = game.GetType().GetCustomAttributes( typeof(AppearanceAttribute), false );
			if( attrs.Length >= 1 )
			{
				AppearanceAttribute appearance = (AppearanceAttribute) attrs[0];
				if( appearance.Game != null && appearance.Game != game.Name )
					appearance = null;
				numColors = appearance != null ? appearance.NumberOfColors : 2;
				if( appearance != null && appearance.ColorScheme != null )
					if( ColorSchemeLibrary.Contains( appearance.ColorScheme ) )
						colorScheme = ColorSchemeLibrary.Lookup( appearance.ColorScheme );
				if( appearance != null && appearance.PieceSet != null )
					if( PieceSetLibrary.Contains( appearance.PieceSet ) )
						pieceSet = PieceSetLibrary.Lookup( appearance.PieceSet );
				bool colorSchemeChanged = false;
				if( appearance != null && appearance.Player1Color != null )
				{
					string[] colorNumbers = appearance.Player1Color.Split( ',' );
					colorScheme.PlayerColors[0] = Color.FromArgb( Convert.ToInt32( colorNumbers[0] ),
						Convert.ToInt32( colorNumbers[1] ), Convert.ToInt32( colorNumbers[2] ) );
					colorSchemeChanged = true;
				}
				if( appearance != null && appearance.Player2Color != null )
				{
					string[] colorNumbers = appearance.Player2Color.Split( ',' );
					colorScheme.PlayerColors[1] = Color.FromArgb( Convert.ToInt32( colorNumbers[0] ),
						Convert.ToInt32( colorNumbers[1] ), Convert.ToInt32( colorNumbers[2] ) );
					colorSchemeChanged = true;
				}
				if( colorSchemeChanged )
				{
					colorScheme.Modified = true;
					colorScheme.Name = "(custom)";
				}
			}

			gameKey = getKeyForGame( game, true );
			string customThemeName = null;
			if( game.GetCustomThemes() != null )
				customThemeName = game.GetDefaultCustomTheme();
			Theme theme = new Theme( colorScheme, pieceSet, numColors, customThemeName );
			theme.SaveToRegistry( gameKey );
			return theme;
		}

		private static RegistryKey getKeyForGame( Game game, bool create )
		{
			RegistryKey currentuser = Registry.CurrentUser;
			RegistryKey software = currentuser.OpenSubKey( "Software", true );
			if( software == null )
				software = currentuser.CreateSubKey( "Software" );
			RegistryKey chessv = software.OpenSubKey( "ChessV", true );
			if( chessv == null )
				chessv = software.CreateSubKey( "ChessV" );
			RegistryKey games = chessv.OpenSubKey( "Games", true );
			if( games == null )
				games = chessv.CreateSubKey( "Games" );
			GameAttribute attr = game.GameAttribute;
			string baseGameName = attr.GameName;
			RegistryKey key = games.OpenSubKey( attr.GameName, true );
			if( key != null || !create )
				return key;
			return games.CreateSubKey( attr.GameName );
		}

		private static Theme loadThemeFromRegistry( Game game, RegistryKey gameKey )
		{
			//	find AppearanceAttribute (if any) to fill in any data missing 
			//	from the registry key with game defaults
			AppearanceAttribute appearance = null;
			object[] attrs = game.GetType().GetCustomAttributes( typeof(AppearanceAttribute), true );
			if( attrs.Length >= 1 )
			{
				appearance = (AppearanceAttribute) attrs[0];
				if( appearance.Game != null && appearance.Game != game.Name )
					appearance = null;
			}

			//	get number of colors
			object objNSquareColors = gameKey.GetValue( "NSquareColors" );
			if( objNSquareColors == null )
			{
				if( appearance != null )
					objNSquareColors = appearance.NumberOfColors;
				else
					objNSquareColors = 2;
				gameKey.SetValue( "NSquareColors", objNSquareColors );
			}

			//	get color scheme
			object objColorSchemeName = gameKey.GetValue( "ColorScheme" );
			ColorScheme scheme;
			if( objColorSchemeName == null )
				scheme = new ColorScheme( gameKey );
			else
			{
				if( ColorSchemeLibrary.Contains( (string) objColorSchemeName ) )
					scheme = ColorSchemeLibrary.Lookup( (string) objColorSchemeName );
				else
					scheme = ColorSchemeLibrary.Default;
			}

			//	get piece set
			object objPieceSetName = gameKey.GetValue( "PieceSet" );
			PieceSet pieceSet = null;
			if( objPieceSetName == null )
			{
				if( appearance != null )
				{
					if( PieceSetLibrary.Contains( appearance.PieceSet ) )
						pieceSet = PieceSetLibrary.Lookup( appearance.PieceSet );
					else
						pieceSet = PieceSetLibrary.Default;
				}
				else
					pieceSet = PieceSetLibrary.Default;
				gameKey.SetValue( "PieceSet", pieceSet.Name );
			}
			else
				if( PieceSetLibrary.Contains( (string) objPieceSetName ) )
					pieceSet = PieceSetLibrary.Lookup( (string) objPieceSetName );
				else
					pieceSet = PieceSetLibrary.Default;

			//	get custom theme name
			string customThemeName = (string) gameKey.GetValue( "CustomTheme" );

			//	create the Theme and return
			return new Theme( scheme, pieceSet, (int) objNSquareColors, customThemeName );
		}
	}
}
