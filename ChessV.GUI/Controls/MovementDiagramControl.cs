
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
using ChessV;

namespace ChessV.GUI
{
	public partial class MovementDiagramControl: UserControl
	{
		public Piece Piece { get; set; }
		public PieceType PieceType { get; set; }
		public Theme Theme { get; set; }
		public BoardPresentation BoardPresentation { get; set; }

		public MovementDiagramControl()
		{
			InitializeComponent();
		}

		private void MovementDiagramControl_Paint( object sender, PaintEventArgs e )
		{
			//	Create our graphics objects
			Pen blackOutlinePen = new Pen( Color.Black, 2 );
			Pen redOutlinePen = new Pen( Color.Red, 2 );
			Pen thickRedPen = new Pen( Color.Red, 5 );
			Pen thickGreenPen = new Pen( Color.FromArgb( 0, 255, 0 ), 5 );
			SolidBrush lightSquareBrush = new SolidBrush( Theme == null ? Color.FromArgb( 0xFF, 0xFF, 0xCC ) : Theme.ColorScheme.SquareColors[0] );
			SolidBrush darkSquareBrush = new SolidBrush( Theme == null ? Color.FromArgb( 0x5D, 0x7E, 0x7E ) : Theme.ColorScheme.SquareColors[1] );
			SolidBrush thirdSquareBrush = (Theme != null && Theme.ColorScheme.SquareColors.Count > 2) ? new SolidBrush( Theme.ColorScheme.SquareColors[2] ) : darkSquareBrush;
			SolidBrush redBrush = new SolidBrush( Color.FromArgb( 255, 0, 0 ) );
			SolidBrush greenBrush = new SolidBrush( Color.FromArgb( 0, 255, 0 ) );
			SolidBrush blackBrush = new SolidBrush( Color.Black );

			//	Draw the board
			for( int file = 0; file < 9; file++ )
			{
				for( int rank = 0; rank < 9; rank++ )
				{
					SolidBrush squareBrush = lightSquareBrush;
					if( Theme == null || Theme.NSquareColors == 2 )
						squareBrush = (file + rank) % 2 == 1 ? lightSquareBrush : darkSquareBrush;
					else if( Theme != null && Theme.NSquareColors == 3 )
						squareBrush = (file + rank) % 2 == 1 ? lightSquareBrush : (rank % 2 == 0 ? darkSquareBrush : thirdSquareBrush);
					e.Graphics.FillRectangle( squareBrush, 50 + 36*file, 50 + 36*rank, 36, 36 );
				}
			}
			for( int file = 0; file < 9; file++ )
				for( int rank = 0; rank < 9; rank++ )
					e.Graphics.DrawRectangle( blackOutlinePen, 50 + 36*file, 50 + 36*rank, 36, 36 );

			if( Theme == null )
				//	we are just previewing this control in the Forms Designer
				//	so we have nothing further to present
				return;

			//	Draw miniature piece
			BoardPresentation.PieceSetPresentation.Render( e.Graphics,
				new Rectangle( 50 + 36*4, 50 + 36*4, 36, 36 ), Piece, true );

			//	Draw movement capabilities, first pass ...
			MoveCapability[] moves;
			int nMoves = PieceType.GetMoveCapabilities( out moves );
			for( int z = 0; z < nMoves; z++ )
			{
				MoveCapability move = moves[z];
				Direction direction = Piece.Game.GetDirection( Piece.Game.PlayerDirection( Piece.Player, move.NDirection ) );
				int steps = 1;
				int rank = 4;
				int file = 4;
				int newRank;
				int newFile;
				newRank = rank - direction.RankOffset;
				newFile = file + direction.FileOffset;
				int oldFile = 4;
				int oldRank = 4;
				while( newRank >= 0 && newRank < 9 && newFile >= 0 && newFile < 9 && steps <= move.MaxSteps )
				{
					//	draw indicator for this step
					if( move.MaxSteps == 1 )
					{
						//	handle displaying "paths" for moves that have paths
						if( move.PathInfo != null )
						{
							MovePathInfo pathinfo = move.PathInfo;
							foreach( List<Direction> dirlist in pathinfo.PathDirections )
							{
								int pathRank = 4;
								int pathFile = 4;
								foreach( Direction dir in dirlist )
								{
									int newPathRank = pathRank - dir.RankOffset;
									int newPathFile = pathFile + dir.FileOffset;
									//	draw line
									int startX = 68 + 36 * pathFile;
									int startY = 68 + 36 * pathRank;
									int endX = 68 + 36 * newPathFile;
									int endY = 68 + 36 * newPathRank;
									if( pathRank == 4 && pathFile == 4 )
										FindIntersectionOfLineAndRectangle( startX, startY, endX, endY, 50 + 36*4, 50 + 36*4, 50 + 36*5, 50 + 36*5, ref startX, ref startY );
									e.Graphics.DrawLine( move.CanCapture ? thickRedPen : thickGreenPen, startX, startY, endX, endY );
									pathRank = newPathRank;
									pathFile = newPathFile;
								}
							}
						}

						//	a single step move is displayed 
						//	differently from a sliding move
						Brush fillBr;
						Pen outlinePen;
						if( move.CanCapture && !move.MustCapture )
						{
							outlinePen = blackOutlinePen;
							fillBr = redBrush;
						}
						else if( !move.CanCapture )
						{
							outlinePen = blackOutlinePen;
							fillBr = greenBrush;
						}
						else
						{
							outlinePen = redOutlinePen;
							fillBr = blackBrush;
						}
						e.Graphics.FillEllipse( fillBr, 54 + 36*newFile, 54 + 36*newRank, 28, 28 );
						e.Graphics.DrawEllipse( outlinePen, 54 + 36*newFile, 54 + 36*newRank, 28, 28 );
						if( move.MustCapture )
						{
							StringFormat format = new StringFormat();
							format.Alignment = StringAlignment.Center;
							format.LineAlignment = StringAlignment.Center;
							e.Graphics.DrawString( "X", System.Drawing.SystemFonts.CaptionFont, redBrush, new RectangleF( 51 + 36*newFile, 51 + 36*newRank, 36, 36 ), format );
						}
					}
					else if( steps >= move.MinSteps )
					{
						Brush fillBr;
						Pen outlinePen;
						if( move.CanCapture && !move.MustCapture )
						{
							outlinePen = blackOutlinePen;
							fillBr = redBrush;
						}
						else if( !move.CanCapture )
						{
							outlinePen = blackOutlinePen;
							fillBr = greenBrush;
						}
						else
						{
							outlinePen = redOutlinePen;
							fillBr = blackBrush;
						}
						e.Graphics.FillEllipse( fillBr, 54 + 36*newFile, 54 + 36*newRank, 28, 28 );
						e.Graphics.DrawEllipse( outlinePen, 54 + 36*newFile, 54 + 36*newRank, 28, 28 );
						if( move.MustCapture )
						{
							StringFormat format = new StringFormat();
							format.Alignment = StringAlignment.Center;
							format.LineAlignment = StringAlignment.Center;
							e.Graphics.DrawString( "X", System.Drawing.SystemFonts.CaptionFont, redBrush, new RectangleF( 51 + 36*newFile, 51 + 36*newRank, 36, 36 ), format );
						}
					}
					//	take another step...
					oldFile = newFile;
					oldRank = newRank;
					newRank -= direction.RankOffset;
					newFile += direction.FileOffset;
					steps++;
				}
				steps--;
				if( steps > 1 )
				{
					int nextSquareCenterX;
					int nextSquareCenterY;
					int lineEndX = 0;
					int lineEndY = 0;
					int maxSteps = move.MaxSteps;
					if( steps >= move.MaxSteps )
					{
						nextSquareCenterX = lineEndX = 68 + (36 * oldFile);
						nextSquareCenterY = lineEndY = 68 + (36 * oldRank);
					}
					else
					{
						newRank -= direction.RankOffset;
						newFile += direction.FileOffset;
						nextSquareCenterX = 68 + (36 * newFile);
						nextSquareCenterY = 68 + (36 * newRank);
						if( move.Direction.FileOffset == move.Direction.RankOffset || 
							move.Direction.FileOffset == -move.Direction.RankOffset )
							FindIntersectionOfLineAndRectangle( 212, 212, nextSquareCenterX, 
							nextSquareCenterY, 60 - 36, 60 - 36, 75 + (36 * 9), 75 + (36 * 9), 
								ref lineEndX, ref lineEndY );
						else if( move.Direction.FileOffset == 0 || move.Direction.RankOffset == 0 )
							FindIntersectionOfLineAndRectangle( 212, 212, nextSquareCenterX, 
							nextSquareCenterY, 43 - 36, 43 - 36, 92 + (36 * 9), 92 + (36 * 9), 
								ref lineEndX, ref lineEndY );
						else
							FindIntersectionOfLineAndRectangle( 212, 212, nextSquareCenterX, 
							nextSquareCenterY, 51 - 36, 51 - 36, 84 + (36 * 9), 84 + (36 * 9), 
								ref lineEndX, ref lineEndY );
					}
					int lineStartX = 0;
					int lineStartY = 0;
					FindIntersectionOfLineAndRectangle( 212, 212, nextSquareCenterX, 
						nextSquareCenterY, 51 + (36 * 4), 51 + (36 * 4), 84 + (36 * 4), 84 + (36 * 4), 
						ref lineStartX, ref lineStartY );
					Pen linePen;
					Brush fillBrush;
					if( move.CanCapture && !move.MustCapture )
					{
						linePen = thickRedPen;
						fillBrush = redBrush;
					}
					else if( !move.CanCapture )
					{
						linePen = thickGreenPen;
						fillBrush = greenBrush;
					}
					else
					{
						linePen = thickRedPen;
						fillBrush = redBrush;
					}
					e.Graphics.DrawLine( linePen, lineStartX, lineStartY, lineEndX, lineEndY );
					if( steps < move.MaxSteps )
					{
						double angle = 0.0;
						angle = Math.Atan2( (double) (lineEndY - lineStartY), 
							(double) (lineStartX - lineEndX) );
						Point[] pt = new Point[3];
						pt[0].X = lineEndX;
						pt[0].Y = lineEndY;
						pt[1].X = (int) ((lineEndX + (Math.Cos(angle) * 30)) + 
							(Math.Cos(angle + (Math.PI/2)) * 10));
						pt[1].Y = (int) ((lineEndY - (Math.Sin(angle) * 30)) - 
							(Math.Sin(angle + (Math.PI/2)) * 10));
						pt[2].X = (int) ((lineEndX + (Math.Cos(angle) * 30)) + 
							(Math.Cos(angle - (Math.PI/2)) * 10));
						pt[2].Y = (int) ((lineEndY - (Math.Sin(angle) * 30)) -  
							(Math.Sin(angle - (Math.PI/2)) * 10));
						e.Graphics.FillPolygon( fillBrush, pt );
						e.Graphics.DrawPolygon( linePen, pt );
					}
				}
			}
			//	second pass ...
			for( int z = 0; z < nMoves; z++ )
			{
				MoveCapability move = moves[z];
				Direction direction = Piece.Game.GetDirection( Piece.Game.PlayerDirection( Piece.Player, move.NDirection ) );
				int steps = 1;
				int rank = 4;
				int file = 4;
				int newRank = rank - direction.RankOffset;
				int newFile = file + direction.FileOffset;
				while( newRank >= 0 && newRank < 9 && newFile >= 0 && newFile < 9 && steps <= move.MaxSteps )
				{
					if( move.MaxSteps > 1 )
						//	draw black inner dot
						e.Graphics.FillEllipse( blackBrush, 63 + 36*newFile, 63 + 36*newRank, 11, 11 );
					if( move.PathInfo != null )
					{
						MovePathInfo pathinfo = move.PathInfo;
						foreach( List<Direction> dirlist in pathinfo.PathDirections )
						{
							int pathRank = 4;
							int pathFile = 4;
							foreach( Direction dir in dirlist )
							{
								pathRank = pathRank - dir.RankOffset;
								pathFile = pathFile + dir.FileOffset;
								//	draw black inner dot
								e.Graphics.FillEllipse( blackBrush, 63 + 36 * pathFile, 63 + 36 * pathRank, 11, 11 );
							}
						}
					}
					//	take another step...
					newRank -= direction.RankOffset;
					newFile += direction.FileOffset;
					steps++;
				}
			}
		}

