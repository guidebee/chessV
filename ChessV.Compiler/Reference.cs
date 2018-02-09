
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
using System.Reflection;

namespace ChessV.Compiler
{
	public class Reference
	{
		public object ReferencedObject { get; private set; }
		public string CustomPropertyName { get; private set; }
		public FieldInfo FieldMember { get; private set; }
		public PropertyInfo PropertyMember { get; private set; }
		public MethodInfo FunctionMember { get; private set; }
		public MethodInfo[] FunctionOverloads { get; private set; }
		public bool IsStatic { get; private set; }

		public Reference( object referencedObject )
		{
			ReferencedObject = referencedObject;
		}

		public Reference( object referencedObject, string customPropertyName )
		{
			ReferencedObject = referencedObject;
			CustomPropertyName = customPropertyName;
		}

		public Reference( object referencedObject, FieldInfo fieldMember, bool isStatic = false )
		{
			ReferencedObject = referencedObject;
			FieldMember = fieldMember;
			IsStatic = isStatic;
		}

		public Reference( object referencedObject, PropertyInfo propertyMember, bool isStatic = false )
		{
			ReferencedObject = referencedObject;
			PropertyMember = propertyMember;
			IsStatic = isStatic;
		}

		public Reference( object referencedObject, MethodInfo functionMember, bool isStatic = false )
		{
			ReferencedObject = referencedObject;
			FunctionMember = functionMember;
			IsStatic = isStatic;
		}

		public Reference( object referencedObject, MethodInfo[] functionOverloads )
		{
			ReferencedObject = referencedObject;
			FunctionOverloads = functionOverloads;
			IsStatic = false;
		}

		public Type EffectiveType
		{
			get
			{
				if( FieldMember != null )
					return FieldMember.FieldType;
				if( PropertyMember != null )
					return PropertyMember.PropertyType;
				if( FunctionMember != null )
					return FunctionMember.ReturnType;
				if( FunctionOverloads != null )
					return FunctionOverloads[0].ReturnType;
				return ReferencedObject.GetType();
			}
		}

		public object GetValue( object[] args = null )
		{
			if( FieldMember != null )
				return FieldMember.GetValue( ReferencedObject );
			if( PropertyMember != null )
				return PropertyMember.GetValue( ReferencedObject, null );
			if( FunctionMember != null )
				return FunctionMember.Invoke( FunctionMember.IsStatic ? null : ReferencedObject, args );
			if( FunctionOverloads != null )
				throw new Exception( "!!" );
			return ReferencedObject;
		}

		public void SetValue( object value )
		{
			if( FieldMember != null )
				FieldMember.SetValue( ReferencedObject, value );
			else if( PropertyMember != null )
				PropertyMember.SetValue( ReferencedObject, value, null );
			else
				throw new Exception( "Member is not assignable" );
		}
	}
}
