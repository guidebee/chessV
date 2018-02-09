
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
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Windows.Forms;

namespace ChessV.Compiler
{
	public class Environment
	{
		// *** CONSTRUCTION *** //

		public Environment( Environment parentEnvironment = null )
		{
			ParentEnvironment = parentEnvironment;
			typeSymbols = new Dictionary<string, Type>();
			typeConstructorSignatures = new Dictionary<string, Type[]>();
		}


		// *** PROPERTIES *** //

		public Environment ParentEnvironment { get; protected set; }


		// *** OPERATIONS *** //

		public void AddSymbol( string name, Type type )
		{
			typeSymbols.Add( name, type );
		}

		public void ExecuteMemberFunction( object obj, string functionName )
		{
			FieldInfo fi = obj.GetType().GetField( "FunctionCodeLookup", BindingFlags.Public | BindingFlags.Static );
			Dictionary<string, Antlr4.Runtime.ParserRuleContext> functionCodeLookup;
			functionCodeLookup = (Dictionary<string, Antlr4.Runtime.ParserRuleContext>) fi.GetValue( null );
			Antlr4.Runtime.ParserRuleContext ruleContext = functionCodeLookup[functionName];
			InterpreterVisitor visitor = new InterpreterVisitor( this, obj );
			visitor.Parse( ruleContext );
		}

		public object InstantiateType( string typename, object[] constructorArgs )
		{
			Type objtype = null;
			Environment searchEnv = this;
			while( objtype == null && searchEnv != null )
			{
				if( searchEnv.typeSymbols.ContainsKey( typename ) )
					objtype = searchEnv.typeSymbols[typename];
				else
					searchEnv = searchEnv.ParentEnvironment;
			}
			Type[] constructorArgTypes = null;
			constructorArgTypes = searchEnv.typeConstructorSignatures[typename];
			ConstructorInfo constructor = objtype.GetConstructor( BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public, null,
				constructorArgTypes, null );
			object newobj = constructor.Invoke( constructorArgs );

			return newobj;
		}

		public object LookupSymbol( string symbol )
		{
			if( typeSymbols.ContainsKey( symbol ) )
				return typeSymbols[symbol];
			return null;
		}


		// *** PROTECTED DATA *** //

		protected Dictionary<string, Type> typeSymbols;
		protected Dictionary<string, Type[]> typeConstructorSignatures;
	}
}
