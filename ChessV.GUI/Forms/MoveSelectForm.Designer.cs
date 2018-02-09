
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
	partial class MoveSelectForm
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
			this.listMoves = new System.Windows.Forms.ListBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listMoves
			// 
			this.listMoves.FormattingEnabled = true;
			this.listMoves.Location = new System.Drawing.Point( 12, 12 );
			this.listMoves.Name = "listMoves";
			this.listMoves.Size = new System.Drawing.Size( 378, 160 );
			this.listMoves.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point( 140, 197 );
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size( 123, 32 );
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
			// 
			// MoveSelectForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size( 402, 243 );
			this.Controls.Add( this.btnOK );
			this.Controls.Add( this.listMoves );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MoveSelectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Move";
			this.Load += new System.EventHandler( this.MoveSelectForm_Load );
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.ListBox listMoves;
		private System.Windows.Forms.Button btnOK;
	}
}