
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

namespace ChessV.EngineOptions
{
	public class EngineSpinOption: EngineOption
	{
		public EngineSpinOption
			( string name,
			  object value = null,
			  object defaultValue = null,
			  int min = 0,
			  int max = 0,
			  string alias = null ):
				base
					( name, 
					  typeof(int), 
					  value, 
					  defaultValue, 
					  alias )
		{
		}

		public override bool IsValid( object value )
		{
			if( Min > Max )
				return false;
			
			int intValue;
			if( Int32.TryParse( value.ToString(), out intValue ) )
			{
				if( (intValue >= Min && intValue <= Max) || (Min == 0 && Max == 0) )
					return true;
			}
			return false;
		}

		public int Min { get; set; }
		public int Max { get; set; }
	}
}
