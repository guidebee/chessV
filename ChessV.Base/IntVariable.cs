
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
	public class IntVariable: ICloneable
	{
		private int? minValue;
		private int? maxValue;
		private int? value;

		public int? Value
		{
			get
			{ return value; }

			set
			{
				if( value == null )
					this.value = null;
				else
				{
					if( minValue == null || maxValue == null )
						throw new Exception( "IntRangeVariable: Cannot set value - valid range not set" );
					if( value < minValue || value > maxValue )
						throw new Exception( "IntRangeVariable: Cannot set value - out of valid range" );
					this.value = value;
				}
			}
		}

		public int? MinValue
		{ get { return minValue; } }

		public int? MaxValue
		{ get { return maxValue; } }

		public IntVariable()
		{
		}

		public IntVariable( IntVariable original )
		{
			//	Copy constructor
			minValue = original.minValue;
			maxValue = original.maxValue;
			value = original.value;

		}

		public IntVariable( int min, int max )
		{
			SetRange( min, max );
		}

		public void SetRange( int min, int max )
		{
			minValue = min;
			maxValue = max;
		}

		public object Clone()
		{
			return new IntVariable( this );
		}
	}
}
