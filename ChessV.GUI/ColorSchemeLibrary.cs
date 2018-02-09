
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
using Microsoft.Win32;

namespace ChessV.GUI
{
	public static class ColorSchemeLibrary
	{
		public static void Initialize()
		{
			//	Create color scheme look-up table
			colorSchemes = new Dictionary<string, ColorScheme>();
			//	Get the Color Schemes key (creating if necessary)
			colorSchemesKey = RegistrySettings.RegistryKey.OpenSubKey( "Color Schemes", true );
			if( colorSchemesKey == null )
			{
				//	Since the Color Schemes key didn't exist, we will now populate it with our 
				//	collection of beautiful pre-defined color schemes
				colorSchemesKey = RegistrySettings.RegistryKey.CreateSubKey( "Color Schemes" );
				createDefaultColorSchemes( colorSchemesKey );
			}
			//	Add all discovered/created color schemes to the color schemes index
			string[] colorSchemeNames = colorSchemesKey.GetSubKeyNames();
			foreach( string colorSchemeName in colorSchemeNames )
			{
				try
				{
					colorSchemes.Add( colorSchemeName, new ColorScheme( colorSchemesKey.OpenSubKey( colorSchemeName ) ) );
				}
				catch
				{
					//	We just ignore color schemes that fail to load, e.g., 
					//	perhaps they refer to a texture that's not available
				}
			}
		}

		public static ColorScheme Default
		{
			get
			{
				if( colorSchemes.ContainsKey( "Cape Cod" ) )
					return colorSchemes["Cape Cod"];

				/* The default color scheme has been deleted, so we create an on-the-fly scheme with its colors */
				return new ColorScheme
					( /* BorderColor = */    Color.FromArgb( 0x80, 0x46, 0x46 ), 
					  /* HighlightColor = */ Color.FromArgb( 0x40, 0x00, 0x00 ),
					  /* TextColor = */      Color.FromArgb( 0xFF, 0xFF, 0xCC ),
					  /* SquareColor1 = */   Color.FromArgb( 0xFF, 0xFF, 0xCC ),
					  /* SquareColor2 = */   Color.FromArgb( 0x5D, 0x7E, 0x7E ),
					  /* SquareColor3 = */   Color.FromArgb( 0x64, 0x8C, 0x8C ),
					  /* PlayerColor1 = */   Color.FromArgb( 0xFF, 0xFF, 0xFF ),
					  /* PlayerColor2 = */   Color.FromArgb( 0x59, 0x84, 0xBD ) );
			}
		}

		public static ColorScheme Lookup( string name )
		{
			foreach( KeyValuePair<string, ColorScheme> pair in colorSchemes )
				if( pair.Key.ToLower() == name.ToLower() )
					return pair.Value;
			return null;
		}

		public static bool Contains( string name )
		{
			foreach( KeyValuePair<string, ColorScheme> pair in colorSchemes )
				if( pair.Key.ToLower() == name.ToLower() )
					return true;
			return false;
		}

		public static void NewScheme( ColorScheme colorScheme )
		{
			//	Save new scheme to the registry
			RegistryKey newKey = colorSchemesKey.CreateSubKey( colorScheme.Name );
			colorScheme.WriteToRegistry( newKey );
			//	Add to scheme dictionary
			colorSchemes.Add( colorScheme.Name, colorScheme );
		}

		public static void UpdateScheme( ColorScheme colorScheme )
		{
			colorSchemes[colorScheme.Name] = colorScheme;
			RegistryKey currentKey = colorSchemesKey.OpenSubKey( colorScheme.Name, true );
			if( currentKey == null )
				currentKey = colorSchemesKey.CreateSubKey( colorScheme.Name );
			colorScheme.WriteToRegistry( currentKey );
		}

		public static Dictionary<string, ColorScheme> ColorSchemes
		{
			get
			{ return colorSchemes; }
		}

