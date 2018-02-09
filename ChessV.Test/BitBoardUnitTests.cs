
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessV.Test
{
	[TestClass]
	public class BitBoardUnitTests
	{
		[TestMethod]
		public void BitBoardTest_64Squares()
		{
			BitBoard bb1 = new BitBoard( 64 );
			//	test bit count - should be 0
			Assert.AreEqual( 0, bb1.BitCount );
			//	set a bit and make sure it is set and bit count is 1
			bb1.SetBit( 3 );
			Assert.AreEqual( 1, bb1.GetBit( 3 ) );
			Assert.AreEqual( 1, bb1.BitCount );
			//	clear the bit and ensure it and bit count are 0
			bb1.ClearBit( 3 );
			Assert.AreEqual( 0, bb1.GetBit( 3 ) );
			Assert.AreEqual( 0, bb1.BitCount );

			//	set a bunch of bits and make sure we extract them properly
			bb1.SetBit( 0 );
			bb1.SetBit( 3 );
			bb1.SetBit( 12 );
			bb1.SetBit( 18 );
			bb1.SetBit( 19 );
			bb1.SetBit( 33 );
			bb1.SetBit( 49 );
			bb1.SetBit( 63 );
			Assert.AreEqual( 8, bb1.BitCount );
			Assert.AreEqual( 0, bb1.ExtractLSB() );
			Assert.AreEqual( 7, bb1.BitCount );
			Assert.AreEqual( 3, bb1.ExtractLSB() );
			Assert.AreEqual( 6, bb1.BitCount );
			Assert.AreEqual( 12, bb1.ExtractLSB() );
			Assert.AreEqual( 5, bb1.BitCount );
			Assert.AreEqual( 18, bb1.ExtractLSB() );
			Assert.AreEqual( 4, bb1.BitCount );
			Assert.AreEqual( 19, bb1.ExtractLSB() );
			Assert.AreEqual( 3, bb1.BitCount );
			Assert.AreEqual( 33, bb1.ExtractLSB() );
			Assert.AreEqual( 2, bb1.BitCount );
			Assert.AreEqual( 49, bb1.ExtractLSB() );
			Assert.AreEqual( 1, bb1.BitCount );
			Assert.AreEqual( 63, bb1.ExtractLSB() );
			Assert.AreEqual( 0, bb1.BitCount );
		}
	}
}
