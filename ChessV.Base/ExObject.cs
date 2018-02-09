
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

namespace ChessV
{
	public class ExObject
	{
		public ExObject()
		{
			customProprties = new SymbolTable();
			customAttributes = new List<Attribute>();
		}

		public object GetCustomProperty( string name )
		{
			return customProprties.Lookup( name );
		}

		public void SetCustomProperty( string name, object value )
		{
			customProprties.Set( name, value );
		}

		public void AddAttribute( Attribute attribute )
		{
			customAttributes.Add( attribute );
		}

		public Attribute[] GetCustomAttributes()
		{
			return customAttributes.ToArray();
		}

		public Attribute[] GetCustomAttributes( Type attributeType )
		{
			List<Attribute> filteredList = new List<Attribute>();
			foreach( Attribute attr in customAttributes )
				if( attr.GetType() == attributeType || attr.GetType().IsSubclassOf( attributeType ) )
					filteredList.Add( attr );
			return filteredList.ToArray();
		}

		protected SymbolTable customProprties;
		protected List<Attribute> customAttributes;
	}
}
