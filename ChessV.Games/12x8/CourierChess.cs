
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
	[Game("Courier Chess", typeof(Geometry.Rectangular), 12, 8,
		  Invented = "1200s",
		  InventedBy = "Unknown",
		  Tags = "Chess Variant,Historic,Popular",
		  GameDescription1="Dating back to at least 1202, this very popular",
		  GameDescription2="game was played for six hundred years")]
	[Appearance(ColorScheme = "Sahara")]
	public class CourierChess: Abstract.Generic12x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Knight;
		public PieceType Courier;
		public PieceType Elephant;
		public PieceType Mann;
		public PieceType Schleich;


		// *** CONSTRUCTION *** //

		public CourierChess():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnbcmk1scbnr/1ppppp1pppp1/6q5/p5p4p/P5P4P/6Q5/1PPPPP1PPPP1/RNBCMK1SCBNR";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Queen = new Ferz( "Queen", "Q", 155, 155 ) );
			AddPieceType( Elephant = new Elephant( "Bischof", "B", 100, 100 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Courier = new Bishop( "Courier", "C", 350, 400 ) );
			AddPieceType( Mann = new King( "Mann", "M", 325, 325, "General" ) );
			AddPieceType( Schleich = new Wazir( "Schleich", "S", 145, 145 ) );
		}
		#endregion
	}
}
