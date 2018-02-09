
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
using ChessV.Games.Rules;

namespace ChessV.Games.Abstract
{
	//**********************************************************************
	//
	//                          GenericChess
	//
	//    The Generic game classes make it easier to specify chess-like 
	//    games by providing functionality common to chess variants.  
	//    This is the base class from which other "Generic" classes are 
	//    derived to provide even more common functionality.  
	//
	//    This class provides the requirements for a royal King and 
	//    standard Chess pawns, defines the 50-move rule and draw by 
	//    repitition rule, and sets the standard FEN format.

	public class GenericChess: Game
	{
		// *** PIECE TYPES *** //

		[Royal] public PieceType King;
		public PieceType Pawn;


		// *** GAME VARIABLES *** //

		[GameVariable] public ChoiceVariable StalemateResult { get; set; }
		[GameVariable] public ChoiceVariable PromotionRule { get; set; }
		[GameVariable] public string PromotionTypes { get; set; }
		[GameVariable] public bool BareKing { get; set; }

		
        // *** CONSTRUCTION *** //

        public GenericChess
			( int nFiles,               // number of files on the board
			  int nRanks,               // number of ranks on the board
			  Symmetry symmetry ):      // symmetry determining board mirroring/rotation
                base( 2, nFiles, nRanks, symmetry )
        {
        }


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			FENFormat = "{array} {current player} {castling} {en-passant} {half-move clock} {turn number}";
			FENStart = "#{Array} w #default #default 0 1";
			StalemateResult = new ChoiceVariable( new string[] { "Draw", "Win", "Loss" } );
			StalemateResult.Value = "Draw";
			PromotionRule = new ChoiceVariable( new string[] { "None", "Standard", "Replacement", "Custom" } );
			PromotionRule.Value = "Standard";
			PromotionTypes = "";
			BareKing = false;
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			AddPieceType( King = new King( "King", "K", 0, 0 ) );
			AddPieceType( Pawn = new Pawn( "Pawn", "P", 100, 125 ) );
			castlingType = King;
			pawnType = Pawn;
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			// *** PROMOTION *** //
			if( PromotionRule.Value == "Standard" )
			{
				List<PieceType> availablePromotionTypes = ParseTypeListFromString( PromotionTypes );
				BasicPromotionRule( pawnType, availablePromotionTypes, loc => loc.Rank == Board.NumRanks - 1 );
			}
			else if( PromotionRule.Value == "Replacement" )
				PromoteByReplacementRule( Pawn, loc => loc.Rank == Board.NumRanks - 1 ? 
					Rules.PromotionOption.MustPromote : Rules.PromotionOption.CannotPromote );

			// *** BARE KING *** //
			if( BareKing )
				AddRule( new Rules.BareKingRule() );

			//	We added the Bare King rule first before calling the 
			//	base class because BareKing must happen before the 
			//	checkmate rule to work correctly
			base.AddRules();

			// *** STALEMATE RESULT *** //
			if( StalemateResult.Value == "Loss" )
				((Rules.CheckmateRule) FindRule( typeof(Rules.CheckmateRule), true )).StalemateResult = MoveEventResponse.GameLost;
			else if( StalemateResult.Value == "Win" )
				((Rules.CheckmateRule) FindRule( typeof( Rules.CheckmateRule ), true )).StalemateResult = MoveEventResponse.GameWon;

			// *** FIFTY-MOVE RULE *** //
			AddRule( new Rules.Move50Rule( Pawn ) );

			// *** DRAW-BY-REPETITION RULE *** //
			AddRule( new Rules.RepetitionDrawRule() );
		}
		#endregion

		#region ReorderRules
		public override void ReorderRules()
		{
			//	pass to base class first
			base.ReorderRules();
			//	if we have a RepetitionDrawRule, it needs to be at the 
			//	end of the list so it will get the messages last
			Rule repetitionDrawRule = null;
			foreach( Rule rule in rules )
			{
				if( rule is RepetitionDrawRule )
				{
					repetitionDrawRule = rule;
					rules.Remove( rule );
					break;
				}
			}
			if( repetitionDrawRule != null )
				rules.Add( repetitionDrawRule );
		}
		#endregion

		#region AddEvaluations
		public override void AddEvaluations()
		{
			base.AddEvaluations();

			//  Add pawn structure evaluation
			AddEvaluation( new Evaluations.PawnStructureEvaluation() );

			//	Add development evaluation
			AddEvaluation( new Evaluations.DevelopmentEvaluation() );

			//	Check for colorbound pieces
			bool colorboundPieces = false;
			for( int x = 0; x < nPieceTypes; x++ )
				if( pieceTypes[x].NumSlices == 2 )
					colorboundPieces = true;
			if( colorboundPieces )
				AddEvaluation( new Evaluations.ColorbindingEvaluation() );
		}
		#endregion


		// *** HELPER FUNCTIONS *** //

		#region EnPassant
		public void EnPassantRule( PieceType pawnType, int nDirection )
		{ AddRule( new EnPassantRule( pawnType, nDirection ) ); }

		public void EnPassantRule( PieceType pawnType, Direction direction )
		{ EnPassantRule( pawnType, GetDirectionNumber( direction ) ); }
		#endregion

		#region Castling
		public void CastlingRule()
		{
			castlingRule = new CastlingRule();
			AddRule( castlingRule );
		}

		public void FlexibleCastlingRule()
		{
			castlingRule = new FlexibleCastlingRule();
			AddRule( castlingRule );
		}

		public void castlingMove( int player, int kingFrom, int kingTo, int otherFrom, int otherTo, char privChar )
		{
			castlingRule.AddCastlingMove( player, kingFrom, kingTo, otherFrom, otherTo, privChar );
		}

		public void CastlingMove( int player, string kingFrom, string kingTo, string otherFrom, string otherTo, char privChar )
		{
			castlingMove( player, NotationToSquare( kingFrom ), NotationToSquare( kingTo ),
				NotationToSquare( otherFrom ), NotationToSquare( otherTo ), privChar );
		}

		public void FlexibleCastlingMove( int player, string kingFrom, string kingTo, string otherFrom, char privChar, bool allowMoveOntoCastlingPiece = false )
		{
			castlingMove( player, NotationToSquare( kingFrom ), NotationToSquare( kingTo ),
				NotationToSquare( otherFrom ), allowMoveOntoCastlingPiece ? 1 : 0, privChar );
		}
		#endregion

		#region Promotion
		public void BasicPromotionRule( PieceType promotingType, List<PieceType> availablePromotionTypes, ConditionalLocationDelegate destinationConditionDelegate )
		{
			AddRule( new BasicPromotionRule( promotingType, availablePromotionTypes, destinationConditionDelegate ) );
		}

		public void BasicPromotionRule( PieceType promotingType, List<PieceType> availablePromotionTypes, ConditionalLocationDelegate destinationConditionDelegate, ConditionalLocationDelegate originConditionDelegate )
		{
			AddRule( new BasicPromotionRule( promotingType, availablePromotionTypes, destinationConditionDelegate, originConditionDelegate ) );
		}

		public void PromoteByReplacementRule( PieceType promotingType, OptionalPromotionLocationDelegate conditionDelegate )
		{
			AddRule( new PromoteByReplacementRule( promotingType, conditionDelegate ) );
		}
		#endregion


		// *** PROTECTED DATA MEMBERS *** //

		protected CastlingRule castlingRule;
		protected PieceType castlingType;
		protected PieceType pawnType;
	}
}
