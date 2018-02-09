
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

namespace ChessV
{
	public abstract class EngineOption
	{
		public string Name { get; set; }
		public Type ValueType { get; set; }
		public object Value { get; set; }
		public object DefaultValue { get; set; }
		public string Alias { get; set; }

		public EngineOption
			( string name, 
			  Type valueType, 
			  object value = null,
			  object defaultValue = null,
			  string alias = null )
		{
			Name = name; 
			ValueType = valueType;
			Value = value;
			DefaultValue = defaultValue;
			Alias = alias;
		}

		public bool IsValid()
		{
			if( Name == null )
				return false;
			if( !IsValid( Value ) )
				return false;
			if( DefaultValue != null && !IsValid( DefaultValue ) )
				return false;
			return true;
		}

		public abstract bool IsValid( object value );
	}
}
