
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
using System.Drawing;

namespace ChessV
{
	public class AppearanceAttribute: Attribute
	{
		public int NumberOfColors { get; set; }
		public string ColorScheme { get; set; }
		public string PieceSet { get; set; }
		public string Game { get; set; }
		public string Player1Color { get; set; }
		public string Player2Color { get; set; }

		public AppearanceAttribute()
		{
			NumberOfColors = 2;
		}
	}
}
