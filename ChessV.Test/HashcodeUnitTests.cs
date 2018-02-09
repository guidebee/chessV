
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
	public class HashcodeUnitTests
	{
		[TestMethod]
		public void HashcodeTests1()
		{
			//	Using Grand Chess, play and take back sequences of moves and make sure that 
			//	we are getting the same position hash code when we return to a position
			ChessV.Games.GrandChess grandChessGame = new Games.GrandChess();
			object[] attrs = grandChessGame.GetType().GetCustomAttributes( typeof(GameAttribute), false );
			grandChessGame.Initialize( ((GameAttribute) attrs[0]), null, null );
			ulong roothash = grandChessGame.GetPositionHashCode( 1 );
			grandChessGame.PlayMoves( "e3e5 e8e6 d3d4 d8d7 f3f5 i9h7 f5f6 g8g7 g3g5 g7f6 e5f6 g9f7 h3h4 f7h5" );
			ulong hash_a = grandChessGame.GetPositionHashCode( 1 );
			grandChessGame.TakeBackMoves( 10 );
			grandChessGame.PlayMoves( "g3g4 i9h7 i2h4 b9c7 b2c4 a10e10 f3f5 c9g5" );
			ulong hash_b = grandChessGame.GetPositionHashCode( 1 );
			grandChessGame.TakeBackMoves( 8 );
			grandChessGame.PlayMoves( "f3f5 i9h7 f5f6 g8g7 g3g5 g7f6 e5f6 g9f7 h3h4 f7h5" );
			Assert.AreEqual( hash_a, grandChessGame.GetPositionHashCode( 1 ) );
			grandChessGame.TakeBackMoves( 14 );
			Assert.AreEqual( roothash, grandChessGame.GetPositionHashCode( 1 ) );
			grandChessGame.PlayMoves( "e3e5 e8e6 d3d4 d8d7 g3g4 i9h7 i2h4 b9c7 b2c4 a10e10 f3f5 c9g5" );
			Assert.AreEqual( hash_b, grandChessGame.GetPositionHashCode( 1 ) );
			grandChessGame.TakeBackMoves( 12 );
			Assert.AreEqual( roothash, grandChessGame.GetPositionHashCode( 1 ) );
		}
	}
}
