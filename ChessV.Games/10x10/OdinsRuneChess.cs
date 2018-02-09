
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
using ChessV.Games.Pieces.OdinsRune;

namespace ChessV.Games
{
	[Game("Odin's Rune Chess", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2005",
		  InventedBy = "Gary K. Gifford",
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme="Valhalla", PieceSet="Runes")]
	public class OdinsRuneChess: Game
	{
		// *** PIECE TYPES *** //

		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Valkyrie;
		public PieceType King;
		public PieceType Pawn;
		public PieceType ForestOx;


		// *** CONSTRUCTION *** //

		public OdinsRuneChess():
			base
				( /* num players = */ 2,
				  /* num files = */ 10,
				  /* num ranks = */ 10,
				  /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {half-move clock} {turn number}";
			FENStart = "#{Array} w 0 1";
			Array = "rfbvkkvbfr/pppppppppp/10/10/10/10/10/10/PPPPPPPPPP/RFBVKKVBFR";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			AddPieceType( King = new OdinKing( "King", "K", 500, 500, "King" ) );
			AddPieceType( Pawn = new OdinPawn( "Pawn", "P", 200, 200 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 375, 425 ) );
			AddPieceType( Valkyrie = new Valkyrie( "Valkyrie", "V", 900, 950, "Queen" ) );
			AddPieceType( ForestOx = new ForestOx( "Forest Ox", "F", 950, 950, "Knight" ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			AddRule( new Rules.Extinction.ExtinctionRule( "K" ) );
			AddRule( new Rules.NoMoveResultRule( MoveEventResponse.GameLost ) );
		}
		#endregion
	}
}
