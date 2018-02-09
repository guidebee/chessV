
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
	partial class ExceptionForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtExceptionDetails = new System.Windows.Forms.TextBox();
			this.btnOuterException = new System.Windows.Forms.Button();
			this.btnInnerException = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnSaveLog = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(82, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(409, 64);
			this.label1.TabIndex = 0;
			this.label1.Text = "An Error has Occured";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 79);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(550, 71);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtExceptionDetails
			// 
			this.txtExceptionDetails.AcceptsReturn = true;
			this.txtExceptionDetails.BackColor = System.Drawing.Color.LemonChiffon;
			this.txtExceptionDetails.Location = new System.Drawing.Point(12, 153);
			this.txtExceptionDetails.Multiline = true;
			this.txtExceptionDetails.Name = "txtExceptionDetails";
			this.txtExceptionDetails.ReadOnly = true;
			this.txtExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtExceptionDetails.Size = new System.Drawing.Size(550, 185);
			this.txtExceptionDetails.TabIndex = 3;
			this.txtExceptionDetails.WordWrap = false;
			this.txtExceptionDetails.Enter += new System.EventHandler(this.txtExceptionDetails_Enter);
			// 
			// btnOuterException
			// 
			this.btnOuterException.Image = global::ChessV.GUI.Properties.Resources.icon_back;
			this.btnOuterException.Location = new System.Drawing.Point(12, 349);
			this.btnOuterException.Name = "btnOuterException";
			this.btnOuterException.Size = new System.Drawing.Size(144, 32);
			this.btnOuterException.TabIndex = 7;
			this.btnOuterException.Text = "   Outer Exception";
			this.btnOuterException.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOuterException.UseVisualStyleBackColor = true;
			this.btnOuterException.Click += new System.EventHandler(this.btnOuterException_Click);
			// 
			// btnInnerException
			// 
			this.btnInnerException.Image = global::ChessV.GUI.Properties.Resources.icon_forward;
			this.btnInnerException.Location = new System.Drawing.Point(160, 349);
			this.btnInnerException.Name = "btnInnerException";
			this.btnInnerException.Size = new System.Drawing.Size(144, 32);
			this.btnInnerException.TabIndex = 8;
			this.btnInnerException.Text = "   Inner Exception";
			this.btnInnerException.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnInnerException.UseVisualStyleBackColor = true;
			this.btnInnerException.Click += new System.EventHandler(this.btnInnerException_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Image = global::ChessV.GUI.Properties.Resources.icon_cancel;
			this.btnClose.Location = new System.Drawing.Point(439, 349);
			this.btnClose.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnClose.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(123, 32);
			this.btnClose.TabIndex = 6;
			this.btnClose.Text = "    &Close";
			this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::ChessV.GUI.Properties.Resources.icon_bomb;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// btnSaveLog
			// 
			this.btnSaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveLog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnSaveLog.Image = global::ChessV.GUI.Properties.Resources.icon_load;
			this.btnSaveLog.Location = new System.Drawing.Point(310, 349);
			this.btnSaveLog.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnSaveLog.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnSaveLog.Name = "btnSaveLog";
			this.btnSaveLog.Size = new System.Drawing.Size(123, 32);
			this.btnSaveLog.TabIndex = 9;
			this.btnSaveLog.Text = "    &Save Log";
			this.btnSaveLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSaveLog.UseVisualStyleBackColor = true;
			this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
			// 
			// ExceptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(574, 393);
			this.Controls.Add(this.btnSaveLog);
			this.Controls.Add(this.btnInnerException);
			this.Controls.Add(this.btnOuterException);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtExceptionDetails);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ExceptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ChessV Error";
			this.Load += new System.EventHandler(this.ExceptionForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtExceptionDetails;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnOuterException;
		private System.Windows.Forms.Button btnInnerException;
		private System.Windows.Forms.Button btnSaveLog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
	}
}