		private static void createDefaultColorSchemes( RegistryKey colorSchemesKey )
		{
			#region Create Pre-Defined Schemes in registry
			#region Cape Cod
			RegistryKey capeCod = colorSchemesKey.CreateSubKey( "Cape Cod" );
			capeCod.SetValue( "BorderColor", unchecked((Int32) 0xFF804646), RegistryValueKind.DWord );
			capeCod.SetValue( "HighlightColor", unchecked((Int32) 0xFF400000), RegistryValueKind.DWord );
			capeCod.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFCC), RegistryValueKind.DWord );
			capeCod.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFCC), RegistryValueKind.DWord );
			capeCod.SetValue( "SquareColor2", unchecked((Int32) 0xFF5D7E7E), RegistryValueKind.DWord );
			capeCod.SetValue( "SquareColor3", unchecked((Int32) 0xFF648C8C), RegistryValueKind.DWord );
			capeCod.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			capeCod.SetValue( "PlayerColor2", unchecked((Int32) 0xFF5984BD), RegistryValueKind.DWord );
			#endregion

			#region Surrealistic Summer
			RegistryKey surrealisticSummer = colorSchemesKey.CreateSubKey( "Surrealistic Summer" );
			surrealisticSummer.SetValue( "BorderColor", unchecked((Int32) 0xFF111199), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "HighlightColor", unchecked((Int32) 0xFF111199), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "TextColor", unchecked((Int32) 0xFFFCCCC11), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "SquareColor1", unchecked((Int32) 0xFFCCCC11), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "SquareColor2", unchecked((Int32) 0xFF339933), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "SquareColor3", unchecked((Int32) 0xFF6A783A), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			surrealisticSummer.SetValue( "PlayerColor2", unchecked((Int32) 0xFF5984BD), RegistryValueKind.DWord );
			#endregion 

			#region Lesotho
			RegistryKey lesotho = colorSchemesKey.CreateSubKey( "Lesotho" );
			lesotho.SetValue( "BorderColor", unchecked((Int32) 0xFF111199), RegistryValueKind.DWord );
			lesotho.SetValue( "HighlightColor", unchecked((Int32) 0xFF111199), RegistryValueKind.DWord );
			lesotho.SetValue( "TextColor", unchecked((Int32) 0xFFFEFEDD), RegistryValueKind.DWord );
			lesotho.SetValue( "SquareColor1", unchecked((Int32) 0xFFFEFEDD), RegistryValueKind.DWord );
			lesotho.SetValue( "SquareColor2", unchecked((Int32) 0xFF22BB22), RegistryValueKind.DWord );
			lesotho.SetValue( "SquareColor3", unchecked((Int32) 0xFF319331), RegistryValueKind.DWord );
			lesotho.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			lesotho.SetValue( "PlayerColor2", unchecked((Int32) 0xFF5984BD), RegistryValueKind.DWord );
			#endregion

			#region Cinnamon
			RegistryKey cinnamon = colorSchemesKey.CreateSubKey( "Cinnamon" );
			cinnamon.SetValue( "BorderColor", unchecked((Int32) 0xFF543636), RegistryValueKind.DWord );
			cinnamon.SetValue( "HighlightColor", unchecked((Int32) 0xFF0000), RegistryValueKind.DWord );
			cinnamon.SetValue( "TextColor", unchecked((Int32) 0xFFFFEFCD), RegistryValueKind.DWord );
			cinnamon.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFEFCD), RegistryValueKind.DWord );
			cinnamon.SetValue( "SquareColor2", unchecked((Int32) 0xFFAD7665), RegistryValueKind.DWord );
			cinnamon.SetValue( "SquareColor3", unchecked((Int32) 0xFF925C4E), RegistryValueKind.DWord );
			cinnamon.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			cinnamon.SetValue( "PlayerColor2", unchecked((Int32) 0xFFEA0000), RegistryValueKind.DWord );
			#endregion

			#region Sublimation
			RegistryKey sublimation = colorSchemesKey.CreateSubKey( "Sublimation" );
			sublimation.SetValue( "BorderColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			sublimation.SetValue( "HighlightColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			sublimation.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			sublimation.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			sublimation.SetValue( "SquareColor2", unchecked((Int32) 0xFF6C7B71), RegistryValueKind.DWord );
			sublimation.SetValue( "SquareColor3", unchecked((Int32) 0xFF838A7D), RegistryValueKind.DWord );
			sublimation.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			sublimation.SetValue( "PlayerColor2", unchecked((Int32) 0xFF91C28D), RegistryValueKind.DWord );
			#endregion

			#region Sahara
			RegistryKey sahara = colorSchemesKey.CreateSubKey( "Sahara" );
			sahara.SetValue( "BorderColor", unchecked((Int32) 0xFF623100), RegistryValueKind.DWord );
			sahara.SetValue( "HighlightColor", unchecked((Int32) 0xFF623100), RegistryValueKind.DWord );
			sahara.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			sahara.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFCC), RegistryValueKind.DWord );
			sahara.SetValue( "SquareColor2", unchecked((Int32) 0xFFC08040), RegistryValueKind.DWord );
			sahara.SetValue( "SquareColor3", unchecked((Int32) 0xFFC89159), RegistryValueKind.DWord );
			sahara.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			sahara.SetValue( "PlayerColor2", unchecked((Int32) 0xFFDFC77B), RegistryValueKind.DWord );
			#endregion

			#region Orchid
			RegistryKey orchid = colorSchemesKey.CreateSubKey( "Orchid" );
			orchid.SetValue( "BorderColor", unchecked((Int32) 0xFF000071), RegistryValueKind.DWord );
			orchid.SetValue( "HighlightColor", unchecked((Int32) 0xFF000071), RegistryValueKind.DWord );
			orchid.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			orchid.SetValue( "SquareColor1", unchecked((Int32) 0xFFDED6E4), RegistryValueKind.DWord );
			orchid.SetValue( "SquareColor2", unchecked((Int32) 0xFF5555AA), RegistryValueKind.DWord );
			orchid.SetValue( "SquareColor3", unchecked((Int32) 0xFF648C8C), RegistryValueKind.DWord );
			orchid.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			orchid.SetValue( "PlayerColor2", unchecked((Int32) 0xFF9DA5FD), RegistryValueKind.DWord );
			#endregion

			#region Luna Decorabat
			RegistryKey lunaDecorabat = colorSchemesKey.CreateSubKey( "Luna Decorabat" );
			lunaDecorabat.SetValue( "BorderColor", unchecked((Int32) 0xFF576257), RegistryValueKind.DWord );
			lunaDecorabat.SetValue( "HighlightColor", unchecked((Int32) 0xFF33CC00), RegistryValueKind.DWord );
			lunaDecorabat.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFCC), RegistryValueKind.DWord );
			lunaDecorabat.SetValue( "SquareTexture1", "White Marble", RegistryValueKind.String );
			lunaDecorabat.SetValue( "SquareTexture2", "Dark Marble", RegistryValueKind.String );
			lunaDecorabat.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			lunaDecorabat.SetValue( "PlayerColor2", unchecked((Int32) 0xFFAACEBD), RegistryValueKind.DWord );
			#endregion

			#region Marmoor Quadraut
			RegistryKey marmoorQuadraut = colorSchemesKey.CreateSubKey( "Marmoor Quadraut" );
			marmoorQuadraut.SetValue( "BorderColor", unchecked((Int32) 0xFF3A1010), RegistryValueKind.DWord );
			marmoorQuadraut.SetValue( "HighlightColor", unchecked((Int32) 0xFFFFFF00), RegistryValueKind.DWord );
			marmoorQuadraut.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFCC), RegistryValueKind.DWord );
			marmoorQuadraut.SetValue( "SquareTexture1", "Orange Marble", RegistryValueKind.String );
			marmoorQuadraut.SetValue( "SquareTexture2", "Wine Marble", RegistryValueKind.String );
			marmoorQuadraut.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			marmoorQuadraut.SetValue( "PlayerColor2", unchecked((Int32) 0xFFFFFFB6), RegistryValueKind.DWord );
			#endregion

			#region Buckingham Green
			RegistryKey buckinghamGreen = colorSchemesKey.CreateSubKey( "Buckingham Green" );
			buckinghamGreen.SetValue( "BorderColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "HighlightColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFD6), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFD6), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "SquareColor2", unchecked((Int32) 0xFF9CB5A5), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "SquareColor3", unchecked((Int32) 0xFF006352), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			buckinghamGreen.SetValue( "PlayerColor2", unchecked((Int32) 0xFF5984BD), RegistryValueKind.DWord );
			#endregion

			#region Lemon Cappuccino
			RegistryKey lemonCappuccino = colorSchemesKey.CreateSubKey( "Lemon Cappuccino" );
			lemonCappuccino.SetValue( "BorderColor", unchecked( (Int32) 0xFF7B6453 ), RegistryValueKind.DWord );
			lemonCappuccino.SetValue( "HighlightColor", unchecked( (Int32) 0xFF000000 ), RegistryValueKind.DWord );
			lemonCappuccino.SetValue( "TextColor", unchecked( (Int32) 0xFFFFFFCC ), RegistryValueKind.DWord );
			lemonCappuccino.SetValue( "SquareTexture1", "Pink Marble", RegistryValueKind.String );
			lemonCappuccino.SetValue( "SquareTexture2", "Light Marble", RegistryValueKind.String );
			lemonCappuccino.SetValue( "SquareTexture3", "White Marble", RegistryValueKind.String );
			lemonCappuccino.SetValue( "PlayerColor1", unchecked( (Int32) 0xFFF1F0C5 ), RegistryValueKind.DWord );
			lemonCappuccino.SetValue( "PlayerColor2", unchecked( (Int32) 0xFFBFBBAA ), RegistryValueKind.DWord );
			#endregion

			#region Rosaliya
			RegistryKey rosaliya = colorSchemesKey.CreateSubKey( "Rosaliya" );
			rosaliya.SetValue( "BorderColor", unchecked((Int32) 0xFFA55F61), RegistryValueKind.DWord );
			rosaliya.SetValue( "HighlightColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			rosaliya.SetValue( "TextColor", unchecked((Int32) 0xFFFFFF80), RegistryValueKind.DWord );
			rosaliya.SetValue( "SquareTexture1", "Light Marble", RegistryValueKind.String );
			rosaliya.SetValue( "SquareTexture2", "Pink Marble", RegistryValueKind.String );
			rosaliya.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			rosaliya.SetValue( "PlayerColor2", unchecked((Int32) 0xFFB18384), RegistryValueKind.DWord );
			#endregion

			#region Grayscale
			RegistryKey grayscale = colorSchemesKey.CreateSubKey( "Grayscale" );
			grayscale.SetValue( "BorderColor", unchecked((Int32) 0xFF575757), RegistryValueKind.DWord );
			grayscale.SetValue( "HighlightColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			grayscale.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			grayscale.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			grayscale.SetValue( "SquareColor2", unchecked((Int32) 0xFFC0C0C0), RegistryValueKind.DWord );
			grayscale.SetValue( "SquareColor3", unchecked((Int32) 0xFFB0B0B0), RegistryValueKind.DWord );
			grayscale.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			grayscale.SetValue( "PlayerColor2", unchecked((Int32) 0xFFC8C8C8), RegistryValueKind.DWord );
			#endregion

			#region Callisto
			RegistryKey callisto = colorSchemesKey.CreateSubKey( "Callisto" );
			callisto.SetValue( "BorderColor", unchecked( (Int32) 0xFF111199 ), RegistryValueKind.DWord );
			callisto.SetValue( "HighlightColor", unchecked( (Int32) 0xFF666699 ), RegistryValueKind.DWord );
			callisto.SetValue( "TextColor", unchecked( (Int32) 0xFFEEEE22 ), RegistryValueKind.DWord );
			callisto.SetValue( "SquareColor1", unchecked( (Int32) 0xFFFFFFD5 ), RegistryValueKind.DWord );
			callisto.SetValue( "SquareColor2", unchecked( (Int32) 0xFF79B7A4 ), RegistryValueKind.DWord );
			callisto.SetValue( "SquareColor3", unchecked( (Int32) 0xFF70B0B0 ), RegistryValueKind.DWord );
			callisto.SetValue( "PlayerColor1", unchecked( (Int32) 0xFFFFFFFF ), RegistryValueKind.DWord );
			callisto.SetValue( "PlayerColor2", unchecked( (Int32) 0xFF5984BD ), RegistryValueKind.DWord );
			#endregion

			#region Valhalla
			RegistryKey valhalla = colorSchemesKey.CreateSubKey( "Valhalla" );
			valhalla.SetValue( "BorderColor", unchecked((Int32) 0xFFA0A0A0), RegistryValueKind.DWord );
			valhalla.SetValue( "HighlightColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			valhalla.SetValue( "TextColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			valhalla.SetValue( "SquareColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			valhalla.SetValue( "SquareColor2", unchecked((Int32) 0xFF008000), RegistryValueKind.DWord );
			valhalla.SetValue( "SquareColor3", unchecked((Int32) 0xFF00A000), RegistryValueKind.DWord );
			valhalla.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFF80), RegistryValueKind.DWord );
			valhalla.SetValue( "PlayerColor2", unchecked((Int32) 0xFF80FFFF), RegistryValueKind.DWord );
			#endregion

			#region Brushed Steel
			RegistryKey brushedSteel = colorSchemesKey.CreateSubKey( "Brushed Steel" );
			brushedSteel.SetValue( "BorderColor", unchecked((Int32) 0xFF555555), RegistryValueKind.DWord );
			brushedSteel.SetValue( "HighlightColor", unchecked((Int32) 0xFF804646), RegistryValueKind.DWord );
			brushedSteel.SetValue( "TextColor", unchecked((Int32) 0xFFCECECE), RegistryValueKind.DWord );
			brushedSteel.SetValue( "SquareTexture1", "Light Metal", RegistryValueKind.String );
			brushedSteel.SetValue( "SquareTexture2", "Dark Metal", RegistryValueKind.String );
			brushedSteel.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			brushedSteel.SetValue( "PlayerColor2", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			#endregion

			#region Norwegian Wood
			RegistryKey norwegianWood = colorSchemesKey.CreateSubKey( "Norwegian Wood" );
			norwegianWood.SetValue( "BorderColor", unchecked((Int32) 0xFF000000), RegistryValueKind.DWord );
			norwegianWood.SetValue( "HighlightColor", unchecked((Int32) 0xFFAA2828), RegistryValueKind.DWord );
			norwegianWood.SetValue( "TextColor", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			norwegianWood.SetValue( "SquareTexture1", "Light Wood", RegistryValueKind.String );
			norwegianWood.SetValue( "SquareTexture2", "Dark Wood", RegistryValueKind.String );
			norwegianWood.SetValue( "PlayerColor1", unchecked((Int32) 0xFFFFFFFF), RegistryValueKind.DWord );
			norwegianWood.SetValue( "PlayerColor2", unchecked((Int32) 0xFFE68D4D), RegistryValueKind.DWord );
			#endregion
			#endregion
		}

		//	The registry key that contains all the color schemes as sub-keys
		private static RegistryKey colorSchemesKey;

		//	A lookup table of all the different color schemes discovered in the Color Schemes registry key
		private static Dictionary<string, ColorScheme> colorSchemes;
	}
}
