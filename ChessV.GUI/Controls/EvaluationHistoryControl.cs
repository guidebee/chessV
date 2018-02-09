
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
	public partial class EvaluationHistoryControl: UserControl
	{
		private Game game;
		private Color[] pieceColors;
		private List<KeyValuePair<int, double>> evaluations;

		public EvaluationHistoryControl()
		{
			InitializeComponent();
		}

		public void Initialize( Game game, Theme theme )
		{
			this.game = game;
			pieceColors = new Color[2];
			pieceColors[0] = theme.ColorScheme.PlayerColors[0];
			pieceColors[1] = theme.ColorScheme.PlayerColors[1];
			evaluations = new List<KeyValuePair<int, double>>();
		}

		public void AddEvaluation( int player, double evaluation )
		{
			evaluations.Add( new KeyValuePair<int, double>( player, evaluation ) );
			Invalidate();
		}

		private void EvaluationHistoryControl_Paint( object sender, PaintEventArgs e )
		{
			SolidBrush br1 = new SolidBrush( BackColor );
			e.Graphics.FillRectangle( br1, e.ClipRectangle );

			//	Determine some dimensions for our graph
			int graphLeft = 25;
			int graphHeight = Size.Height - 10;
			int graphWidth = Size.Width - 30;
			int graphCenter = Size.Height / 2;
			double unitHeight = (double) graphHeight / 10.0;

			//	Draw graph axis
			Pen thickBlackPen = new Pen( Color.Black, 2 );
			e.Graphics.DrawLine( thickBlackPen, graphLeft, 5, graphLeft, graphHeight + 5 );
			e.Graphics.DrawLine( thickBlackPen, graphLeft, graphCenter, graphLeft + graphWidth, graphCenter );
			for( int x = -5; x <= 5; x++ )
			{
				int graduationHeight = Math.Min( Math.Max( graphCenter + (int) Math.Round( unitHeight * x ), 5 ), graphHeight + 5 );
				e.Graphics.DrawLine( thickBlackPen, graphLeft - 3, graduationHeight, graphLeft + 1, graduationHeight );
			}

			//	If game is null it probably means we're in the Forms Designer
			//	so none of the following would really work.
			if( game != null && evaluations != null && evaluations.Count > 0 )
			{
				Pen thinBlackPen = new Pen( Color.Black, 1 );
				SolidBrush[] playerBrushes = { new SolidBrush( pieceColors[0] ), new SolidBrush( pieceColors[1] ) };
				int widthPerBar = Math.Min( 8, graphWidth / evaluations.Count );
				int barNumber = 0;
				foreach( KeyValuePair<int, double> pair in evaluations )
				{
					int barHeight = (int) Math.Min( Math.Abs( pair.Value ) * unitHeight, unitHeight * 5.0 );
					if( pair.Value > 0.0 )
					{
						e.Graphics.FillRectangle( playerBrushes[pair.Key], new Rectangle( graphLeft + barNumber*widthPerBar, graphCenter - barHeight, widthPerBar, barHeight ) );
						e.Graphics.DrawRectangle( thinBlackPen, new Rectangle( graphLeft + barNumber*widthPerBar, graphCenter - barHeight, widthPerBar, barHeight ) );
					}
					else if( pair.Value < 0.0 )
					{
						e.Graphics.FillRectangle( playerBrushes[pair.Key], new Rectangle( graphLeft + barNumber*widthPerBar, graphCenter, widthPerBar, barHeight ) );
						e.Graphics.DrawRectangle( thinBlackPen, new Rectangle( graphLeft + barNumber*widthPerBar, graphCenter, widthPerBar, barHeight ) );
					}
					barNumber++;
				}
				//	Draw center line again so it's on top of the rectanges
				e.Graphics.DrawLine( thickBlackPen, graphLeft, graphCenter, graphLeft + graphWidth, graphCenter );
			}
		}
	}
}
