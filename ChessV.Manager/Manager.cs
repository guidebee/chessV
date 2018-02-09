
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
using System.Reflection;
using System.IO;
using System.Threading;
using ChessV;

namespace ChessV.Manager
{
	/************************************************************************

	                                 Manager
	
	The Manager class contains a number of indexes cataloging all the types 
	of Games that have been defined, all the PieceTypes, and all the XBoard 
	protocol engines.  It also contains the code creating new instances 
	of Games and Engines.
	
    ************************************************************************/

	public class Manager
	{
		// *** PUBLIC MEMBERS *** //

		//	A lookup table of all the Game-derived classes discovered that implement different variants
		public Dictionary<string, Type> GameClasses;

		//	A lookup table of all the GameAttribute attributes attached to game classes discovered indexed by game name
		public Dictionary<string, GameAttribute> GameAttributes;

		//	A library of all the pre-defined (built in) piece types
		static public PieceTypeLibrary PieceTypeLibrary;

		//	The main Environment (for running user scripts in the interpereter)
		public ChessV.Compiler.Environment Environment;

		//	The library of all XBoard/Winboard engines discovered in the Engines\XBoard directory available for use
		public EngineLibrary EngineLibrary;

		//	A bogus record to designate the internal chess engine
		public EngineConfiguration InternalEngine;


		// *** CONSTRUCTION *** //

		#region Constructor
		public Manager()
		{
			#region Create Member Objects
			GameClasses = new Dictionary<string, Type>();
			GameAttributes = new Dictionary<string, GameAttribute>();
			Environment = new Compiler.Environment();
			PieceTypeLibrary = new PieceTypeLibrary();
			EngineLibrary = new EngineLibrary();
			InternalEngine = new EngineConfiguration();
			#endregion

			#region Load Internal Games

			// *** LOAD INTERNAL GAMES *** //

			//	Load games and piece types from the main ChessV.Base module
			Module module = typeof(Game).Module;
			loadPieceTypesFromModule( module );
			loadGamesFromModule( module );

			//	Load games and piece types from the ChessV.Games DLL
			string gamesDllName = module.FullyQualifiedName.Substring( 0, module.FullyQualifiedName.LastIndexOf( '\\' ) + 1 ) + "ChessV.Games.dll";
			Assembly gamesAssembly = Assembly.LoadFile( gamesDllName );
			foreach( Module gamesModule in gamesAssembly.GetModules() )
			{
				loadPieceTypesFromModule( (Module) gamesModule );
				loadGamesFromModule( (Module) gamesModule );
				loadPieceTypePropertyAttributesFromModule( (Module) gamesModule );
			}
			#endregion

			#region Load Games from Include Folder

			// *** LOAD GAMES FROM INCLUDE FOLDER *** //

			AppDomain myDomain = Thread.GetDomain();
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = "DynamicGamesAssembly";
			System.Reflection.Emit.AssemblyBuilder assemblyBuilder = myDomain.DefineDynamicAssembly( assemblyName, System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave );
			System.Reflection.Emit.ModuleBuilder dynamicModule = assemblyBuilder.DefineDynamicModule( "ChessVDynamicGames" );
			Compiler.Compiler compiler = new Compiler.Compiler( assemblyBuilder, dynamicModule, Environment );
			string[] includeFiles = Directory.GetFiles( "Include", "*.cvc" );
			foreach( string file in includeFiles )
			{
				TextReader reader = new StreamReader( file );
				compiler.ProcessInput( reader );
				reader.Close();
			}
			foreach( KeyValuePair<string, Type> pair in compiler.GameTypes )
			{
				GameClasses.Add( pair.Key, pair.Value );
				GameAttributes.Add( pair.Key, compiler.GameAttributes[pair.Key] );
			}
			#endregion
		}
		#endregion


		// *** OPERATIONS *** //

