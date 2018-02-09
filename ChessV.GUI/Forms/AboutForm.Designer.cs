
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
	partial class AboutForm
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnAcknowledgements = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(142, 478);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(346, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Copyright (C) 2012-2017 by Greg Strong";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(145, 498);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(343, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Released under the GNU General Public License 3.0";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(148, 458);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(340, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Version 2.1";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnAcknowledgements
			// 
			this.btnAcknowledgements.BackColor = System.Drawing.Color.Gray;
			this.btnAcknowledgements.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAcknowledgements.Location = new System.Drawing.Point(21, 488);
			this.btnAcknowledgements.Name = "btnAcknowledgements";
			this.btnAcknowledgements.Size = new System.Drawing.Size(117, 26);
			this.btnAcknowledgements.TabIndex = 4;
			this.btnAcknowledgements.Text = "Acknowledgements";
			this.btnAcknowledgements.UseVisualStyleBackColor = false;
			this.btnAcknowledgements.Click += new System.EventHandler(this.btnAcknowledgements_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::ChessV.GUI.Properties.Resources.Chessv;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(604, 377);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Gray;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnClose.Location = new System.Drawing.Point(507, 488);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(97, 26);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.Black;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.ForeColor = System.Drawing.Color.White;
			this.textBox1.Location = new System.Drawing.Point(202, 399);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(337, 46);
			this.textBox1.TabIndex = 5;
			this.textBox1.Text = "BTC: 1MjZpkKL2vfznBmiFovmZaZheDzYoxP3Td\r\nLTC: LSTDWhoKa6C3rVSAsGhS32JqqSzXgJ42PZ\r" +
    "\nDASH: XjM7sdrQidP7p5MpfGHd6zxrbN6VmqB1fH";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.PaleGoldenrod;
			this.label4.Location = new System.Drawing.Point(121, 408);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 24);
			this.label4.TabIndex = 6;
			this.label4.Text = "Donate:";
			// 
			// AboutForm
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(630, 528);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnAcknowledgements);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About ChessV 2.1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnAcknowledgements;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label4;
	}
}