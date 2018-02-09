
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
	partial class NewEngineForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEngineForm));
			this.label2 = new System.Windows.Forms.Label();
			this.txtExecutable = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnDiscoverEngine = new System.Windows.Forms.Button();
			this.btnMoreLess = new System.Windows.Forms.Button();
			this.btnBrowseWorkingDirectory = new System.Windows.Forms.Button();
			this.btnBrowseExecutable = new System.Windows.Forms.Button();
			this.btnRemoveArgument = new System.Windows.Forms.Button();
			this.btnEditArgument = new System.Windows.Forms.Button();
			this.btnNewArgument = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.listArguments = new System.Windows.Forms.ListBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Executable:";
			// 
			// txtExecutable
			// 
			this.txtExecutable.Location = new System.Drawing.Point(117, 27);
			this.txtExecutable.Name = "txtExecutable";
			this.txtExecutable.Size = new System.Drawing.Size(374, 20);
			this.txtExecutable.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Working Directory:";
			// 
			// txtWorkingDirectory
			// 
			this.txtWorkingDirectory.Location = new System.Drawing.Point(117, 69);
			this.txtWorkingDirectory.Name = "txtWorkingDirectory";
			this.txtWorkingDirectory.Size = new System.Drawing.Size(374, 20);
			this.txtWorkingDirectory.TabIndex = 6;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(285, 292);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnDiscoverEngine
			// 
			this.btnDiscoverEngine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDiscoverEngine.Image = global::ChessV.GUI.Properties.Resources.icon_next;
			this.btnDiscoverEngine.Location = new System.Drawing.Point(423, 292);
			this.btnDiscoverEngine.Name = "btnDiscoverEngine";
			this.btnDiscoverEngine.Size = new System.Drawing.Size(123, 32);
			this.btnDiscoverEngine.TabIndex = 9;
			this.btnDiscoverEngine.Text = "Discover Engine";
			this.btnDiscoverEngine.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.btnDiscoverEngine.UseVisualStyleBackColor = true;
			this.btnDiscoverEngine.Click += new System.EventHandler(this.btnDiscoverEngine_Click);
			// 
			// btnMoreLess
			// 
			this.btnMoreLess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnMoreLess.Image = ((System.Drawing.Image)(resources.GetObject("btnMoreLess.Image")));
			this.btnMoreLess.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnMoreLess.Location = new System.Drawing.Point(18, 292);
			this.btnMoreLess.Name = "btnMoreLess";
			this.btnMoreLess.Size = new System.Drawing.Size(123, 32);
			this.btnMoreLess.TabIndex = 8;
			this.btnMoreLess.Text = "    More";
			this.btnMoreLess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnMoreLess.UseVisualStyleBackColor = true;
			this.btnMoreLess.Click += new System.EventHandler(this.btnMoreLess_Click);
			// 
			// btnBrowseWorkingDirectory
			// 
			this.btnBrowseWorkingDirectory.Image = global::ChessV.GUI.Properties.Resources.icon_open_folder;
			this.btnBrowseWorkingDirectory.Location = new System.Drawing.Point(497, 64);
			this.btnBrowseWorkingDirectory.Name = "btnBrowseWorkingDirectory";
			this.btnBrowseWorkingDirectory.Size = new System.Drawing.Size(32, 28);
			this.btnBrowseWorkingDirectory.TabIndex = 7;
			this.btnBrowseWorkingDirectory.UseVisualStyleBackColor = true;
			this.btnBrowseWorkingDirectory.Click += new System.EventHandler(this.btnBrowseWorkingDirectory_Click);
			// 
			// btnBrowseExecutable
			// 
			this.btnBrowseExecutable.Image = global::ChessV.GUI.Properties.Resources.icon_open_folder;
			this.btnBrowseExecutable.Location = new System.Drawing.Point(497, 22);
			this.btnBrowseExecutable.Name = "btnBrowseExecutable";
			this.btnBrowseExecutable.Size = new System.Drawing.Size(32, 28);
			this.btnBrowseExecutable.TabIndex = 4;
			this.btnBrowseExecutable.UseVisualStyleBackColor = true;
			this.btnBrowseExecutable.Click += new System.EventHandler(this.btnBrowseExecutable_Click);
			// 
			// btnRemoveArgument
			// 
			this.btnRemoveArgument.Location = new System.Drawing.Point(330, 241);
			this.btnRemoveArgument.Name = "btnRemoveArgument";
			this.btnRemoveArgument.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveArgument.TabIndex = 15;
			this.btnRemoveArgument.Text = "Remove";
			this.btnRemoveArgument.UseVisualStyleBackColor = true;
			this.btnRemoveArgument.Visible = false;
			this.btnRemoveArgument.Click += new System.EventHandler(this.btnRemoveArgument_Click);
			// 
			// btnEditArgument
			// 
			this.btnEditArgument.Location = new System.Drawing.Point(247, 241);
			this.btnEditArgument.Name = "btnEditArgument";
			this.btnEditArgument.Size = new System.Drawing.Size(75, 23);
			this.btnEditArgument.TabIndex = 14;
			this.btnEditArgument.Text = "Edit";
			this.btnEditArgument.UseVisualStyleBackColor = true;
			this.btnEditArgument.Visible = false;
			this.btnEditArgument.Click += new System.EventHandler(this.btnEditArgument_Click);
			// 
			// btnNewArgument
			// 
			this.btnNewArgument.Location = new System.Drawing.Point(164, 241);
			this.btnNewArgument.Name = "btnNewArgument";
			this.btnNewArgument.Size = new System.Drawing.Size(75, 23);
			this.btnNewArgument.TabIndex = 13;
			this.btnNewArgument.Text = "New";
			this.btnNewArgument.UseVisualStyleBackColor = true;
			this.btnNewArgument.Visible = false;
			this.btnNewArgument.Click += new System.EventHandler(this.btnNewArgument_Click);
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Khaki;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(164, 118);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(241, 23);
			this.label5.TabIndex = 11;
			this.label5.Text = "Command Line Arguments";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label5.Visible = false;
			// 
			// listArguments
			// 
			this.listArguments.FormattingEnabled = true;
			this.listArguments.Location = new System.Drawing.Point(164, 140);
			this.listArguments.Name = "listArguments";
			this.listArguments.Size = new System.Drawing.Size(241, 95);
			this.listArguments.TabIndex = 12;
			this.listArguments.Visible = false;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// NewEngineForm
			// 
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(569, 336);
			this.Controls.Add(this.btnRemoveArgument);
			this.Controls.Add(this.btnEditArgument);
			this.Controls.Add(this.btnNewArgument);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listArguments);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnDiscoverEngine);
			this.Controls.Add(this.btnMoreLess);
			this.Controls.Add(this.btnBrowseWorkingDirectory);
			this.Controls.Add(this.txtWorkingDirectory);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnBrowseExecutable);
			this.Controls.Add(this.txtExecutable);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewEngineForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add New Engine";
			this.Load += new System.EventHandler(this.NewEngineForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtExecutable;
		private System.Windows.Forms.Button btnBrowseExecutable;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtWorkingDirectory;
		private System.Windows.Forms.Button btnBrowseWorkingDirectory;
		private System.Windows.Forms.Button btnMoreLess;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnDiscoverEngine;
		private System.Windows.Forms.Button btnRemoveArgument;
		private System.Windows.Forms.Button btnEditArgument;
		private System.Windows.Forms.Button btnNewArgument;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listArguments;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Timer timer;
	}
}