
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
	[Game("Courier Chess Moderno", typeof(Geometry.Rectangular), 12, 8,
		  Invented = "2008",
		  InventedBy = "Jose Carrillo",
		  Tags = "Chess Variant",
		  GameDescription1 = "A modernized version of the classic game of Courier Chess",
		  GameDescription2 = "created by Jose Carrillo in 2008")]
	public class CourierChessModerno: Abstract.Generic12x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Knight;
		public PieceType Elephant;
		public PieceType Bishop;
		public PieceType Man;
		public PieceType Schleich;


		// *** CONSTRUCTION *** //

		public CourierChessModerno() :
			base
				 ( /* symmetry = */ new RotationalSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {castling} {elephants} {en-passant} {half-move clock} {turn number}";
			FENStart = "#{Array} w #default #default #default 0 1";
			Array = "rnebsqkmbenr/pppppppppppp/12/12/12/12/PPPPPPPPPPPP/RNEBMKQSBENR";
			PromotionTypes = "QRBNMSE";
			Castling.Value = "3-3";
			PawnDoubleMove = true;
			EnPassant = true;
			BareKing = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			AddPieceType( Queen = new Ferz( "Queen", "Q", 155, 155 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 350, 400 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Man = new King( "Mann", "M", 325, 325, "General" ) );
			AddPieceType( Schleich = new Wazir( "Schleich", "S", 145, 145 ) );
			AddPieceType( Elephant = new SilverGeneral( "Elephant", "E", 200, 200 ) );

			//	we need to add these pieces so that their moves will be available 
			//	for the extra moves for unmoved elephant added in AddRules:
			AddPieceType( new Dabbabah( null, null, 0, 0 ) );
			AddPieceType( new Elephant( null, null, 0, 0 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			var elephantRule = new Rules.ExtraMovesForUnmovedPieceRule( Elephant, "elephants" );
			elephantRule.AddMove( MoveCapability.Step( new Direction( 2, 0 ) ) );
			elephantRule.AddMove( MoveCapability.Step( new Direction( 2, 2 ) ) );
			elephantRule.AddMove( MoveCapability.Step( new Direction( 2, -2 ) ) );
			AddRule( elephantRule );
		}
		#endregion
	}
}
