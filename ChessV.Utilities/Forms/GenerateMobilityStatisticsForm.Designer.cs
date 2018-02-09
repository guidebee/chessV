
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
	partial class GenerateMobilityStatisticsForm
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
			this.txtNumberOfFiles = new System.Windows.Forms.TextBox();
			this.txtNumberOfRanks = new System.Windows.Forms.TextBox();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(126, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of Files:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(116, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Number of Ranks:";
			// 
			// txtNumberOfFiles
			// 
			this.txtNumberOfFiles.Location = new System.Drawing.Point(215, 25);
			this.txtNumberOfFiles.Name = "txtNumberOfFiles";
			this.txtNumberOfFiles.Size = new System.Drawing.Size(100, 20);
			this.txtNumberOfFiles.TabIndex = 2;
			this.txtNumberOfFiles.Text = "8";
			// 
			// txtNumberOfRanks
			// 
			this.txtNumberOfRanks.Location = new System.Drawing.Point(215, 59);
			this.txtNumberOfRanks.Name = "txtNumberOfRanks";
			this.txtNumberOfRanks.Size = new System.Drawing.Size(100, 20);
			this.txtNumberOfRanks.TabIndex = 3;
			this.txtNumberOfRanks.Text = "8";
			// 
			// btnCalculate
			// 
			this.btnCalculate.Location = new System.Drawing.Point(169, 110);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(107, 28);
			this.btnCalculate.TabIndex = 4;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// GenerateMobilityStatisticsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(452, 150);
			this.Controls.Add(this.btnCalculate);
			this.Controls.Add(this.txtNumberOfRanks);
			this.Controls.Add(this.txtNumberOfFiles);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "GenerateMobilityStatisticsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Generate Mobility Statistics";
			this.Load += new System.EventHandler(this.GenerateMobilityStatisticsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtNumberOfFiles;
		private System.Windows.Forms.TextBox txtNumberOfRanks;
		private System.Windows.Forms.Button btnCalculate;
	}
}