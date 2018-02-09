
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
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	public partial class MoveSelectForm: Form
	{
		protected Game game;
		protected List<MoveInfo> moves;
		public MoveInfo SelectedMove { get; private set; }

		public MoveSelectForm( Game game, List<MoveInfo> moves )
		{
			this.game = game;
			this.moves = moves;

			InitializeComponent();
		}

		private void MoveSelectForm_Load( object sender, EventArgs e )
		{
			//	When we populate the list with the moves from which to select, we 
			//	want to display a "friendly" description of the options where appropriate.
			//	What follows are handlers for various options.

			//	List of promotions: this is the most common option.  All the moves 
			//	are the same except for the type of piece to which to promote.
			MoveInfo move1 = moves[0];
			bool allMovesMatch = true;
			int nonPromotionMoves = 0;
			foreach( MoveInfo move in moves )
			{
				if( move.FromSquare != move1.FromSquare ||
					move.ToSquare != move1.ToSquare ||
					(move.MoveType | MoveType.PromotionProperty) != (move1.MoveType | MoveType.PromotionProperty) )
				{
					allMovesMatch = false;
					break;
				}
				if( (move.MoveType & MoveType.PromotionProperty) == 0 )
				{
					nonPromotionMoves++;
					if( move == move1 )
						move1 = moves[1];
				}
			}
			if( allMovesMatch && nonPromotionMoves <= 1 )
			{
				foreach( MoveInfo move in moves )
				{
					string description = move.PromotionType == 0
						? "Do not promote"
						: "Promote to " + game.GetPieceType( move.PromotionType ).Name;
					listMoves.Items.Add( description );
				}
				return;
			}

			//	Choice of extra captures: this happens in Odin's Rune for example.
			//	The moves are same except for choice of extra piece to capture.
			allMovesMatch = true;
			int nonExtraCaptureMoves = 0;
			foreach( MoveInfo move in moves )
			{
				if( move.FromSquare != move1.FromSquare ||
					move.ToSquare != move1.ToSquare ||
					(move.MoveType != MoveType.ExtraCapture && move.MoveType != MoveType.StandardMove && move.MoveType != MoveType.StandardCapture) )
				{
					if( (move.MoveType & MoveType.CustomMove) == MoveType.CustomMove )
					{
						//	if this is a custom move, it does not necessarily mean we can't 
						//	use this handling, but it requires a Rule that provides a description
						string description = game.DescribeMove( move, MoveNotation.MoveSelectionText );
						if( description != null )
							continue;
					}
					allMovesMatch = false;
					break;
				}
				if( move.MoveType != MoveType.ExtraCapture )
					nonExtraCaptureMoves++;
			}
			if( allMovesMatch && nonExtraCaptureMoves <= 1 )
			{
				foreach( MoveInfo move in moves )
				{
					string description = (move.MoveType & MoveType.CustomMove) == MoveType.CustomMove 
						? game.DescribeMove( move, MoveNotation.MoveSelectionText )
						: (move.MoveType != MoveType.ExtraCapture
							? "No extra capture"
							: "Capture piece on " + game.GetSquareNotation( move.Tag ));
					listMoves.Items.Add( description );
				}
				return;
			}

			//	Choice of locations for move relay target: this also happens in 
			//	Odin's Rune.  MoveRelay moves on top of another piece and then 
			//	moves that piece to another location.  If there is a choice of 
			//	locations, we'll handle that here.
			allMovesMatch = true;
			int nonMoveRelayMoves = 0;
			foreach( MoveInfo move in moves )
			{
				if( move.FromSquare != move1.FromSquare ||
					move.ToSquare != move1.ToSquare ||
					(move.MoveType != MoveType.MoveRelay && move.MoveType != MoveType.StandardMove && move.MoveType != MoveType.StandardCapture) )
				{
					allMovesMatch = false;
					break;
				}
				if( move.MoveType != MoveType.MoveRelay )
					nonMoveRelayMoves++;
			}
			if( allMovesMatch && nonMoveRelayMoves <= 1 )
			{
				foreach( MoveInfo move in moves )
				{
					string description = move.MoveType != MoveType.MoveRelay
						? (move.MoveType == MoveType.StandardMove ? "Standard move" : "Standard capture")
						: "Relocate target to  " + game.GetSquareNotation( move.Tag );
					listMoves.Items.Add( description );
				}
				return;
			}

			//	Choice of move types: if every possible move is of a different type, 
			//	and they are named below, then just display the list of move types
			if( moveTypeNames == null )
			{
				moveTypeNames = new Dictionary<MoveType, string>();
				moveTypeNames.Add( MoveType.StandardMove, "Standard Move" );
				moveTypeNames.Add( MoveType.StandardCapture, "Capture" );
				moveTypeNames.Add( MoveType.MoveWithPromotion, "Move and Promote" );
				moveTypeNames.Add( MoveType.CaptureWithPromotion, "Capture and Promote" );
				moveTypeNames.Add( MoveType.Castling, "Castling" );
				moveTypeNames.Add( MoveType.EnPassant, "En Passant Capture" );
				moveTypeNames.Add( MoveType.Drop, "Drop Piece" );
				moveTypeNames.Add( MoveType.Replace, "Replace Piece" );
				moveTypeNames.Add( MoveType.BaroqueCapture, "Rifle Capture" );
				moveTypeNames.Add( MoveType.Swap, "Swap" );
				moveTypeNames.Add( MoveType.Pass, "Pass" );
			}
			allMovesMatch = true;
			List<MoveType> typesFound = new List<MoveType>();
			foreach( MoveInfo move in moves )
			{
				if( (move.MoveType == MoveType.BaroqueCapture && move.ToSquare != move.Tag) || 
					!moveTypeNames.ContainsKey( move.MoveType ) || typesFound.Contains( move.MoveType ) )
				{
					allMovesMatch = false;
					break;
				}
				typesFound.Add( move.MoveType );
			}
			if( allMovesMatch )
			{
				foreach( MoveInfo move in moves )
					listMoves.Items.Add( moveTypeNames[move.MoveType] );
				return;
			}

			//	Fallback option: just display the move's generic "Descriptive" notation
			foreach( MoveInfo move in moves )
			{
				string description = game.DescribeMove( move, MoveNotation.Descriptive );
				listMoves.Items.Add( description );
			}
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			int index = listMoves.SelectedIndex;
			if( index >= 0 )
			{
				SelectedMove = moves[index];
				DialogResult = System.Windows.Forms.DialogResult.OK;
				Close();
			}
		}

		private static Dictionary<MoveType, string> moveTypeNames;
	}
}
