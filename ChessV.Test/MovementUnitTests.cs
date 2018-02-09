
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
using ChessV;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessV.Test
{
	[TestClass]
	public class MovementUnitTests
	{
		[TestMethod]
		public void MovementTests1()
		{
			Movement m1 = new Movement( 16, 254, 1, MoveType.EnPassant, 33 );
			Assert.AreEqual( m1.FromSquare, 16 );
			Assert.AreEqual( m1.ToSquare, 254 );
			Assert.AreEqual( m1.Player, 1 );
			Assert.AreEqual( m1.MoveType, MoveType.EnPassant );
			Assert.AreEqual( m1.Tag, 33 );
			Movement m2 = new Movement( m1.Hash );
			Assert.AreEqual( m2.FromSquare, 16 );
			Assert.AreEqual( m2.ToSquare, 254 );
			Assert.AreEqual( m2.Player, 1 );
			Assert.AreEqual( m2.MoveType, MoveType.EnPassant );
			Assert.AreEqual( m2.Tag, 33 );
			Assert.AreEqual( m1, m2 );
			Assert.AreEqual( m1.Hash, m2.Hash );
		}
	}
}
