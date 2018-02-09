
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

namespace ChessV.Games
{
	//**********************************************************************
	//
	//                              WildCastle
	//

	[Game("Wild Castle", typeof(Geometry.Rectangular), 8, 8,
		  Invented = "Unknown",
		  InventedBy = "Unknown",
		  Tags = "Chess Variant,Random Array",
		  GameDescription1 = "A derivative of standard Chess with randomized piece setup, but with normal",
		  GameDescription2 = "castling rules because the location of the rooks and king do not change",
		  Definitions = "Castling=Standard")]
	class WildCastle: FischerRandomChess
	{
		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			PositionNumber = new IntVariable( 1, 18 );
		}
		#endregion

		#region LookupGameVariable
		public override object LookupGameVariable( string variableName )
		{
			if( variableName.ToUpper() == "ARRAY" )
			{
				//	if the position is unassigned, just show the pieces that aren't randomized
				if( PositionNumber.Value == null )
					return "r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R";
				char[] array = new char[] { 'r', ' ', ' ', ' ', 'k', ' ', ' ', 'r' };
				int position = (int) PositionNumber.Value - 1;
				int lightBishop = position % 3;
				position = position / 3;
				array[lightBishop*2 + 1] = 'b';
				int darkBishop = position % 2;
				position = position / 2;
				array[darkBishop*4 + 2] = 'b';
				int queen = position;
				for( int x = 0; x < 8; x++ )
					if( array[x] == ' ' )
						if( queen-- == 0 )
							array[x] = 'q';
						else
							array[x] = 'n';
				string pieces = new string( array );
				return pieces + "/pppppppp/8/8/8/8/PPPPPPPP/" + pieces.ToUpper();
			}
			return base.LookupGameVariable( variableName );
		}
		#endregion
	}
}
