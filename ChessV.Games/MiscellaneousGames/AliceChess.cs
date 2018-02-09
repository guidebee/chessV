
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
using ChessV.Games.Rules.Alice;

namespace ChessV.Games
{
	//**********************************************************************
	//
	//                           AliceChess
	//
	//    This class implements Alice Chess.  This game is Chess with an 
	//    extra board that starts off empty.  With each move, a piece is 
	//    transferred to the other board.  A move must be legal on the 
	//    board on which it is made and the corresponding destination 
	//    square on the other board must be empty.  
	//
	//    This class, along with the AliceRule class (which handles the 
	//    movement between boards with minimal fuss), and the TwoBoards
	//    class (which handles the initialization and display of a pair 
	//    of boards) demonstrate a number of techniques for extending 
	//    ChessV for novel games.

	[Game("Alice Chess", typeof(Geometry.Rectangular), 8, 8, 2,
		  Invented = "1953",
		  InventedBy = "V. R. Parton", 
		  Tags = "Chess Variant,Multiple Boards,Historic,Popular")]
	[Appearance(ColorScheme = "Sublimation")]
	public class AliceChessGame: Chess
	{
		// *** CONSTRUCTION *** //

		public AliceChessGame()
		{
		}


		// *** INITIALIZATION *** //

		#region CreateBoard
		//	We override the CreateBoard function so the game uses a board of 
		//	type TwoBoards instead of Board.  This is enough to trigger the 
		//	two boards architecture and proper rendering to the display.
		public override Board CreateBoard( int nPlayers, int nFiles, int nRanks, Symmetry symmetry )
		{ return new ChessV.Boards.TwoBoards( nFiles, nRanks ); }
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();


			// *** ALICE RULE *** //

			//	This is the rule that transfrms all standard moves and captures 
			//	seemlessly into cross-board moves under the usual restrictions of 
			//	Alice Chess.  Only Castling and En Passant require additional handling.
			AliceRule aliceRule = new AliceRule();
			aliceRule.RoyalType = King;
			AddRule( aliceRule );


			// *** CASTLING *** //

			//	Swap out any CastlingRule with an AliceCastlingRule
			Rule castlingRule = FindRule( typeof(Rules.CastlingRule) );
			if( castlingRule != null )
				ReplaceRule( castlingRule, new AliceCastlingRule( (Rules.CastlingRule) castlingRule ) );
			//	Swap out any FlexibleCastlingRule with an AliceFlexibleCastlingRule
			Rule flexibleCastlingRule = FindRule( typeof( Rules.FlexibleCastlingRule ) );
			if( flexibleCastlingRule != null )
				ReplaceRule( flexibleCastlingRule, new AliceFlexibleCastlingRule( (Rules.FlexibleCastlingRule) flexibleCastlingRule ) );


			// *** EN PASSANT *** //

			//	Swap out any EnPassantRule with an AliceEnPassantRule
			Rule enPassantRule = FindRule( typeof(Rules.EnPassantRule) );
			if( enPassantRule != null )
				ReplaceRule( enPassantRule, new AliceEnPassantRule( (Rules.EnPassantRule) enPassantRule ) );
		}
		#endregion
	}
}
