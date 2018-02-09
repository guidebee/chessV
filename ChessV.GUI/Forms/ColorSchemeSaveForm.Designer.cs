
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
	partial class ColorSchemeSaveForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ColorSchemeSaveForm ) );
			this.optOverwrite = new System.Windows.Forms.RadioButton();
			this.optSaveAs = new System.Windows.Forms.RadioButton();
			this.txtNewSchemeName = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// optOverwrite
			// 
			this.optOverwrite.AutoSize = true;
			this.optOverwrite.Location = new System.Drawing.Point( 31, 22 );
			this.optOverwrite.Name = "optOverwrite";
			this.optOverwrite.Size = new System.Drawing.Size( 136, 17 );
			this.optOverwrite.TabIndex = 0;
			this.optOverwrite.TabStop = true;
			this.optOverwrite.Text = "Overwrite color scheme";
			this.optOverwrite.UseVisualStyleBackColor = true;
			this.optOverwrite.CheckedChanged += new System.EventHandler( this.optOverwrite_CheckedChanged );
			// 
			// optSaveAs
			// 
			this.optSaveAs.AutoSize = true;
			this.optSaveAs.Location = new System.Drawing.Point( 31, 50 );
			this.optSaveAs.Name = "optSaveAs";
			this.optSaveAs.Size = new System.Drawing.Size( 127, 17 );
			this.optSaveAs.TabIndex = 1;
			this.optSaveAs.TabStop = true;
			this.optSaveAs.Text = "Save as new scheme";
			this.optSaveAs.UseVisualStyleBackColor = true;
			this.optSaveAs.CheckedChanged += new System.EventHandler( this.optSaveAs_CheckedChanged );
			// 
			// txtNewSchemeName
			// 
			this.txtNewSchemeName.Location = new System.Drawing.Point( 58, 81 );
			this.txtNewSchemeName.Name = "txtNewSchemeName";
			this.txtNewSchemeName.Size = new System.Drawing.Size( 267, 20 );
			this.txtNewSchemeName.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image) (resources.GetObject( "btnCancel.Image" )));
			this.btnCancel.Location = new System.Drawing.Point( 205, 129 );
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size( 112, 36 );
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "    &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point( 65, 129 );
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size( 112, 36 );
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "    &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// ColorSchemeSaveForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size( 383, 178 );
			this.Controls.Add( this.btnCancel );
			this.Controls.Add( this.btnOK );
			this.Controls.Add( this.txtNewSchemeName );
			this.Controls.Add( this.optSaveAs );
			this.Controls.Add( this.optOverwrite );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ColorSchemeSaveForm";
			this.Text = "Save Color Scheme";
			this.Load += new System.EventHandler( this.ColorSchemeSaveForm_Load );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton optOverwrite;
		private System.Windows.Forms.RadioButton optSaveAs;
		private System.Windows.Forms.TextBox txtNewSchemeName;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}