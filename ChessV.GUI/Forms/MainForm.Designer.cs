
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
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.tabIndexPage = new System.Windows.Forms.TabPage();
			this.lvMasterIndex = new System.Windows.Forms.ListView();
			this.colGameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colBoard = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colInvented = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colInventor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelGameIndexHeader = new System.Windows.Forms.Panel();
			this.btnFilterIndex = new System.Windows.Forms.Button();
			this.lblMasterGameIndex = new System.Windows.Forms.Label();
			this.pictGamesIcon = new System.Windows.Forms.PictureBox();
			this.panelSubGames = new System.Windows.Forms.Panel();
			this.panelShuffledVariants = new System.Windows.Forms.Panel();
			this.label22 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.btnRandom = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnStartGame = new System.Windows.Forms.Button();
			this.lblRuleNote = new System.Windows.Forms.Label();
			this.pictSubGamePreview = new System.Windows.Forms.PictureBox();
			this.label8 = new System.Windows.Forms.Label();
			this.lblSubGameCategory = new System.Windows.Forms.Label();
			this.panelCapablancaVariants = new System.Windows.Forms.Panel();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblGameName = new System.Windows.Forms.Label();
			this.lvGames = new System.Windows.Forms.ListView();
			this.lvNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvInventorColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvInventedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.largeImageList = new System.Windows.Forms.ImageList(this.components);
			this.btnAbout = new System.Windows.Forms.Button();
			this.startTimer = new System.Windows.Forms.Timer(this.components);
			this.btnQuit = new System.Windows.Forms.Button();
			this.btnEngines = new System.Windows.Forms.Button();
			this.btnLoadGame = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.mainTabControl.SuspendLayout();
			this.tabIndexPage.SuspendLayout();
			this.panelGameIndexHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictGamesIcon)).BeginInit();
			this.panelSubGames.SuspendLayout();
			this.panelShuffledVariants.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictSubGamePreview)).BeginInit();
			this.panelCapablancaVariants.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(45, 231);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(296, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Orthodox Chess";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.Visible = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(45, 246);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(296, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "and Variants";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Visible = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(397, 246);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(296, 19);
			this.label3.TabIndex = 5;
			this.label3.Text = "and Variants";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label3.Visible = false;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(397, 231);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(296, 19);
			this.label4.TabIndex = 4;
			this.label4.Text = "Capablanca Chess";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label4.Visible = false;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(397, 532);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(296, 19);
			this.label6.TabIndex = 7;
			this.label6.Text = "Grand Chess";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label6.Visible = false;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(45, 510);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(296, 19);
			this.label7.TabIndex = 9;
			this.label7.Text = "Shatranj";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label7.Visible = false;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(45, 529);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(296, 19);
			this.label5.TabIndex = 10;
			this.label5.Text = "and Variants";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label5.Visible = false;
			// 
			// mainTabControl
			// 
			this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainTabControl.Controls.Add(this.tabIndexPage);
			this.mainTabControl.Location = new System.Drawing.Point(12, 12);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(1094, 572);
			this.mainTabControl.TabIndex = 14;
			// 
			// tabIndexPage
			// 
			this.tabIndexPage.Controls.Add(this.lvMasterIndex);
			this.tabIndexPage.Controls.Add(this.panelGameIndexHeader);
			this.tabIndexPage.Location = new System.Drawing.Point(4, 22);
			this.tabIndexPage.Name = "tabIndexPage";
			this.tabIndexPage.Size = new System.Drawing.Size(1086, 546);
			this.tabIndexPage.TabIndex = 0;
			this.tabIndexPage.Text = "Index";
			this.tabIndexPage.UseVisualStyleBackColor = true;
			// 
			// lvMasterIndex
			// 
			this.lvMasterIndex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGameName,
            this.colBoard,
            this.colInvented,
            this.colInventor});
			this.lvMasterIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvMasterIndex.FullRowSelect = true;
			this.lvMasterIndex.Location = new System.Drawing.Point(12, 77);
			this.lvMasterIndex.Name = "lvMasterIndex";
			this.lvMasterIndex.Size = new System.Drawing.Size(1057, 455);
			this.lvMasterIndex.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvMasterIndex.TabIndex = 2;
			this.lvMasterIndex.UseCompatibleStateImageBehavior = false;
			this.lvMasterIndex.View = System.Windows.Forms.View.Details;
			this.lvMasterIndex.DoubleClick += new System.EventHandler(this.lvMasterIndex_DoubleClick);
			// 
			// colGameName
			// 
			this.colGameName.Text = "Game";
			this.colGameName.Width = 250;
			// 
			// colBoard
			// 
			this.colBoard.Text = "Board";
			this.colBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colBoard.Width = 100;
			// 
			// colInvented
			// 
			this.colInvented.Text = "Invented";
			this.colInvented.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colInvented.Width = 120;
			// 
			// colInventor
			// 
			this.colInventor.Text = "Inventor";
			this.colInventor.Width = 400;
			// 
			// panelGameIndexHeader
			// 
			this.panelGameIndexHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(170)))));
			this.panelGameIndexHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelGameIndexHeader.Controls.Add(this.btnFilterIndex);
			this.panelGameIndexHeader.Controls.Add(this.lblMasterGameIndex);
			this.panelGameIndexHeader.Controls.Add(this.pictGamesIcon);
			this.panelGameIndexHeader.Location = new System.Drawing.Point(12, 11);
			this.panelGameIndexHeader.Name = "panelGameIndexHeader";
			this.panelGameIndexHeader.Size = new System.Drawing.Size(1057, 68);
			this.panelGameIndexHeader.TabIndex = 1;
			// 
			// btnFilterIndex
			// 
			this.btnFilterIndex.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnFilterIndex.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterIndex.Image")));
			this.btnFilterIndex.Location = new System.Drawing.Point(982, 5);
			this.btnFilterIndex.Name = "btnFilterIndex";
			this.btnFilterIndex.Size = new System.Drawing.Size(56, 56);
			this.btnFilterIndex.TabIndex = 2;
			this.btnFilterIndex.Text = "Filter";
			this.btnFilterIndex.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFilterIndex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnFilterIndex.UseVisualStyleBackColor = true;
			this.btnFilterIndex.Click += new System.EventHandler(this.btnFilterIndex_Click);
			// 
			// lblMasterGameIndex
			// 
			this.lblMasterGameIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMasterGameIndex.Location = new System.Drawing.Point(100, 1);
			this.lblMasterGameIndex.Name = "lblMasterGameIndex";
			this.lblMasterGameIndex.Size = new System.Drawing.Size(578, 64);
			this.lblMasterGameIndex.TabIndex = 1;
			this.lblMasterGameIndex.Text = "Master Game Index";
			this.lblMasterGameIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictGamesIcon
			// 
			this.pictGamesIcon.Image = global::ChessV.GUI.Properties.Resources.icon_games;
			this.pictGamesIcon.Location = new System.Drawing.Point(14, 1);
			this.pictGamesIcon.Name = "pictGamesIcon";
			this.pictGamesIcon.Size = new System.Drawing.Size(64, 64);
			this.pictGamesIcon.TabIndex = 0;
			this.pictGamesIcon.TabStop = false;
			// 
			// panelSubGames
			// 
			this.panelSubGames.BackColor = System.Drawing.Color.Beige;
			this.panelSubGames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelSubGames.Controls.Add(this.panelShuffledVariants);
			this.panelSubGames.Controls.Add(this.btnRandom);
			this.panelSubGames.Controls.Add(this.btnBack);
			this.panelSubGames.Controls.Add(this.btnStartGame);
			this.panelSubGames.Controls.Add(this.lblRuleNote);
			this.panelSubGames.Controls.Add(this.pictSubGamePreview);
			this.panelSubGames.Controls.Add(this.label8);
			this.panelSubGames.Controls.Add(this.lblSubGameCategory);
			this.panelSubGames.Controls.Add(this.panelCapablancaVariants);
			this.panelSubGames.Controls.Add(this.lblGameName);
			this.panelSubGames.Controls.Add(this.lvGames);
			this.panelSubGames.Location = new System.Drawing.Point(12, 32);
			this.panelSubGames.Name = "panelSubGames";
			this.panelSubGames.Size = new System.Drawing.Size(1092, 552);
			this.panelSubGames.TabIndex = 15;
			this.panelSubGames.Visible = false;
			// 
			// panelShuffledVariants
			// 
			this.panelShuffledVariants.Controls.Add(this.label22);
			this.panelShuffledVariants.Controls.Add(this.label21);
			this.panelShuffledVariants.Controls.Add(this.label20);
			this.panelShuffledVariants.Controls.Add(this.label19);
			this.panelShuffledVariants.Controls.Add(this.label18);
			this.panelShuffledVariants.Location = new System.Drawing.Point(517, 47);
			this.panelShuffledVariants.Name = "panelShuffledVariants";
			this.panelShuffledVariants.Size = new System.Drawing.Size(451, 166);
			this.panelShuffledVariants.TabIndex = 10;
			this.panelShuffledVariants.Visible = false;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(100, 123);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(251, 13);
			this.label22.TabIndex = 4;
			this.label22.Text = "or requiring too much time memorizing opening lines.";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(69, 105);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(313, 13);
			this.label21.TabIndex = 3;
			this.label21.Text = "been too thoroughly studied, reducing the enjoyment of the game";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(35, 87);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(380, 13);
			this.label20.TabIndex = 2;
			this.label20.Text = "These games are popular with many players that feel that Chess openings have";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(88, 39);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(275, 13);
			this.label19.TabIndex = 1;
			this.label19.Text = "the initial locations of pieces according to various criteria.";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(52, 21);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(346, 13);
			this.label18.TabIndex = 0;
			this.label18.Text = "This is a family of variants that differ from orthodox Chess by randomizing";
			// 
			// btnRandom
			// 
			this.btnRandom.Image = global::ChessV.GUI.Properties.Resources.icon_random;
			this.btnRandom.Location = new System.Drawing.Point(825, 497);
			this.btnRandom.Name = "btnRandom";
			this.btnRandom.Size = new System.Drawing.Size(112, 36);
			this.btnRandom.TabIndex = 9;
			this.btnRandom.Text = "   &Random";
			this.btnRandom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRandom.UseVisualStyleBackColor = true;
			this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
			// 
			// btnBack
			// 
			this.btnBack.Image = global::ChessV.GUI.Properties.Resources.icon_back;
			this.btnBack.Location = new System.Drawing.Point(557, 497);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(112, 36);
			this.btnBack.TabIndex = 8;
			this.btnBack.Text = "    &Back";
			this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnStartGame
			// 
			this.btnStartGame.BackColor = System.Drawing.Color.Beige;
			this.btnStartGame.Image = global::ChessV.GUI.Properties.Resources.icon_start;
			this.btnStartGame.Location = new System.Drawing.Point(690, 497);
			this.btnStartGame.Name = "btnStartGame";
			this.btnStartGame.Size = new System.Drawing.Size(112, 36);
			this.btnStartGame.TabIndex = 7;
			this.btnStartGame.Text = "  &Start Game";
			this.btnStartGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnStartGame.UseVisualStyleBackColor = true;
			this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
			// 
			// lblRuleNote
			// 
			this.lblRuleNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRuleNote.Location = new System.Drawing.Point(514, 461);
			this.lblRuleNote.Name = "lblRuleNote";
			this.lblRuleNote.Size = new System.Drawing.Size(454, 23);
			this.lblRuleNote.TabIndex = 6;
			this.lblRuleNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictSubGamePreview
			// 
			this.pictSubGamePreview.Location = new System.Drawing.Point(517, 254);
			this.pictSubGamePreview.Name = "pictSubGamePreview";
			this.pictSubGamePreview.Size = new System.Drawing.Size(451, 206);
			this.pictSubGamePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictSubGamePreview.TabIndex = 5;
			this.pictSubGamePreview.TabStop = false;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(170)))));
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(15, 52);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(384, 23);
			this.label8.TabIndex = 4;
			this.label8.Text = "Games";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSubGameCategory
			// 
			this.lblSubGameCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(170)))));
			this.lblSubGameCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSubGameCategory.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSubGameCategory.Location = new System.Drawing.Point(-1, -1);
			this.lblSubGameCategory.Name = "lblSubGameCategory";
			this.lblSubGameCategory.Size = new System.Drawing.Size(1092, 31);
			this.lblSubGameCategory.TabIndex = 3;
			this.lblSubGameCategory.Text = "label8";
			this.lblSubGameCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelCapablancaVariants
			// 
			this.panelCapablancaVariants.BackColor = System.Drawing.Color.Transparent;
			this.panelCapablancaVariants.Controls.Add(this.label17);
			this.panelCapablancaVariants.Controls.Add(this.label16);
			this.panelCapablancaVariants.Controls.Add(this.label15);
			this.panelCapablancaVariants.Controls.Add(this.label14);
			this.panelCapablancaVariants.Controls.Add(this.label13);
			this.panelCapablancaVariants.Controls.Add(this.label12);
			this.panelCapablancaVariants.Controls.Add(this.label11);
			this.panelCapablancaVariants.Controls.Add(this.label10);
			this.panelCapablancaVariants.Controls.Add(this.label9);
			this.panelCapablancaVariants.Location = new System.Drawing.Point(517, 47);
			this.panelCapablancaVariants.Name = "panelCapablancaVariants";
			this.panelCapablancaVariants.Size = new System.Drawing.Size(451, 166);
			this.panelCapablancaVariants.TabIndex = 2;
			this.panelCapablancaVariants.Visible = false;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(112, 144);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(218, 13);
			this.label17.TabIndex = 9;
			this.label17.Text = "of the pieces varies, as well as castling rules.";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(44, 131);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(355, 13);
			this.label16.TabIndex = 8;
			this.label16.Text = "Not all variants use the standard piece naming, and the initial arrangement";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(24, 108);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(395, 13);
			this.label15.TabIndex = 7;
			this.label15.Text = "this idea goes back to at least 1617 when Pietro Carrera published Carrera\'s Ches" +
    "s";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(7, 94);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(428, 13);
			this.label14.TabIndex = 6;
			this.label14.Text = "Although generally associated with former World Chess Champion Jose Raul Capablan" +
    "ca,";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(122, 69);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(199, 13);
			this.label13.TabIndex = 5;
			this.label13.Text = "Chancellor - moves like a Rook or Knight";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(117, 55);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(208, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Archbishop - moves like a Bishop or Knight";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(106, 32);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(230, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "combines the movements of Rook and Bishop.)";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(18, 18);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(406, 13);
			this.label10.TabIndex = 2;
			this.label10.Text = "containing the two missing piece compounds.  (The Queen is a compound piece that";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(29, 4);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(385, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "This is a family of games that extend Chess by adding two extra files to the boar" +
    "d";
			// 
			// lblGameName
			// 
			this.lblGameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameName.Location = new System.Drawing.Point(413, 218);
			this.lblGameName.Name = "lblGameName";
			this.lblGameName.Size = new System.Drawing.Size(659, 32);
			this.lblGameName.TabIndex = 1;
			this.lblGameName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lvGames
			// 
			this.lvGames.BackColor = System.Drawing.Color.White;
			this.lvGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvNameColumn,
            this.lvInventorColumn,
            this.lvInventedColumn});
			this.lvGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lvGames.FullRowSelect = true;
			this.lvGames.HideSelection = false;
			this.lvGames.LargeImageList = this.largeImageList;
			this.lvGames.Location = new System.Drawing.Point(15, 74);
			this.lvGames.Name = "lvGames";
			this.lvGames.ShowGroups = false;
			this.lvGames.Size = new System.Drawing.Size(384, 459);
			this.lvGames.SmallImageList = this.largeImageList;
			this.lvGames.TabIndex = 0;
			this.lvGames.TileSize = new System.Drawing.Size(362, 50);
			this.lvGames.UseCompatibleStateImageBehavior = false;
			this.lvGames.View = System.Windows.Forms.View.Tile;
			this.lvGames.SelectedIndexChanged += new System.EventHandler(this.olvGames_SelectedIndexChanged);
			// 
			// lvNameColumn
			// 
			this.lvNameColumn.Text = "Game";
			this.lvNameColumn.Width = 300;
			// 
			// lvInventorColumn
			// 
			this.lvInventorColumn.Text = "Inventor";
			this.lvInventorColumn.Width = 0;
			// 
			// lvInventedColumn
			// 
			this.lvInventedColumn.Text = "Invented";
			this.lvInventedColumn.Width = 0;
			// 
			// largeImageList
			// 
			this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
			this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.largeImageList.Images.SetKeyName(0, "GameIcon_64x64.png");
			this.largeImageList.Images.SetKeyName(1, "VintageGameIcon_64x64.png");
			// 
			// btnAbout
			// 
			this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnAbout.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnAbout.Image = global::ChessV.GUI.Properties.Resources.icon_about;
			this.btnAbout.Location = new System.Drawing.Point(12, 600);
			this.btnAbout.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnAbout.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(123, 32);
			this.btnAbout.TabIndex = 16;
			this.btnAbout.Text = "   About ";
			this.btnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// startTimer
			// 
			this.startTimer.Tick += new System.EventHandler(this.startTimer_Tick);
			// 
			// btnQuit
			// 
			this.btnQuit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnQuit.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnQuit.Image = global::ChessV.GUI.Properties.Resources.icon_exit;
			this.btnQuit.Location = new System.Drawing.Point(498, 600);
			this.btnQuit.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnQuit.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(123, 32);
			this.btnQuit.TabIndex = 12;
			this.btnQuit.Text = "      &Quit  ";
			this.btnQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// btnEngines
			// 
			this.btnEngines.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnEngines.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnEngines.Image = ((System.Drawing.Image)(resources.GetObject("btnEngines.Image")));
			this.btnEngines.Location = new System.Drawing.Point(684, 600);
			this.btnEngines.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnEngines.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnEngines.Name = "btnEngines";
			this.btnEngines.Size = new System.Drawing.Size(123, 32);
			this.btnEngines.TabIndex = 13;
			this.btnEngines.Text = "   Engines";
			this.btnEngines.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnEngines.UseVisualStyleBackColor = true;
			this.btnEngines.Click += new System.EventHandler(this.btnEngines_Click);
			// 
			// btnLoadGame
			// 
			this.btnLoadGame.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnLoadGame.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnLoadGame.Image = global::ChessV.GUI.Properties.Resources.icon_load;
			this.btnLoadGame.Location = new System.Drawing.Point(311, 600);
			this.btnLoadGame.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnLoadGame.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnLoadGame.Name = "btnLoadGame";
			this.btnLoadGame.Size = new System.Drawing.Size(123, 32);
			this.btnLoadGame.TabIndex = 11;
			this.btnLoadGame.Text = "    &Load Game";
			this.btnLoadGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnLoadGame.UseVisualStyleBackColor = true;
			this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "Save game files (*.sgf)|*.sgf|All files (*.*)|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.CancelButton = this.btnQuit;
			this.ClientSize = new System.Drawing.Size(1118, 648);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.mainTabControl);
			this.Controls.Add(this.btnEngines);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.btnLoadGame);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panelSubGames);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ChessV 2.1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mainTabControl.ResumeLayout(false);
			this.tabIndexPage.ResumeLayout(false);
			this.panelGameIndexHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictGamesIcon)).EndInit();
			this.panelSubGames.ResumeLayout(false);
			this.panelShuffledVariants.ResumeLayout(false);
			this.panelShuffledVariants.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictSubGamePreview)).EndInit();
			this.panelCapablancaVariants.ResumeLayout(false);
			this.panelCapablancaVariants.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnLoadGame;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnEngines;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.Panel panelSubGames;
		private System.Windows.Forms.ListView lvGames;
		private System.Windows.Forms.ColumnHeader lvNameColumn;
		private System.Windows.Forms.ColumnHeader lvInventorColumn;
		private System.Windows.Forms.ColumnHeader lvInventedColumn;
		private System.Windows.Forms.ImageList largeImageList;
		private System.Windows.Forms.Label lblGameName;
		private System.Windows.Forms.Panel panelCapablancaVariants;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblSubGameCategory;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.PictureBox pictSubGamePreview;
		private System.Windows.Forms.Label lblRuleNote;
		private System.Windows.Forms.Button btnRandom;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnStartGame;
		private System.Windows.Forms.TabPage tabIndexPage;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.Timer startTimer;
		private System.Windows.Forms.Panel panelShuffledVariants;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Panel panelGameIndexHeader;
		private System.Windows.Forms.Label lblMasterGameIndex;
		private System.Windows.Forms.PictureBox pictGamesIcon;
		private System.Windows.Forms.Button btnFilterIndex;
		private System.Windows.Forms.ListView lvMasterIndex;
		private System.Windows.Forms.ColumnHeader colGameName;
		private System.Windows.Forms.ColumnHeader colBoard;
		private System.Windows.Forms.ColumnHeader colInvented;
		private System.Windows.Forms.ColumnHeader colInventor;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}