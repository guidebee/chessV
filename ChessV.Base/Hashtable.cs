
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
using System.Runtime.InteropServices;

namespace ChessV
{
	public class Hashtable
	{
		public Hashtable()
		{
			tabledata = null;
			generation = 0;
			size = 0;
			writes = 0;
		}

		public void SetSize( uint sizeInMB )
		{
			if( sizeInMB < 32 )
				sizeInMB = 32;
			if( sizeInMB > 1024 )
				sizeInMB = 1024;

			UInt32 newSize = 1024;

			// we store a cluster of hash entries for each hashcode set and newSize is
			// the maximum number of storable sets of hashcodes
			ulong hashentrysize = (ulong) System.Runtime.InteropServices.Marshal.SizeOf( typeof(TTHashEntry) );
			ulong sizeInBytes = ((ulong) sizeInMB << 20);
			for( ; newSize * 4 * hashentrysize <= sizeInBytes; newSize *= 2 ) ;
			newSize /= 2;

			size = newSize * 4;
			tabledata = new TTHashEntry[size];

			if( tabledata == null )
				//	Failure!!!
				throw new Exception( "Allocation of memory for Transposition Table failed" );
		}

		public void Clear()
		{
			for( int x = 0; x < size; x++ )
				tabledata[x] = new TTHashEntry();
		}

		public bool Lookup( UInt64 hashcode, ref TTHashEntry hash )
		{
			uint groupStart = (uint) hashcode & (size - 1) & 0xFFFFFFFCU;
			for( int slot = 0; slot < 4; slot++ )
			{
				if( tabledata[groupStart+slot].CheckHash( hashcode ) )
				{
					hash = tabledata[groupStart+slot];
					return true;
				}
			}
			return false;
		}

		public void Store( UInt64 hashcode, int score, int depth, UInt32 movehash, TTHashEntry.HashType hashtype )
		{
			uint groupStart = (uint) hashcode & (size - 1) & 0xFFFFFFFCU;
			uint replace = groupStart;

			for( uint slot = 0; slot < 4; slot++ )
			{
				if( tabledata[groupStart+slot].CheckHash( hashcode ) )
				{
					if( Movement.GetMoveTypeFromHash( movehash ) == MoveType.Invalid )
						movehash = tabledata[groupStart+slot].MoveHash;
					tabledata[groupStart+slot].SetData( hashcode, movehash, hashtype, depth, score, generation );
					return;
				}
				else if( tabledata[groupStart+slot].Type == TTHashEntry.HashType.NoHash )
				{
					tabledata[groupStart+slot].SetData( hashcode, movehash, hashtype, depth, score, generation );
					return;
				}
				//	relacement scheme lifted from Stockfish
				int c1 = (tabledata[replace].Generation == generation ? 2 : 0);
				int c2 = (tabledata[groupStart+slot].Generation == generation || tabledata[groupStart+slot].Type == TTHashEntry.HashType.Exact ? -2 : 0);
				int c3 = (tabledata[groupStart+slot].Depth < tabledata[replace].Depth ? 1 : 0);
				if( c1 + c2 + c3 > 0 )
					replace = groupStart + slot;
			}
			tabledata[replace].SetData( hashcode, movehash, hashtype, depth, score, generation );
			writes++;
		}

		public void NextGeneration()
		{
			generation++;
		}

		private TTHashEntry[] tabledata;
		uint generation;
		uint size;
		ulong writes;
	}
}