		private void FindIntersectionOfLineAndRectangle
			( int lineStartX,
			  int lineStartY,
			  int lineEndX,
			  int lineEndY,
			  int rectLeft, 
			  int rectTop, 
			  int rectRight, 
			  int rectBottom, 
			  ref int intersectX, 
			  ref int intersectY )
		{
			double slope = 0.0;
			bool slopeIsInfinite = false;
			if( lineStartX == lineEndX )
				slopeIsInfinite = true;
			else if( lineStartY != lineEndY )
				slope = (double) (lineStartY - lineEndY) / (double) (lineEndX - lineStartX);

			//	does line intersect with right edge?
			if( slope <= 1.0 && slope >= -1.0 && lineEndX > lineStartX )
			{
				intersectX = rectRight;
				intersectY = (int) (slope * (lineStartX - intersectX) + lineStartY);
			}
			//	does line intersect with top edge?
			else if( (slopeIsInfinite || slope >= 1.0 || slope <= -1.0) && lineEndY < lineStartY )
			{
				intersectY = rectTop;
				if( slopeIsInfinite )
					intersectX = lineStartX;
				else
					intersectX = (int) ((lineStartY - intersectY) / slope + lineStartX);
			}
			//	does line intersect with left edge?
			else if( slope <= 1.0 && slope >= -1.0 && lineEndX < lineStartX )
			{
				intersectX = rectLeft;
				intersectY = (int) (slope * (lineStartX - intersectX) + lineStartY);
			}
			//	must intersect with bottom edge
			else
			{
				intersectY = rectBottom;
				if( slopeIsInfinite )
					intersectX = lineStartX;
				else
					intersectX = (int) ((lineStartY - intersectY) / slope + lineStartX);
			}
		}
	}
}
