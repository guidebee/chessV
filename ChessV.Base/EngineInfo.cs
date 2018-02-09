
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
using System.Text;

namespace ChessV
{
	public enum Protocol
	{
		XBoard2,
		VBoard
	}

	public class EngineInfo
	{
		public bool Internal { get; private set; }
		public string Name { get; private set; }
		public string Path { get; private set; }
		public string Exe { get; private set; }
		public Protocol Protocol { get; private set; }

		public EngineInfo( string name, string path, string exe, Protocol protocol )
		{ Internal = false;  Name = name; Path = path; Exe = exe; Protocol = protocol; }

		public EngineInfo()
		{ Internal = true; }
	}
}
