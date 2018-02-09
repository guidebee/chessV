
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
	partial class PiecePropertiesForm
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.ctrlPieceInfoControl = new ChessV.GUI.PieceInfoControl();
			this.ctrlMovementDiagram = new ChessV.GUI.MovementDiagramControl();
			this.tabMidgamePST = new System.Windows.Forms.TabPage();
			this.btnCopyMidgamePST = new System.Windows.Forms.Button();
			this.pictMidgamePST = new System.Windows.Forms.PictureBox();
			this.tabEndgamePST = new System.Windows.Forms.TabPage();
			this.btnCopyEndgamePST = new System.Windows.Forms.Button();
			this.pictEndgamePST = new System.Windows.Forms.PictureBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabMidgamePST.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictMidgamePST)).BeginInit();
			this.tabEndgamePST.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictEndgamePST)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabMidgamePST);
			this.tabControl1.Controls.Add(this.tabEndgamePST);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(581, 584);
			this.tabControl1.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.ctrlPieceInfoControl);
			this.tabGeneral.Controls.Add(this.ctrlMovementDiagram);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(573, 558);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// ctrlPieceInfoControl
			// 
			this.ctrlPieceInfoControl.BoardPresentation = null;
			this.ctrlPieceInfoControl.Location = new System.Drawing.Point(25, 7);
			this.ctrlPieceInfoControl.Name = "ctrlPieceInfoControl";
			this.ctrlPieceInfoControl.Piece = null;
			this.ctrlPieceInfoControl.PieceType = null;
			this.ctrlPieceInfoControl.Size = new System.Drawing.Size(540, 113);
			this.ctrlPieceInfoControl.TabIndex = 1;
			this.ctrlPieceInfoControl.Theme = null;
			// 
			// ctrlMovementDiagram
			// 
			this.ctrlMovementDiagram.BackColor = System.Drawing.Color.White;
			this.ctrlMovementDiagram.BoardPresentation = null;
			this.ctrlMovementDiagram.Location = new System.Drawing.Point(73, 119);
			this.ctrlMovementDiagram.Name = "ctrlMovementDiagram";
			this.ctrlMovementDiagram.Piece = null;
			this.ctrlMovementDiagram.PieceType = null;
			this.ctrlMovementDiagram.Size = new System.Drawing.Size(428, 428);
			this.ctrlMovementDiagram.TabIndex = 0;
			this.ctrlMovementDiagram.Theme = null;
			// 
			// tabMidgamePST
			// 
			this.tabMidgamePST.Controls.Add(this.btnCopyMidgamePST);
			this.tabMidgamePST.Controls.Add(this.pictMidgamePST);
			this.tabMidgamePST.Location = new System.Drawing.Point(4, 22);
			this.tabMidgamePST.Name = "tabMidgamePST";
			this.tabMidgamePST.Padding = new System.Windows.Forms.Padding(3);
			this.tabMidgamePST.Size = new System.Drawing.Size(573, 558);
			this.tabMidgamePST.TabIndex = 1;
			this.tabMidgamePST.Text = "Midgame PST";
			this.tabMidgamePST.UseVisualStyleBackColor = true;
			// 
			// btnCopyMidgamePST
			// 
			this.btnCopyMidgamePST.Location = new System.Drawing.Point(531, 516);
			this.btnCopyMidgamePST.Name = "btnCopyMidgamePST";
			this.btnCopyMidgamePST.Size = new System.Drawing.Size(42, 22);
			this.btnCopyMidgamePST.TabIndex = 1;
			this.btnCopyMidgamePST.Text = "Copy";
			this.btnCopyMidgamePST.UseVisualStyleBackColor = true;
			this.btnCopyMidgamePST.Click += new System.EventHandler(this.btnCopyMidgamePST_Click);
			// 
			// pictMidgamePST
			// 
			this.pictMidgamePST.Location = new System.Drawing.Point(26, 23);
			this.pictMidgamePST.Name = "pictMidgamePST";
			this.pictMidgamePST.Size = new System.Drawing.Size(520, 492);
			this.pictMidgamePST.TabIndex = 0;
			this.pictMidgamePST.TabStop = false;
			// 
			// tabEndgamePST
			// 
			this.tabEndgamePST.Controls.Add(this.btnCopyEndgamePST);
			this.tabEndgamePST.Controls.Add(this.pictEndgamePST);
			this.tabEndgamePST.Location = new System.Drawing.Point(4, 22);
			this.tabEndgamePST.Name = "tabEndgamePST";
			this.tabEndgamePST.Size = new System.Drawing.Size(573, 558);
			this.tabEndgamePST.TabIndex = 2;
			this.tabEndgamePST.Text = "Endgame PST";
			this.tabEndgamePST.UseVisualStyleBackColor = true;
			// 
			// btnCopyEndgamePST
			// 
			this.btnCopyEndgamePST.Location = new System.Drawing.Point(531, 516);
			this.btnCopyEndgamePST.Name = "btnCopyEndgamePST";
			this.btnCopyEndgamePST.Size = new System.Drawing.Size(42, 22);
			this.btnCopyEndgamePST.TabIndex = 2;
			this.btnCopyEndgamePST.Text = "Copy";
			this.btnCopyEndgamePST.UseVisualStyleBackColor = true;
			this.btnCopyEndgamePST.Click += new System.EventHandler(this.btnCopyEndgamePST_Click);
			// 
			// pictEndgamePST
			// 
			this.pictEndgamePST.Location = new System.Drawing.Point(26, 23);
			this.pictEndgamePST.Name = "pictEndgamePST";
			this.pictEndgamePST.Size = new System.Drawing.Size(520, 492);
			this.pictEndgamePST.TabIndex = 1;
			this.pictEndgamePST.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(241, 602);
			this.btnOK.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnOK.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// PiecePropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(605, 646);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PiecePropertiesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PiecePropertiesForm";
			this.Load += new System.EventHandler(this.PiecePropertiesForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabMidgamePST.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictMidgamePST)).EndInit();
			this.tabEndgamePST.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictEndgamePST)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabMidgamePST;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TabPage tabEndgamePST;
		private System.Windows.Forms.PictureBox pictMidgamePST;
		private System.Windows.Forms.PictureBox pictEndgamePST;
		private MovementDiagramControl ctrlMovementDiagram;
		private System.Windows.Forms.Button btnCopyMidgamePST;
		private System.Windows.Forms.Button btnCopyEndgamePST;
		private PieceInfoControl ctrlPieceInfoControl;
	}
}