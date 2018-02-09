
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
	partial class AcknowledgementsForm
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
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.listAcknowledgements = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(12, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(603, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "functions of this program are based";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(603, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Thanks to Ilari Pihlajisto and Arto Jonsson, authors of CuteChess on which the XB" +
    "oard engine automation";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(603, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Thanks to H. G. Muller for his work on enhancing the XBoard engine communication " +
    "protocol";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(12, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(603, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "to support Chess variants and for helping to promote support for variants in gene" +
    "ral";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Gray;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnClose.Location = new System.Drawing.Point(265, 256);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(97, 26);
			this.btnClose.TabIndex = 9;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = false;
			// 
			// listAcknowledgements
			// 
			this.listAcknowledgements.BackColor = System.Drawing.Color.Black;
			this.listAcknowledgements.ForeColor = System.Drawing.Color.Silver;
			this.listAcknowledgements.FormattingEnabled = true;
			this.listAcknowledgements.Items.AddRange(new object[] {
            "Graphics for the Standard piece set by David Howe",
            "Graphics for the Abstract piece set by Fergus Duniho",
            "Eurasian graphics by Fergus Duniho",
            "Some icons are from the Crystal Clear Action icon pack by Everaldo Coelho",
            "Some icons are from the Human-O2 icon pack by schollidesign",
            "Some icons are from the Ultimate Gnome icon pack by New Moon",
            "The Modify gear icon is from the Aqua Gloss icon pack by Deziner Folio",
            "The Engines gear icon is from the Nuove icon pack by Alexandre Moore",
            "Review mode play/stop icons from the Snowish icon pack by Alexandre Moore",
            "The Knight/Chess icon is by the IconFactory"});
			this.listAcknowledgements.Location = new System.Drawing.Point(12, 100);
			this.listAcknowledgements.Name = "listAcknowledgements";
			this.listAcknowledgements.Size = new System.Drawing.Size(603, 147);
			this.listAcknowledgements.TabIndex = 10;
			// 
			// AcknowledgementsForm
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(627, 294);
			this.Controls.Add(this.listAcknowledgements);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AcknowledgementsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Acknowledgements";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ListBox listAcknowledgements;
	}
}