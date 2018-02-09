
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

namespace ChessV
{
	public struct TTHashEntry
	{
		public enum HashType: int
		{
			NoHash = 0,
			Exact = 1,
			UpperBound = 2,
			LowerBound = 3,
			Quiescent = 4,
			MoveOnly = 5
		}

		private UInt64 hash;
		private UInt32 moveHash;
		private Int32 data;

		public bool CheckHash( UInt64 hashToCheck )
		{
			return (hash & 0xFFFFFFFFFFFFFE00UL) == (hashToCheck & 0xFFFFFFFFFFFFFE00UL);
		}

		public int Generation
		{
			get
			{ return (int) (hash & 0x00000000000001FFUL); }
		}

		public UInt32 MoveHash
		{
			get
			{ return moveHash; }
		}

		public HashType Type
		{
			get
			{ return (HashType) (data & 0x000000FF); }
		}

		public int Depth
		{
			get
			{ return (data >> 8) & 0x000000FF; }
		}

		public int Score
		{
			get
			{ return data >> 16; }
		}

		public void SetData( UInt64 hash, UInt32 moveHash, HashType hashType, int depth, int score, uint generation )
		{
			this.moveHash = moveHash;  // store the best move

			// the lower 9 bits of m_hash store the generation
			this.hash = (hash & 0xFFFFFFFFFFFFFE00UL) | (UInt64) generation;
		
			// the remaining information is packed into data as follows
			data = 
				  (score << 16)  // bits 16-31 contain the score
				| (depth << 8)   // bits 8-15 contain the depth
				| (int) (hashType);  // bits 0-7 contain the hash type
		}
	}
}