		#region CreateGame
		public Game CreateGame
			( string name,                                   //  name of game to create
			  Dictionary<string, string> definitions = null, //  optional dict of defined Game Variables
			  InitializationHelper initHelper = null )       //  optional init helper (null if non-interactive)
		{
			//	look up the Type of the requested Game class
			Type gameClass = GameClasses[name];

			//	look up the GameAttribute of the requested game
			GameAttribute gameAttribute = GameAttributes[name];

			//	use reflection to find the default constructor
			ConstructorInfo ci = gameClass.GetConstructor( new Type[] { } );

			//	invoke the constructor to create the game object
			Game newgame = (Game) ci.Invoke( null );

			//	find the Game's Environment field and set it to a newly 
			//	constructed interpreter/compiler Environment with this Manager's 
			//	global Environment as the parent Environment.
			FieldInfo environmentField = newgame.GetType().GetField( "Environment" );
			if( environmentField != null )
				environmentField.SetValue( newgame, new Compiler.Environment( Environment ) );

			//	initialize the Game with the Initialize method - this is essential
			newgame.Initialize( gameAttribute, definitions, initHelper );

			//	return newly constructed Game object
			return newgame;
		}
		#endregion

		#region LoadGame
		public Game LoadGame( TextReader reader )
		{
			//	create a SavedGameReader which does most of the work
			SavedGameReader savedGame = new SavedGameReader( reader );

			//	Find the name of the internally-defined Game we should be creating. 
			//	Usually, this is the name specified in the saved game file, but it 
			//	also supports creating new named Games derived from existing games. 
			//	In this case, we want the name of the existing base Game.
			string gameName;
			if( GameAttributes.ContainsKey( savedGame.GameName ) )
				gameName = savedGame.GameName;
			else if( savedGame.BaseGameName != null && GameAttributes.ContainsKey( savedGame.BaseGameName ) )
				gameName = savedGame.BaseGameName;
			else
				throw new Exception( "Saved game file specifies unknown variant: " + savedGame.GameName );

			//	create the Game object now that we have determined the name
			Game loadedGame = CreateGame( gameName, savedGame.VariableDefinitions );

			//	play out any moves saved with the game
			loadedGame.PlayMoves( savedGame.Moves );

			//	return the new Game object
			return loadedGame;
		}
		#endregion


		// *** HELPER FUNCTIONS *** //

		#region loadGamesFromModule
		protected void loadGamesFromModule( Module module )
		{
			Type[] types = module.GetTypes();
			foreach( Type type in types )
			{
				object[] customAttrs = type.GetCustomAttributes( typeof( GameAttribute ), false );
				if( customAttrs != null && customAttrs.Length >= 1 )
					foreach( object attr in customAttrs )
					{
						GameAttribute gameAttribute = (GameAttribute) attr;
						GameClasses.Add( gameAttribute.GameName, type );
						GameAttributes.Add( gameAttribute.GameName, gameAttribute );
						Environment.AddSymbol( gameAttribute.GameName, type );
					}
			}
		}
		#endregion

		#region loadPieceTypesFromModule
		protected void loadPieceTypesFromModule( Module module )
		{
			Type[] types = module.GetTypes();
			foreach( Type type in types )
			{
				object[] customAttrs = type.GetCustomAttributes( typeof(PieceTypeAttribute), false );
				if( customAttrs != null && customAttrs.Length >= 1 )
					foreach( object attr in customAttrs )
					{
						PieceTypeAttribute pieceTypeAttribute = (PieceTypeAttribute) attr;
						PieceTypeLibrary.Add( pieceTypeAttribute.Name, type );
						Environment.AddSymbol( pieceTypeAttribute.Name, type );
					}
			}
		}
		#endregion

		#region loadPieceTypePropertyAttributesFromModule
		protected void loadPieceTypePropertyAttributesFromModule( Module module )
		{
			Type[] types = module.GetTypes();
			foreach( Type type in types )
			{
				if( type.IsSubclassOf( typeof(PieceTypePropertyAttribute) ) )
					Environment.AddSymbol( type.Name, type );
			}
		}
		#endregion
	}
}
