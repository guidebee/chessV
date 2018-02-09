
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
using System.Drawing;
using ChessV.Games.Rules.Gross;

namespace ChessV.Games
{
	[Game("Gross Chess", typeof(Geometry.Rectangular), 12, 12,
		  Invented = "2009",
		  InventedBy = "Fergus Duniho",
		  Tags = "Chess Variant")]
	[Appearance(ColorScheme = "Surrealistic Summer", PieceSet="Abstract", Player2Color="255,0,0")]
	public class GrossChess: Abstract.Generic12x12
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Chancellor;
		public PieceType Archbishop;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Cannon;
		public PieceType Vao;
		public PieceType Wizard;
		public PieceType Champion;


		// *** CONSTRUCTION *** //

		public GrossChess():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "mavwc2cwvam/1rsnbqkbnsr1/pppppppppppp/12/12/12/12/12/12/PPPPPPPPPPPP/1RSNBQKBNSR1/MAVWC2CWVAM";
			Castling.Value = "Custom";
			PromotionRule.Value = "Custom";
			PawnMultipleMove.Value = "@3(2,3)";
			EnPassant = true;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1250, 1350 ) );
			AddPieceType( Chancellor = new Chancellor( "Marshall", "M", 1000, 1100 ) );
			AddPieceType( Archbishop = new Archbishop( "Archbishop", "A", 800, 850 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 650, 750 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 500, 550 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 250, 250 ) );
			AddPieceType( Cannon = new Cannon( "Cannon", "C", 500, 250 ) );
			AddPieceType( Vao = new Vao( "Vao", "V", 300, 150 ) );
			AddPieceType( Wizard = new Wizard( "Wizard", "W", 600, 550 ) );
			AddPieceType( Champion = new Champion( "Champion", "S", 600, 600 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();

			//	add custom pawn promotion rule
			if( PromotionRule.Value == "Custom" )
			{
				AddRule( new GrossChessPromotionRule( Pawn,
					loc => loc.Rank == 11 ? Rules.PromotionOption.MustPromote :
						(loc.Rank == 9 || loc.Rank == 10 ? Rules.PromotionOption.CanPromote : Rules.PromotionOption.CannotPromote) ) );
			}

			//	add custom castling rule
			if( Castling.Value == "Custom" )
			{
				FlexibleCastlingRule();
				FlexibleCastlingMove( 0, "g2", "i2", "k2", 'K' );
				FlexibleCastlingMove( 0, "g2", "e2", "b2", 'Q' );
				FlexibleCastlingMove( 1, "g11", "i11", "k11", 'k' );
				FlexibleCastlingMove( 1, "g11", "e11", "b11", 'q' );
			}
		}
		#endregion

		#region AddEvaluations
		public override void AddEvaluations()
		{
			base.AddEvaluations();

			//	Customize the development evaluation function
			Evaluations.DevelopmentEvaluation eval = (Evaluations.DevelopmentEvaluation) 
				FindEvaluation( typeof(Evaluations.DevelopmentEvaluation) );
			eval.HeavyPieceThreshold = 600;
		}
		#endregion
	}
}
