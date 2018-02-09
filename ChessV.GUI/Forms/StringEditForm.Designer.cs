
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
	partial class StringEditForm
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
			this.label = new System.Windows.Forms.Label();
			this.txtString = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label
			// 
			this.label.Location = new System.Drawing.Point( 12, 21 );
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size( 132, 13 );
			this.label.TabIndex = 0;
			this.label.Text = "String:";
			this.label.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtString
			// 
			this.txtString.Location = new System.Drawing.Point( 150, 18 );
			this.txtString.Name = "txtString";
			this.txtString.Size = new System.Drawing.Size( 383, 20 );
			this.txtString.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::ChessV.GUI.Properties.Resources.icon_cancel;
			this.btnCancel.Location = new System.Drawing.Point( 332, 60 );
			this.btnCancel.MaximumSize = new System.Drawing.Size( 123, 32 );
			this.btnCancel.MinimumSize = new System.Drawing.Size( 123, 32 );
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size( 123, 32 );
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point( 168, 60 );
			this.btnOK.MaximumSize = new System.Drawing.Size( 123, 32 );
			this.btnOK.MinimumSize = new System.Drawing.Size( 123, 32 );
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size( 123, 32 );
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// StringEditForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size( 622, 104 );
			this.Controls.Add( this.btnCancel );
			this.Controls.Add( this.btnOK );
			this.Controls.Add( this.txtString );
			this.Controls.Add( this.label );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "StringEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "String";
			this.Load += new System.EventHandler( this.StringEditForm_Load );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label;
		private System.Windows.Forms.TextBox txtString;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}