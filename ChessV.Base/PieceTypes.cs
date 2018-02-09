
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

namespace ChessV
{
	// *** Orthodox Chess Pieces *** //

	#region Orthodox Chess
	#region Rook
	[PieceType("Rook", "Chess")]
	public class Rook: PieceType 
	{
		public Rook( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Rook", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			#region Customize piece-square-tables for the Rook
			PSTMidgameInSmallCenter = 0;
			PSTMidgameInLargeCenter = 0;
			PSTMidgameSmallCenterAttacks = 2;
			PSTMidgameLargeCenterAttacks = 2;
			PSTMidgameForwardness = 0;
			PSTMidgameGlobalOffset = 0;
			PSTEndgameInSmallCenter = 0;
			PSTEndgameInLargeCenter = 0;
			PSTEndgameSmallCenterAttacks = 0;
			PSTEndgameLargeCenterAttacks = 0;
			PSTEndgameForwardness = 0;
			PSTEndgameGlobalOffset = 0;
			#endregion
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  0,  1 ) );
			type.Slide( new Direction(  0, -1 ) );
			type.Slide( new Direction(  1,  0 ) );
			type.Slide( new Direction( -1,  0 ) );
		}
	}
	#endregion

	#region Bishop
	[PieceType("Bishop", "Chess")]
	public class Bishop: PieceType 
	{
		public Bishop( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Bishop", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction( 1, 1 ) );
			type.Slide( new Direction( 1, -1 ) );
			type.Slide( new Direction( -1, 1 ) );
			type.Slide( new Direction( -1, -1 ) );
		}
	}
	#endregion

	#region Queen
	[PieceType("Queen", "Chess")]
	public class Queen: PieceType
	{
		public Queen( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Queen", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Rook.AddMoves( type );
			Bishop.AddMoves( type );
		}
	}
	#endregion

	#region Knight
	[PieceType("Knight", "Chess")]
	public class Knight: PieceType 
	{
		public Knight( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Knight", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			//	Customize piece-square-tables for the Knight
			PSTMidgameInSmallCenter = 12;
			PSTMidgameInLargeCenter = 8;
			PSTMidgameForwardness = 2;
			PSTMidgameLargeCenterAttacks = 4;
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 1, 2 ) );
			type.Step( new Direction( 2, 1 ) );
			type.Step( new Direction( 2, -1 ) );
			type.Step( new Direction( 1, -2 ) );
			type.Step( new Direction( -1, -2 ) );
			type.Step( new Direction( -2, -1 ) );
			type.Step( new Direction( -2, 1 ) );
			type.Step( new Direction( -1, 2 ) );
		}
	}
	#endregion

	#region King
	[PieceType("King", "Chess")]
	public class King: PieceType 
	{
		public King( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "King", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			#region customize piece-square-tables for the King
			//	midgame tables
			PSTMidgameInSmallCenter = 0;
			PSTMidgameInLargeCenter = 0;
			PSTMidgameSmallCenterAttacks = 0;
			PSTMidgameLargeCenterAttacks = 0;
			PSTMidgameForwardness = -15;
			//	endgame tables
			PSTEndgameForwardness = 8;
			PSTEndgameInLargeCenter = 15;
			#endregion
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Wazir.AddMoves( type );
		}
	}
	#endregion

	#region Pawn
	[PieceType("Pawn", "Chess")]
	public class Pawn: PieceType 
	{
		public Pawn( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Pawn", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			//	Customize the piece-square-tables for the Pawn
			PSTMidgameForwardness = 6;
			PSTEndgameForwardness = 12;
			PSTMidgameInSmallCenter = 8;
		}

		public static new void AddMoves( PieceType type )
		{
			type.StepMoveOnly( new Direction( 1, 0 ) );
			type.StepCaptureOnly( new Direction( 1, 1 ) );
			type.StepCaptureOnly( new Direction( 1, -1 ) );
		}

		public override void Initialize( Game game )
		{
			base.Initialize( game );

			//	Set the pawn hash keys, used for the pawn structure hash table.
			//	Every other type has zeros here (assigned by the base class 
			//	implementation of this function.)  This override sets the 
			//	values to non-zero values for the pawn piece type only.
			for( int player = 0; player < game.NumPlayers; player++ )
				pawnHashKeyIndex[player] = 256 * (player + 1);
		}
	}
	#endregion
	#endregion


	// *** Chess Missing Compounds *** //

	#region Chess Missing Compounds
	#region Archbishop
	[PieceType("Archbishop", "Chess Missing Compounds")]
	public class Archbishop: PieceType
	{
		public Archbishop( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Archbishop", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Bishop.AddMoves( type );
			Knight.AddMoves( type );
		}
	}
	#endregion

	#region Chancellor
	[PieceType("Chancellor", "Chess Missing Compounds")]
	public class Chancellor: PieceType
	{
		public Chancellor( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Chancellor", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Rook.AddMoves( type );
			Knight.AddMoves( type );
		}
	}
	#endregion

	#region Amazon
	[PieceType("Amazon", "Chess Missing Compounds")]
	public class Amazon: PieceType
	{
		public Amazon( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Amazon", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Queen.AddMoves( type );
			Knight.AddMoves( type );
		}
	}
	#endregion
	#endregion


	// *** Movement Atoms *** //

	#region Movement Atoms
	#region Wazir
	[PieceType("Wazir", "Movement Atoms")]
	public class Wazir: PieceType
	{
		public Wazir( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Wazir", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 0, 1 ) );
			type.Step( new Direction( 0, -1 ) );
			type.Step( new Direction( 1, 0 ) );
			type.Step( new Direction( -1, 0 ) );
		}
	}
	#endregion

	#region Ferz
	[PieceType("Ferz", "Movement Atoms")]
	public class Ferz: PieceType
	{
		public Ferz( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Ferz", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 1, 1 ) );
			type.Step( new Direction( 1, -1 ) );
			type.Step( new Direction( -1, 1 ) );
			type.Step( new Direction( -1, -1 ) );
		}
	}
	#endregion

	#region Elephant
	[PieceType("Elephant", "Movement Atoms")]
	public class Elephant: PieceType
	{
		public Elephant( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Elephant", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 2, 2 ) );
			type.Step( new Direction( 2, -2 ) );
			type.Step( new Direction( -2, 2 ) );
			type.Step( new Direction( -2, -2 ) );
		}
	}
	#endregion

	#region Dabbabah
	[PieceType("Dabbabah", "Movement Atoms")]
	public class Dabbabah: PieceType
	{
		public Dabbabah( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Dabbabah", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 0, 2 ) );
			type.Step( new Direction( 0, -2 ) );
			type.Step( new Direction( 2, 0 ) );
			type.Step( new Direction( -2, 0 ) );
		}
	}
	#endregion

	#region Tribbabah
	[PieceType("Tribbabah", "Movement Atoms")]
	public class Tribbabah: PieceType
	{
		public Tribbabah( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Tribbabah", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  0,  3 ) );
			type.Step( new Direction(  0, -3 ) );
			type.Step( new Direction(  3,  0 ) );
			type.Step( new Direction( -3,  0 ) );
		}
	}
	#endregion

	#region Camel
	[PieceType("Camel", "Movement Atoms")]
	public class Camel: PieceType
	{
		public Camel( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Camel", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 1, 3 ) );
			type.Step( new Direction( 3, 1 ) );
			type.Step( new Direction( 3, -1 ) );
			type.Step( new Direction( 1, -3 ) );
			type.Step( new Direction( -1, -3 ) );
			type.Step( new Direction( -3, -1 ) );
			type.Step( new Direction( -3, 1 ) );
			type.Step( new Direction( -1, 3 ) );
		}
	}
	#endregion

	#region Zebra
	[PieceType("Zebra", "Movement Atoms")]
	public class Zebra: PieceType
	{
		public Zebra( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Zebra", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  2,  3 ) );
			type.Step( new Direction(  3,  2 ) );
			type.Step( new Direction(  3, -2 ) );
			type.Step( new Direction(  2, -3 ) );
			type.Step( new Direction( -2, -3 ) );
			type.Step( new Direction( -3, -2 ) );
			type.Step( new Direction( -3,  2 ) );
			type.Step( new Direction( -2,  3 ) );
		}
	}
	#endregion

	#region Knightrider
	[PieceType("Knightrider", "Movement Atoms" )]
	public class Knightrider: PieceType
	{
		public Knightrider( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Knightrider", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			//	Customize piece-square-tables for the Knightrider
			PSTMidgameInSmallCenter = 12;
			PSTMidgameInLargeCenter = 9;
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  1,  2 ) );
			type.Slide( new Direction(  2,  1 ) );
			type.Slide( new Direction(  2, -1 ) );
			type.Slide( new Direction(  1, -2 ) );
			type.Slide( new Direction( -1, -2 ) );
			type.Slide( new Direction( -2, -1 ) );
			type.Slide( new Direction( -2,  1 ) );
			type.Slide( new Direction( -1,  2 ) );
		}
	}
	#endregion
	#endregion


	// *** Chess with Different Armies *** //

	#region Chess with Different Armies
	#region Lion
	[PieceType("Lion", "Chess with Different Armies")]
	public class Lion: PieceType
	{
		public Lion( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Lion", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Dabbabah.AddMoves( type );
			Tribbabah.AddMoves( type );
		}
	}
	#endregion

	#region War Elephant
	[PieceType("War Elephant", "Chess with Different Armies")]
	public class WarElephant: PieceType
	{
		public WarElephant( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "War Elephant", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Elephant Ferz Dabbabah";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Elephant.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region Phoenix
	[PieceType("Phoenix", "Chess with Different Armies")]
	public class Phoenix: PieceType
	{
		public Phoenix( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Phoenix", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Elephant Wazir";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Elephant.AddMoves( type );
		}
	}
	#endregion

	#region Cleric
	[PieceType("Cleric", "Chess with Different Armies")]
	public class Cleric: PieceType
	{
		public Cleric( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Cleric", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Bishop Debbabah";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Bishop.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region Short Rook
	[PieceType("Short Rook", "Chess with Different Armies")]
	public class ShortRook: PieceType
	{
		public ShortRook( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Short Rook", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  0,  1 ), 4 );
			type.Slide( new Direction(  0, -1 ), 4 );
			type.Slide( new Direction(  1,  0 ), 4 );
			type.Slide( new Direction( -1,  0 ), 4 );
		}
	}
	#endregion

	#region Bowman
	[PieceType("Bowman", "Chess with Different Armies")]
	public class Bowman: PieceType
	{
		public Bowman( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Bowman", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Wazir Dabbabah";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region Narrow Knight
	[PieceType("Narrow Knight", "Chess with Different Armies")]
	public class NarrowKnight: PieceType
	{
		public NarrowKnight( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Narrow Knight", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			type.Step( new Direction(  2,  1 ) );
			type.Step( new Direction(  2, -1 ) );
			type.Step( new Direction( -2, -1 ) );
			type.Step( new Direction( -2,  1 ) );
		}
	}
	#endregion

	#region Charging Rook
	[PieceType("Charging Rook", "Chess with Different Armies")]
	public class ChargingRook: PieceType 
	{
		public ChargingRook( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Charging Rook", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction( 0,  1 ) );
			type.Slide( new Direction( 0, -1 ) );
			type.Slide( new Direction( 1,  0 ) );
			type.Step( new Direction( -1,  0 ) );
			type.Step( new Direction( -1,  1 ) );
			type.Step( new Direction( -1, -1 ) );
		}
	}
	#endregion

	#region Charging Knight
	[PieceType("Charging Knight", "Chess with Different Armies")]
	public class ChargingKnight: PieceType 
	{
		public ChargingKnight( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "ChargingKnight", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  1,  2 ) );
			type.Step( new Direction(  2,  1 ) );
			type.Step( new Direction(  2, -1 ) );
			type.Step( new Direction(  1, -2 ) );
			type.Step( new Direction( -1,  1 ) );
			type.Step( new Direction( -1,  0 ) );
			type.Step( new Direction( -1, -1 ) );
			type.Step( new Direction(  0,  1 ) );
			type.Step( new Direction(  0, -1 ) );
		}
	}
	#endregion

	#region Colonel
	[PieceType("Colonel", "Chess with Different Armies")]
	public class Colonel: PieceType 
	{
		public Colonel( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Colonel", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  1,  2 ) );
			type.Step( new Direction(  2,  1 ) );
			type.Step( new Direction(  2, -1 ) );
			type.Step( new Direction(  1, -2 ) );
			type.Step( new Direction( -1,  1 ) );
			type.Step( new Direction( -1,  0 ) );
			type.Step( new Direction( -1, -1 ) );
			type.Slide( new Direction( 1,  0 ) );
			type.Slide( new Direction( 0,  1 ) );
			type.Slide( new Direction( 0, -1 ) );
		}
	}
	#endregion
	#endregion


	// *** Miscellaneous Compounds *** //

	#region Miscellaneous Compounds
	#region General
	[PieceType("General", "Miscellaneous Compounds")]
	public class General: PieceType
	{
		public General( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			King.AddMoves( type );
		}
	}
	#endregion

	#region ElephantFerz
	[PieceType("Elephant Ferz", "Miscellaneous Compounds")]
	public class ElephantFerz: PieceType
	{
		public ElephantFerz( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Elephant Ferz", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Elephant.AddMoves( type );
			Ferz.AddMoves( type );
		}
	}
	#endregion

	#region Unicorn
	[PieceType("Unicorn", "Miscellaneous Compounds")]
	public class Unicorn: PieceType
	{
		public Unicorn( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Unicorn", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Bishop.AddMoves( type );
			Knightrider.AddMoves( type );
		}
	}
	#endregion

	#region Squirrel
	[PieceType("Squirrel", "Miscellaneous Compounds")]
	public class Squirrel: PieceType
	{
		public Squirrel( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Squirrel", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Knight.AddMoves( type );
			Elephant.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region Wildebeest
	[PieceType("Wildebeest", "Miscellaneous Compounds")]
	public class Wildebeest: PieceType
	{
		public Wildebeest( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Wildebeest", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Knight.AddMoves( type );
			Camel.AddMoves( type );
		}
	}
	#endregion

	#region Scout
	[PieceType("Scout", "Miscellaneous Compounds")]
	public class Scout: PieceType
	{
		public Scout( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Scout", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Tribbabah.AddMoves( type );
		}
	}
	#endregion

	#region Wizard
	[PieceType("Wizard", "Miscellaneous Compounds")]
	public class Wizard: PieceType
	{
		public Wizard( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Wizard", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Camel.AddMoves( type );
		}
	}
	#endregion

	#region Champion
	[PieceType("Champion", "Miscellaneous Compounds")]
	public class Champion: PieceType
	{
		public Champion( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Champion", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Dabbabah.AddMoves( type );
			Elephant.AddMoves( type );
		}
	}
	#endregion

	#region Centaur
	[PieceType("Centaur", "Miscellaneous Compounds")]
	public class Centaur: PieceType
	{
		public Centaur( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Centaur", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Knight General";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			King.AddMoves( type );
			Knight.AddMoves( type );
		}
	}
	#endregion

	#region Caliph
	[PieceType("Caliph", "Miscellaneous Compounds")]
	public class Caliph: PieceType
	{
		public Caliph( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Caliph", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Camel Bishop";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Bishop.AddMoves( type );
			Camel.AddMoves( type );
		}
	}
	#endregion

	#region Dragon King
	[PieceType("Dragon King", "Miscellaneous Compounds")]
	public class DragonKing: PieceType
	{
		public DragonKing( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Dragon King", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Rook.AddMoves( type );
			Ferz.AddMoves( type );
		}
	}
	#endregion

	#region Dragon Horse
	[PieceType("Dragon Horse", "Miscellaneous Compounds")]
	public class DragonHorse: PieceType
	{
		public DragonHorse( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Dragon Horse", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Bishop.AddMoves( type );
			Wazir.AddMoves( type );
		}
	}
	#endregion

	#region Minister
	[PieceType("Ministor", "Miscellaneous Compounds")]
	public class Minister: PieceType
	{
		public Minister( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Minister", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Knight.AddMoves( type );
			Wazir.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region High Priestess
	[PieceType("High Priestess", "Miscellaneous Compounds")]
	public class HighPriestess: PieceType
	{
		public HighPriestess( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "High Priestess", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Knight.AddMoves( type );
			Elephant.AddMoves( type );
			Ferz.AddMoves( type );
		}
	}
	#endregion
	#endregion


	// *** Multi-Path *** //

	#region Multi-Path
	#region Falcon
	[PieceType("Falcon", "Multi-Path")]
	public class Falcon: PieceType
	{
		public Falcon( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Falcon", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			FallbackImage = "Bird";
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 3, 1 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 0 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, 1 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, 0 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 1 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 0 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 1, 1 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 0, 1 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 1 ), new Direction( 0, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 0, 1 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 1, 1 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 0, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1,  0 ), new Direction( 1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1,  0 ), new Direction( 1, -1 ), new Direction( 1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1,  0 ), new Direction( 1,  0 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, -1 ), new Direction( 1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1,  0 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1,  0 ), new Direction( 1, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 1, -1 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 0, -1 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, -1 ), new Direction( 0, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 0, -1 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 1, -1 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 0, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 0 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, 1 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, 0 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 1 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 0 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, 1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction(  0, 1 ), new Direction( -1, 1 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction(  0, 1 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 1 ), new Direction(  0, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction(  0, 1 ), new Direction(  0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction(  0, 1 ), new Direction( -1, 1 ), new Direction(  0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction(  0, 1 ), new Direction(  0, 1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1,  0 ), new Direction( -1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1,  0 ), new Direction( -1, -1 ), new Direction( -1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1,  0 ), new Direction( -1,  0 ), new Direction( -1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, -1 ), new Direction( -1,  0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1,  0 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1,  0 ), new Direction( -1, -1 ), new Direction( -1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction(  0, -1 ), new Direction( -1, -1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction(  0, -1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, -1 ), new Direction(  0, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction(  0, -1 ), new Direction(  0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction(  0, -1 ), new Direction( -1, -1 ), new Direction(  0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction(  0, -1 ), new Direction(  0, -1 ), new Direction( -1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region FreePadwar
	[PieceType("Free Padwar", "Multi-Path")]
	public class FreePadwar: PieceType
	{
		public FreePadwar( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Free Padwar", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
			FallbackImage = "Elephant";
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  1,  1 ), 2 );
			type.Slide( new Direction(  1, -1 ), 2 );
			type.Slide( new Direction( -1,  1 ), 2 );
			type.Slide( new Direction( -1, -1 ), 2 );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 2, 0 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region ChainedPadwar
	[PieceType("Chained Padwar", "Multi-Path")]
	public class ChainedPadwar: PieceType
	{
		public ChainedPadwar( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Chained Padwar", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			MoveCapability move = new MoveCapability();
			move.MinSteps = 2;
			move.MaxSteps = 2;
			move.CanCapture = true;
			move.Direction = new Direction( 1, 1 );
			type.AddMoveCapability( move );

			move = new MoveCapability();
			move.MinSteps = 2;
			move.MaxSteps = 2;
			move.CanCapture = true;
			move.Direction = new Direction( 1, -1 );
			type.AddMoveCapability( move );

			move = new MoveCapability();
			move.MinSteps = 2;
			move.MaxSteps = 2;
			move.CanCapture = true;
			move.Direction = new Direction( -1, 1 );
			type.AddMoveCapability( move );

			move = new MoveCapability();
			move.MinSteps = 2;
			move.MaxSteps = 2;
			move.CanCapture = true;
			move.Direction = new Direction( -1, -1 );
			type.AddMoveCapability( move );

			//	add the multi-path moves
			move = MoveCapability.Step( new Direction( 2, 0 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
			move = new MoveCapability();

			move = MoveCapability.Step( new Direction( -2, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region Oliphant
	[PieceType("Oliphant", "Multi-Path")]
	public class Oliphant: PieceType
	{
		public Oliphant( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Oliphant", name, notation, midgameValue, endgameValue, preferredImageName == null ? "Ferz Elephantrider" : preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Elephant.AddMoves( type );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 3, 3 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 2 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, -2 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 2 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, -2 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 4, 4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 2 ), new Direction( 2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 4, -4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, -2 ), new Direction( 2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -4, 4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 2 ), new Direction( -2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -4, -4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, -2 ), new Direction( -2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region Lightning Warmachine
	[PieceType("Lightning Warmachine", "Multi-Path")]
	public class LightningWarmachine: PieceType
	{
		public LightningWarmachine( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Lightning Warmachine", name, notation, midgameValue, endgameValue, preferredImageName == null ? "Wazir Dabbabahrider" : preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Dabbabah.AddMoves( type );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 3, 0 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 0 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 0 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, 2 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 0, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, -2 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 0, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 4, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 0 ), new Direction( 2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -4, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 0 ), new Direction( -2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, 2 ), new Direction( 0, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -4 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, -2 ), new Direction( 0, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region Bent Hero
	[PieceType("Bent Hero", "Multi-Path")]
	public class BentHero: PieceType
	{
		public BentHero( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Bent Hero", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Wazir.AddMoves( type );
			Dabbabah.AddMoves( type );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 3, 0 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 0 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 0 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, 2 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 0, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, -2 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 0, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 0 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 0 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( -2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 0, 2 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 2 ), new Direction( 1, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( 0, 2 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 2 ), new Direction( -1, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 0 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( -2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 0 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 2, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( 0, -2 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -2 ), new Direction( -1, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 0, -2 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -2 ), new Direction( 1, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region Bent Shaman
	[PieceType( "Bent Shaman", "Multi-Path" )]
	public class BentShaman: PieceType
	{
		public BentShaman( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Bent Shaman", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Elephant.AddMoves( type );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 3, 3 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 2 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 2 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, -2 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, -2 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 2 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 2 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, 2 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( 2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, 3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, 2 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( -2, 2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -3, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, -2 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 3, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, -2 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -2, -2 ), new Direction( 1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( -2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, -3 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 2, -2 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( 2, -2 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion

	#region Sliding General
	[PieceType("Sliding General", "Multi-Path")]
	public class SlidingGeneral: PieceType
	{
		public SlidingGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Sliding General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Ferz.AddMoves( type );
			Wazir.AddMoves( type );

			//	add the multi-path moves
			MoveCapability move = MoveCapability.Step( new Direction( 2, 0 ) );
			MovePathInfo movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 1 ), new Direction( -1, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( 1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( 0, 1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, 1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, 0 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, 1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, 1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, 0 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, 0 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -2, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( -1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( -1, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( -1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 0, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( -1, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( -1, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 1, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 0, -1 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 0, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, -2 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );

			move = MoveCapability.Step( new Direction( 2, -1 ) );
			movePath = new MovePathInfo();
			movePath.AddPath( new List<Direction>() { new Direction( 1, -1 ), new Direction( 1, 0 ) } );
			movePath.AddPath( new List<Direction>() { new Direction( 1, 0 ), new Direction( 1, -1 ) } );
			move.PathInfo = movePath;
			type.AddMoveCapability( move );
		}
	}
	#endregion
	#endregion


	// *** Shogi *** //

	#region Shogi
	#region Gold General
	[PieceType("Gold General", "Shogi")]
	public class GoldGeneral: PieceType
	{
		public GoldGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Gold General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction( 1, 0 ) );
			type.Step( new Direction( 1, 1 ) );
			type.Step( new Direction( 1, -1 ) );
			type.Step( new Direction( 0, 1 ) );
			type.Step( new Direction( 0, -1 ) );
			type.Step( new Direction( -1, 0 ) );
		}
	}
	#endregion

	#region Silver General
	[PieceType("Silver General", "Shogi")]
	public class SilverGeneral: PieceType
	{
		public SilverGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Silver General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  1,  0 ) );
			type.Step( new Direction(  1,  1 ) );
			type.Step( new Direction(  1, -1 ) );
			type.Step( new Direction( -1,  1 ) );
			type.Step( new Direction( -1, -1 ) );
		}
	}
	#endregion

	#region Copper General
	[PieceType("Copper General", "Shogi")]
	public class CopperGeneral: PieceType
	{
		public CopperGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Copper General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Step( new Direction(  1,  0 ) );
			type.Step( new Direction(  1,  1 ) );
			type.Step( new Direction(  1, -1 ) );
			type.Step( new Direction( -1,  0 ) );
		}
	}
	#endregion

	#region Vertical Mover
	[PieceType("Vertical Mover", "Shogi")]
	public class VerticalMover: PieceType
	{
		public VerticalMover( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Vertical Mover", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  1,  0 ) );
			type.Slide( new Direction( -1,  0 ) );
			type.Step( new Direction(  0,  1 ) );
			type.Step( new Direction(  0, -1 ) );
		}
	}
	#endregion

	#region Side Mover
	[PieceType("Side Mover", "Shogi")]
	public class SideMover: PieceType
	{
		public SideMover( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Side Mover", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction( 0,  1 ) );
			type.Slide( new Direction( 0, -1 ) );
			type.Step( new Direction(  1,  0 ) );
			type.Step( new Direction( -1,  0 ) );
		}
	}
	#endregion
	#endregion


	// *** Miscellaneous *** //

	#region Miscellaneous
	#region Cannon
	[PieceType( "Cannon", "Miscellaneous" )]
	public class Cannon: PieceType
	{
		public Cannon( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Cannon", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );

			#region Customize piece-square-tables for the Cannon
			//	Midgame PST
			PSTMidgameInSmallCenter = 0;
			PSTMidgameInLargeCenter = 0;
			PSTMidgameSmallCenterAttacks = 2;
			PSTMidgameLargeCenterAttacks = 4;
			PSTMidgameForwardness = 0;
			PSTMidgameGlobalOffset = 0;
			//	Endgame PST
			PSTEndgameInSmallCenter = 0;
			PSTEndgameInLargeCenter = 0;
			PSTEndgameSmallCenterAttacks = 0;
			PSTEndgameLargeCenterAttacks = 0;
			PSTEndgameForwardness = 0;
			PSTEndgameGlobalOffset = 0;
			#endregion
		}

		public static new void AddMoves( PieceType type )
		{
			type.CannonMove( new Direction( 0, 1 ) );
			type.CannonMove( new Direction( 0, -1 ) );
			type.CannonMove( new Direction( 1, 0 ) );
			type.CannonMove( new Direction( -1, 0 ) );
		}
	}
	#endregion

	#region Vao
	[PieceType("Vao", "Miscellaneous")]
	public class Vao: PieceType
	{
		public Vao( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ) :
			base( "Vao", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.CannonMove( new Direction( 1, 1 ) );
			type.CannonMove( new Direction( 1, -1 ) );
			type.CannonMove( new Direction( -1, 1 ) );
			type.CannonMove( new Direction( -1, -1 ) );
		}
	}
	#endregion

	#region Camel General
	[PieceType("Camel General", "Miscellaneous")]
	public class CamelGeneral: PieceType
	{
		public CamelGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Camel General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			King.AddMoves( type );
			Camel.AddMoves( type );
		}
	}
	#endregion

	#region Jumping General
	[PieceType("Jumping General", "Miscellaneous")]
	public class JumpingGeneral: PieceType
	{
		public JumpingGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Jumping General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			King.AddMoves( type );
			Elephant.AddMoves( type );
			Dabbabah.AddMoves( type );
		}
	}
	#endregion

	#region VerticalMoverGeneral
	[PieceType("Vertical Mover General", "Miscellaneous")]
	public class VerticalMoverGeneral: PieceType
	{
		public VerticalMoverGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Vertical Mover General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction(  1,  0 ) );
			type.Slide( new Direction( -1,  0 ) );
			type.Step( new Direction(  0,  1 ) );
			type.Step( new Direction(  0, -1 ) );
			Ferz.AddMoves( type );
		}
	}
	#endregion

	#region SideMoverGeneral
	[PieceType("Side Mover General", "Miscellaneous")]
	public class SideMoverGeneral: PieceType
	{
		public SideMoverGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Side Mover General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			type.Slide( new Direction( 0,  1 ) );
			type.Slide( new Direction( 0, -1 ) );
			type.Step( new Direction(  1,  0 ) );
			type.Step( new Direction( -1,  0 ) );
			Ferz.AddMoves( type );
		}
	}
	#endregion

	#region SquirrelGeneral
	[PieceType("Squirrel General", "Miscellaneous")]
	public class SquirrelGeneral: PieceType
	{
		public SquirrelGeneral( string name, string notation, int midgameValue, int endgameValue, string preferredImageName = null ):
			base( "Squirrel General", name, notation, midgameValue, endgameValue, preferredImageName )
		{
			AddMoves( this );
		}

		public static new void AddMoves( PieceType type )
		{
			Squirrel.AddMoves( type );
			King.AddMoves( type );
		}
	}
	#endregion
	#endregion
}
