
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
	partial class DebugForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtDebugOutput = new System.Windows.Forms.TextBox();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// txtDebugOutput
			// 
			this.txtDebugOutput.BackColor = System.Drawing.Color.White;
			this.txtDebugOutput.Location = new System.Drawing.Point(12, 12);
			this.txtDebugOutput.MaxLength = 8388352;
			this.txtDebugOutput.Multiline = true;
			this.txtDebugOutput.Name = "txtDebugOutput";
			this.txtDebugOutput.ReadOnly = true;
			this.txtDebugOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDebugOutput.Size = new System.Drawing.Size(598, 367);
			this.txtDebugOutput.TabIndex = 0;
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// DebugForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 391);
			this.Controls.Add(this.txtDebugOutput);
			this.Name = "DebugForm";
			this.Text = "DebugForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
			this.Load += new System.EventHandler(this.DebugForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtDebugOutput;
		private System.Windows.Forms.Timer timer;
	}
}