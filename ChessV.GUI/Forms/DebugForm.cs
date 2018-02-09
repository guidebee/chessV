
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
	public partial class DebugForm: Form
	{
		public DebugForm( IDebugMessageLog messageLog, GameForm gameForm )
		{
			MessageLog = messageLog;
			GameForm = gameForm;
			linesSeen = 0;

			InitializeComponent();
		}

		public IDebugMessageLog MessageLog { get; private set; }
		public GameForm GameForm { get; private set; }

		private void DebugForm_Load( object sender, EventArgs e )
		{
			timer.Start();
		}

		private void timer_Tick( object sender, EventArgs e )
		{
			StringBuilder append = new StringBuilder( 10000 );
			for( int x = linesSeen; x < MessageLog.MessageCount; x++ )
				append.Append( MessageLog.Messages[linesSeen++] + "\r\n" );
			txtDebugOutput.Text += append.ToString();
		}

		private int linesSeen;

		private void DebugForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			Visible = false;
			e.Cancel = true;
		}
	}
}
