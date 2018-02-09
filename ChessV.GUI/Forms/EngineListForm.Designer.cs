
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
	partial class EngineListForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineListForm));
			this.btnDone = new System.Windows.Forms.Button();
			this.chkAutodetectEngines = new System.Windows.Forms.CheckBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.lvEngineList = new System.Windows.Forms.ListView();
			this.hdrNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hdrInternalNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hdrProtocolColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hdrCommandColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// btnDone
			// 
			this.btnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnDone.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnDone.Location = new System.Drawing.Point(479, 426);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(123, 36);
			this.btnDone.TabIndex = 5;
			this.btnDone.Text = "   &Done";
			this.btnDone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// chkAutodetectEngines
			// 
			this.chkAutodetectEngines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkAutodetectEngines.AutoSize = true;
			this.chkAutodetectEngines.Location = new System.Drawing.Point(12, 393);
			this.chkAutodetectEngines.Name = "chkAutodetectEngines";
			this.chkAutodetectEngines.Size = new System.Drawing.Size(198, 17);
			this.chkAutodetectEngines.TabIndex = 1;
			this.chkAutodetectEngines.Text = "Auto-Detect New Engines at Startup";
			this.chkAutodetectEngines.UseVisualStyleBackColor = true;
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
			this.btnRemove.Location = new System.Drawing.Point(248, 426);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(112, 36);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "   &Remove";
			this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnModify
			// 
			this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
			this.btnModify.Location = new System.Drawing.Point(130, 426);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(112, 36);
			this.btnModify.TabIndex = 3;
			this.btnModify.Text = "  &Modify";
			this.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnModify.UseVisualStyleBackColor = true;
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
			this.btnNew.Location = new System.Drawing.Point(12, 426);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(112, 36);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "    &New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// lvEngineList
			// 
			this.lvEngineList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrNameColumn,
            this.hdrInternalNameColumn,
            this.hdrProtocolColumn,
            this.hdrCommandColumn});
			this.lvEngineList.FullRowSelect = true;
			this.lvEngineList.Location = new System.Drawing.Point(12, 12);
			this.lvEngineList.Name = "lvEngineList";
			this.lvEngineList.Size = new System.Drawing.Size(590, 375);
			this.lvEngineList.TabIndex = 6;
			this.lvEngineList.UseCompatibleStateImageBehavior = false;
			this.lvEngineList.View = System.Windows.Forms.View.Details;
			// 
			// hdrNameColumn
			// 
			this.hdrNameColumn.Text = "Nane";
			this.hdrNameColumn.Width = 90;
			// 
			// hdrInternalNameColumn
			// 
			this.hdrInternalNameColumn.Text = "Internal Name";
			this.hdrInternalNameColumn.Width = 110;
			// 
			// hdrProtocolColumn
			// 
			this.hdrProtocolColumn.Text = "Protocol";
			this.hdrProtocolColumn.Width = 70;
			// 
			// hdrCommandColumn
			// 
			this.hdrCommandColumn.Text = "Command";
			this.hdrCommandColumn.Width = 316;
			// 
			// EngineListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.ClientSize = new System.Drawing.Size(614, 468);
			this.Controls.Add(this.lvEngineList);
			this.Controls.Add(this.chkAutodetectEngines);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnModify);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.btnDone);
			this.Name = "EngineListForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Engines";
			this.Load += new System.EventHandler(this.EngineListForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.CheckBox chkAutodetectEngines;
		private System.Windows.Forms.ListView lvEngineList;
		private System.Windows.Forms.ColumnHeader hdrNameColumn;
		private System.Windows.Forms.ColumnHeader hdrInternalNameColumn;
		private System.Windows.Forms.ColumnHeader hdrProtocolColumn;
		private System.Windows.Forms.ColumnHeader hdrCommandColumn;
	}
}