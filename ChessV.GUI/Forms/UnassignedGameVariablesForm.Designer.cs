
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
	partial class UnassignedGameVariablesForm
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
			this.lblGameVariable1 = new System.Windows.Forms.Label();
			this.lblGameVariable2 = new System.Windows.Forms.Label();
			this.pickVariable1 = new System.Windows.Forms.ComboBox();
			this.pickVariable2 = new System.Windows.Forms.ComboBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.txtVariable1 = new System.Windows.Forms.TextBox();
			this.txtVariable2 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictSubGamePreview = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictSubGamePreview)).BeginInit();
			this.SuspendLayout();
			// 
			// lblGameVariable1
			// 
			this.lblGameVariable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameVariable1.Location = new System.Drawing.Point(79, 24);
			this.lblGameVariable1.Name = "lblGameVariable1";
			this.lblGameVariable1.Size = new System.Drawing.Size(134, 23);
			this.lblGameVariable1.TabIndex = 0;
			this.lblGameVariable1.Text = "variable 1:";
			this.lblGameVariable1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblGameVariable1.Visible = false;
			// 
			// lblGameVariable2
			// 
			this.lblGameVariable2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameVariable2.Location = new System.Drawing.Point(76, 59);
			this.lblGameVariable2.Name = "lblGameVariable2";
			this.lblGameVariable2.Size = new System.Drawing.Size(137, 23);
			this.lblGameVariable2.TabIndex = 1;
			this.lblGameVariable2.Text = "variable 2:";
			this.lblGameVariable2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblGameVariable2.Visible = false;
			// 
			// pickVariable1
			// 
			this.pickVariable1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickVariable1.FormattingEnabled = true;
			this.pickVariable1.Location = new System.Drawing.Point(219, 26);
			this.pickVariable1.Name = "pickVariable1";
			this.pickVariable1.Size = new System.Drawing.Size(189, 21);
			this.pickVariable1.TabIndex = 2;
			this.pickVariable1.Visible = false;
			this.pickVariable1.SelectedIndexChanged += new System.EventHandler(this.pickVariables_SelectedIndexChanged);
			// 
			// pickVariable2
			// 
			this.pickVariable2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickVariable2.FormattingEnabled = true;
			this.pickVariable2.Location = new System.Drawing.Point(219, 61);
			this.pickVariable2.Name = "pickVariable2";
			this.pickVariable2.Size = new System.Drawing.Size(189, 21);
			this.pickVariable2.TabIndex = 3;
			this.pickVariable2.Visible = false;
			this.pickVariable2.SelectedIndexChanged += new System.EventHandler(this.pickVariables_SelectedIndexChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(208, 375);
			this.btnOK.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnOK.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 13;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtVariable1
			// 
			this.txtVariable1.Location = new System.Drawing.Point(219, 26);
			this.txtVariable1.Name = "txtVariable1";
			this.txtVariable1.Size = new System.Drawing.Size(189, 20);
			this.txtVariable1.TabIndex = 14;
			this.txtVariable1.Visible = false;
			this.txtVariable1.TextChanged += new System.EventHandler(this.txtVariables_TextChanged);
			this.txtVariable1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVariables_KeyPress);
			// 
			// txtVariable2
			// 
			this.txtVariable2.Location = new System.Drawing.Point(219, 62);
			this.txtVariable2.Name = "txtVariable2";
			this.txtVariable2.Size = new System.Drawing.Size(189, 20);
			this.txtVariable2.TabIndex = 15;
			this.txtVariable2.Visible = false;
			this.txtVariable2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVariables_KeyPress);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::ChessV.GUI.Properties.Resources.icon_game_parameters;
			this.pictureBox1.Location = new System.Drawing.Point(34, 30);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.TabIndex = 16;
			this.pictureBox1.TabStop = false;
			// 
			// pictSubGamePreview
			// 
			this.pictSubGamePreview.Location = new System.Drawing.Point(12, 105);
			this.pictSubGamePreview.Name = "pictSubGamePreview";
			this.pictSubGamePreview.Size = new System.Drawing.Size(515, 254);
			this.pictSubGamePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictSubGamePreview.TabIndex = 6;
			this.pictSubGamePreview.TabStop = false;
			// 
			// UnassignedGameVariablesForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(539, 419);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.txtVariable2);
			this.Controls.Add(this.txtVariable1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.pictSubGamePreview);
			this.Controls.Add(this.pickVariable2);
			this.Controls.Add(this.pickVariable1);
			this.Controls.Add(this.lblGameVariable2);
			this.Controls.Add(this.lblGameVariable1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "UnassignedGameVariablesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Configure Game";
			this.Load += new System.EventHandler(this.GameVariablesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictSubGamePreview)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblGameVariable1;
		private System.Windows.Forms.Label lblGameVariable2;
		private System.Windows.Forms.ComboBox pickVariable1;
		private System.Windows.Forms.ComboBox pickVariable2;
		private System.Windows.Forms.PictureBox pictSubGamePreview;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtVariable1;
		private System.Windows.Forms.TextBox txtVariable2;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}