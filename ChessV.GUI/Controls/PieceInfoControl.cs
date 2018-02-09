
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class PieceInfoControl: UserControl
	{
		public Piece Piece { get; set; }
		public PieceType PieceType { get; set; }
		public Theme Theme { get; set; }
		public BoardPresentation BoardPresentation { get; set; }

		public PieceInfoControl()
		{
			InitializeComponent();
		}

		private void PieceInfoControl_Load( object sender, EventArgs e )
		{
		}

		public void Initialize()
		{
			int boardVisibility = 0;
			int slice = PieceType.SliceLookup[Piece.Square];
			for( int x = 0; x < Piece.Board.NumSquares; x++ )
				if( PieceType.SliceLookup[x] == slice )
					boardVisibility++;
			lblPieceName.Text = PieceType.Name;
			lblBoardVisibility.Text = boardVisibility + " of " + Piece.Board.NumSquares.ToString();
			lblMidgameValue.Text = PieceType.MidgameValue.ToString();
			lblEndgameValue.Text = PieceType.EndgameValue.ToString();
			lblAveMobility.Text = PieceType.AverageMobility.ToString( "F2" );
			lblAveDirectionsAttacked.Text = PieceType.AverageDirectionsAttacked.ToString( "F2" );
			lblAveSafeChecks.Text = PieceType.AverageSafeChecks.ToString( "F2" );
			Invalidate();
		}

		private void PieceInfoControl_Paint( object sender, PaintEventArgs e )
		{
			//	Draw miniature piece
			if( BoardPresentation != null )
				BoardPresentation.PieceSetPresentation.Render( e.Graphics,
					new Rectangle( 102, 27, 0, 0 ), Piece );

			Bitmap target = Properties.Resources.target;
			ColorPalette palette = target.Palette;
			for( int x = 0; x < palette.Entries.Length; x++ )
				if( palette.Entries[x] == Color.FromArgb( 0, 255, 0 ) )
					palette.Entries[x] = Color.FromArgb( 1, 0, 0 );
			ImageAttributes attr = new ImageAttributes();
			attr.SetColorKey( target.GetPixel( 0, 0 ), target.GetPixel( 0, 0 ) );
			e.Graphics.DrawImage( target, new Rectangle( -16, 15, target.Width, target.Height),
					0, 0, target.Width, target.Height, GraphicsUnit.Pixel, attr );
		}
	}
}
