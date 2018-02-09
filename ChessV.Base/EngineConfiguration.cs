
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG
  
  THIS FILE DERIVED FROM CUTE CHESS BY ILARI PIHLAJISTO AND ARTO JONSSON

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

namespace ChessV
{
	public class EngineConfiguration
	{
		//	Helper enum that pecifies the available modes for specifying 
		//	engine restart between matches
		public enum RestartMode
		{
			On,		//	The engine is always restarted between games
			Off,	//	The engine is never restarted between games
			Auto	//	The engine decides whether to restart
		}


		// *** PROPERTIES *** //

		#region Properties
		//	The engine's name (by folder name for auto-detected engines)
		public string FriendlyName { get; set; }

		//	The engine's internal name
		public string InternalName { get; set; }

		//	The engine's folder name
		public string FolderName { get; set; }

		//	The command which is used to launch the engine
		public string Command { get; set; }

		//	The working directory the engine uses
		public string WorkingDirectory { get; set; }

		//	The communication protocol the engine uses
		public string Protocol { get; set; }

		//	The command line arguments sent to the engine
		public List<string> Arguments { get; set; }

		//	The initialization strings to send to the engine
		public List<string> InitStrings { get; set; }

		//	List of the chess variants the engine can play
		public List<string> SupportedVariants { get; set; }

		//	List of the XBoard features that the engine supports
		public List<string> SupportedFeatures { get; set; }

		//	The options sent to the engine
		public List<EngineOption> EngineOptions { get; set; }

		//	Is the evaluation always from white's point-of-view?
		public bool WhiteEvalPov { get; set; }

		//	The restart mode of the engine (restart between games?)
		public RestartMode Restart { get; set; }

		//	True if result claims from the engine are validated;
		//	With validation on (the default) the engine will forfeit the
		//	game if it makes an incorrect result claim.
		public bool ClaimsValidated { get; set; }

		//	The RegistryKey that stores these settings
		public RegistryKey RegistryKey { get; set; }
		#endregion


		//	Creates an empty chess engine configuration.
		public EngineConfiguration()
		{
			WhiteEvalPov = false;
			ClaimsValidated = true;
			Restart = RestartMode.Auto;
			Arguments = new List<string>();
			InitStrings = new List<string>();
			SupportedVariants = new List<string>();
			SupportedFeatures = new List<string>();
			EngineOptions = new List<EngineOption>();
		}

		//	Creates a new chess engine configuration with specified 
		//	name, command and protocol settings.
		public EngineConfiguration
			( string name,
			  string command,
			  string protocol )
		{
			FolderName = name;
			FriendlyName = "XboardEngine";
			InternalName = "XboardEngine";
			Command = command;
			Protocol = protocol;
			WhiteEvalPov = false;
			ClaimsValidated = true;
			Restart = RestartMode.Auto;
			Arguments = new List<string>();
			InitStrings = new List<string>();
			SupportedVariants = new List<string>();
			SupportedFeatures = new List<string>();
			EngineOptions = new List<EngineOption>();
		}

		//	Loads the engine config from the specified registry key
		public EngineConfiguration
			( string name,
			  RegistryKey regkey )
		{
			RegistryKey = regkey;
			FolderName = name;
			LoadFromRegistry();
		}

