
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

namespace ChessV.GUI
{
	partial class BoardControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timerPieceLiftTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timerPieceLiftTimer
			// 
			this.timerPieceLiftTimer.Interval = 200;
			this.timerPieceLiftTimer.Tick += new System.EventHandler(this.timerPieceLiftTimer_Tick);
			// 
			// BoardControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "BoardControl";
			this.Size = new System.Drawing.Size(397, 302);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardControl_Paint);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BoardControl_MouseClick);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardControl_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoardControl_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoardControl_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerPieceLiftTimer;
	}
}
