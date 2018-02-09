
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
using System.Reflection.Emit;

namespace ChessV.Compiler
{
	public class ClassFactory
	{
		public ClassFactory()
		{
			nextGameNumber = 1;
			nextPieceTypeNumber = 1;
		}

		public Type CreateGameWrapper( ModuleBuilder module, Type baseGameType, PartialDefinition partial )
		{
			//	Start building new type
			TypeBuilder typeBuilder = module.DefineType( getNextDynamicGameName(), TypeAttributes.Class | TypeAttributes.Public, baseGameType );

			// *** CREATE CONSTRUCTOR *** //
			Type[] constructorArgs = new Type[] { };
			var baseClassConstructor = baseGameType.GetConstructor( BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
				constructorArgs, null );
			ConstructorInfo baseSymmetryConstructor = null;
			if( baseClassConstructor == null )
			{
				baseClassConstructor = baseGameType.GetConstructor( BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
					new Type[] { typeof(Symmetry) }, null );
				if( baseClassConstructor != null )
				{
					Type baseSymmetryType = partial.VariableAssignments["Symmetry"].GetType();
					baseSymmetryConstructor = baseSymmetryType.GetConstructor( constructorArgs );
					if( baseSymmetryConstructor == null )
						throw new Exception( "Abstract Game requires a Symmetry to be defined" );
				}
				else
					throw new Exception( "The base Game class is not supported - too many undefined parameters" );
			}
			var constructorBuilder = typeBuilder.DefineConstructor( MethodAttributes.Public, CallingConventions.Standard, constructorArgs );
			var ilGenerator = constructorBuilder.GetILGenerator();
			ilGenerator.Emit( OpCodes.Ldarg_0 );
			if( baseSymmetryConstructor != null )
				ilGenerator.Emit( OpCodes.Newobj, baseSymmetryConstructor );
			ilGenerator.Emit( OpCodes.Call, baseClassConstructor );
			ilGenerator.Emit( OpCodes.Nop );
			ilGenerator.Emit( OpCodes.Nop );
			//	Add any necessary instructions to initialize data members
			generateConstructorCode( ilGenerator, baseGameType, partial.VariableAssignments );
			ilGenerator.Emit( OpCodes.Ret );


			// *** ADD GAME VARIABLE MEMBERS *** //
			foreach( KeyValuePair<string, Type> pair in partial.MemberVariableDeclarations )
			{
				//	Create a property for each of these with private member, getter, 
				//	and setter methods and apply the GameVariableAttribute to the property

				//	create private field to hold value
				string fieldname = "_" + pair.Key.ToLower();
				var field = typeBuilder.DefineField( fieldname, pair.Value, FieldAttributes.Private );
				//	create property
				var propertyBuilder = typeBuilder.DefineProperty( pair.Key, PropertyAttributes.None, pair.Value, null );
				//	create getter method
				var getter = typeBuilder.DefineMethod( "get_" + pair.Key, MethodAttributes.Public, pair.Value, Type.EmptyTypes );
				var il = getter.GetILGenerator();
				il.Emit( OpCodes.Ldarg_0 );        // Push "this" on the stack
				il.Emit( OpCodes.Ldfld, field );   // Load the field "_name"
				il.Emit( OpCodes.Ret );            // Return
				propertyBuilder.SetGetMethod( getter );
				//	create setter method
				var setter = typeBuilder.DefineMethod( "set_" + pair.Key, MethodAttributes.Public, null, new Type[] { pair.Value } );
				il = setter.GetILGenerator();
				il.Emit( OpCodes.Ldarg_0 );        // Push "this" on the stack
				il.Emit( OpCodes.Ldarg_1 );        // Push "value" on the stack
				il.Emit( OpCodes.Stfld, field );   // Set the field "_name" to "value"
				il.Emit( OpCodes.Ret );            // Return
				propertyBuilder.SetSetMethod( setter );
				ConstructorInfo ci = typeof(GameVariableAttribute).GetConstructor( Type.EmptyTypes );
				CustomAttributeBuilder cab = new CustomAttributeBuilder( ci, new object[] { } );
				propertyBuilder.SetCustomAttribute( cab );
			}


			// *** ADD ENVIRONMENT FIELD *** //
			FieldBuilder environmentField = typeBuilder.DefineField( "Environment", typeof(Environment), FieldAttributes.Public );

			// *** ADD FUNCTION CODE LOOKUP FIELD *** //
			FieldBuilder functionCodeLookupField = typeBuilder.DefineField( "FunctionCodeLookup", typeof(Dictionary<string, Antlr4.Runtime.ParserRuleContext>), FieldAttributes.Public | FieldAttributes.Static );


			// *** OVERLOADED FUNCTIONS *** //
			foreach( KeyValuePair<string, Antlr4.Runtime.ParserRuleContext> pair in partial.FunctionDeclarations )
			{
				string functionName = pair.Key;
				generateFunctionWrapper( typeBuilder, baseGameType, environmentField, functionCodeLookupField, functionName );
			}

			//	Complete type creation
			Type newtype = typeBuilder.CreateType();

			//	Assign the static function lookup member
			FieldInfo fi = newtype.GetField( "FunctionCodeLookup", BindingFlags.Public | BindingFlags.Static );
			fi.SetValue( null, partial.FunctionDeclarations );

			return newtype;
		}

