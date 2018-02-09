
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
using System.Reflection;
using System.Reflection.Emit;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace ChessV.Compiler
{
	public class Compiler
	{
		public Compiler( AssemblyBuilder assembly, ModuleBuilder module, Environment globalEnvironment )
		{
			assemblyBuilder = assembly;
			moduleBuilder = module;
			this.globalEnvironment = globalEnvironment;
			classFactory = new ClassFactory();
			PieceTypes = new Dictionary<string,Type>();
			GameTypes = new Dictionary<string, Type>();
			GameAttributes = new Dictionary<string, GameAttribute>();
		}

		public void ProcessInput( TextReader input )
		{
			AntlrInputStream instream = new AntlrInputStream( input );
			ChessVCLexer lexx = new ChessVCLexer( instream );
			CommonTokenStream tokens = new CommonTokenStream( lexx );
			ChessVCParser parser = new ChessVCParser( tokens );
			var chunkContext = parser.chunk();
			CompilerVisitor visitor = new CompilerVisitor( this );
			visitor.Visit( chunkContext );
		}

		public void AddPieceType( PartialDefinition partial )
		{
			Type newtype = classFactory.CreatePieceTypeWrapper( moduleBuilder, typeof(PieceType), partial );
			PieceTypes.Add( partial.Name, newtype );
			globalEnvironment.AddSymbol( partial.Name, PieceTypes[partial.Name] );
		}

		public void AddGame( PartialDefinition partial )
		{
			object baseObj = globalEnvironment.LookupSymbol( partial.BaseName );
			if( baseObj == null )
				throw new Exception( "Specified base game is unknown: " + partial.BaseName );
			if( !(baseObj is Type) )
				throw new Exception( "Specified base game is not of correct type: " + partial.BaseName );
			Type baseGameType = (Type) baseObj;
			if( !baseGameType.IsSubclassOf( typeof(Game) ) )
				throw new Exception( "Specified base game is not of correct type: " + partial.BaseName );
			Type newGameType = classFactory.CreateGameWrapper( moduleBuilder, baseGameType, partial );
			object[] attrobjs = baseGameType.GetCustomAttributes( typeof(GameAttribute), false );
			GameAttribute baseGameAttribute = (GameAttribute) attrobjs[0];
			GameAttribute newGameAttribute = new GameAttribute( partial.Name, baseGameAttribute.GeometryType, baseGameAttribute.GeometryParameters );
			if( partial.VariableAssignments.ContainsKey( "Invented" ) )
				newGameAttribute.Invented = (string) partial.VariableAssignments["Invented"];
			if( partial.VariableAssignments.ContainsKey( "InventedBy" ) )
				newGameAttribute.InventedBy = (string) partial.VariableAssignments["InventedBy"];
			if( partial.VariableAssignments.ContainsKey( "Tags" ) )
				newGameAttribute.Tags = (string) partial.VariableAssignments["Tags"];
			else
				newGameAttribute.Tags = "";
			GameTypes.Add( partial.Name, newGameType );
			GameAttributes.Add( partial.Name, newGameAttribute );
		}

		public Dictionary<string, Type> PieceTypes { get; private set; }
		public Dictionary<string, Type> GameTypes { get; private set; }
		public Dictionary<string, GameAttribute> GameAttributes { get; private set; }

		protected ClassFactory classFactory;
		protected ModuleBuilder moduleBuilder;
		protected AssemblyBuilder assemblyBuilder;
		protected Environment globalEnvironment;
	}
}
