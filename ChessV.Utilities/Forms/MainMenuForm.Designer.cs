
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

namespace ChessV.Utilities
{
	partial class MainMenuForm
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
			this.btnCreateOpeningBook = new System.Windows.Forms.Button();
			this.lblCreateOpeningBook = new System.Windows.Forms.Label();
			this.btnCalculateMobility = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCreateOpeningBook
			// 
			this.btnCreateOpeningBook.Location = new System.Drawing.Point(21, 27);
			this.btnCreateOpeningBook.Name = "btnCreateOpeningBook";
			this.btnCreateOpeningBook.Size = new System.Drawing.Size(142, 32);
			this.btnCreateOpeningBook.TabIndex = 0;
			this.btnCreateOpeningBook.Text = "Create Opening Book";
			this.btnCreateOpeningBook.UseVisualStyleBackColor = true;
			// 
			// lblCreateOpeningBook
			// 
			this.lblCreateOpeningBook.Location = new System.Drawing.Point(179, 27);
			this.lblCreateOpeningBook.Name = "lblCreateOpeningBook";
			this.lblCreateOpeningBook.Size = new System.Drawing.Size(318, 32);
			this.lblCreateOpeningBook.TabIndex = 1;
			this.lblCreateOpeningBook.Text = "Generate a compiled opening book for a game from a text file listing the various " +
    "opening lines";
			this.lblCreateOpeningBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCalculateMobility
			// 
			this.btnCalculateMobility.Location = new System.Drawing.Point(21, 81);
			this.btnCalculateMobility.Name = "btnCalculateMobility";
			this.btnCalculateMobility.Size = new System.Drawing.Size(142, 32);
			this.btnCalculateMobility.TabIndex = 2;
			this.btnCalculateMobility.Text = "Calculate Mobility";
			this.btnCalculateMobility.UseVisualStyleBackColor = true;
			this.btnCalculateMobility.Click += new System.EventHandler(this.btnCalculateMobility_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(179, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(318, 32);
			this.label1.TabIndex = 3;
			this.label1.Text = "Calculate mobility statistics for common piece types on a board of specified size" +
    "";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainMenuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(518, 152);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCalculateMobility);
			this.Controls.Add(this.lblCreateOpeningBook);
			this.Controls.Add(this.btnCreateOpeningBook);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MainMenuForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ChessV Utilities";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCreateOpeningBook;
		private System.Windows.Forms.Label lblCreateOpeningBook;
		private System.Windows.Forms.Button btnCalculateMobility;
		private System.Windows.Forms.Label label1;
	}
}

