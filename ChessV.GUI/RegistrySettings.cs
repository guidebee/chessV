
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
	public static class RegistrySettings
	{
		// *** PROPERTIES *** //

		//	RegistryKey of the root of all ChessV settings
		public static RegistryKey RegistryKey { get; private set; }

		//	The version of registry settings (this is different than 
		//	the version of ChessV itself - it only changes when the 
		//	way we store registry information changes)
		public static int RegistryVersion { get; private set; }

		//	Do we automatically detect new engines at startup?
		static public bool AutodetectNewEngines
		{
			get
			{ return autodetectNewEngines; }

			set
			{
				autodetectNewEngines = value;
				RegistryKey.SetValue( "AutodetectEngines", value ? 1 : 0 );
			}
		}


		// *** INITIALIZATION *** //

		public static void Initialize()
		{
			//	Find the ChessV key in HKEY_CURRENT_USER\Software (create if necessary)
			RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey( "Software", true );
			if( softwareKey == null )
				softwareKey = Registry.CurrentUser.CreateSubKey( "Software" );
			RegistryKey = softwareKey.OpenSubKey( "ChessV", true );
			if( RegistryKey == null )
			{
				RegistryKey = softwareKey.CreateSubKey( "ChessV" );
				RegistryKey.SetValue( "RegistryVersion", 1, RegistryValueKind.DWord );
				AutodetectNewEngines = Program.RunningOnWindows;
			}
			else
			{
				object version = RegistryKey.GetValue( "RegistryVersion" );
				if( version == null )
				{
					if( RegistryKey.OpenSubKey( "Color Schemes" ) != null )
						RegistryKey.DeleteSubKeyTree( "Color Schemes" );
					if( RegistryKey.OpenSubKey( "Games" ) != null )
						RegistryKey.DeleteSubKeyTree( "Games" );
					RegistryKey.SetValue( "RegistryVersion", 1, RegistryValueKind.DWord );
					RegistryVersion = 1;
				}
				else
				{
					RegistryVersion = (int) version;
					object autodetect = RegistryKey.GetValue( "AutodetectEngines" );
					if( autodetect != null && (int) autodetect == 0 )
						autodetectNewEngines = false;
					else
						autodetectNewEngines = true;
				}
			}
		}


		// *** PRIVATE DATA *** //

		static private bool autodetectNewEngines;
	}
}
