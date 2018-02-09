
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
	[Game("Grand Shatranj", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2006",
		  InventedBy = "Joe Joyce",
		  Tags = "Chess Variant")]
	[Game("Gilded Grand Shatranj", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2006",
		  InventedBy = "Joe Joyce",
		  Tags = "Chess Variant",
		  Definitions = "Variant=Gilded Grand Shatranj")]
	public class GrandShatranj: Abstract.Generic10x10
	{
		// *** PIECE TYPES *** //

		public PieceType Rook;
		public PieceType Knight;
		public PieceType Oliphant;
		public PieceType LightningWarmachine;
		public PieceType JumpingGeneral;
		public PieceType Minister;
		public PieceType HighPriestess;


		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable Variant { get; set; }


		// *** CONSTRUCTION *** //

		public GrandShatranj():
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}

		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Variant = new ChoiceVariable( new string[] { "Grand Shatranj D", "Grand Shatranj R", "Gilded Grand Shatranj" } );
			Array = "#{BlackArray}/10/10/10/10/#{WhiteArray}";
		}
		#endregion

		#region SetOtherVariables
		public override void SetOtherVariables()
		{
			base.SetOtherVariables();
			if( Variant.Value == "Grand Shatranj D" )
			{
				SetCustomProperty( "BlackArray", "l8l/1nojkmhon1/pppppppppp" );
				SetCustomProperty( "WhiteArray", "PPPPPPPPPP/1NOJKMHON1/L8L" );
			}
			else if( Variant.Value == "Grand Shatranj R" )
			{
				SetCustomProperty( "BlackArray", "r8r/1nojkmhon1/pppppppppp" );
				SetCustomProperty( "WhiteArray", "PPPPPPPPPP/1NOJKMHON1/R8R" );
			}
			else if( Variant.Value == "Gilded Grand Shatranj" )
			{
				SetCustomProperty( "BlackArray", "l2o2o2l/1rnhjkmnr1/pppppppppp" );
				SetCustomProperty( "WhiteArray", "PPPPPPPPPP/1RNHJKMNR1/L2O2O2L" );
			}
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Oliphant = new Oliphant( "Oliphant", "O", 500, 500 ) );
			AddPieceType( JumpingGeneral = new JumpingGeneral( "Jumping General", "J", 600, 600 ) );
			AddPieceType( Minister = new Minister( "Minister", "M", 575, 575, "KnightWazirDabbabah" ) );
			AddPieceType( HighPriestess = new HighPriestess( "High Priestess", "H", 575, 575, "ElephantKnight" ) );

			if( Variant.Value == "Grand Shatranj D" || Variant.Value == "Gilded Grand Shatranj" )
				AddPieceType( LightningWarmachine = new LightningWarmachine( "Lightning Warmachine", "L", 600, 600 ) );
			if( Variant.Value == "Grand Shatranj R" || Variant.Value == "Gilded Grand Shatranj" )
				AddPieceType( Rook = new Rook( "Rook", "R", 550, 650 ) );
			base.AddPieceTypes();
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
