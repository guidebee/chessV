
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
using System.Drawing.Imaging;
using System.IO;
using ChessV;

namespace ChessV.GUI
{
	public class BitmapPieceSetPresentation
	{
		// *** PROPERTIES *** //

		public BitmapPieceSet PieceSet { get; protected set; }
		public Game Game { get; protected set; }
		protected ColorScheme colorScheme { get; set; }
		protected PieceType[] pieceTypes;
		protected int nPieceTypes;
		protected Bitmap[,] bitmaps;


		// *** CONSTRUCTION *** //

		public BitmapPieceSetPresentation( Game game, BitmapPieceSet pieceSet )
		{
			Game = game;
			PieceSet = pieceSet;
		}

		public void Initialize( Theme theme )
		{
			colorScheme = theme.ColorScheme;
			nPieceTypes = Game.GetPieceTypes( out pieceTypes );
			bitmaps = new Bitmap[Game.NumPlayers, nPieceTypes];
			for( int ntype = 0; ntype < nPieceTypes; ntype++ )
			{
				string piecePath = theme.PieceSet.GetFilenameForPiece( pieceTypes[ntype] );
				if( piecePath == null )
					throw new Exception( "No graphic found for piece " + pieceTypes[ntype].Name + " in Piece Set " + PieceSet.Name );
				Bitmap bmp1;
				Bitmap bmp2;
				//	if piece set is pre-colored, add "W" and "B" to the beginning of the filename 
				//	to get the names of the files for each player
				if( PieceSet.PreColored )
				{
					//	load the bitmap for player 0
					bmp1 = new Bitmap( piecePath );
					//	load the bitmap again for player 1
					bmp2 = new Bitmap( piecePath.Substring( 0, piecePath.LastIndexOf( Path.DirectorySeparatorChar ) + 1 ) + "B" +
						piecePath.Substring( piecePath.LastIndexOf( Path.DirectorySeparatorChar ) + 2 ) );
				}
				else
				{
					//	load the bitmap for player 0
					bmp1 = new Bitmap( piecePath );
					//	load the bitmap again for player 1
					bmp2 = new Bitmap( piecePath );
				}
				if( !PieceSet.PreColored )
				{
					//	Adjust color palette for player 0's colors
					ColorPalette palette = bmp1.Palette;
					adjustColorPaletteForPlayer( palette, bmp1.GetPixel( 0, 0 ), colorScheme.PlayerColors[0] );
					bmp1.Palette = palette;
					//	Adjust color palette for player 1's colors
					palette = bmp2.Palette;
					adjustColorPaletteForPlayer( palette, bmp2.GetPixel( 0, 0 ), colorScheme.PlayerColors[1] );
					bmp2.Palette = palette;
				}
				//	Update the bitmaps mapping with these 
				//	properly loaded and colored bitmaps
				bitmaps[0, ntype] = bmp1;
				bitmaps[1, ntype] = bmp2;
			}
		}


		// *** RENDERING *** //

		public void Render( Graphics gr, Rectangle rect, int player, int typeNumber, bool scale = false )
		{
			ImageAttributes attr = new ImageAttributes();
			attr.SetColorKey( bitmaps[player, typeNumber].GetPixel( 0, 0 ), bitmaps[player, typeNumber].GetPixel( 0, 0 ) );

			if( scale )
			{
				gr.DrawImage( bitmaps[player, typeNumber], rect,
					0, 0, bitmaps[player, typeNumber].Width, bitmaps[player, typeNumber].Height, GraphicsUnit.Pixel, attr );
			}
			else
			{
				int xoffset = ((PieceSet.Width + 6) - bitmaps[player, typeNumber].Width) / 2 + 1;
				int yoffset = ((PieceSet.Height + 6) - bitmaps[player, typeNumber].Height) / 2 + 1;
				gr.DrawImage( bitmaps[player, typeNumber], new Rectangle( rect.Left + xoffset, rect.Top + yoffset, bitmaps[player, typeNumber].Width, bitmaps[player, typeNumber].Height ),
					0, 0, bitmaps[player, typeNumber].Width, bitmaps[player, typeNumber].Height, GraphicsUnit.Pixel, attr );
			}
		}

		public void Render( Graphics gr, Rectangle rect, Piece piece, bool scale = false )
		{
			Render( gr, rect, piece.Player, piece.TypeNumber, scale );
		}

		public void RenderFloatingPiece( Graphics gr, Point point, Piece piece )
		{
			Bitmap bmp = bitmaps[piece.Player, piece.TypeNumber];
			ImageAttributes attr = new ImageAttributes();
			attr.SetColorKey( bmp.GetPixel( 0, 0 ), bmp.GetPixel( 0, 0 ) );
			gr.DrawImage( bmp, new Rectangle( point.X - (bmp.Width / 2), point.Y - (bmp.Height / 2), bmp.Width, bmp.Height ), 
				0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attr );
		}


		// *** PROTECTED HELPER FUNCTIONS *** //

		protected void adjustColorPaletteForPlayer( ColorPalette palette, Color transparencyColor, Color playerColor )
		{
			for( int x = 0; x < palette.Entries.Length; x++ )
			{
				if( playerColor.ToArgb() == Color.FromArgb( 0, 0, 0 ).ToArgb() )
				{
					//	if the piece color is set to pure black we handle it 
					//	in a special way - we switch the black and white in 
					//	the color palette
					if( palette.Entries[x].ToArgb() == Color.FromArgb( 255, 255, 255 ).ToArgb() )
						palette.Entries[x] = Color.FromArgb( 0, 0, 0 );
					else if( palette.Entries[x].ToArgb() == Color.FromArgb( 0, 0, 0 ).ToArgb() )
						palette.Entries[x] = Color.FromArgb( 255, 255, 255 );
				}
				else
				{
					//	the normal handling is to replace whatever is pure 
					//	white in the color palette with the player's color
					if( palette.Entries[x].ToArgb() == Color.FromArgb( 255, 255, 255 ).ToArgb() )
						palette.Entries[x] = playerColor;
					//	and we change whatever the transparency color was 
					//	to RGB(1,0,0).  We do this to compensate for a bug 
					//	with the rendering of transparency in Linux/Mono
					else if( palette.Entries[x].ToArgb() == transparencyColor.ToArgb() )
						palette.Entries[x] = Color.FromArgb( 1, 0, 0 );
				}
			}
		}
	}
}
