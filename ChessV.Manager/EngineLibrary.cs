
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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace ChessV.Manager
{
	//**********************************************************************
	//
	//                         EngineLibrary
	//
	//    The maintains the library of registered engines and information 
	//    about which variants and xboard features they support.  It loads 
	//    and stores this information in the registry and handles auto-
	//    discovery of new engines.  Finally, it will determine which 
	//    engines can play a given variant, either directly or through an 
	//    EngineGameAdaptor object.

	public class EngineLibrary
	{
		public EngineLibrary()
		{
			engines = new List<EngineConfiguration>();
			timerFactory = new TimerFactory();
			discoverEnvironment();
		}

		public void InitializeEngines( RegistryKey registryKey, bool autodetectEngines )
		{
			enginesKey = registryKey;
			//	First, load in any manually dsicovered engines
			autoDiscoverEngines = false;
			discoverEngineFolder( "Manual" );
			//	Now load auto-discovered engines in usual folders
			autoDiscoverEngines = autodetectEngines;
			//	Iterate through all subfolders under Engines\XBoard and make a list
			engineDirList = new List<string>();
			string xboardEnginePath = "Engines" + Path.DirectorySeparatorChar + "XBoard";
			if( !Directory.Exists( xboardEnginePath ) )
				Directory.CreateDirectory( xboardEnginePath );
			string[] engineDirs = Directory.GetDirectories( xboardEnginePath );
			foreach( string engineDirectory in engineDirs )
				engineDirList.Add( engineDirectory );
			if( engineDirList.Count > 0 )
			{
				timer = timerFactory.NewTimer();
				timer.Interval = 100;
				timer.Tick += timerTick;
				timer.Start();
			}
		}

		public List<EngineConfigurationWithAdaptor> FindEngines( Game game )
		{
			List<EngineConfigurationWithAdaptor> supportingEngines = new List<EngineConfigurationWithAdaptor>();

			foreach( EngineConfiguration engine in engines )
			{
				if( game.GameAttribute.XBoardName != null &&
					engine.SupportedVariants.Contains( game.GameAttribute.XBoardName ) )
					//	this engine directly supports this variant
					supportingEngines.Add( new EngineConfigurationWithAdaptor( engine, null ) );
				else
				{
					//	check with the Game class to see if engine can be adapted
					EngineGameAdaptor adaptor = game.TryCreateAdaptor( engine );
					if( adaptor != null )
					{
						adaptor.Initialize( game );
						supportingEngines.Add( new EngineConfigurationWithAdaptor( engine, adaptor ) );
					}
				}
			}

			return supportingEngines;
		}

		public List<EngineConfiguration> GetAllEngines()
		{
			return engines;
		}

		public void RemoveEngine( EngineConfiguration engine )
		{
			engines.Remove( engine );
		}

		protected void timerTick( object sender, System.EventArgs e )
		{
			timer.Stop();
			if( engineDirList.Count > 0 )
			{
				string engineDir = engineDirList[0];
				if( engineExecutableList == null )
				{
					discoverEngineFolder( engineDir );
					timer.Start();
				}
				else if( engineExecutableList.Count > 0 )
				{
					string engine = engineExecutableList[0];
					engineExecutableList.RemoveAt( 0 );
					discoverExecutable( engineDir, engine );
				}
				else
				{
					engineDirList.RemoveAt( 0 );
					engineExecutableList = null;
					timer.Start();
				}
			}
		}

		protected void discoverEnvironment()
		{
			//	are we running on Windows?
			isWindows = Path.DirectorySeparatorChar == '\\';

			//	are we on a 64-bit OS?
			is64Bit = true;
			if( isWindows )
				is64Bit = SystemEnvironment.IsOS64Bit();
		}

		protected void discoverExecutable( string engineDir, string executable )
		{
			string folderName = engineDir.Substring( engineDir.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );
			//	fire it up to see what variants/features it supports
			EngineConfiguration configuration = new EngineConfiguration( folderName, executable, "xboard" );
			configuration.WorkingDirectory = engineDir;
			//	if this directory contains a file named 'variants.txt' we will 
			//	automatically pass that in as a command line argument (Sjaak II)
			if( File.Exists( engineDir + @"\variants.txt" ) )
				configuration.AddArgument( "variants.txt" );
			currentConfiguration = configuration;
			DebugMessageLog log = new DebugMessageLog();
			EngineBuilder builder = new EngineBuilder( log, configuration );
			TimerFactory timerFactory = new TimerFactory();
			XBoardEngine engine = (XBoardEngine) builder.Create( timerFactory, playerReady );
		}

		public void ManualEngineDiscover( string workingDirectory, string executable, List<string> arguments )
		{
			EngineConfiguration configuration = new EngineConfiguration( "Manual", executable, "xboard" );
			configuration.WorkingDirectory = workingDirectory;
			currentConfiguration = configuration;
			if( arguments != null )
				foreach( string arg in arguments )
					configuration.AddArgument( arg );
			DebugMessageLog log = new DebugMessageLog();
			EngineBuilder builder = new EngineBuilder( log, configuration );
			TimerFactory timerFactory = new TimerFactory();
			XBoardEngine engine = (XBoardEngine) builder.Create( timerFactory, playerReady );
		}

		protected void discoverEngineFolder( string engineDir )
		{
			string folderName = engineDir.Substring( engineDir.LastIndexOf( Path.DirectorySeparatorChar ) + 1 );
			//	Does a registry key for this engine already exist?
			RegistryKey engineFolderKey = enginesKey.OpenSubKey( folderName );
			if( engineFolderKey == null )
			{
				if( autoDiscoverEngines )
				{
					engineExecutableList = new List<string>();

					//	Look for all EXE files in this folder and determine which to discover.
					//	We discover all of them except when there are multiple files with names 
					//	that are the same except for environment designations (32, 64, etc.)

					//	This map will hold all the "root" names (environ designations removed) 
					//	and map them to the list of all EXEs.  Then we will pick the best
					Dictionary<string, List<string>> exeMap = new Dictionary<string, List<string>>();

					//	This is a list of all the name suffixes that will be considered 
					//	environment designations and be stripped to produce the root name.
					//	These must be ordered with longest (most inclusive) first
					string[] environSuffixes = new string[] { 
						"WIN32-SSE2", "WIN32-SSE3", "WIN32-SSE42", "WIN64-SSE2", "WIN64-SSE3", "WIN64-SSE42",
						"64_MS", "32_MS", "64BIT", "32BIT", "64", "32" };

					//	Look through all EXE files in the directory and categorize them
					string[] executables = Directory.GetFiles( engineDir, "*.exe" );
					foreach( string exe in executables )
					{
						string name = exe.Substring( 0, exe.Length - 4 );
						bool hasSuffix = false;
						foreach( string suffix in environSuffixes )
						{
							if( name.Length > suffix.Length &&
								name.Substring( name.Length - suffix.Length ).ToUpper() == suffix )
							{
								//	This EXE has a suffix so add it to the map
								string basename = name.Substring( 0, name.Length - suffix.Length );
								if( exeMap.ContainsKey( basename ) )
									exeMap[basename].Add( name );
								else
								{
									exeMap.Add( basename, new List<string>() );
									exeMap[basename].Add( name );
								}
								hasSuffix = true;
								break;
							}
						}
						//	If no suffix, then just add the EXE to the list for discovery
						if( !hasSuffix )
							engineExecutableList.Add( exe );
					}

					//	Look through each entry in the map and pick the best match
					foreach( KeyValuePair<string, List<string>> pair in exeMap )
					{
						string bestEXE = null;
						int bestScore = -1;
						foreach( string exe in pair.Value )
						{
							int score = 0;
							if( exe.ToUpper().IndexOf( "CHESS" ) >= 0 )
								score += 1;
							if( exe.IndexOf( "64" ) >= 0 && is64Bit )
								score += 5;
							if( exe.IndexOf( "64" ) >= 0 && !is64Bit )
								score -= 5;
							if( exe.ToUpper().IndexOf( "SSE" ) >= 0 )
								score += 2;
							if( score > bestScore )
							{
								bestScore = score;
								bestEXE = exe;
							}
						}
						engineExecutableList.Add( bestEXE + ".exe" );
					}
				}
			}
			else
			{
				//	Iterate through all numbered sub-keys and read in the engine info
				int engineNumber = 1;
				while( true )
				{
					RegistryKey engineKey = engineFolderKey.OpenSubKey( engineNumber.ToString(), true );
					if( engineKey != null )
					{
						engines.Add( new EngineConfiguration( folderName, engineKey ) );
						engineNumber++;
					}
					else
						break;
				}
				if( engineDirList != null )
				{
					//	engineDirList will be null while loading the "Manual" 
					//	registry key of manually-discovered engines
					engineDirList.RemoveAt( 0 );
					if( engineDirList.Count > 0 )
						timer.Start();
				}
			}
		}

		//	The playerReady event is raised once the engine process has started and 
		//	the initial protocol handshake has been completed.  At this point the 
		//	Engine object has learned about the engine's features by interrogating it 
		//	so we can store them in the EngineConfiguration.
		protected void playerReady( Player player )
		{
			XBoardEngine engine = (XBoardEngine) player;
			currentConfiguration.InternalName = engine.Name;
			currentConfiguration.FriendlyName = engine.Name;
			currentConfiguration.SupportedFeatures = engine.SupportedFeatures;
			currentConfiguration.SupportedVariants = engine.Variants;
			RegistryKey engineFolderKey = enginesKey.OpenSubKey( currentConfiguration.FolderName, true );
			RegistryKey engineKey = null;
			if( engineFolderKey == null )
			{
				//	This is the first engine discovered in this folder - create the folder 
				//	key and place the engine in subkey "1"
				engineFolderKey = enginesKey.CreateSubKey( currentConfiguration.FolderName );
				engineKey = engineFolderKey.CreateSubKey( "1" );
			}
			else
			{
				//	We have previously discovered engines in this folder - create a folder
				//	with the next available integer
				string[] subKeyNames = engineFolderKey.GetSubKeyNames();
				int engineNumber = 1;
				while( true )
				{
					bool found = false;
					foreach( string subKey in subKeyNames )
						if( subKey == engineNumber.ToString() )
							found = true;
					if( found )
						engineNumber++;
					else
						break;
				}
				engineKey = engineFolderKey.CreateSubKey( engineNumber.ToString() );
			}
			//	Save the information for this engine to the newly created registry key
			currentConfiguration.RegistryKey = engineKey;
			currentConfiguration.SaveToRegistry();
			engines.Add( currentConfiguration );

			if( engineDirList.Count > 0 )
				timer.Start();
		}

		public void RegistryKeyRenamed( string rootKeyName, string oldSubKeyName, string newSubKeyName )
		{
			foreach( EngineConfiguration engine in engines )
			{
				if( engine.RegistryKey.Name.Substring( engine.RegistryKey.Name.IndexOf( @"\" ) + 1 ) == 
					rootKeyName + oldSubKeyName )
				{
					RegistryKey parentKey = Registry.CurrentUser.OpenSubKey( rootKeyName );
					engine.RegistryKey = parentKey.OpenSubKey( newSubKeyName, true );
				}
			}
		}

		//	List of the EngineConfigurations for all configured engines
		protected List<EngineConfiguration> engines { get; set; }

		//	The registry key that stores our engine configuration information
		protected RegistryKey enginesKey { get; set; }

		//	The TimerFactory passed to the engines being started
		protected TimerFactory timerFactory { get; set; }

		//	A queue of the paths of the new engines that need to be 'discovered' - 
		//	that is, started and queried to determine what they support
		protected List<string> engineDirList { get; set; }

		//	A queue of the filenames of the EXEs within the current directory 
		//	that need to be 'discovered'
		protected List<string> engineExecutableList { get; set; }

		//	A timer to simulated threading - with each tick the discovery 
		//	of an engine is started
		protected Timer timer { get; set; }

		//	The EngineConfiguration object of the engine currently being discovered
		protected EngineConfiguration currentConfiguration { get; set; }

		//	Are we auto-discovering new engines?
		protected bool autoDiscoverEngines { get; set; }

		//	Are we operating on a Microsoft Windows platform?
		protected bool isWindows { get; set; }

		//	Is this computer running a 64-bit operating system?
		protected bool is64Bit { get; set; }
	}
}
