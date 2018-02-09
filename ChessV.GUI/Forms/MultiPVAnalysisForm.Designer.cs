
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
	partial class MultiPVAnalysisForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiPVAnalysisForm));
			this.label1 = new System.Windows.Forms.Label();
			this.numericVariations = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.numericFixedDepth = new System.Windows.Forms.NumericUpDown();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericVariations)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericFixedDepth)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(138, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Number of Variations:";
			// 
			// numericVariations
			// 
			this.numericVariations.Location = new System.Drawing.Point(252, 37);
			this.numericVariations.Name = "numericVariations";
			this.numericVariations.Size = new System.Drawing.Size(63, 20);
			this.numericVariations.TabIndex = 1;
			this.numericVariations.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(179, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Fixed Depth:";
			// 
			// numericFixedDepth
			// 
			this.numericFixedDepth.Location = new System.Drawing.Point(252, 92);
			this.numericFixedDepth.Name = "numericFixedDepth";
			this.numericFixedDepth.Size = new System.Drawing.Size(63, 20);
			this.numericFixedDepth.TabIndex = 3;
			this.numericFixedDepth.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(269, 144);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(94, 144);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// MultiPVAnalysisForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(486, 188);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.numericFixedDepth);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericVariations);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MultiPVAnalysisForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Multi-Variation Analysis";
			((System.ComponentModel.ISupportInitialize)(this.numericVariations)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericFixedDepth)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericVariations;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericFixedDepth;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}