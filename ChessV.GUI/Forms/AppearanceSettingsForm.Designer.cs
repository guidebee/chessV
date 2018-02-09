
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
	partial class AppearanceSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppearanceSettingsForm));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabBoardColors = new System.Windows.Forms.TabPage();
			this.pictColorSchemes = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelHighlightColor = new System.Windows.Forms.Panel();
			this.groupText = new System.Windows.Forms.GroupBox();
			this.panelTextColor = new System.Windows.Forms.Panel();
			this.btnSaveScheme = new System.Windows.Forms.Button();
			this.groupPieces = new System.Windows.Forms.GroupBox();
			this.panelPlayerColor2 = new System.Windows.Forms.Panel();
			this.panelPlayerColor1 = new System.Windows.Forms.Panel();
			this.groupBorder = new System.Windows.Forms.GroupBox();
			this.panelBorderColor = new System.Windows.Forms.Panel();
			this.groupSquares = new System.Windows.Forms.GroupBox();
			this.panelSquareColor3 = new System.Windows.Forms.Panel();
			this.panelSquareColor2 = new System.Windows.Forms.Panel();
			this.panelSquareColor1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pickColorScheme = new System.Windows.Forms.ComboBox();
			this.tabPieces = new System.Windows.Forms.TabPage();
			this.pictPieceSets = new System.Windows.Forms.PictureBox();
			this.pickPieceSet = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabBoardColors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictColorSchemes)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupText.SuspendLayout();
			this.groupPieces.SuspendLayout();
			this.groupBorder.SuspendLayout();
			this.groupSquares.SuspendLayout();
			this.tabPieces.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictPieceSets)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabBoardColors);
			this.tabControl.Controls.Add(this.tabPieces);
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(610, 452);
			this.tabControl.TabIndex = 0;
			// 
			// tabBoardColors
			// 
			this.tabBoardColors.Controls.Add(this.label3);
			this.tabBoardColors.Controls.Add(this.pictColorSchemes);
			this.tabBoardColors.Controls.Add(this.groupBox1);
			this.tabBoardColors.Controls.Add(this.groupText);
			this.tabBoardColors.Controls.Add(this.btnSaveScheme);
			this.tabBoardColors.Controls.Add(this.groupPieces);
			this.tabBoardColors.Controls.Add(this.groupBorder);
			this.tabBoardColors.Controls.Add(this.groupSquares);
			this.tabBoardColors.Controls.Add(this.label1);
			this.tabBoardColors.Controls.Add(this.pickColorScheme);
			this.tabBoardColors.Location = new System.Drawing.Point(4, 22);
			this.tabBoardColors.Name = "tabBoardColors";
			this.tabBoardColors.Padding = new System.Windows.Forms.Padding(3);
			this.tabBoardColors.Size = new System.Drawing.Size(602, 426);
			this.tabBoardColors.TabIndex = 0;
			this.tabBoardColors.Text = "Board Colors";
			this.tabBoardColors.UseVisualStyleBackColor = true;
			// 
			// pictColorSchemes
			// 
			this.pictColorSchemes.Image = global::ChessV.GUI.Properties.Resources.icon_color_schemes;
			this.pictColorSchemes.Location = new System.Drawing.Point(42, 13);
			this.pictColorSchemes.Name = "pictColorSchemes";
			this.pictColorSchemes.Size = new System.Drawing.Size(48, 48);
			this.pictColorSchemes.TabIndex = 6;
			this.pictColorSchemes.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panelHighlightColor);
			this.groupBox1.Location = new System.Drawing.Point(209, 268);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(185, 86);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Square Highlight";
			// 
			// panelHighlightColor
			// 
			this.panelHighlightColor.BackColor = System.Drawing.Color.Sienna;
			this.panelHighlightColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelHighlightColor.Location = new System.Drawing.Point(68, 20);
			this.panelHighlightColor.Name = "panelHighlightColor";
			this.panelHighlightColor.Size = new System.Drawing.Size(50, 50);
			this.panelHighlightColor.TabIndex = 3;
			this.panelHighlightColor.Click += new System.EventHandler(this.panelHighlightColor_Click);
			// 
			// groupText
			// 
			this.groupText.Controls.Add(this.panelTextColor);
			this.groupText.Location = new System.Drawing.Point(406, 268);
			this.groupText.Name = "groupText";
			this.groupText.Size = new System.Drawing.Size(185, 86);
			this.groupText.TabIndex = 4;
			this.groupText.TabStop = false;
			this.groupText.Text = "Text";
			// 
			// panelTextColor
			// 
			this.panelTextColor.BackColor = System.Drawing.Color.Yellow;
			this.panelTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTextColor.Location = new System.Drawing.Point(67, 18);
			this.panelTextColor.Name = "panelTextColor";
			this.panelTextColor.Size = new System.Drawing.Size(50, 50);
			this.panelTextColor.TabIndex = 4;
			this.panelTextColor.Click += new System.EventHandler(this.panelTextColor_Click);
			// 
			// btnSaveScheme
			// 
			this.btnSaveScheme.Image = global::ChessV.GUI.Properties.Resources.icon_save;
			this.btnSaveScheme.Location = new System.Drawing.Point(240, 371);
			this.btnSaveScheme.Name = "btnSaveScheme";
			this.btnSaveScheme.Size = new System.Drawing.Size(123, 32);
			this.btnSaveScheme.TabIndex = 5;
			this.btnSaveScheme.Text = "  Save Scheme";
			this.btnSaveScheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveScheme.UseVisualStyleBackColor = true;
			this.btnSaveScheme.Click += new System.EventHandler(this.btnSaveScheme_Click);
			// 
			// groupPieces
			// 
			this.groupPieces.Controls.Add(this.panelPlayerColor2);
			this.groupPieces.Controls.Add(this.panelPlayerColor1);
			this.groupPieces.Location = new System.Drawing.Point(12, 176);
			this.groupPieces.Name = "groupPieces";
			this.groupPieces.Size = new System.Drawing.Size(579, 86);
			this.groupPieces.TabIndex = 2;
			this.groupPieces.TabStop = false;
			this.groupPieces.Text = "Pieces";
			// 
			// panelPlayerColor2
			// 
			this.panelPlayerColor2.BackColor = System.Drawing.Color.DarkBlue;
			this.panelPlayerColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelPlayerColor2.Location = new System.Drawing.Point(301, 19);
			this.panelPlayerColor2.Name = "panelPlayerColor2";
			this.panelPlayerColor2.Size = new System.Drawing.Size(50, 50);
			this.panelPlayerColor2.TabIndex = 1;
			this.panelPlayerColor2.Click += new System.EventHandler(this.panelPlayerColor2_Click);
			// 
			// panelPlayerColor1
			// 
			this.panelPlayerColor1.BackColor = System.Drawing.Color.Snow;
			this.panelPlayerColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelPlayerColor1.Location = new System.Drawing.Point(228, 19);
			this.panelPlayerColor1.Name = "panelPlayerColor1";
			this.panelPlayerColor1.Size = new System.Drawing.Size(50, 50);
			this.panelPlayerColor1.TabIndex = 0;
			this.panelPlayerColor1.Click += new System.EventHandler(this.panelPlayerColor1_Click);
			// 
			// groupBorder
			// 
			this.groupBorder.Controls.Add(this.panelBorderColor);
			this.groupBorder.Location = new System.Drawing.Point(12, 268);
			this.groupBorder.Name = "groupBorder";
			this.groupBorder.Size = new System.Drawing.Size(185, 86);
			this.groupBorder.TabIndex = 3;
			this.groupBorder.TabStop = false;
			this.groupBorder.Text = "Border";
			// 
			// panelBorderColor
			// 
			this.panelBorderColor.BackColor = System.Drawing.Color.Sienna;
			this.panelBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelBorderColor.Location = new System.Drawing.Point(66, 20);
			this.panelBorderColor.Name = "panelBorderColor";
			this.panelBorderColor.Size = new System.Drawing.Size(50, 50);
			this.panelBorderColor.TabIndex = 3;
			this.panelBorderColor.Click += new System.EventHandler(this.panelBorderColor_Click);
			// 
			// groupSquares
			// 
			this.groupSquares.Controls.Add(this.panelSquareColor3);
			this.groupSquares.Controls.Add(this.panelSquareColor2);
			this.groupSquares.Controls.Add(this.panelSquareColor1);
			this.groupSquares.Location = new System.Drawing.Point(12, 82);
			this.groupSquares.Name = "groupSquares";
			this.groupSquares.Size = new System.Drawing.Size(579, 88);
			this.groupSquares.TabIndex = 1;
			this.groupSquares.TabStop = false;
			this.groupSquares.Text = "Squares";
			// 
			// panelSquareColor3
			// 
			this.panelSquareColor3.BackColor = System.Drawing.Color.DarkKhaki;
			this.panelSquareColor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSquareColor3.Location = new System.Drawing.Point(338, 20);
			this.panelSquareColor3.Name = "panelSquareColor3";
			this.panelSquareColor3.Size = new System.Drawing.Size(50, 50);
			this.panelSquareColor3.TabIndex = 4;
			this.panelSquareColor3.Click += new System.EventHandler(this.panelSquareColor3_Click);
			// 
			// panelSquareColor2
			// 
			this.panelSquareColor2.BackColor = System.Drawing.Color.BurlyWood;
			this.panelSquareColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSquareColor2.Location = new System.Drawing.Point(265, 20);
			this.panelSquareColor2.Name = "panelSquareColor2";
			this.panelSquareColor2.Size = new System.Drawing.Size(50, 50);
			this.panelSquareColor2.TabIndex = 3;
			this.panelSquareColor2.Click += new System.EventHandler(this.panelSquareColor2_Click);
			// 
			// panelSquareColor1
			// 
			this.panelSquareColor1.BackColor = System.Drawing.Color.PaleGoldenrod;
			this.panelSquareColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSquareColor1.Location = new System.Drawing.Point(191, 20);
			this.panelSquareColor1.Name = "panelSquareColor1";
			this.panelSquareColor1.Size = new System.Drawing.Size(50, 50);
			this.panelSquareColor1.TabIndex = 2;
			this.panelSquareColor1.Click += new System.EventHandler(this.panelSquareColor1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(161, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Color Scheme:";
			// 
			// pickColorScheme
			// 
			this.pickColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickColorScheme.FormattingEnabled = true;
			this.pickColorScheme.Location = new System.Drawing.Point(243, 25);
			this.pickColorScheme.Name = "pickColorScheme";
			this.pickColorScheme.Size = new System.Drawing.Size(199, 21);
			this.pickColorScheme.TabIndex = 0;
			this.pickColorScheme.SelectedIndexChanged += new System.EventHandler(this.pickColorScheme_SelectedIndexChanged);
			// 
			// tabPieces
			// 
			this.tabPieces.Controls.Add(this.pictPieceSets);
			this.tabPieces.Controls.Add(this.pickPieceSet);
			this.tabPieces.Controls.Add(this.label2);
			this.tabPieces.Location = new System.Drawing.Point(4, 22);
			this.tabPieces.Name = "tabPieces";
			this.tabPieces.Padding = new System.Windows.Forms.Padding(3);
			this.tabPieces.Size = new System.Drawing.Size(602, 426);
			this.tabPieces.TabIndex = 1;
			this.tabPieces.Text = "Pieces";
			this.tabPieces.UseVisualStyleBackColor = true;
			// 
			// pictPieceSets
			// 
			this.pictPieceSets.Image = global::ChessV.GUI.Properties.Resources.icon_piece_sets;
			this.pictPieceSets.Location = new System.Drawing.Point(42, 15);
			this.pictPieceSets.Name = "pictPieceSets";
			this.pictPieceSets.Size = new System.Drawing.Size(48, 48);
			this.pictPieceSets.TabIndex = 2;
			this.pictPieceSets.TabStop = false;
			// 
			// pickPieceSet
			// 
			this.pickPieceSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickPieceSet.FormattingEnabled = true;
			this.pickPieceSet.Location = new System.Drawing.Point(243, 28);
			this.pickPieceSet.Name = "pickPieceSet";
			this.pickPieceSet.Size = new System.Drawing.Size(199, 21);
			this.pickPieceSet.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(176, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Piece Sets:";
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(168, 470);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(343, 470);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(138, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(327, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Colors can be changed by clicking the squares below";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AppearanceSettingsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(634, 514);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "AppearanceSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Appearance";
			this.Load += new System.EventHandler(this.AppearanceSettingsForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabBoardColors.ResumeLayout(false);
			this.tabBoardColors.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictColorSchemes)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupText.ResumeLayout(false);
			this.groupPieces.ResumeLayout(false);
			this.groupBorder.ResumeLayout(false);
			this.groupSquares.ResumeLayout(false);
			this.tabPieces.ResumeLayout(false);
			this.tabPieces.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictPieceSets)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabBoardColors;
		private System.Windows.Forms.TabPage tabPieces;
		private System.Windows.Forms.Button btnSaveScheme;
		private System.Windows.Forms.GroupBox groupPieces;
		private System.Windows.Forms.Panel panelPlayerColor2;
		private System.Windows.Forms.Panel panelPlayerColor1;
		private System.Windows.Forms.GroupBox groupBorder;
		private System.Windows.Forms.Panel panelBorderColor;
		private System.Windows.Forms.GroupBox groupSquares;
		private System.Windows.Forms.Panel panelSquareColor3;
		private System.Windows.Forms.Panel panelSquareColor2;
		private System.Windows.Forms.Panel panelSquareColor1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox pickColorScheme;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.ComboBox pickPieceSet;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupText;
		private System.Windows.Forms.Panel panelTextColor;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panelHighlightColor;
		private System.Windows.Forms.PictureBox pictColorSchemes;
		private System.Windows.Forms.PictureBox pictPieceSets;
		private System.Windows.Forms.Label label3;
	}
}