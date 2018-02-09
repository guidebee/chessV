
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
	partial class PerftForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtDepth = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 46, 42 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 39, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Depth:";
			// 
			// txtDepth
			// 
			this.txtDepth.Location = new System.Drawing.Point( 91, 39 );
			this.txtDepth.Name = "txtDepth";
			this.txtDepth.Size = new System.Drawing.Size( 63, 20 );
			this.txtDepth.TabIndex = 1;
			this.txtDepth.Text = "4";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point( 122, 239 );
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size( 75, 23 );
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// PerftForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 330, 273 );
			this.Controls.Add( this.btnOK );
			this.Controls.Add( this.txtDepth );
			this.Controls.Add( this.label1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "PerftForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PERFT (Move Generator Test)";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDepth;
		private System.Windows.Forms.Button btnOK;
	}
}