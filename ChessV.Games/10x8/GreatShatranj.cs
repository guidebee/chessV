
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
	[Game("Great Shatranj", typeof(Geometry.Rectangular), 10, 8,
		  XBoardName = "great",
		  Invented = "2006",
		  InventedBy = "Joe Joyce",
		  Tags = "Chess Variant" )]
	public class GreatShatranj: Abstract.Generic10x8
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Variant { get; set; }


		// *** PIECE TYPES *** //

		public PieceType General;
		public PieceType Minister;
		public PieceType HighPriestess;
		public PieceType Elephant;
		public PieceType Knight;
		public PieceType Bowman;
		public PieceType Rook;


		// *** CONSTRUCTION *** //

		public GreatShatranj():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Variant = new ChoiceVariable( new string[] { "Great Shatranj D", "Great Shatranj R" } );
		}
		#endregion

		#region LookupGameVariable
		public override object LookupGameVariable( string variableName )
		{
			if( variableName.ToUpper() == "ARRAY" )
			{
				if( Variant.Value == null )
					return "1negkmhen1/pppppppppp/10/10/10/10/PPPPPPPPPP/1NEGKMHEN1";
				if( Variant.Value == "Great Shatranj D" )
					return "dnegkmhend/pppppppppp/10/10/10/10/PPPPPPPPPP/DNEGKMHEND";
				if( Variant.Value == "Great Shatranj R" )
					return "rnegkmhenr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNEGKMHENR";
			}
			return base.LookupGameVariable( variableName );
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( General = new King( "General", "G", 300, 300, "Guard" ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 285, 285 ) );
			AddPieceType( Minister = new Minister( "Minister", "M", 600, 600, "KnightWazirDabbabah" ) );
			AddPieceType( HighPriestess = new HighPriestess( "High Priestess", "H", 625, 625, "ElephantKnight" ) );
			AddPieceType( Elephant = new Elephant( "Elephant", "E", 250, 250 ) );
			Ferz.AddMoves( Elephant );
			if( Variant.Value == "Great Shatranj D" )
				AddPieceType( Bowman = new Bowman( "Dabbabah", "D", 270, 270 ) );
			if( Variant.Value == "Great Shatranj R" )
				AddPieceType( Rook = new Rook( "Rook", "R", 600, 600 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
		}
		#endregion
	}
}