		public Type CreatePieceTypeWrapper( ModuleBuilder module, Type basePieceType, PartialDefinition partial )
		{
			//	Start building new type
			TypeBuilder typeBuilder = module.DefineType( getNextDynamicPieceTypeName(), TypeAttributes.Class | TypeAttributes.Public, basePieceType );

			// *** CREATE CONSTRUCTOR *** //
			Type[] constructorArgs = new Type[] { typeof(string), typeof(string), typeof(int), typeof(int), typeof(string) };
			Type[] baseConstructorArgs = new Type[] { typeof(string), typeof(string), typeof(string), typeof(int), typeof(int), typeof(string) };
			var baseClassConstructor = basePieceType.GetConstructor( BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic, null,
				baseConstructorArgs, null );
			var constructorBuilder = typeBuilder.DefineConstructor( MethodAttributes.Public, CallingConventions.Standard, constructorArgs );
			var ilGenerator = constructorBuilder.GetILGenerator();
			ilGenerator.Emit( OpCodes.Ldarg_0 );
			ilGenerator.Emit( OpCodes.Ldstr, partial.Name );
			ilGenerator.Emit( OpCodes.Ldarg_1 );
			ilGenerator.Emit( OpCodes.Ldarg_2 );
			ilGenerator.Emit( OpCodes.Ldarg_3 );
			ilGenerator.Emit( OpCodes.Ldarg_S, 4 );
			ilGenerator.Emit( OpCodes.Ldarg_S, 5 );
			ilGenerator.Emit( OpCodes.Call, baseClassConstructor );
			ilGenerator.Emit( OpCodes.Nop );
			ilGenerator.Emit( OpCodes.Nop );
			//	Add any necessary instructions to initialize data members
			generateConstructorCode( ilGenerator, basePieceType, partial.VariableAssignments );
			ilGenerator.Emit( OpCodes.Ret );


			// *** ADD ENVIRONMENT FIELD *** //
			FieldBuilder environmentField = typeBuilder.DefineField( "Environment", typeof(Environment), FieldAttributes.Public );

			// *** ADD FUNCTION CODE LOOKUP FIELD *** //
			FieldBuilder functionCodeLookupField = typeBuilder.DefineField( "FunctionCodeLookup", typeof(Dictionary<string, Antlr4.Runtime.ParserRuleContext>), FieldAttributes.Public | FieldAttributes.Static );


			// *** OVERLOADED FUNCTIONS *** //
			foreach( KeyValuePair<string, Antlr4.Runtime.ParserRuleContext> pair in partial.FunctionDeclarations )
			{
				string functionName = pair.Key;
				generateFunctionWrapper( typeBuilder, basePieceType, environmentField, functionCodeLookupField, functionName );
			}

			//	Complete type creation
			Type newtype = typeBuilder.CreateType();

			//	Assign the static function lookup member
			FieldInfo fi = newtype.GetField( "FunctionCodeLookup", BindingFlags.Public | BindingFlags.Static );
			fi.SetValue( null, partial.FunctionDeclarations );

			return newtype;
		}

