
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
	partial class LoadFENForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadFENForm));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblGameName = new System.Windows.Forms.Label();
			this.lblFENFormat = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblGameStartFEN = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCurrentFEN = new System.Windows.Forms.TextBox();
			this.listPieceTypes = new System.Windows.Forms.ListView();
			this.label4 = new System.Windows.Forms.Label();
			this.colNotation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTypeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colInternalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(386, 333);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(211, 333);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblGameName
			// 
			this.lblGameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameName.Location = new System.Drawing.Point(12, 9);
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = new System.Drawing.Size(696, 41);
			this.lblGameName.TabIndex = 4;
			this.lblGameName.Text = "Game Name";
			this.lblGameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFENFormat
			// 
			this.lblFENFormat.AutoSize = true;
			this.lblFENFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFENFormat.Location = new System.Drawing.Point(141, 69);
			this.lblFENFormat.Name = "lblFENFormat";
			this.lblFENFormat.Size = new System.Drawing.Size(83, 16);
			this.lblFENFormat.TabIndex = 5;
			this.lblFENFormat.Text = "FEN Format:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(52, 69);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "FEN Format:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(15, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Game Start FEN:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblGameStartFEN
			// 
			this.lblGameStartFEN.AutoSize = true;
			this.lblGameStartFEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameStartFEN.Location = new System.Drawing.Point(141, 96);
			this.lblGameStartFEN.Name = "lblGameStartFEN";
			this.lblGameStartFEN.Size = new System.Drawing.Size(105, 16);
			this.lblGameStartFEN.TabIndex = 8;
			this.lblGameStartFEN.Text = "Game Start FEN";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(15, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Current FEN:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// txtCurrentFEN
			// 
			this.txtCurrentFEN.Location = new System.Drawing.Point(141, 125);
			this.txtCurrentFEN.Name = "txtCurrentFEN";
			this.txtCurrentFEN.Size = new System.Drawing.Size(456, 20);
			this.txtCurrentFEN.TabIndex = 10;
			// 
			// listPieceTypes
			// 
			this.listPieceTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNotation,
            this.colTypeName,
            this.colInternalName});
			this.listPieceTypes.FullRowSelect = true;
			this.listPieceTypes.GridLines = true;
			this.listPieceTypes.Location = new System.Drawing.Point(162, 173);
			this.listPieceTypes.MultiSelect = false;
			this.listPieceTypes.Name = "listPieceTypes";
			this.listPieceTypes.ShowGroups = false;
			this.listPieceTypes.Size = new System.Drawing.Size(402, 145);
			this.listPieceTypes.TabIndex = 11;
			this.listPieceTypes.UseCompatibleStateImageBehavior = false;
			this.listPieceTypes.View = System.Windows.Forms.View.Details;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(159, 157);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(97, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Piece Types Used:";
			// 
			// colNotation
			// 
			this.colNotation.Text = "Notation";
			this.colNotation.Width = 80;
			// 
			// colTypeName
			// 
			this.colTypeName.Text = "Type Name in Game";
			this.colTypeName.Width = 160;
			// 
			// colInternalName
			// 
			this.colInternalName.Text = "Internal Name";
			this.colInternalName.Width = 120;
			// 
			// LoadFENForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(720, 377);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.listPieceTypes);
			this.Controls.Add(this.txtCurrentFEN);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblGameStartFEN);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblFENFormat);
			this.Controls.Add(this.lblGameName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "LoadFENForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Load Position by FEN";
			this.Load += new System.EventHandler(this.LoadFENForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblGameName;
		private System.Windows.Forms.Label lblFENFormat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblGameStartFEN;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCurrentFEN;
		private System.Windows.Forms.ListView listPieceTypes;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ColumnHeader colNotation;
		private System.Windows.Forms.ColumnHeader colTypeName;
		private System.Windows.Forms.ColumnHeader colInternalName;
	}
}