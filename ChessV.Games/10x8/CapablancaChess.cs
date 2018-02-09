
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
	//********************************************************************
	//
	//                       CapablancaChess
	//
	//    This class implements Jose Raul Capablanca's 10x8 variant 
	//    that adds the missing compounds to Chess.  It is used as a 
	//    the base class for a number of similar variants that differ 
	//    mostly in piece arrangement (array) and castling rule.

	[Game("Capablanca Chess", typeof(Geometry.Rectangular), 10, 8, 
		  XBoardName = "capablanca",
		  Invented = "1940",
		  InventedBy = "Jose Raul Capablanca", 
		  Tags = "Chess Variant,Capablanca Variant,Historic,Popular")]
	[Game("Bird's Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "1874",
		  InventedBy = "Henry Bird", 
		  Tags = "Chess Variant,Capablanca Variant,Historic",
		  Definitions="Array=rnbcqkabnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNBCQKABNR")]
	[Game("Carrera's Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "1617",
		  InventedBy = "Pietro Carrera", 
		  Tags = "Chess Variant,Capablanca Variant,Historic",
		  Definitions = "Castling=None;Array=rcnbkqbnar/pppppppppp/10/10/10/10/PPPPPPPPPP/RCNBKQBNAR")]
	[Game("Embassy Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2005",
		  InventedBy = "Kevin Hill", 
		  Tags = "Chess Variant,Capablanca Variant",
		  Definitions = "Array=rnbqkcabnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNBQKCABNR")]
	[Game("Schoolbook Chess", typeof(Geometry.Rectangular), 10, 8, 
		  XBoardName = "schoolbook",
		  Invented = "2006",
		  InventedBy = "Sam Trenholme", 
		  Tags = "Chess Variant,Capablanca Variant",
		  Definitions = "Castling=Flexible;Array=rqnbakbncr/pppppppppp/10/10/10/10/PPPPPPPPPP/RQNBAKBNCR")]
	[Game("Gothic Chess", typeof(Geometry.Rectangular), 10, 8,
		  XBoardName = "gothic",
		  Invented = "2002",
		  InventedBy = "Ed Trice",
		  Tags = "Chess Variant,Capablanca Variant",
		  Definitions = "Castling=Standard;Array=rnbqckabnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNBQCKABNR")]
	[Game("Victorian Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2005",
		  InventedBy = "David Paulowich;John Kipling Lewis", 
		  Tags = "Chess Variant,Capablanca Variant",
		  Definitions="Castling=Close-Rook;Array=crnbkabnrq/pppppppppp/10/10/10/10/PPPPPPPPPP/CRNBKABNRQ;PromotionTypes=QCA")]
	[Game("Opti Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2006",
		  InventedBy = "Derek Nalls", 
		  Tags = "Chess Variant,Capablanca Variant",
		  Definitions="Castling=Close-Rook;Array=nrcbqkbarn/pppppppppp/10/10/10/10/PPPPPPPPPP/NRCBQKBARN")]
	[Appearance(ColorScheme="Sahara", Game="Schoolbook Chess")]
	public class CapablancaChess: Abstract.Generic10x8
	{
		// *** PIECE TYPES *** //

		public PieceType Queen;
		public PieceType Rook;
		public PieceType Bishop;
		public PieceType Knight;
		public PieceType Archbishop;
		public PieceType Chancellor;


		// *** CONSTRUCTION *** //

		public CapablancaChess(): 
			base
				( /* symmetry = */ new MirrorSymmetry() )
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rnabqkbcnr/pppppppppp/10/10/10/10/PPPPPPPPPP/RNABQKBCNR";
			PawnDoubleMove = true;
			EnPassant = true;
			Castling.Value = "Standard";
			PromotionRule.Value = "Standard";
			PromotionTypes = "QCARBN";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			AddPieceType( Queen = new Queen( "Queen", "Q", 1100, 1150 ) );
			AddPieceType( Rook = new Rook( "Rook", "R", 550, 600 ) );
			AddPieceType( Bishop = new Bishop( "Bishop", "B", 350, 350 ) );
			AddPieceType( Knight = new Knight( "Knight", "N", 300, 300 ) );
			AddPieceType( Archbishop = new Archbishop( "Archbishop", "A", 975, 1000 ) );
			AddPieceType( Chancellor = new Chancellor( "Chancellor", "C", 1050, 1100 ) );
		}
		#endregion


		// *** XBOARD ENGINE SUPPORT *** //

		#region TryCreateAdaptor
		public override EngineGameAdaptor TryCreateAdaptor( EngineConfiguration config )
		{
			if( config.SupportedVariants.Contains( "capablanca" ) &&
				config.SupportedFeatures.Contains( "setboard" ) && 
				Castling.Value == "Standard" )
			{
				//	It is possible that this engine might be adaptable ...
				
				//	Verify the basics.  If pawn moves are different, forget it
				if( PawnDoubleMove == true && EnPassant == true )
				{
					EngineGameAdaptor adaptor = new EngineGameAdaptor( "capablanca" );
					adaptor.IssueSetboard = true;
					//	Do we need to translate piece notations?
					if( Chancellor.Notation != "C" )
						adaptor.TranslatePieceNotation( Chancellor.Notation, "C" );
					if( Archbishop.Notation != "A" )
						adaptor.TranslatePieceNotation( Archbishop.Notation, "A" );
					//	If the Kings are on the "f" file, we're good with what we have
					if( StartingPieces["f1"] == new GenericPiece( 0, King ) )
						return adaptor;
					//	If the Kings are on the "e" file, we can do it, but we need 
					//	to mirror the board (make the engine think we are playing a 
					//	mirror image.)
					if( StartingPieces["e1"] == new GenericPiece( 0, King ) )
					{
						adaptor.MirrorBoard = true;
						return adaptor;
					}
				}
			}
			return null;
		}
		#endregion

	}

	[Game("Modern Carrera's Chess", typeof(Geometry.Rectangular), 10, 8,
		  XBoardName = "moderncarrera",
		  Invented = "1999",
		  InventedBy = "Fergus Duniho;Sam Trenholme", 
		  Tags = "Chess Variant,Capablanca Variant")]
	[Appearance(ColorScheme="Buckingham Green")]
	public class ModernCarrerasChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public ModernCarrerasChess()
		{
		}


		// *** INITIALIZATION *** //

		#region ChangePieceNames
		public override void ChangePieceNames()
		{
			Chancellor.Name = "Marshall";
			Chancellor.Notation = "M";
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "ranbqkbnmr/pppppppppp/10/10/10/10/PPPPPPPPPP/RANBQKBNMR";
			Castling.Value = "Standard";
			PromotionTypes = "QMARBN";
		}
		#endregion
	}

	[Game("Grotesque Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2004",
		  InventedBy = "Fergus Duniho",
		  Tags = "Chess Variant,Capablanca Variant")]
	[Appearance(ColorScheme="Cinnamon", PieceSet="Abstract")]
	public class GrotesqueChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public GrotesqueChess()
		{
		}


		// *** INITIALIZATION *** //

		#region ChangePieceNames
		public override void ChangePieceNames()
		{
			Archbishop.Name = "Equerry";
			Archbishop.Notation = "E";
			Chancellor.Name = "Guard";
			Chancellor.Notation = "G";
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rbqnkgnebr/pppppppppp/10/10/10/10/PPPPPPPPPP/RBQNKGNEBR";
			Castling.Value = "Flexible";
			PromotionTypes = "QGERBN";
		}
		#endregion
	}

	[Game("Ladorean Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2005",
		  InventedBy = "Bernhard U. Hermes", 
		  Tags = "Chess Variant,Capablanca Variant")]
	public class LadoreanChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public LadoreanChess()
		{
		}


		// *** INITIALIZATION *** //

		#region ChangePieceNames
		public override void ChangePieceNames()
		{
			Archbishop.Name = "Cardinal";
			Archbishop.Notation = "C";
			Chancellor.Name = "Marshall";
			Chancellor.Notation = "M";
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rbqnkcnmbr/pppppppppp/10/10/10/10/PPPPPPPPPP/RBQNKCNMBR";
			Castling.Value = "Flexible";
			PromotionTypes = "QMCRBN";
		}
		#endregion
	}

	[Game("Univers Chess", typeof(Geometry.Rectangular), 10, 8,
		  Invented = "2006",
		  InventedBy = "Fergus Duniho;Bruno Violet", 
		  Tags = "Chess Variant,Capablanca Variant")]
	public class UniversChess: CapablancaChess
	{
		// *** CONSTRUCTION *** //

		public UniversChess()
		{
		}


		// *** INITIALIZATION *** //

		#region ChangePieceNames
		public override void ChangePieceNames()
		{
			Archbishop.Name = "Paladin";
			Archbishop.Notation = "A";
			Chancellor.Name = "Marshall";
			Chancellor.Notation = "M";
		}
		#endregion

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "rbnmqkanbr/pppppppppp/10/10/10/10/PPPPPPPPPP/RBNMQKANBR";
			Castling.Value = "Flexible";
			PromotionTypes = "QMARBN";
		}
		#endregion
	}
}