		protected void generateFunctionWrapper( TypeBuilder typeBuilder, Type baseType, FieldBuilder environmentField, FieldBuilder functionCodeLookupField, string functionName )
		{
			MemberInfo[] membersWithName = baseType.GetMember( functionName );
			if( membersWithName.Length == 0 )
				throw new Exception( "Unknown function: " + functionName );
			if( membersWithName.Length > 1 )
				throw new Exception( "Ambiguous function: " + functionName );
			if( membersWithName[0].MemberType != MemberTypes.Method )
				throw new Exception( "Identifier does not denote a function: " + functionName );
			MethodInfo mi = (MethodInfo) membersWithName[0];
			if( mi.IsStatic )
			{
				//	verify arguments
				ParameterInfo[] parameters = mi.GetParameters();
				if( parameters.Length > 1 )
					throw new Exception( "Function contains unexpected arguments: " + functionName );
				if( parameters.Length == 1 )
				{
					ParameterInfo pi = parameters[0];
					if( pi.ParameterType != baseType && !pi.ParameterType.IsSubclassOf( baseType ) )
						throw new Exception( "Function contains unexpected arguments: " + functionName );
					MethodBuilder mb = typeBuilder.DefineMethod( functionName, MethodAttributes.Public );
					ILGenerator generator = mb.GetILGenerator();
					//	call the base class implementation
					generator.Emit( OpCodes.Ldarg_0 );
					generator.EmitCall( OpCodes.Call, mi, null );
					//	call the intperpreter to handle function execution
					generator.Emit( OpCodes.Ldarg_0 );
					generator.Emit( OpCodes.Ldfld, environmentField );
					generator.Emit( OpCodes.Ldarg_0 );
					generator.Emit( OpCodes.Ldstr, functionName );
					MethodInfo environmentExecuteCode = typeof(Environment).GetMethod( "ExecuteMemberFunction" );
					generator.Emit( OpCodes.Callvirt, environmentExecuteCode );
					generator.Emit( OpCodes.Ret );
				}
			}
			else
			{
				ParameterInfo[] parameters = mi.GetParameters();
				if( parameters.Length > 0 )
					throw new Exception( "Function contains unexpected arguments: " + functionName );
				MethodBuilder mb = typeBuilder.DefineMethod( functionName, MethodAttributes.Public | MethodAttributes.Virtual );
				ILGenerator generator = mb.GetILGenerator();
				//	call the base class implementation
				generator.Emit( OpCodes.Ldarg_0 );
				generator.EmitCall( OpCodes.Call, mi, null );
				//	call the intperpreter to handle function execution
				generator.Emit( OpCodes.Ldarg_0 );
				generator.Emit( OpCodes.Ldfld, environmentField );
				generator.Emit( OpCodes.Ldarg_0 );
				generator.Emit( OpCodes.Ldstr, functionName );
				MethodInfo environmentExecuteCode = typeof(Environment).GetMethod( "ExecuteMemberFunction" );
				generator.Emit( OpCodes.Callvirt, environmentExecuteCode );
				generator.Emit( OpCodes.Ret );
			}
		}

		protected void generateConstructorCode( ILGenerator generator, Type baseType, Dictionary<string, object> members )
		{
			foreach( KeyValuePair<string, object> pair in members )
			{
				string memberName = pair.Key;
				object memberObject = pair.Value;

				MemberInfo[] membersWithName = baseType.GetMember( memberName );
				if( membersWithName.Length == 0 )
					throw new Exception( "Unknown member: " + memberName );
				else if( membersWithName.Length > 1 )
					throw new Exception( "Multiple members with name: " + memberName );
				MemberInfo mi = membersWithName[0];
				if( mi.MemberType != MemberTypes.Field && mi.MemberType != MemberTypes.Property )
					throw new Exception( "Attempt to assign member that is not assignable: " + memberName );
				Type variableType = mi.MemberType == MemberTypes.Field ? ((FieldInfo) mi).FieldType : ((PropertyInfo) mi).PropertyType;
				if( variableType == typeof(int) )
				{
					if( memberObject.GetType() != typeof( int ) )
						throw new Exception( "Type mismatch assigning member " + memberName + " - required type is int" );

					if( mi.MemberType == MemberTypes.Property )
					{
						PropertyInfo pi = (PropertyInfo) mi;
						generator.Emit( OpCodes.Ldarg_0 );
						generator.Emit( OpCodes.Ldc_I4, (int) memberObject );
						MethodInfo setter = pi.GetSetMethod( true );
						if( setter == null )
							throw new Exception( "Property is not assignable: " + memberName );
						generator.EmitCall( OpCodes.Call, setter, null );
					}
					else
					{
						FieldInfo fi = (FieldInfo) mi;
						generator.Emit( OpCodes.Ldarg_0 );
						generator.Emit( OpCodes.Ldc_I4, (int) memberObject );
						generator.Emit( OpCodes.Stfld, fi );
					}
				}
				else if( variableType == typeof(string) )
				{
					if( memberObject.GetType() != typeof(string) )
						throw new Exception( "Type mismatch assigning member " + memberName + " - required type is string" );

					if( mi.MemberType == MemberTypes.Property )
					{
						PropertyInfo pi = (PropertyInfo) mi;
						generator.Emit( OpCodes.Ldarg_0 );
						generator.Emit( OpCodes.Ldstr, (string) memberObject );
						MethodInfo setter = pi.GetSetMethod( true );
						if( setter == null )
							throw new Exception( "Property is not assignable: " + memberName );
						generator.EmitCall( OpCodes.Call, setter, null );
					}
					else
					{
						FieldInfo fi = (FieldInfo) mi;
						generator.Emit( OpCodes.Ldarg_0 );
						generator.Emit( OpCodes.Ldstr, (string) memberObject );
						generator.Emit( OpCodes.Stfld, fi );
					}
				}
			}
		}

		protected string getNextDynamicGameName()
		{
			return "dynamicGame" + (nextGameNumber++).ToString();
		}

		protected string getNextDynamicPieceTypeName()
		{
			return "dynamicPieceType" + (nextPieceTypeNumber++).ToString();
		}

		protected int nextGameNumber;
		protected int nextPieceTypeNumber;
	}
}
