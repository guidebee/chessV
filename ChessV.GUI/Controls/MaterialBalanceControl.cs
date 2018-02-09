
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
using System.Windows.Forms;
using System.Drawing.Imaging;
using ChessV;

namespace ChessV.GUI
{
	public partial class MaterialBalanceControl: UserControl
	{
		// *** PROPERTIES *** //

		public Theme Theme { get; set; }
		public Game Game { get; private set; }


		// *** PROTECTED DATA *** //

		protected List<Piece>[] unmatchedPieces;
		protected BitmapPieceSetPresentation pieceSetPresentation;
		protected int squareWidth;
		protected int squareHeight;


		// *** CONSTRUCTION *** //

		public MaterialBalanceControl()
		{
			InitializeComponent();
		}


		// *** INITIALIZATION *** //

		public void Initialize( Game game, Board board, Theme theme )
		{
			Game = game;
			UpdateTheme( theme );
			unmatchedPieces = new List<Piece>[game.NumPlayers];
			UpdateUnmatchedPieceLists();
			game.MovePlayed += MovePlayed;
		}


		// *** OPERATIONS *** //

		public void MovePlayed( MoveInfo moveInfo )
		{
			UpdateUnmatchedPieceLists();
			Invalidate();
		}

		public void UpdateTheme( Theme theme )
		{
			Theme = theme;
			squareWidth = 50;
			squareHeight = 50;
			if( Theme.PieceSet is BitmapPieceSet )
			{
				squareWidth = ((BitmapPieceSet) Theme.PieceSet).Width + 5;
				squareHeight = ((BitmapPieceSet) Theme.PieceSet).Height + 5;
				pieceSetPresentation = new BitmapPieceSetPresentation( Game, (BitmapPieceSet) Theme.PieceSet );
				pieceSetPresentation.Initialize( Theme );
			}
			else
				throw new Exception( "Non-Bitmap piece sets not implemented yet" );
			Invalidate();
		}

		private void UpdateUnmatchedPieceLists()
		{
			for( int player = 0; player < Game.NumPlayers; player++ )
				unmatchedPieces[player] = Game.GetPieceList( player );
			for( int player = 0; player < Game.NumPlayers; player++ )
			{
				List<Piece> myPieces = unmatchedPieces[player];
				List<Piece> hisPieces = new List<Piece>( Game.GetPieceList( player ^ 1 ) );
				List<Piece> matchedPieces = new List<Piece>();
				foreach( Piece piece in myPieces )
				{
					foreach( Piece match in hisPieces )
						if( piece.PieceType == match.PieceType )
						{
							matchedPieces.Add( piece );
							hisPieces.Remove( match );
							break;
						}
				}
				foreach( Piece match in matchedPieces )
					unmatchedPieces[player].Remove( match );
			}
		}


		// *** EVENT HANDLERS *** //

		//	control's paint event handler
		private void MaterialBalanceControl_Paint( object sender, PaintEventArgs e )
		{
			SolidBrush br1 = new SolidBrush( BackColor );
			e.Graphics.FillRectangle( br1, e.ClipRectangle );

			//	If game is null it probably means we're in the Forms Designer
			//	so none of the following would really work.
			if( Game != null )
			{
				for( int player = 0; player < Game.NumPlayers; player++ )
				{
					int n = 0;
					foreach( Piece piece in unmatchedPieces[player] )
					{
						int xoffset = pieceSetPresentation.PieceSet.Width / 2;
						int yoffset = pieceSetPresentation.PieceSet.Height / 2;

						pieceSetPresentation.RenderFloatingPiece( e.Graphics, new Point( (n * squareWidth) + xoffset,
							((Game.NumPlayers - player - 1) * squareHeight) + yoffset ), piece );
						n++;
					}
				}
			}
		}
	}
}
