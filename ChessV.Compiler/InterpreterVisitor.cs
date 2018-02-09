
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
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace ChessV.Compiler
{
	public class InterpreterVisitor: ChessVCBaseVisitor<object>
	{
		public InterpreterVisitor( Environment environment, object hostObject )
		{
			env = environment;
			obj = hostObject;
		}

		public object Parse( ParserRuleContext context )
		{
			return base.VisitBlock( (ChessVCParser.BlockContext) context );
		}

		public override object VisitObjectid( ChessVCParser.ObjectidContext context )
		{
			string name = context.GetText();
			object scope = obj;
			Reference reference = null;
			while( true )
			{
				string partial = name.IndexOf( '.' ) >= 0
					? name.Substring( 0, name.IndexOf( '.' ) )
					: name;
				reference = findReference( partial, scope );
				if( reference == null )
					throw new Exception( "Unknown identifier - " + partial );
				if( name.IndexOf( '.' ) >= 0 )
				{
					scope = reference.GetValue();
					name = name.Substring( name.IndexOf( '.' ) + 1 );
				}
				else
					return reference;
			}
		}

		public override object VisitAssignment( ChessVCParser.AssignmentContext context )
		{
			Reference exprref = (Reference) Visit( context.expr() );
			Reference assign = (Reference) Visit( context.objectid() );
			if( assign.CustomPropertyName != null )
				((ExObject) assign.ReferencedObject).SetCustomProperty( assign.CustomPropertyName, exprref.GetValue() );
			else if( exprref.EffectiveType != assign.EffectiveType && !exprref.EffectiveType.IsSubclassOf( assign.EffectiveType ) )
			{
				//	 see if the type to be assigned to has a constructor that take this type of argument
				ConstructorInfo ci = assign.EffectiveType.GetConstructor( new Type[] { exprref.EffectiveType } );
				if( ci != null )
				{
					assign.SetValue( ci.Invoke( new object[] { exprref.GetValue() } ) );
					return assign;
				}
			}
			else
			{
				try
				{
					assign.SetValue( exprref.GetValue() );
				}
				catch( Exception ex )
				{
					throw new Exception( "Assignment operation failed", ex );
				}
			}
			return assign;
		}

		public override object VisitFunctionCall( ChessVCParser.FunctionCallContext context )
		{
			Reference fnref = (Reference) Visit( context.objectid() );
			//	handle special case of static method of a type that takes a reference to the 
			//	object of that type - to make the code for this case more clean, we'll handle 
			//	the passing of 'this' as the parameter automatically.
			if( context.argumentList() == null && fnref.FunctionMember != null && 
				fnref.FunctionMember.GetParameters().Length == 1 && fnref.FunctionMember.IsStatic )
			{
				object rtnobj = fnref.FunctionMember.Invoke( null, new object[] { obj } );
				return wrapObjectIntoReference( rtnobj );
			}
			Reference[] arglist = (Reference[]) Visit( context.argumentList() );
			if( fnref.FunctionMember != null )
			{
				ParameterInfo[] pi = fnref.FunctionMember.GetParameters();
				if( fnref.FunctionMember.GetParameters().Length != arglist.Length )
					throw new Exception( "Incorrect number of arguments specified for function: " + context.objectid().GetText() );
				if( !canMatchSignature( fnref.FunctionMember, arglist ) )
				{
					//	before we decide we can't call this function, check to see if 
					//	there is only one argument, that argument is a type, that type 
					//	has a constructor taking no arguments, and if, after construction, 
					//	the object is of the required type.  If all this pans out, 
					//	we'll just construct a new object of this type.
					if( arglist.Length == 1 && arglist[0].EffectiveType is Type && 
						((Type) arglist[0].GetValue()).IsSubclassOf( fnref.FunctionMember.GetParameters()[0].ParameterType ) )
					{
						Type t = (Type) arglist[0].GetValue();
						ConstructorInfo ci = t.GetConstructor( new Type[] { } );
						if( ci != null )
						{
							object[] arg = new object[1];
							arg[0] = ci.Invoke( null );
							return wrapObjectIntoReference( fnref.GetValue( arg ) );
						}
					}
					throw new Exception( "Arguments of incorrect type provided for function: " + context.objectid().GetText() );
				}
				object[] args = new object[arglist.Length];
				for( int x = 0; x < arglist.Length; x++ )
					args[x] = arglist[x].GetValue();
				return wrapObjectIntoReference( fnref.GetValue( args ) );
			}
			//	we have overloaded functions - search for one that works
			foreach( MethodInfo mi in fnref.FunctionOverloads )
			{
				if( canMatchSignature( mi, arglist ) )
				{
					ParameterInfo[] parameters = mi.GetParameters();
					object[] args = new object[parameters.Length];
					for( int x = 0; x < parameters.Length; x++ )
						args[x] = x < arglist.Length ? arglist[x].GetValue() : Type.Missing;
					return wrapObjectIntoReference( mi.Invoke( fnref.ReferencedObject, args ) );
				}
			}
			throw new Exception( "No overloaded found with compatible arguments for function: " + context.objectid().GetText() );
		}

		public override object VisitIfStatement( ChessVCParser.IfStatementContext context )
		{
			Reference condition = (Reference) Visit( context.expr() );
			bool ifvalue = true;
			if( condition.EffectiveType == typeof( bool ) )
				ifvalue = (bool) condition.GetValue();
			else
				ifvalue = condition.GetValue() != null;
			if( ifvalue )
				return Visit( context.statement( 0 ) );
			else
				return Visit( context.statement( 1 ) );
		}

		public override object VisitOpEquality( ChessVCParser.OpEqualityContext context )
		{
			Reference op1 = (Reference) Visit( context.expr( 0 ) );
			Reference op2 = (Reference) Visit( context.expr( 1 ) );
			if( context.children[1].GetText() == "==" )
				return wrapObjectIntoReference( op1.GetValue().Equals( op2.GetValue() ) );
			else
				return wrapObjectIntoReference( !(op1.GetValue().Equals( op2.GetValue() )) );
		}

		protected bool canMatchSignature( MethodInfo mi, Reference[] arglist )
		{
			ParameterInfo[] parameters = mi.GetParameters();
			int requiredParams = parameters.Length;
			for( int x = requiredParams - 1; x >= 0; x-- )
			{
				if( (parameters[x].Attributes & ParameterAttributes.Optional) != 0 )
					requiredParams--;
				else
					break;
			}
			if( arglist.Length < requiredParams || arglist.Length > parameters.Length )
				return false;
			for( int x = 0; x < arglist.Length; x++ )
			{
				if( parameters[x].ParameterType != arglist[x].EffectiveType && 
					!arglist[x].EffectiveType.IsSubclassOf( parameters[x].ParameterType ) )
					return false;
			}
			return true;
		}

		public override object VisitConstDir( ChessVCParser.ConstDirContext context )
		{
			int rankOffset = Convert.ToInt32( context.INTEGER( 0 ).GetText() );
			int fileOffset = Convert.ToInt32( context.INTEGER( 1 ).GetText() );
			if( context.ChildCount >= 6 )
			{
				if( context.GetChild( 1 ).GetText() == "-" )
					rankOffset = -rankOffset;
				if( context.GetChild( 3 ).GetText() == "-" ||
					context.GetChild( 4 ).GetText() == "-" )
					fileOffset = -fileOffset;
			}
			return new Reference( new Direction( rankOffset, fileOffset ) );
		}

		public override object VisitArgumentList( ChessVCParser.ArgumentListContext context )
		{
			Reference[] arglist = new Reference[1 + (context.ChildCount - 1)/2];
			for( int x = 0; x < 1 + (context.ChildCount - 1) / 2; x++ )
				arglist[x] = (Reference) Visit( context.expr( x ) );
			return arglist;
		}

		public override object VisitListExpr( ChessVCParser.ListExprContext context )
		{
			ChessVCParser.ExprContext[] elementContexts = context.expr();
			List<Reference> elementReferences = new List<Reference>();
			foreach( ChessVCParser.ExprContext cxt in elementContexts )
				elementReferences.Add( (Reference) Visit( cxt ) );
			if( elementReferences.Count == 0 )
				return wrapObjectIntoReference( new List<object>() { } );
			if( elementReferences[0].EffectiveType == typeof(string) )
			{
				bool allString = true;
				//	ensure all elements are type of string
				for( int x = 1; allString && x < elementReferences.Count; x++ )
					if( elementReferences[x].EffectiveType != typeof(string) )
						allString = false;
				if( allString )
				{
					List<string> strlist = new List<string>();
					foreach( Reference elem in elementReferences )
						strlist.Add( (string) elem.GetValue() );
					return wrapObjectIntoReference( strlist );
				}
			}
			List<object> objlist = new List<object>();
			foreach( Reference elem in elementReferences )
				objlist.Add( elem.GetValue() );
			return wrapObjectIntoReference( objlist );
		}

		public override object VisitConstInt( ChessVCParser.ConstIntContext context )
		{
			return new Reference( Convert.ToInt32( context.GetText() ) );
		}

		public override object VisitConstStr( ChessVCParser.ConstStrContext context )
		{
			string str = context.GetText();
			//	trim quotation marks
			return new Reference( str.Substring( 1, str.Length - 2 ) );
		}

		public override object VisitConstBoolFalse( ChessVCParser.ConstBoolFalseContext context )
		{
			return new Reference( false );
		}

		public override object VisitConstBoolTrue( ChessVCParser.ConstBoolTrueContext context )
		{
			return new Reference( true );
		}

		protected Reference wrapObjectIntoReference( object obj )
		{
			if( obj == null )
				return null;
			if( obj is Reference )
				return (Reference) obj;
			return new Reference( obj );
		}

		protected Reference findReference( string name, object scope )
		{
			//	if the identifier is enclosed in single quotes, strip them off
			if( name.Length > 2 && name[0] == '\'' && name[name.Length-1] == '\'' )
				name = name.Substring( 1, name.Length - 2 );
			//	if name begins with @ it is a custom property
			if( name[0] == '@' )
				return new Reference( scope, name.Substring( 1 ) );
			//	determine type of scope object (which may be an actual type)
			bool scopeIsType = scope is Type;
			Type scopeType = scopeIsType ? (Type) scope : scope.GetType();
			//	See if scope object has member with this name - first check static
			MemberInfo[] membersWithName = scopeType.GetMember( name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic );
			if( membersWithName.Length > 1 )
				throw new Exception( "Ambiguous member name: " + name );
			if( membersWithName.Length == 1 )
			{
				MemberInfo mi = membersWithName[0];
				if( mi.MemberType == MemberTypes.Field )
					return new Reference( scope, (FieldInfo) mi, true );
				else if( mi.MemberType == MemberTypes.Property )
					return new Reference( scope, (PropertyInfo) mi, true );
				else if( mi.MemberType == MemberTypes.Method )
					return new Reference( scope, (MethodInfo) mi, true );
			}
			//	See if scope object has member with this name - check instance members
			membersWithName = scopeType.GetMember( name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
			if( membersWithName.Length == 1 )
			{
				MemberInfo mi = membersWithName[0];
				if( mi.MemberType == MemberTypes.Field )
					return new Reference( scope, (FieldInfo) mi, false );
				else if( mi.MemberType == MemberTypes.Property )
					return new Reference( scope, (PropertyInfo) mi, false );
				else if( mi.MemberType == MemberTypes.Method )
					return new Reference( scope, (MethodInfo) mi, false );
			}
			else if( membersWithName.Length > 1 )
			{
				MethodInfo[] overloads = new MethodInfo[membersWithName.Length];
				for( int x = 0; x < membersWithName.Length; x++ )
					overloads[x] = (MethodInfo) membersWithName[x];
				return new Reference( scope, overloads );
			}
			//	Next, check the custom properties of the ExObject
			object customProperty = scope is ExObject ? ((ExObject) scope).GetCustomProperty( name ) : null;
			if( customProperty != null )
				return new Reference( customProperty );
			//	If scope object doesn't have it, we check the environment 
			//	(and, recursively, parent environments)
			Environment environment = env;
			while( environment != null )
			{
				object o = environment.LookupSymbol( name );
				if( o != null )
					return new Reference( o );
				environment = environment.ParentEnvironment;
			}
			return null;
		}

		protected Environment env;
		protected object obj;
	}
}
