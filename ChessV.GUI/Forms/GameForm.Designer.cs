
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
	partial class GameForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.boardControl = new ChessV.GUI.BoardControl();
			this.listThinking1 = new System.Windows.Forms.ListView();
			this.headerScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerNodes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.headerPV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.pictFirst = new System.Windows.Forms.PictureBox();
			this.pictPrevious = new System.Windows.Forms.PictureBox();
			this.pictLast = new System.Windows.Forms.PictureBox();
			this.pictNext = new System.Windows.Forms.PictureBox();
			this.pictStop = new System.Windows.Forms.PictureBox();
			this.panelClocks = new System.Windows.Forms.Panel();
			this.labelTime1 = new System.Windows.Forms.Label();
			this.labelTime0 = new System.Windows.Forms.Label();
			this.labelPlayer1 = new System.Windows.Forms.Label();
			this.labelPlayer0 = new System.Windows.Forms.Label();
			this.lblReviewMode = new System.Windows.Forms.Label();
			this.listMoves = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabMaterial = new System.Windows.Forms.TabPage();
			this.mbControl = new ChessV.GUI.MaterialBalanceControl();
			this.tabEvalHistory = new System.Windows.Forms.TabPage();
			this.ehControl = new ChessV.GUI.EvaluationHistoryControl();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_SaveGame = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Game = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_TakeBackMove = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_TakeBackAllMoves = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_ComputerPlays0 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ComputerPlays1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_StopThinking = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_QuickAnalysis = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_MultiPVAnalysis = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_Perft = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_LoadPositionByFEN = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Options = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_Appearance = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_EnableCustomTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_UncheckeredBoard = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_CheckeredBoard = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ThreeColorBoard = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_HighlightComputerMove = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitem_RotateBoard = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Tools = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_ShowEngineDebugWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.menu_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitem_About = new System.Windows.Forms.ToolStripMenuItem();
			this.menuPieceContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuitem_ShowEngineStatisticsWindow = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictFirst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictPrevious)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictLast)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictNext)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictStop)).BeginInit();
			this.panelClocks.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabMaterial.SuspendLayout();
			this.tabEvalHistory.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.menuPieceContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.BlanchedAlmond;
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(924, 718);
			this.splitContainer1.SplitterDistance = 499;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.boardControl);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Black;
			this.splitContainer2.Panel2.Controls.Add(this.listThinking1);
			this.splitContainer2.Size = new System.Drawing.Size(499, 718);
			this.splitContainer2.SplitterDistance = 498;
			this.splitContainer2.TabIndex = 0;
			// 
			// boardControl
			// 
			this.boardControl.HighlightMove = false;
			this.boardControl.Location = new System.Drawing.Point(0, 0);
			this.boardControl.Name = "boardControl";
			this.boardControl.RotateBoard = false;
			this.boardControl.Size = new System.Drawing.Size(496, 496);
			this.boardControl.TabIndex = 0;
			// 
			// listThinking1
			// 
			this.listThinking1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
			this.listThinking1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerScore,
            this.headerTime,
            this.headerNodes,
            this.headerPV});
			this.listThinking1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listThinking1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listThinking1.Location = new System.Drawing.Point(0, 0);
			this.listThinking1.Name = "listThinking1";
			this.listThinking1.Size = new System.Drawing.Size(495, 212);
			this.listThinking1.TabIndex = 0;
			this.listThinking1.UseCompatibleStateImageBehavior = false;
			this.listThinking1.View = System.Windows.Forms.View.Details;
			// 
			// headerScore
			// 
			this.headerScore.Text = "Score";
			this.headerScore.Width = 80;
			// 
			// headerTime
			// 
			this.headerTime.Text = "Time";
			this.headerTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.headerTime.Width = 70;
			// 
			// headerNodes
			// 
			this.headerNodes.Text = "Nodes";
			this.headerNodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.headerNodes.Width = 70;
			// 
			// headerPV
			// 
			this.headerPV.Text = "PV";
			this.headerPV.Width = 400;
			// 
			// splitContainer3
			// 
			this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer3.Panel1.Controls.Add(this.pictFirst);
			this.splitContainer3.Panel1.Controls.Add(this.pictPrevious);
			this.splitContainer3.Panel1.Controls.Add(this.pictLast);
			this.splitContainer3.Panel1.Controls.Add(this.pictNext);
			this.splitContainer3.Panel1.Controls.Add(this.pictStop);
			this.splitContainer3.Panel1.Controls.Add(this.panelClocks);
			this.splitContainer3.Panel1.Controls.Add(this.lblReviewMode);
			this.splitContainer3.Panel1.Controls.Add(this.listMoves);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
			this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer3.Size = new System.Drawing.Size(421, 718);
			this.splitContainer3.SplitterDistance = 563;
			this.splitContainer3.TabIndex = 0;
			// 
			// pictFirst
			// 
			this.pictFirst.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pictFirst.Image = global::ChessV.GUI.Properties.Resources.icon_gray_first;
			this.pictFirst.Location = new System.Drawing.Point(118, 531);
			this.pictFirst.Name = "pictFirst";
			this.pictFirst.Size = new System.Drawing.Size(31, 25);
			this.pictFirst.TabIndex = 7;
			this.pictFirst.TabStop = false;
			this.pictFirst.Click += new System.EventHandler(this.pictFirst_Click);
			// 
			// pictPrevious
			// 
			this.pictPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pictPrevious.Image = global::ChessV.GUI.Properties.Resources.icon_gray_previous;
			this.pictPrevious.Location = new System.Drawing.Point(159, 531);
			this.pictPrevious.Name = "pictPrevious";
			this.pictPrevious.Size = new System.Drawing.Size(25, 25);
			this.pictPrevious.TabIndex = 6;
			this.pictPrevious.TabStop = false;
			this.pictPrevious.Click += new System.EventHandler(this.pictPrevious_Click);
			this.pictPrevious.DoubleClick += new System.EventHandler(this.pictPrevious_Click);
			// 
			// pictLast
			// 
			this.pictLast.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pictLast.Image = global::ChessV.GUI.Properties.Resources.icon_gray_last;
			this.pictLast.Location = new System.Drawing.Point(266, 531);
			this.pictLast.Name = "pictLast";
			this.pictLast.Size = new System.Drawing.Size(31, 25);
			this.pictLast.TabIndex = 5;
			this.pictLast.TabStop = false;
			this.pictLast.Click += new System.EventHandler(this.pictLast_Click);
			// 
			// pictNext
			// 
			this.pictNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pictNext.Image = global::ChessV.GUI.Properties.Resources.icon_gray_next;
			this.pictNext.Location = new System.Drawing.Point(231, 531);
			this.pictNext.Name = "pictNext";
			this.pictNext.Size = new System.Drawing.Size(25, 25);
			this.pictNext.TabIndex = 4;
			this.pictNext.TabStop = false;
			this.pictNext.Click += new System.EventHandler(this.pictNext_Click);
			this.pictNext.DoubleClick += new System.EventHandler(this.pictNext_Click);
			// 
			// pictStop
			// 
			this.pictStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pictStop.Image = global::ChessV.GUI.Properties.Resources.icon_gray_stop;
			this.pictStop.ImageLocation = "";
			this.pictStop.Location = new System.Drawing.Point(195, 531);
			this.pictStop.Name = "pictStop";
			this.pictStop.Size = new System.Drawing.Size(24, 24);
			this.pictStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictStop.TabIndex = 3;
			this.pictStop.TabStop = false;
			this.pictStop.Click += new System.EventHandler(this.pictStop_Click);
			// 
			// panelClocks
			// 
			this.panelClocks.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panelClocks.BackColor = System.Drawing.Color.Transparent;
			this.panelClocks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panelClocks.Controls.Add(this.labelTime1);
			this.panelClocks.Controls.Add(this.labelTime0);
			this.panelClocks.Controls.Add(this.labelPlayer1);
			this.panelClocks.Controls.Add(this.labelPlayer0);
			this.panelClocks.Location = new System.Drawing.Point(4, 3);
			this.panelClocks.Name = "panelClocks";
			this.panelClocks.Size = new System.Drawing.Size(409, 74);
			this.panelClocks.TabIndex = 1;
			// 
			// labelTime1
			// 
			this.labelTime1.BackColor = System.Drawing.Color.Black;
			this.labelTime1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelTime1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTime1.ForeColor = System.Drawing.Color.White;
			this.labelTime1.Location = new System.Drawing.Point(206, 21);
			this.labelTime1.Name = "labelTime1";
			this.labelTime1.Size = new System.Drawing.Size(91, 35);
			this.labelTime1.TabIndex = 3;
			this.labelTime1.Text = "0:00";
			this.labelTime1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTime0
			// 
			this.labelTime0.BackColor = System.Drawing.Color.White;
			this.labelTime0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelTime0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTime0.ForeColor = System.Drawing.Color.Black;
			this.labelTime0.Location = new System.Drawing.Point(109, 21);
			this.labelTime0.Name = "labelTime0";
			this.labelTime0.Size = new System.Drawing.Size(91, 35);
			this.labelTime0.TabIndex = 2;
			this.labelTime0.Text = "0:00";
			this.labelTime0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelPlayer1
			// 
			this.labelPlayer1.BackColor = System.Drawing.Color.Transparent;
			this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayer1.Location = new System.Drawing.Point(303, 12);
			this.labelPlayer1.Name = "labelPlayer1";
			this.labelPlayer1.Size = new System.Drawing.Size(100, 50);
			this.labelPlayer1.TabIndex = 1;
			this.labelPlayer1.Text = "ChessV";
			this.labelPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelPlayer0
			// 
			this.labelPlayer0.BackColor = System.Drawing.Color.Transparent;
			this.labelPlayer0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayer0.Location = new System.Drawing.Point(3, 12);
			this.labelPlayer0.Name = "labelPlayer0";
			this.labelPlayer0.Size = new System.Drawing.Size(100, 50);
			this.labelPlayer0.TabIndex = 0;
			this.labelPlayer0.Text = "Player";
			this.labelPlayer0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblReviewMode
			// 
			this.lblReviewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblReviewMode.Location = new System.Drawing.Point(327, 534);
			this.lblReviewMode.Name = "lblReviewMode";
			this.lblReviewMode.Size = new System.Drawing.Size(85, 20);
			this.lblReviewMode.TabIndex = 2;
			this.lblReviewMode.Text = "Review Mode";
			this.lblReviewMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblReviewMode.Visible = false;
			// 
			// listMoves
			// 
			this.listMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listMoves.BackColor = System.Drawing.Color.LemonChiffon;
			this.listMoves.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.listMoves.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listMoves.FullRowSelect = true;
			this.listMoves.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listMoves.Location = new System.Drawing.Point(-2, 83);
			this.listMoves.Name = "listMoves";
			this.listMoves.Size = new System.Drawing.Size(421, 445);
			this.listMoves.TabIndex = 0;
			this.listMoves.UseCompatibleStateImageBehavior = false;
			this.listMoves.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Move No";
			this.columnHeader1.Width = 55;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Move";
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Description";
			this.columnHeader3.Width = 157;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabMaterial);
			this.tabControl1.Controls.Add(this.tabEvalHistory);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(417, 147);
			this.tabControl1.TabIndex = 0;
			// 
			// tabMaterial
			// 
			this.tabMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
			this.tabMaterial.Controls.Add(this.mbControl);
			this.tabMaterial.Location = new System.Drawing.Point(4, 22);
			this.tabMaterial.Name = "tabMaterial";
			this.tabMaterial.Padding = new System.Windows.Forms.Padding(3);
			this.tabMaterial.Size = new System.Drawing.Size(409, 121);
			this.tabMaterial.TabIndex = 0;
			this.tabMaterial.Text = "Material Balance";
			// 
			// mbControl
			// 
			this.mbControl.BackColor = System.Drawing.Color.LemonChiffon;
			this.mbControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mbControl.Location = new System.Drawing.Point(3, 3);
			this.mbControl.Name = "mbControl";
			this.mbControl.Size = new System.Drawing.Size(403, 115);
			this.mbControl.TabIndex = 0;
			this.mbControl.Theme = null;
			// 
			// tabEvalHistory
			// 
			this.tabEvalHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
			this.tabEvalHistory.Controls.Add(this.ehControl);
			this.tabEvalHistory.Location = new System.Drawing.Point(4, 22);
			this.tabEvalHistory.Name = "tabEvalHistory";
			this.tabEvalHistory.Padding = new System.Windows.Forms.Padding(3);
			this.tabEvalHistory.Size = new System.Drawing.Size(405, 121);
			this.tabEvalHistory.TabIndex = 1;
			this.tabEvalHistory.Text = "Evaluation History";
			// 
			// ehControl
			// 
			this.ehControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ehControl.Location = new System.Drawing.Point(3, 3);
			this.ehControl.Name = "ehControl";
			this.ehControl.Size = new System.Drawing.Size(399, 115);
			this.ehControl.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Game,
            this.menu_Options,
            this.menu_Tools,
            this.menu_Help});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(924, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menu_File
			// 
			this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_SaveGame,
            this.menuitem_Exit});
			this.menu_File.Name = "menu_File";
			this.menu_File.Size = new System.Drawing.Size(37, 20);
			this.menu_File.Text = "File";
			// 
			// menuitem_SaveGame
			// 
			this.menuitem_SaveGame.Name = "menuitem_SaveGame";
			this.menuitem_SaveGame.Size = new System.Drawing.Size(132, 22);
			this.menuitem_SaveGame.Text = "Save Game";
			this.menuitem_SaveGame.Click += new System.EventHandler(this.menuitem_SaveGame_Click);
			// 
			// menuitem_Exit
			// 
			this.menuitem_Exit.Name = "menuitem_Exit";
			this.menuitem_Exit.Size = new System.Drawing.Size(132, 22);
			this.menuitem_Exit.Text = "Exit";
			this.menuitem_Exit.Click += new System.EventHandler(this.menuitem_Exit_Click);
			// 
			// menu_Game
			// 
			this.menu_Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_TakeBackMove,
            this.menuitem_TakeBackAllMoves,
            this.toolStripSeparator1,
            this.menuitem_ComputerPlays0,
            this.menuitem_ComputerPlays1,
            this.menuitem_StopThinking,
            this.toolStripSeparator4,
            this.menuitem_QuickAnalysis,
            this.menuitem_MultiPVAnalysis,
            this.menuitem_Perft,
            this.toolStripSeparator6,
            this.menuitem_LoadPositionByFEN});
			this.menu_Game.Name = "menu_Game";
			this.menu_Game.Size = new System.Drawing.Size(50, 20);
			this.menu_Game.Text = "Game";
			this.menu_Game.DropDownOpening += new System.EventHandler(this.menu_Game_DropDownOpening);
			// 
			// menuitem_TakeBackMove
			// 
			this.menuitem_TakeBackMove.Enabled = false;
			this.menuitem_TakeBackMove.Name = "menuitem_TakeBackMove";
			this.menuitem_TakeBackMove.Size = new System.Drawing.Size(186, 22);
			this.menuitem_TakeBackMove.Text = "Take Back Move";
			this.menuitem_TakeBackMove.Click += new System.EventHandler(this.menuitem_TakeBackMove_Click);
			// 
			// menuitem_TakeBackAllMoves
			// 
			this.menuitem_TakeBackAllMoves.Enabled = false;
			this.menuitem_TakeBackAllMoves.Name = "menuitem_TakeBackAllMoves";
			this.menuitem_TakeBackAllMoves.Size = new System.Drawing.Size(186, 22);
			this.menuitem_TakeBackAllMoves.Text = "Take Back All Moves";
			this.menuitem_TakeBackAllMoves.Click += new System.EventHandler(this.menuitem_TakeBackAllMoves_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
			// 
			// menuitem_ComputerPlays0
			// 
			this.menuitem_ComputerPlays0.Enabled = false;
			this.menuitem_ComputerPlays0.Name = "menuitem_ComputerPlays0";
			this.menuitem_ComputerPlays0.Size = new System.Drawing.Size(186, 22);
			this.menuitem_ComputerPlays0.Text = "Computer Plays 0";
			this.menuitem_ComputerPlays0.Click += new System.EventHandler(this.menuitem_ComputerPlays0_Click);
			// 
			// menuitem_ComputerPlays1
			// 
			this.menuitem_ComputerPlays1.Enabled = false;
			this.menuitem_ComputerPlays1.Name = "menuitem_ComputerPlays1";
			this.menuitem_ComputerPlays1.Size = new System.Drawing.Size(186, 22);
			this.menuitem_ComputerPlays1.Text = "Computer Plays 1";
			this.menuitem_ComputerPlays1.Click += new System.EventHandler(this.menuitem_ComputerPlays1_Click);
			// 
			// menuitem_StopThinking
			// 
			this.menuitem_StopThinking.Name = "menuitem_StopThinking";
			this.menuitem_StopThinking.Size = new System.Drawing.Size(186, 22);
			this.menuitem_StopThinking.Text = "Stop Thinking";
			this.menuitem_StopThinking.Click += new System.EventHandler(this.menuitem_StopThinking_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(183, 6);
			// 
			// menuitem_QuickAnalysis
			// 
			this.menuitem_QuickAnalysis.Name = "menuitem_QuickAnalysis";
			this.menuitem_QuickAnalysis.Size = new System.Drawing.Size(186, 22);
			this.menuitem_QuickAnalysis.Text = "Quick Analysis";
			this.menuitem_QuickAnalysis.Click += new System.EventHandler(this.quickAnalysisToolStripMenuItem_Click);
			// 
			// menuitem_MultiPVAnalysis
			// 
			this.menuitem_MultiPVAnalysis.Name = "menuitem_MultiPVAnalysis";
			this.menuitem_MultiPVAnalysis.Size = new System.Drawing.Size(186, 22);
			this.menuitem_MultiPVAnalysis.Text = "Multi-PV Analysis ...";
			this.menuitem_MultiPVAnalysis.Click += new System.EventHandler(this.menuitem_MultiPVAnalysis_Click);
			// 
			// menuitem_Perft
			// 
			this.menuitem_Perft.Name = "menuitem_Perft";
			this.menuitem_Perft.Size = new System.Drawing.Size(186, 22);
			this.menuitem_Perft.Text = "Perft";
			this.menuitem_Perft.Click += new System.EventHandler(this.perftToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(183, 6);
			// 
			// menuitem_LoadPositionByFEN
			// 
			this.menuitem_LoadPositionByFEN.Name = "menuitem_LoadPositionByFEN";
			this.menuitem_LoadPositionByFEN.Size = new System.Drawing.Size(186, 22);
			this.menuitem_LoadPositionByFEN.Text = "Load Position by FEN";
			this.menuitem_LoadPositionByFEN.Click += new System.EventHandler(this.menuitem_LoadPositionByFEN_Click);
			// 
			// menu_Options
			// 
			this.menu_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_Appearance,
            this.menuitem_EnableCustomTheme,
            this.toolStripSeparator2,
            this.menuitem_UncheckeredBoard,
            this.menuitem_CheckeredBoard,
            this.menuitem_ThreeColorBoard,
            this.toolStripSeparator5,
            this.menuitem_HighlightComputerMove,
            this.toolStripSeparator3,
            this.menuitem_RotateBoard});
			this.menu_Options.Name = "menu_Options";
			this.menu_Options.Size = new System.Drawing.Size(61, 20);
			this.menu_Options.Text = "Options";
			this.menu_Options.DropDownOpening += new System.EventHandler(this.optionsToolStripMenuItem_DropDownOpening);
			// 
			// menuitem_Appearance
			// 
			this.menuitem_Appearance.Name = "menuitem_Appearance";
			this.menuitem_Appearance.Size = new System.Drawing.Size(214, 22);
			this.menuitem_Appearance.Text = "Appearance";
			this.menuitem_Appearance.Click += new System.EventHandler(this.appearanceToolStripMenuItem_Click);
			// 
			// menuitem_EnableCustomTheme
			// 
			this.menuitem_EnableCustomTheme.Name = "menuitem_EnableCustomTheme";
			this.menuitem_EnableCustomTheme.Size = new System.Drawing.Size(214, 22);
			this.menuitem_EnableCustomTheme.Text = "Enable Custom Theme";
			this.menuitem_EnableCustomTheme.Click += new System.EventHandler(this.menuitem_EnableCustomTheme_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
			// 
			// menuitem_UncheckeredBoard
			// 
			this.menuitem_UncheckeredBoard.Name = "menuitem_UncheckeredBoard";
			this.menuitem_UncheckeredBoard.Size = new System.Drawing.Size(214, 22);
			this.menuitem_UncheckeredBoard.Text = "Uncheckered Board";
			this.menuitem_UncheckeredBoard.Click += new System.EventHandler(this.menuitem_UncheckeredBoard_Click);
			// 
			// menuitem_CheckeredBoard
			// 
			this.menuitem_CheckeredBoard.Name = "menuitem_CheckeredBoard";
			this.menuitem_CheckeredBoard.Size = new System.Drawing.Size(214, 22);
			this.menuitem_CheckeredBoard.Text = "Checkered Board";
			this.menuitem_CheckeredBoard.Click += new System.EventHandler(this.menuitem_CheckeredBoard_Click);
			// 
			// menuitem_ThreeColorBoard
			// 
			this.menuitem_ThreeColorBoard.Name = "menuitem_ThreeColorBoard";
			this.menuitem_ThreeColorBoard.Size = new System.Drawing.Size(214, 22);
			this.menuitem_ThreeColorBoard.Text = "Three-Color Board";
			this.menuitem_ThreeColorBoard.Click += new System.EventHandler(this.menuitem_ThreeColorBoard_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
			// 
			// menuitem_HighlightComputerMove
			// 
			this.menuitem_HighlightComputerMove.Name = "menuitem_HighlightComputerMove";
			this.menuitem_HighlightComputerMove.Size = new System.Drawing.Size(214, 22);
			this.menuitem_HighlightComputerMove.Text = "Highlight Computer Move";
			this.menuitem_HighlightComputerMove.Click += new System.EventHandler(this.menuitem_HighlightComputerMove_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
			// 
			// menuitem_RotateBoard
			// 
			this.menuitem_RotateBoard.Name = "menuitem_RotateBoard";
			this.menuitem_RotateBoard.Size = new System.Drawing.Size(214, 22);
			this.menuitem_RotateBoard.Text = "Rotate Board";
			this.menuitem_RotateBoard.Click += new System.EventHandler(this.menuitem_RotateBoard_Click);
			// 
			// menu_Tools
			// 
			this.menu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_ShowEngineDebugWindow,
            this.menuitem_ShowEngineStatisticsWindow});
			this.menu_Tools.Name = "menu_Tools";
			this.menu_Tools.Size = new System.Drawing.Size(48, 20);
			this.menu_Tools.Text = "Tools";
			this.menu_Tools.DropDownOpening += new System.EventHandler(this.menu_Tools_DropDownOpening);
			// 
			// menuitem_ShowEngineDebugWindow
			// 
			this.menuitem_ShowEngineDebugWindow.Name = "menuitem_ShowEngineDebugWindow";
			this.menuitem_ShowEngineDebugWindow.Size = new System.Drawing.Size(238, 22);
			this.menuitem_ShowEngineDebugWindow.Text = "Show Engine Debug Window";
			this.menuitem_ShowEngineDebugWindow.Click += new System.EventHandler(this.menuitem_ShowEngineDebugWindow_Click);
			// 
			// menu_Help
			// 
			this.menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_About});
			this.menu_Help.Name = "menu_Help";
			this.menu_Help.Size = new System.Drawing.Size(44, 20);
			this.menu_Help.Text = "Help";
			// 
			// menuitem_About
			// 
			this.menuitem_About.Name = "menuitem_About";
			this.menuitem_About.Size = new System.Drawing.Size(148, 22);
			this.menuitem_About.Text = "About ChessV";
			this.menuitem_About.Click += new System.EventHandler(this.menuitem_About_Click);
			// 
			// menuPieceContext
			// 
			this.menuPieceContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
			this.menuPieceContext.Name = "menuPieceContext";
			this.menuPieceContext.Size = new System.Drawing.Size(128, 26);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// timer
			// 
			this.timer.Interval = 400;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// menuitem_ShowEngineStatisticsWindow
			// 
			this.menuitem_ShowEngineStatisticsWindow.Name = "menuitem_ShowEngineStatisticsWindow";
			this.menuitem_ShowEngineStatisticsWindow.Size = new System.Drawing.Size(238, 22);
			this.menuitem_ShowEngineStatisticsWindow.Text = "Show Engine Statistics Window";
			this.menuitem_ShowEngineStatisticsWindow.Click += new System.EventHandler(this.menuitem_ShowEngineStatisticsWindow_Click);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(924, 742);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "GameForm";
			this.Text = "Chess";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
			this.Load += new System.EventHandler(this.GameForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictFirst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictPrevious)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictLast)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictNext)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictStop)).EndInit();
			this.panelClocks.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabMaterial.ResumeLayout(false);
			this.tabEvalHistory.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.menuPieceContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private BoardControl boardControl;
		private MaterialBalanceControl mbControl;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menu_File;
		private System.Windows.Forms.ToolStripMenuItem menu_Game;
		private System.Windows.Forms.ToolStripMenuItem menu_Options;
		private System.Windows.Forms.ToolStripMenuItem menu_Help;
		private System.Windows.Forms.ListView listThinking1;
		private System.Windows.Forms.ColumnHeader headerScore;
		private System.Windows.Forms.ColumnHeader headerTime;
		private System.Windows.Forms.ColumnHeader headerNodes;
		private System.Windows.Forms.ColumnHeader headerPV;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabMaterial;
		private System.Windows.Forms.TabPage tabEvalHistory;
		private System.Windows.Forms.ListView listMoves;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ToolStripMenuItem menuitem_QuickAnalysis;
		private System.Windows.Forms.ToolStripMenuItem menuitem_Perft;
		private System.Windows.Forms.ToolStripMenuItem menuitem_Appearance;
		private System.Windows.Forms.ToolStripMenuItem menuitem_SaveGame;
		private System.Windows.Forms.ToolStripMenuItem menuitem_Exit;
		private System.Windows.Forms.ToolStripMenuItem menuitem_TakeBackMove;
		private System.Windows.Forms.ToolStripMenuItem menuitem_TakeBackAllMoves;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuitem_RotateBoard;
		private System.Windows.Forms.ToolStripMenuItem menuitem_About;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menuitem_UncheckeredBoard;
		private System.Windows.Forms.ToolStripMenuItem menuitem_CheckeredBoard;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ThreeColorBoard;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ComputerPlays0;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ComputerPlays1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuitem_HighlightComputerMove;
		private System.Windows.Forms.Panel panelClocks;
		private System.Windows.Forms.Label labelTime1;
		private System.Windows.Forms.Label labelTime0;
		private System.Windows.Forms.Label labelPlayer1;
		private System.Windows.Forms.Label labelPlayer0;
		private System.Windows.Forms.ContextMenuStrip menuPieceContext;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menu_Tools;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ShowEngineDebugWindow;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private EvaluationHistoryControl ehControl;
		private System.Windows.Forms.ToolStripMenuItem menuitem_EnableCustomTheme;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem menuitem_LoadPositionByFEN;
		private System.Windows.Forms.ToolStripMenuItem menuitem_StopThinking;
		private System.Windows.Forms.Label lblReviewMode;
		private System.Windows.Forms.PictureBox pictStop;
		private System.Windows.Forms.PictureBox pictNext;
		private System.Windows.Forms.PictureBox pictLast;
		private System.Windows.Forms.PictureBox pictFirst;
		private System.Windows.Forms.PictureBox pictPrevious;
		private System.Windows.Forms.ToolStripMenuItem menuitem_MultiPVAnalysis;
		private System.Windows.Forms.ToolStripMenuItem menuitem_ShowEngineStatisticsWindow;
	}
}