
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
	partial class SquareColorForm
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
			this.optTexture = new System.Windows.Forms.RadioButton();
			this.optColor = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.pickTexture = new System.Windows.Forms.ComboBox();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.optNotUsed = new System.Windows.Forms.RadioButton();
			this.panelSquareColor = new System.Windows.Forms.Panel();
			this.linkChooseColor = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// optTexture
			// 
			this.optTexture.AutoSize = true;
			this.optTexture.Location = new System.Drawing.Point( 39, 20 );
			this.optTexture.Name = "optTexture";
			this.optTexture.Size = new System.Drawing.Size( 92, 17 );
			this.optTexture.TabIndex = 0;
			this.optTexture.TabStop = true;
			this.optTexture.Text = "Use a Texture";
			this.optTexture.UseVisualStyleBackColor = true;
			this.optTexture.CheckedChanged += new System.EventHandler( this.options_CheckedChanged );
			// 
			// optColor
			// 
			this.optColor.AutoSize = true;
			this.optColor.Location = new System.Drawing.Point( 39, 83 );
			this.optColor.Name = "optColor";
			this.optColor.Size = new System.Drawing.Size( 106, 17 );
			this.optColor.TabIndex = 3;
			this.optColor.TabStop = true;
			this.optColor.Text = "Use a Solid Color";
			this.optColor.UseVisualStyleBackColor = true;
			this.optColor.CheckedChanged += new System.EventHandler( this.options_CheckedChanged );
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::ChessV.GUI.Properties.Resources.icon_cancel;
			this.btnCancel.Location = new System.Drawing.Point( 261, 154 );
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size( 113, 32 );
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "    &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point( 121, 154 );
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size( 113, 32 );
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "    &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 94, 47 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 46, 13 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Texture:";
			// 
			// pickTexture
			// 
			this.pickTexture.FormattingEnabled = true;
			this.pickTexture.Location = new System.Drawing.Point( 146, 44 );
			this.pickTexture.Name = "pickTexture";
			this.pickTexture.Size = new System.Drawing.Size( 180, 21 );
			this.pickTexture.TabIndex = 2;
			this.pickTexture.SelectedIndexChanged += new System.EventHandler( this.pickTexture_SelectedIndexChanged );
			// 
			// optNotUsed
			// 
			this.optNotUsed.AutoSize = true;
			this.optNotUsed.Location = new System.Drawing.Point( 39, 116 );
			this.optNotUsed.Name = "optNotUsed";
			this.optNotUsed.Size = new System.Drawing.Size( 70, 17 );
			this.optNotUsed.TabIndex = 5;
			this.optNotUsed.TabStop = true;
			this.optNotUsed.Text = "Not Used";
			this.optNotUsed.UseVisualStyleBackColor = true;
			this.optNotUsed.CheckedChanged += new System.EventHandler( this.options_CheckedChanged );
			// 
			// panelSquareColor
			// 
			this.panelSquareColor.BackColor = System.Drawing.Color.PaleGoldenrod;
			this.panelSquareColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSquareColor.Location = new System.Drawing.Point( 381, 48 );
			this.panelSquareColor.Name = "panelSquareColor";
			this.panelSquareColor.Size = new System.Drawing.Size( 50, 50 );
			this.panelSquareColor.TabIndex = 8;
			// 
			// linkChooseColor
			// 
			this.linkChooseColor.AutoSize = true;
			this.linkChooseColor.Location = new System.Drawing.Point( 151, 85 );
			this.linkChooseColor.Name = "linkChooseColor";
			this.linkChooseColor.Size = new System.Drawing.Size( 43, 13 );
			this.linkChooseColor.TabIndex = 4;
			this.linkChooseColor.TabStop = true;
			this.linkChooseColor.Text = "Choose";
			this.linkChooseColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.linkChooseColor_LinkClicked );
			// 
			// SquareColorForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size( 477, 196 );
			this.Controls.Add( this.linkChooseColor );
			this.Controls.Add( this.panelSquareColor );
			this.Controls.Add( this.optNotUsed );
			this.Controls.Add( this.pickTexture );
			this.Controls.Add( this.label1 );
			this.Controls.Add( this.btnCancel );
			this.Controls.Add( this.btnOK );
			this.Controls.Add( this.optColor );
			this.Controls.Add( this.optTexture );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SquareColorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Square Color or Texture";
			this.Load += new System.EventHandler( this.TextureOrColorForm_Load );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton optTexture;
		private System.Windows.Forms.RadioButton optColor;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox pickTexture;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.RadioButton optNotUsed;
		private System.Windows.Forms.Panel panelSquareColor;
		private System.Windows.Forms.LinkLabel linkChooseColor;
	}
}