		//	Loads or reloads from the registry
		public void LoadFromRegistry()
		{
			WhiteEvalPov = false;
			ClaimsValidated = true;
			Arguments = new List<string>();
			InitStrings = new List<string>();
			EngineOptions = new List<EngineOption>();

			InternalName = (string) RegistryKey.GetValue( "InternalName" );
			FriendlyName = (string) RegistryKey.GetValue( "FriendlyName" );
			Command = (string) RegistryKey.GetValue( "Command" );
			WorkingDirectory = (string) RegistryKey.GetValue( "WorkingDir" );
			Protocol = (string) RegistryKey.GetValue( "Protocol" );
			string allVariants = (string) RegistryKey.GetValue( "Variants" );
			if( allVariants != null )
				SupportedVariants = split( allVariants, ',' );
			string allFeatures = (string) RegistryKey.GetValue( "Features" );
			if( allFeatures != null )
				SupportedFeatures = split( allFeatures, ',' );
			string allArguments = (string) RegistryKey.GetValue( "Arguments" );
			if( allArguments != null )
				Arguments = split( allArguments, ',' );
			string allInitStrings = (string) RegistryKey.GetValue( "InitStrings" );
			if( allInitStrings != null )
				InitStrings = split( allInitStrings, ',' );
			string restart = (string) RegistryKey.GetValue( "RestartMode" );
			if( restart == "On" )
				Restart = RestartMode.On;
			else if( restart == "Off" )
				Restart = RestartMode.Off;
			else
				Restart = RestartMode.Auto;
		}

		//	Saves the EngineConfiguration to the registry
		public void SaveToRegistry()
		{
			if( RegistryKey == null )
				throw new Exception( "ERROR: EngineConfiguration.UpdateSettings - RegistryKey is null" );
			RegistryKey.SetValue( "FriendlyName", FriendlyName );
			RegistryKey.SetValue( "InternalName", InternalName );
			RegistryKey.SetValue( "Command", Command );
			RegistryKey.SetValue( "WorkingDir", WorkingDirectory );
			RegistryKey.SetValue( "Protocol", Protocol );
			RegistryKey.SetValue( "Variants", concatenate( SupportedVariants, ',' ) );
			RegistryKey.SetValue( "Features", concatenate( SupportedFeatures, ',' ) );
			RegistryKey.SetValue( "Arguments", concatenate( Arguments, ',' ) );
			RegistryKey.SetValue( "InitStrings", concatenate( InitStrings, ',' ) );
			RegistryKey.SetValue( "RestartMode", Restart == RestartMode.Auto ? "Auto" : (Restart == RestartMode.Off ? "Off" : "On") );
		}

		//	Adds new command line argument
		public void AddArgument( string argument )
		{
			if( Arguments == null )
				Arguments = new List<string>();
			Arguments.Add( argument );
		}

		//	Removes a command line argument
		public void RemoveArgument( string argument )
		{
			if( Arguments != null )
				Arguments.Remove( argument );
		}

		//	Adds new initialization string
		public void AddInitString( string initString )
		{
			if( InitStrings == null )
				InitStrings = new List<string>();
			string[] split = initString.Split( '\n' );
			foreach( string str in split )
				InitStrings.Add( str );
		}

		//	Removes an init string
		public void RemoveInitString( string initString )
		{
			if( InitStrings != null )
				InitStrings.Remove( initString );
		}

		//	Adds new option
		public void AddOption( EngineOption option )
		{
			if( EngineOptions == null )
				EngineOptions = new List<EngineOption>();
			EngineOptions.Add( option );
		}

		//	Sets the given option to the given value.
		//	If an option with the given name doesn't exist, a new
		//	EngineTextOption object is added to the configuration.
		public void SetOption( string name, object value )
		{
			foreach( EngineOption option in EngineOptions )
			{
				if( option.Name == name )
				{
					if( !option.IsValid( value ) )
					{
//						qWarning("Invalid value for engine option %s: %s",
//							 qPrintable(name),
//							 qPrintable(value.toString()));
					}
				}
				else
					option.Value = value;
			}
		}


		// *** HELPER FUNCTIONS *** //

		//	Concatenate a list of strings together with the given delimiter
		protected string concatenate( List<string> stringlist, char delim )
		{
			string concat = "";
			foreach( string str in stringlist )
			{
				if( concat.Length > 0 )
					concat += delim;
				concat += str;
			}
			return concat;
		}

		//	Splits a string on the given delimiter and returns a list of strings
		protected List<string> split( string concatenatedString, char delim )
		{
			string[] parts = concatenatedString.Split( delim );
			List<string> returnval = new List<string>();
			foreach( string part in parts )
				if( part != "" )
					returnval.Add( part );
			return returnval;
		}
	}
}
