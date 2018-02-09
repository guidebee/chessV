
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

namespace ChessV.Geometry
{
	public class Rectangular: BoardGeometry
	{
		public int NumRanks { get; private set; }
		public int NumFiles { get; private set; }
		public int NumExtraSquares { get; private set; }

		public override string Shape
		{ get { return "Rectangular"; } }

		public Rectangular
			( int nFiles,				//	number of files per board
			  int nRanks,				//	number of ranks per board
			  int nBoards = 1,			//	number of boards
			  int nExtraSquares = 0 ):	//	total extra squares
			base( (nFiles * nRanks * nBoards) + nExtraSquares, nBoards, 2 )
		{
			NumFiles = nFiles;
			NumRanks = nRanks;
			NumExtraSquares = nExtraSquares;
		}

		public override string ToString()
		{
			return
				NumFiles.ToString() + " x " +
				NumRanks.ToString() +
				(NumberOfBoards > 1 ? " x " + NumberOfBoards.ToString() : "") +
				(NumExtraSquares > 0 ? " (+" + NumExtraSquares.ToString() + ")" : "");
		}
	}
}
