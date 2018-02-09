
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
	partial class GameIndexFilterForm
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
			this.pictFilterIcon = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkTagDifferentArmies = new System.Windows.Forms.CheckBox();
			this.chkTagMultipleBoards = new System.Windows.Forms.CheckBox();
			this.chkTagRandomArray = new System.Windows.Forms.CheckBox();
			this.chkTagChessVariant = new System.Windows.Forms.CheckBox();
			this.chkTagPopular = new System.Windows.Forms.CheckBox();
			this.chkTagHistoric = new System.Windows.Forms.CheckBox();
			this.chkTagRegional = new System.Windows.Forms.CheckBox();
			this.chkFilterGameIndex = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictFilterIcon)).BeginInit();
			this.groupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictFilterIcon
			// 
			this.pictFilterIcon.Image = global::ChessV.GUI.Properties.Resources.icon_filter_large;
			this.pictFilterIcon.Location = new System.Drawing.Point(115, 12);
			this.pictFilterIcon.Name = "pictFilterIcon";
			this.pictFilterIcon.Size = new System.Drawing.Size(73, 70);
			this.pictFilterIcon.TabIndex = 0;
			this.pictFilterIcon.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(194, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(236, 70);
			this.label1.TabIndex = 1;
			this.label1.Text = "Game Index Filter";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.groupBox1);
			this.groupBox.Controls.Add(this.chkFilterGameIndex);
			this.groupBox.Location = new System.Drawing.Point(12, 96);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(537, 179);
			this.groupBox.TabIndex = 2;
			this.groupBox.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkTagDifferentArmies);
			this.groupBox1.Controls.Add(this.chkTagMultipleBoards);
			this.groupBox1.Controls.Add(this.chkTagRandomArray);
			this.groupBox1.Controls.Add(this.chkTagChessVariant);
			this.groupBox1.Controls.Add(this.chkTagPopular);
			this.groupBox1.Controls.Add(this.chkTagHistoric);
			this.groupBox1.Controls.Add(this.chkTagRegional);
			this.groupBox1.Location = new System.Drawing.Point(17, 31);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(494, 112);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Required Tags";
			// 
			// chkTagDifferentArmies
			// 
			this.chkTagDifferentArmies.AutoSize = true;
			this.chkTagDifferentArmies.Location = new System.Drawing.Point(327, 53);
			this.chkTagDifferentArmies.Name = "chkTagDifferentArmies";
			this.chkTagDifferentArmies.Size = new System.Drawing.Size(100, 17);
			this.chkTagDifferentArmies.TabIndex = 6;
			this.chkTagDifferentArmies.Text = "Different Armies";
			this.chkTagDifferentArmies.UseVisualStyleBackColor = true;
			// 
			// chkTagMultipleBoards
			// 
			this.chkTagMultipleBoards.AutoSize = true;
			this.chkTagMultipleBoards.Location = new System.Drawing.Point(327, 30);
			this.chkTagMultipleBoards.Name = "chkTagMultipleBoards";
			this.chkTagMultipleBoards.Size = new System.Drawing.Size(98, 17);
			this.chkTagMultipleBoards.TabIndex = 5;
			this.chkTagMultipleBoards.Text = "Multiple Boards";
			this.chkTagMultipleBoards.UseVisualStyleBackColor = true;
			// 
			// chkTagRandomArray
			// 
			this.chkTagRandomArray.AutoSize = true;
			this.chkTagRandomArray.Location = new System.Drawing.Point(190, 53);
			this.chkTagRandomArray.Name = "chkTagRandomArray";
			this.chkTagRandomArray.Size = new System.Drawing.Size(93, 17);
			this.chkTagRandomArray.TabIndex = 4;
			this.chkTagRandomArray.Text = "Random Array";
			this.chkTagRandomArray.UseVisualStyleBackColor = true;
			// 
			// chkTagChessVariant
			// 
			this.chkTagChessVariant.AutoSize = true;
			this.chkTagChessVariant.Location = new System.Drawing.Point(190, 30);
			this.chkTagChessVariant.Name = "chkTagChessVariant";
			this.chkTagChessVariant.Size = new System.Drawing.Size(91, 17);
			this.chkTagChessVariant.TabIndex = 3;
			this.chkTagChessVariant.Text = "Chess Variant";
			this.chkTagChessVariant.UseVisualStyleBackColor = true;
			// 
			// chkTagPopular
			// 
			this.chkTagPopular.AutoSize = true;
			this.chkTagPopular.Location = new System.Drawing.Point(53, 76);
			this.chkTagPopular.Name = "chkTagPopular";
			this.chkTagPopular.Size = new System.Drawing.Size(62, 17);
			this.chkTagPopular.TabIndex = 2;
			this.chkTagPopular.Text = "Popular";
			this.chkTagPopular.UseVisualStyleBackColor = true;
			// 
			// chkTagHistoric
			// 
			this.chkTagHistoric.AutoSize = true;
			this.chkTagHistoric.Location = new System.Drawing.Point(53, 53);
			this.chkTagHistoric.Name = "chkTagHistoric";
			this.chkTagHistoric.Size = new System.Drawing.Size(61, 17);
			this.chkTagHistoric.TabIndex = 1;
			this.chkTagHistoric.Text = "Historic";
			this.chkTagHistoric.UseVisualStyleBackColor = true;
			// 
			// chkTagRegional
			// 
			this.chkTagRegional.AutoSize = true;
			this.chkTagRegional.Location = new System.Drawing.Point(53, 30);
			this.chkTagRegional.Name = "chkTagRegional";
			this.chkTagRegional.Size = new System.Drawing.Size(68, 17);
			this.chkTagRegional.TabIndex = 0;
			this.chkTagRegional.Text = "Regional";
			this.chkTagRegional.UseVisualStyleBackColor = true;
			// 
			// chkFilterGameIndex
			// 
			this.chkFilterGameIndex.AutoSize = true;
			this.chkFilterGameIndex.Location = new System.Drawing.Point(27, -1);
			this.chkFilterGameIndex.Name = "chkFilterGameIndex";
			this.chkFilterGameIndex.Size = new System.Drawing.Size(108, 17);
			this.chkFilterGameIndex.TabIndex = 0;
			this.chkFilterGameIndex.Text = "Filter Game Index";
			this.chkFilterGameIndex.UseVisualStyleBackColor = true;
			this.chkFilterGameIndex.CheckedChanged += new System.EventHandler(this.chkFilterGameIndex_CheckedChanged);
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(219, 305);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// GameIndexFilterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(561, 349);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictFilterIcon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "GameIndexFilterForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Filter Master Index";
			this.Load += new System.EventHandler(this.GameIndexFilterForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictFilterIcon)).EndInit();
			this.groupBox.ResumeLayout(false);
			this.groupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictFilterIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkFilterGameIndex;
		private System.Windows.Forms.CheckBox chkTagChessVariant;
		private System.Windows.Forms.CheckBox chkTagPopular;
		private System.Windows.Forms.CheckBox chkTagHistoric;
		private System.Windows.Forms.CheckBox chkTagRegional;
		private System.Windows.Forms.CheckBox chkTagRandomArray;
		private System.Windows.Forms.CheckBox chkTagMultipleBoards;
		private System.Windows.Forms.CheckBox chkTagDifferentArmies;
		private System.Windows.Forms.Button btnOK;
	}
}