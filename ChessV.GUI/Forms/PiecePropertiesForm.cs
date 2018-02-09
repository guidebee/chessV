
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
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	public partial class PiecePropertiesForm: Form
	{
		public Piece Piece { get; private set; }
		public BoardControl BoardControl { get; private set; }

		public PiecePropertiesForm( Piece piece, BoardControl boardControl )
		{
			BoardControl = boardControl;
			Piece = piece;

			InitializeComponent();
		}

		private void PiecePropertiesForm_Load( object sender, EventArgs e )
		{
			ctrlMovementDiagram.Piece = Piece;
			ctrlMovementDiagram.PieceType = Piece.PieceType;
			ctrlMovementDiagram.Theme = BoardControl.Theme;
			ctrlMovementDiagram.BoardPresentation = BoardControl.Presentation;

			ctrlPieceInfoControl.Piece = Piece;
			ctrlPieceInfoControl.PieceType = Piece.PieceType;
			ctrlPieceInfoControl.Theme = BoardControl.Theme;
			ctrlPieceInfoControl.BoardPresentation = BoardControl.Presentation;
			ctrlPieceInfoControl.Initialize();

			Bitmap b = BoardControl.Presentation.Render( false, BoardControl.RotateBoard, Piece );
			if( b != null )
			{
				Bitmap scaled = null;
				double xscale = (double) pictMidgamePST.Size.Width / (double) b.Width;
				xscale = xscale > 1.0 ? 1.0 : xscale;
				double yscale = (double) pictMidgamePST.Size.Height / (double) b.Height;
				yscale = yscale > 1.0 ? 1.0 : yscale;
				double scale = xscale > yscale ? yscale : xscale;
				scaled = new Bitmap( b, (int) (b.Width * scale), (int) (b.Height * scale) );
				pictMidgamePST.Image = scaled;

				b = BoardControl.Presentation.Render( false, BoardControl.RotateBoard, Piece, true );
				if( b != null )
				{
					scaled = new Bitmap( b, (int) (b.Width * scale), (int) (b.Height * scale) );
					pictEndgamePST.Image = scaled;
				}
			}
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			Close();
		}

		private void btnCopyMidgamePST_Click( object sender, EventArgs e )
		{
			StringBuilder s = new StringBuilder();
			for( int rank = 0; rank < Piece.Board.NumRanks; rank++ )
			{
				for( int file = 0; file < Piece.Board.NumFiles; file++ )
				{
					s.Append( Piece.PieceType.GetMidgamePST( Piece.Board.LocationToSquare( new Location( rank, file ) ) ).ToString( "D" ).PadLeft( 4 ) + 
						(file < Piece.Board.NumFiles - 1 ? ", " : "") );
				}
				s.Append( "\n" );
			}
			Clipboard.SetText( s.ToString() );
		}

		private void btnCopyEndgamePST_Click( object sender, EventArgs e )
		{
			StringBuilder s = new StringBuilder();
			for( int rank = 0; rank < Piece.Board.NumRanks; rank++ )
			{
				for( int file = 0; file < Piece.Board.NumFiles; file++ )
				{
					s.Append( (Piece.PieceType.GetEndgamePST( (Piece.Board.LocationToSquare( new Location( rank, file ) ) ) ) + 20).ToString( "D" ).PadLeft( 4 ) +
						(file < Piece.Board.NumFiles - 1 ? ", " : "") );
				}
				s.Append( "\n" );
			}
			Clipboard.SetText( s.ToString() );
		}
	}
}
