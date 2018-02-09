
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
	partial class GameSettingsForm
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
			this.groupBoxTC = new System.Windows.Forms.GroupBox();
			this.panelTimePerGame = new System.Windows.Forms.Panel();
			this.labelTimeMSec = new System.Windows.Forms.Label();
			this.labelTimeSec = new System.Windows.Forms.Label();
			this.labelTimeMin = new System.Windows.Forms.Label();
			this.txtTimeIncrementMSec = new System.Windows.Forms.TextBox();
			this.txtTimeIncrementSec = new System.Windows.Forms.TextBox();
			this.txtTimeIncrementMin = new System.Windows.Forms.TextBox();
			this.txtTimeBaseMSec = new System.Windows.Forms.TextBox();
			this.txtTimeBaseSec = new System.Windows.Forms.TextBox();
			this.txtTimeBaseMin = new System.Windows.Forms.TextBox();
			this.labelTimePerGameIncrement = new System.Windows.Forms.Label();
			this.labelTimePerGameBase = new System.Windows.Forms.Label();
			this.labelTimePerGame = new System.Windows.Forms.Label();
			this.panelUnlimited = new System.Windows.Forms.Panel();
			this.labelUnlimited = new System.Windows.Forms.Label();
			this.labelTimeUnlimited2 = new System.Windows.Forms.Label();
			this.labelTimeUnlimited1 = new System.Windows.Forms.Label();
			this.panelFixedDepth = new System.Windows.Forms.Panel();
			this.txtFixedDepth = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelFixedDepth = new System.Windows.Forms.Label();
			this.optTimeTimePerGame = new System.Windows.Forms.RadioButton();
			this.optTimeFixedNodes = new System.Windows.Forms.RadioButton();
			this.optTimeFixedDepth = new System.Windows.Forms.RadioButton();
			this.optTimeFixedPerMove = new System.Windows.Forms.RadioButton();
			this.optTimeUnlimited = new System.Windows.Forms.RadioButton();
			this.panelTimePerMove = new System.Windows.Forms.Panel();
			this.lblTimeMoveMSec = new System.Windows.Forms.Label();
			this.lblTimeMoveSec = new System.Windows.Forms.Label();
			this.lblTimeMoveMin = new System.Windows.Forms.Label();
			this.txtTimeMoveMSec = new System.Windows.Forms.TextBox();
			this.txtTimeMoveSec = new System.Windows.Forms.TextBox();
			this.txtTimeMoveMin = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelTimePerMove = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBoxPlayers = new System.Windows.Forms.GroupBox();
			this.panelPlayerVsComputer = new System.Windows.Forms.Panel();
			this.pickEngine = new System.Windows.Forms.ComboBox();
			this.labelEngine = new System.Windows.Forms.Label();
			this.pickComputerSide = new System.Windows.Forms.ComboBox();
			this.labelComputerPlays = new System.Windows.Forms.Label();
			this.labelPlayerAgainstComputer = new System.Windows.Forms.Label();
			this.panelTwoPeople = new System.Windows.Forms.Panel();
			this.labelTwoPeopleTwo = new System.Windows.Forms.Label();
			this.labelTwoPeople1 = new System.Windows.Forms.Label();
			this.labelTwoPeople = new System.Windows.Forms.Label();
			this.panelTwoComputers = new System.Windows.Forms.Panel();
			this.pickEngine2 = new System.Windows.Forms.ComboBox();
			this.labelSecondEngine = new System.Windows.Forms.Label();
			this.pickEngine1 = new System.Windows.Forms.ComboBox();
			this.labelFirstEngine = new System.Windows.Forms.Label();
			this.labelTwoComputers = new System.Windows.Forms.Label();
			this.optPlayersTwoComputers = new System.Windows.Forms.RadioButton();
			this.optPlayersPlayerVsComputer = new System.Windows.Forms.RadioButton();
			this.optPlayersTwoPeople = new System.Windows.Forms.RadioButton();
			this.groupBoxGame = new System.Windows.Forms.GroupBox();
			this.panelGame = new System.Windows.Forms.Panel();
			this.lblGameDescription2 = new System.Windows.Forms.Label();
			this.lblGameDescription1 = new System.Windows.Forms.Label();
			this.labelGameTitle = new System.Windows.Forms.Label();
			this.groupBoxTC.SuspendLayout();
			this.panelTimePerGame.SuspendLayout();
			this.panelUnlimited.SuspendLayout();
			this.panelFixedDepth.SuspendLayout();
			this.panelTimePerMove.SuspendLayout();
			this.groupBoxPlayers.SuspendLayout();
			this.panelPlayerVsComputer.SuspendLayout();
			this.panelTwoPeople.SuspendLayout();
			this.panelTwoComputers.SuspendLayout();
			this.groupBoxGame.SuspendLayout();
			this.panelGame.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxTC
			// 
			this.groupBoxTC.Controls.Add(this.optTimeTimePerGame);
			this.groupBoxTC.Controls.Add(this.optTimeFixedNodes);
			this.groupBoxTC.Controls.Add(this.optTimeFixedDepth);
			this.groupBoxTC.Controls.Add(this.optTimeFixedPerMove);
			this.groupBoxTC.Controls.Add(this.optTimeUnlimited);
			this.groupBoxTC.Controls.Add(this.panelTimePerGame);
			this.groupBoxTC.Controls.Add(this.panelUnlimited);
			this.groupBoxTC.Controls.Add(this.panelFixedDepth);
			this.groupBoxTC.Controls.Add(this.panelTimePerMove);
			this.groupBoxTC.Location = new System.Drawing.Point(12, 328);
			this.groupBoxTC.Name = "groupBoxTC";
			this.groupBoxTC.Size = new System.Drawing.Size(552, 172);
			this.groupBoxTC.TabIndex = 0;
			this.groupBoxTC.TabStop = false;
			this.groupBoxTC.Text = "Time Control";
			// 
			// panelTimePerGame
			// 
			this.panelTimePerGame.BackColor = System.Drawing.Color.Wheat;
			this.panelTimePerGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTimePerGame.Controls.Add(this.labelTimeMSec);
			this.panelTimePerGame.Controls.Add(this.labelTimeSec);
			this.panelTimePerGame.Controls.Add(this.labelTimeMin);
			this.panelTimePerGame.Controls.Add(this.txtTimeIncrementMSec);
			this.panelTimePerGame.Controls.Add(this.txtTimeIncrementSec);
			this.panelTimePerGame.Controls.Add(this.txtTimeIncrementMin);
			this.panelTimePerGame.Controls.Add(this.txtTimeBaseMSec);
			this.panelTimePerGame.Controls.Add(this.txtTimeBaseSec);
			this.panelTimePerGame.Controls.Add(this.txtTimeBaseMin);
			this.panelTimePerGame.Controls.Add(this.labelTimePerGameIncrement);
			this.panelTimePerGame.Controls.Add(this.labelTimePerGameBase);
			this.panelTimePerGame.Controls.Add(this.labelTimePerGame);
			this.panelTimePerGame.Location = new System.Drawing.Point(258, 24);
			this.panelTimePerGame.Name = "panelTimePerGame";
			this.panelTimePerGame.Size = new System.Drawing.Size(231, 125);
			this.panelTimePerGame.TabIndex = 7;
			// 
			// labelTimeMSec
			// 
			this.labelTimeMSec.Location = new System.Drawing.Point(161, 61);
			this.labelTimeMSec.Name = "labelTimeMSec";
			this.labelTimeMSec.Size = new System.Drawing.Size(39, 17);
			this.labelTimeMSec.TabIndex = 11;
			this.labelTimeMSec.Text = "msec";
			this.labelTimeMSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTimeSec
			// 
			this.labelTimeSec.Location = new System.Drawing.Point(116, 61);
			this.labelTimeSec.Name = "labelTimeSec";
			this.labelTimeSec.Size = new System.Drawing.Size(39, 17);
			this.labelTimeSec.TabIndex = 10;
			this.labelTimeSec.Text = "sec";
			this.labelTimeSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTimeMin
			// 
			this.labelTimeMin.Location = new System.Drawing.Point(71, 61);
			this.labelTimeMin.Name = "labelTimeMin";
			this.labelTimeMin.Size = new System.Drawing.Size(39, 17);
			this.labelTimeMin.TabIndex = 9;
			this.labelTimeMin.Text = "min";
			this.labelTimeMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtTimeIncrementMSec
			// 
			this.txtTimeIncrementMSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeIncrementMSec.Location = new System.Drawing.Point(161, 80);
			this.txtTimeIncrementMSec.MaxLength = 3;
			this.txtTimeIncrementMSec.Name = "txtTimeIncrementMSec";
			this.txtTimeIncrementMSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeIncrementMSec.TabIndex = 8;
			this.txtTimeIncrementMSec.Text = "0";
			this.txtTimeIncrementMSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeIncrementSec
			// 
			this.txtTimeIncrementSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeIncrementSec.Location = new System.Drawing.Point(116, 80);
			this.txtTimeIncrementSec.MaxLength = 2;
			this.txtTimeIncrementSec.Name = "txtTimeIncrementSec";
			this.txtTimeIncrementSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeIncrementSec.TabIndex = 7;
			this.txtTimeIncrementSec.Text = "0";
			this.txtTimeIncrementSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeIncrementMin
			// 
			this.txtTimeIncrementMin.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeIncrementMin.Location = new System.Drawing.Point(71, 80);
			this.txtTimeIncrementMin.MaxLength = 6;
			this.txtTimeIncrementMin.Name = "txtTimeIncrementMin";
			this.txtTimeIncrementMin.Size = new System.Drawing.Size(39, 20);
			this.txtTimeIncrementMin.TabIndex = 6;
			this.txtTimeIncrementMin.Text = "0";
			this.txtTimeIncrementMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeBaseMSec
			// 
			this.txtTimeBaseMSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeBaseMSec.Location = new System.Drawing.Point(161, 41);
			this.txtTimeBaseMSec.MaxLength = 3;
			this.txtTimeBaseMSec.Name = "txtTimeBaseMSec";
			this.txtTimeBaseMSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeBaseMSec.TabIndex = 5;
			this.txtTimeBaseMSec.Text = "0";
			this.txtTimeBaseMSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeBaseSec
			// 
			this.txtTimeBaseSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeBaseSec.Location = new System.Drawing.Point(116, 41);
			this.txtTimeBaseSec.MaxLength = 2;
			this.txtTimeBaseSec.Name = "txtTimeBaseSec";
			this.txtTimeBaseSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeBaseSec.TabIndex = 4;
			this.txtTimeBaseSec.Text = "0";
			this.txtTimeBaseSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeBaseMin
			// 
			this.txtTimeBaseMin.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeBaseMin.Location = new System.Drawing.Point(71, 41);
			this.txtTimeBaseMin.MaxLength = 6;
			this.txtTimeBaseMin.Name = "txtTimeBaseMin";
			this.txtTimeBaseMin.Size = new System.Drawing.Size(39, 20);
			this.txtTimeBaseMin.TabIndex = 3;
			this.txtTimeBaseMin.Text = "5";
			this.txtTimeBaseMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelTimePerGameIncrement
			// 
			this.labelTimePerGameIncrement.AutoSize = true;
			this.labelTimePerGameIncrement.Location = new System.Drawing.Point(8, 83);
			this.labelTimePerGameIncrement.Name = "labelTimePerGameIncrement";
			this.labelTimePerGameIncrement.Size = new System.Drawing.Size(57, 13);
			this.labelTimePerGameIncrement.TabIndex = 2;
			this.labelTimePerGameIncrement.Text = "Increment:";
			// 
			// labelTimePerGameBase
			// 
			this.labelTimePerGameBase.AutoSize = true;
			this.labelTimePerGameBase.Location = new System.Drawing.Point(31, 44);
			this.labelTimePerGameBase.Name = "labelTimePerGameBase";
			this.labelTimePerGameBase.Size = new System.Drawing.Size(34, 13);
			this.labelTimePerGameBase.TabIndex = 1;
			this.labelTimePerGameBase.Text = "Base:";
			// 
			// labelTimePerGame
			// 
			this.labelTimePerGame.BackColor = System.Drawing.Color.Goldenrod;
			this.labelTimePerGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTimePerGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTimePerGame.Location = new System.Drawing.Point(-1, -1);
			this.labelTimePerGame.Name = "labelTimePerGame";
			this.labelTimePerGame.Size = new System.Drawing.Size(231, 25);
			this.labelTimePerGame.TabIndex = 0;
			this.labelTimePerGame.Text = "Time per Game";
			this.labelTimePerGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelUnlimited
			// 
			this.panelUnlimited.BackColor = System.Drawing.Color.Wheat;
			this.panelUnlimited.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelUnlimited.Controls.Add(this.labelUnlimited);
			this.panelUnlimited.Controls.Add(this.labelTimeUnlimited2);
			this.panelUnlimited.Controls.Add(this.labelTimeUnlimited1);
			this.panelUnlimited.Location = new System.Drawing.Point(258, 24);
			this.panelUnlimited.Name = "panelUnlimited";
			this.panelUnlimited.Size = new System.Drawing.Size(231, 125);
			this.panelUnlimited.TabIndex = 9;
			// 
			// labelUnlimited
			// 
			this.labelUnlimited.BackColor = System.Drawing.Color.Goldenrod;
			this.labelUnlimited.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelUnlimited.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelUnlimited.Location = new System.Drawing.Point(-1, -1);
			this.labelUnlimited.Name = "labelUnlimited";
			this.labelUnlimited.Size = new System.Drawing.Size(231, 25);
			this.labelUnlimited.TabIndex = 0;
			this.labelUnlimited.Text = "Unlimited";
			this.labelUnlimited.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTimeUnlimited2
			// 
			this.labelTimeUnlimited2.Location = new System.Drawing.Point(3, 64);
			this.labelTimeUnlimited2.Name = "labelTimeUnlimited2";
			this.labelTimeUnlimited2.Size = new System.Drawing.Size(223, 17);
			this.labelTimeUnlimited2.TabIndex = 2;
			this.labelTimeUnlimited2.Text = "Computer will search infinitely until stopped";
			this.labelTimeUnlimited2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTimeUnlimited1
			// 
			this.labelTimeUnlimited1.Location = new System.Drawing.Point(3, 46);
			this.labelTimeUnlimited1.Name = "labelTimeUnlimited1";
			this.labelTimeUnlimited1.Size = new System.Drawing.Size(223, 17);
			this.labelTimeUnlimited1.TabIndex = 1;
			this.labelTimeUnlimited1.Text = "No time limit";
			this.labelTimeUnlimited1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelFixedDepth
			// 
			this.panelFixedDepth.BackColor = System.Drawing.Color.Wheat;
			this.panelFixedDepth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelFixedDepth.Controls.Add(this.txtFixedDepth);
			this.panelFixedDepth.Controls.Add(this.label3);
			this.panelFixedDepth.Controls.Add(this.labelFixedDepth);
			this.panelFixedDepth.Location = new System.Drawing.Point(258, 24);
			this.panelFixedDepth.Name = "panelFixedDepth";
			this.panelFixedDepth.Size = new System.Drawing.Size(231, 125);
			this.panelFixedDepth.TabIndex = 10;
			// 
			// txtFixedDepth
			// 
			this.txtFixedDepth.BackColor = System.Drawing.Color.FloralWhite;
			this.txtFixedDepth.Location = new System.Drawing.Point(105, 60);
			this.txtFixedDepth.Name = "txtFixedDepth";
			this.txtFixedDepth.Size = new System.Drawing.Size(50, 20);
			this.txtFixedDepth.TabIndex = 2;
			this.txtFixedDepth.Text = "5";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(60, 63);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Depth:";
			// 
			// labelFixedDepth
			// 
			this.labelFixedDepth.BackColor = System.Drawing.Color.Goldenrod;
			this.labelFixedDepth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelFixedDepth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelFixedDepth.Location = new System.Drawing.Point(-1, -1);
			this.labelFixedDepth.Name = "labelFixedDepth";
			this.labelFixedDepth.Size = new System.Drawing.Size(231, 25);
			this.labelFixedDepth.TabIndex = 0;
			this.labelFixedDepth.Text = "Fixed Depth";
			this.labelFixedDepth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// optTimeTimePerGame
			// 
			this.optTimeTimePerGame.AutoSize = true;
			this.optTimeTimePerGame.Checked = true;
			this.optTimeTimePerGame.Location = new System.Drawing.Point(53, 53);
			this.optTimeTimePerGame.Name = "optTimeTimePerGame";
			this.optTimeTimePerGame.Size = new System.Drawing.Size(98, 17);
			this.optTimeTimePerGame.TabIndex = 4;
			this.optTimeTimePerGame.TabStop = true;
			this.optTimeTimePerGame.Text = "Time Per Game";
			this.optTimeTimePerGame.UseVisualStyleBackColor = true;
			this.optTimeTimePerGame.CheckedChanged += new System.EventHandler(this.optTimeTimePerGame_CheckedChanged);
			// 
			// optTimeFixedNodes
			// 
			this.optTimeFixedNodes.AutoSize = true;
			this.optTimeFixedNodes.Location = new System.Drawing.Point(53, 122);
			this.optTimeFixedNodes.Name = "optTimeFixedNodes";
			this.optTimeFixedNodes.Size = new System.Drawing.Size(84, 17);
			this.optTimeFixedNodes.TabIndex = 3;
			this.optTimeFixedNodes.Text = "Fixed Nodes";
			this.optTimeFixedNodes.UseVisualStyleBackColor = true;
			// 
			// optTimeFixedDepth
			// 
			this.optTimeFixedDepth.AutoSize = true;
			this.optTimeFixedDepth.Location = new System.Drawing.Point(53, 99);
			this.optTimeFixedDepth.Name = "optTimeFixedDepth";
			this.optTimeFixedDepth.Size = new System.Drawing.Size(82, 17);
			this.optTimeFixedDepth.TabIndex = 2;
			this.optTimeFixedDepth.Text = "Fixed Depth";
			this.optTimeFixedDepth.UseVisualStyleBackColor = true;
			this.optTimeFixedDepth.CheckedChanged += new System.EventHandler(this.optTimeFixedDepth_CheckedChanged);
			// 
			// optTimeFixedPerMove
			// 
			this.optTimeFixedPerMove.AutoSize = true;
			this.optTimeFixedPerMove.Location = new System.Drawing.Point(53, 76);
			this.optTimeFixedPerMove.Name = "optTimeFixedPerMove";
			this.optTimeFixedPerMove.Size = new System.Drawing.Size(125, 17);
			this.optTimeFixedPerMove.TabIndex = 1;
			this.optTimeFixedPerMove.Text = "Fixed Time Per Move";
			this.optTimeFixedPerMove.UseVisualStyleBackColor = true;
			this.optTimeFixedPerMove.CheckedChanged += new System.EventHandler(this.optTimeFixedPerMove_CheckedChanged);
			// 
			// optTimeUnlimited
			// 
			this.optTimeUnlimited.AutoSize = true;
			this.optTimeUnlimited.Location = new System.Drawing.Point(53, 30);
			this.optTimeUnlimited.Name = "optTimeUnlimited";
			this.optTimeUnlimited.Size = new System.Drawing.Size(68, 17);
			this.optTimeUnlimited.TabIndex = 0;
			this.optTimeUnlimited.Text = "Unlimited";
			this.optTimeUnlimited.UseVisualStyleBackColor = true;
			this.optTimeUnlimited.CheckedChanged += new System.EventHandler(this.optTimeUnlimited_CheckedChanged);
			// 
			// panelTimePerMove
			// 
			this.panelTimePerMove.BackColor = System.Drawing.Color.Wheat;
			this.panelTimePerMove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTimePerMove.Controls.Add(this.lblTimeMoveMSec);
			this.panelTimePerMove.Controls.Add(this.lblTimeMoveSec);
			this.panelTimePerMove.Controls.Add(this.lblTimeMoveMin);
			this.panelTimePerMove.Controls.Add(this.txtTimeMoveMSec);
			this.panelTimePerMove.Controls.Add(this.txtTimeMoveSec);
			this.panelTimePerMove.Controls.Add(this.txtTimeMoveMin);
			this.panelTimePerMove.Controls.Add(this.label1);
			this.panelTimePerMove.Controls.Add(this.label2);
			this.panelTimePerMove.Controls.Add(this.labelTimePerMove);
			this.panelTimePerMove.Location = new System.Drawing.Point(258, 24);
			this.panelTimePerMove.Name = "panelTimePerMove";
			this.panelTimePerMove.Size = new System.Drawing.Size(231, 125);
			this.panelTimePerMove.TabIndex = 6;
			// 
			// lblTimeMoveMSec
			// 
			this.lblTimeMoveMSec.Location = new System.Drawing.Point(140, 99);
			this.lblTimeMoveMSec.Name = "lblTimeMoveMSec";
			this.lblTimeMoveMSec.Size = new System.Drawing.Size(39, 17);
			this.lblTimeMoveMSec.TabIndex = 17;
			this.lblTimeMoveMSec.Text = "msec";
			this.lblTimeMoveMSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTimeMoveSec
			// 
			this.lblTimeMoveSec.Location = new System.Drawing.Point(95, 99);
			this.lblTimeMoveSec.Name = "lblTimeMoveSec";
			this.lblTimeMoveSec.Size = new System.Drawing.Size(39, 17);
			this.lblTimeMoveSec.TabIndex = 16;
			this.lblTimeMoveSec.Text = "sec";
			this.lblTimeMoveSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTimeMoveMin
			// 
			this.lblTimeMoveMin.Location = new System.Drawing.Point(50, 99);
			this.lblTimeMoveMin.Name = "lblTimeMoveMin";
			this.lblTimeMoveMin.Size = new System.Drawing.Size(39, 17);
			this.lblTimeMoveMin.TabIndex = 15;
			this.lblTimeMoveMin.Text = "min";
			this.lblTimeMoveMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtTimeMoveMSec
			// 
			this.txtTimeMoveMSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeMoveMSec.Location = new System.Drawing.Point(140, 79);
			this.txtTimeMoveMSec.MaxLength = 3;
			this.txtTimeMoveMSec.Name = "txtTimeMoveMSec";
			this.txtTimeMoveMSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeMoveMSec.TabIndex = 14;
			this.txtTimeMoveMSec.Text = "0";
			this.txtTimeMoveMSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeMoveSec
			// 
			this.txtTimeMoveSec.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeMoveSec.Location = new System.Drawing.Point(95, 79);
			this.txtTimeMoveSec.MaxLength = 2;
			this.txtTimeMoveSec.Name = "txtTimeMoveSec";
			this.txtTimeMoveSec.Size = new System.Drawing.Size(39, 20);
			this.txtTimeMoveSec.TabIndex = 13;
			this.txtTimeMoveSec.Text = "10";
			this.txtTimeMoveSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtTimeMoveMin
			// 
			this.txtTimeMoveMin.BackColor = System.Drawing.Color.FloralWhite;
			this.txtTimeMoveMin.Location = new System.Drawing.Point(50, 79);
			this.txtTimeMoveMin.MaxLength = 6;
			this.txtTimeMoveMin.Name = "txtTimeMoveMin";
			this.txtTimeMoveMin.Size = new System.Drawing.Size(39, 20);
			this.txtTimeMoveMin.TabIndex = 12;
			this.txtTimeMoveMin.Text = "0";
			this.txtTimeMoveMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(223, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "within this amount of time";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(223, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Each move must be made";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTimePerMove
			// 
			this.labelTimePerMove.BackColor = System.Drawing.Color.Goldenrod;
			this.labelTimePerMove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTimePerMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTimePerMove.Location = new System.Drawing.Point(-1, -1);
			this.labelTimePerMove.Name = "labelTimePerMove";
			this.labelTimePerMove.Size = new System.Drawing.Size(231, 25);
			this.labelTimePerMove.TabIndex = 0;
			this.labelTimePerMove.Text = "Time per Move";
			this.labelTimePerMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnOK
			// 
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(140, 514);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::ChessV.GUI.Properties.Resources.icon_cancel;
			this.btnCancel.Location = new System.Drawing.Point(314, 514);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "     &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBoxPlayers
			// 
			this.groupBoxPlayers.Controls.Add(this.panelPlayerVsComputer);
			this.groupBoxPlayers.Controls.Add(this.panelTwoPeople);
			this.groupBoxPlayers.Controls.Add(this.panelTwoComputers);
			this.groupBoxPlayers.Controls.Add(this.optPlayersTwoComputers);
			this.groupBoxPlayers.Controls.Add(this.optPlayersPlayerVsComputer);
			this.groupBoxPlayers.Controls.Add(this.optPlayersTwoPeople);
			this.groupBoxPlayers.Location = new System.Drawing.Point(12, 148);
			this.groupBoxPlayers.Name = "groupBoxPlayers";
			this.groupBoxPlayers.Size = new System.Drawing.Size(552, 174);
			this.groupBoxPlayers.TabIndex = 3;
			this.groupBoxPlayers.TabStop = false;
			this.groupBoxPlayers.Text = "Players";
			// 
			// panelPlayerVsComputer
			// 
			this.panelPlayerVsComputer.BackColor = System.Drawing.Color.Wheat;
			this.panelPlayerVsComputer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelPlayerVsComputer.Controls.Add(this.pickEngine);
			this.panelPlayerVsComputer.Controls.Add(this.labelEngine);
			this.panelPlayerVsComputer.Controls.Add(this.pickComputerSide);
			this.panelPlayerVsComputer.Controls.Add(this.labelComputerPlays);
			this.panelPlayerVsComputer.Controls.Add(this.labelPlayerAgainstComputer);
			this.panelPlayerVsComputer.Location = new System.Drawing.Point(258, 27);
			this.panelPlayerVsComputer.Name = "panelPlayerVsComputer";
			this.panelPlayerVsComputer.Size = new System.Drawing.Size(231, 125);
			this.panelPlayerVsComputer.TabIndex = 3;
			// 
			// pickEngine
			// 
			this.pickEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickEngine.FormattingEnabled = true;
			this.pickEngine.Location = new System.Drawing.Point(105, 81);
			this.pickEngine.Name = "pickEngine";
			this.pickEngine.Size = new System.Drawing.Size(106, 21);
			this.pickEngine.TabIndex = 5;
			// 
			// labelEngine
			// 
			this.labelEngine.AutoSize = true;
			this.labelEngine.Location = new System.Drawing.Point(56, 84);
			this.labelEngine.Name = "labelEngine";
			this.labelEngine.Size = new System.Drawing.Size(43, 13);
			this.labelEngine.TabIndex = 4;
			this.labelEngine.Text = "Engine:";
			// 
			// pickComputerSide
			// 
			this.pickComputerSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickComputerSide.FormattingEnabled = true;
			this.pickComputerSide.Location = new System.Drawing.Point(105, 45);
			this.pickComputerSide.Name = "pickComputerSide";
			this.pickComputerSide.Size = new System.Drawing.Size(106, 21);
			this.pickComputerSide.TabIndex = 3;
			// 
			// labelComputerPlays
			// 
			this.labelComputerPlays.AutoSize = true;
			this.labelComputerPlays.Location = new System.Drawing.Point(16, 49);
			this.labelComputerPlays.Name = "labelComputerPlays";
			this.labelComputerPlays.Size = new System.Drawing.Size(83, 13);
			this.labelComputerPlays.TabIndex = 2;
			this.labelComputerPlays.Text = "Computer Plays:";
			// 
			// labelPlayerAgainstComputer
			// 
			this.labelPlayerAgainstComputer.BackColor = System.Drawing.Color.Goldenrod;
			this.labelPlayerAgainstComputer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPlayerAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayerAgainstComputer.Location = new System.Drawing.Point(-1, -1);
			this.labelPlayerAgainstComputer.Name = "labelPlayerAgainstComputer";
			this.labelPlayerAgainstComputer.Size = new System.Drawing.Size(231, 25);
			this.labelPlayerAgainstComputer.TabIndex = 1;
			this.labelPlayerAgainstComputer.Text = "Player Against Computer";
			this.labelPlayerAgainstComputer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelTwoPeople
			// 
			this.panelTwoPeople.BackColor = System.Drawing.Color.Wheat;
			this.panelTwoPeople.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTwoPeople.Controls.Add(this.labelTwoPeopleTwo);
			this.panelTwoPeople.Controls.Add(this.labelTwoPeople1);
			this.panelTwoPeople.Controls.Add(this.labelTwoPeople);
			this.panelTwoPeople.Location = new System.Drawing.Point(258, 27);
			this.panelTwoPeople.Name = "panelTwoPeople";
			this.panelTwoPeople.Size = new System.Drawing.Size(231, 125);
			this.panelTwoPeople.TabIndex = 5;
			// 
			// labelTwoPeopleTwo
			// 
			this.labelTwoPeopleTwo.Location = new System.Drawing.Point(3, 62);
			this.labelTwoPeopleTwo.Name = "labelTwoPeopleTwo";
			this.labelTwoPeopleTwo.Size = new System.Drawing.Size(223, 17);
			this.labelTwoPeopleTwo.TabIndex = 4;
			this.labelTwoPeopleTwo.Text = "(This can be changed later.)";
			this.labelTwoPeopleTwo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTwoPeople1
			// 
			this.labelTwoPeople1.Location = new System.Drawing.Point(3, 44);
			this.labelTwoPeople1.Name = "labelTwoPeople1";
			this.labelTwoPeople1.Size = new System.Drawing.Size(223, 17);
			this.labelTwoPeople1.TabIndex = 3;
			this.labelTwoPeople1.Text = "All moves will be entered manually";
			this.labelTwoPeople1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTwoPeople
			// 
			this.labelTwoPeople.BackColor = System.Drawing.Color.Goldenrod;
			this.labelTwoPeople.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTwoPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTwoPeople.Location = new System.Drawing.Point(-1, -1);
			this.labelTwoPeople.Name = "labelTwoPeople";
			this.labelTwoPeople.Size = new System.Drawing.Size(231, 25);
			this.labelTwoPeople.TabIndex = 1;
			this.labelTwoPeople.Text = "Two People";
			this.labelTwoPeople.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelTwoComputers
			// 
			this.panelTwoComputers.BackColor = System.Drawing.Color.Wheat;
			this.panelTwoComputers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelTwoComputers.Controls.Add(this.pickEngine2);
			this.panelTwoComputers.Controls.Add(this.labelSecondEngine);
			this.panelTwoComputers.Controls.Add(this.pickEngine1);
			this.panelTwoComputers.Controls.Add(this.labelFirstEngine);
			this.panelTwoComputers.Controls.Add(this.labelTwoComputers);
			this.panelTwoComputers.Location = new System.Drawing.Point(258, 27);
			this.panelTwoComputers.Name = "panelTwoComputers";
			this.panelTwoComputers.Size = new System.Drawing.Size(231, 125);
			this.panelTwoComputers.TabIndex = 4;
			// 
			// pickEngine2
			// 
			this.pickEngine2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickEngine2.FormattingEnabled = true;
			this.pickEngine2.Location = new System.Drawing.Point(105, 81);
			this.pickEngine2.Name = "pickEngine2";
			this.pickEngine2.Size = new System.Drawing.Size(106, 21);
			this.pickEngine2.TabIndex = 5;
			// 
			// labelSecondEngine
			// 
			this.labelSecondEngine.AutoSize = true;
			this.labelSecondEngine.Location = new System.Drawing.Point(16, 84);
			this.labelSecondEngine.Name = "labelSecondEngine";
			this.labelSecondEngine.Size = new System.Drawing.Size(83, 13);
			this.labelSecondEngine.TabIndex = 4;
			this.labelSecondEngine.Text = "Second Engine:";
			// 
			// pickEngine1
			// 
			this.pickEngine1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickEngine1.FormattingEnabled = true;
			this.pickEngine1.Location = new System.Drawing.Point(105, 45);
			this.pickEngine1.Name = "pickEngine1";
			this.pickEngine1.Size = new System.Drawing.Size(106, 21);
			this.pickEngine1.TabIndex = 3;
			// 
			// labelFirstEngine
			// 
			this.labelFirstEngine.AutoSize = true;
			this.labelFirstEngine.Location = new System.Drawing.Point(34, 50);
			this.labelFirstEngine.Name = "labelFirstEngine";
			this.labelFirstEngine.Size = new System.Drawing.Size(65, 13);
			this.labelFirstEngine.TabIndex = 2;
			this.labelFirstEngine.Text = "First Engine:";
			// 
			// labelTwoComputers
			// 
			this.labelTwoComputers.BackColor = System.Drawing.Color.Goldenrod;
			this.labelTwoComputers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelTwoComputers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelTwoComputers.Location = new System.Drawing.Point(-1, -1);
			this.labelTwoComputers.Name = "labelTwoComputers";
			this.labelTwoComputers.Size = new System.Drawing.Size(231, 25);
			this.labelTwoComputers.TabIndex = 1;
			this.labelTwoComputers.Text = "Two Computers";
			this.labelTwoComputers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// optPlayersTwoComputers
			// 
			this.optPlayersTwoComputers.AutoSize = true;
			this.optPlayersTwoComputers.Location = new System.Drawing.Point(53, 111);
			this.optPlayersTwoComputers.Name = "optPlayersTwoComputers";
			this.optPlayersTwoComputers.Size = new System.Drawing.Size(99, 17);
			this.optPlayersTwoComputers.TabIndex = 2;
			this.optPlayersTwoComputers.Text = "Two Computers";
			this.optPlayersTwoComputers.UseVisualStyleBackColor = true;
			this.optPlayersTwoComputers.CheckedChanged += new System.EventHandler(this.optPlayersTwoComputers_CheckedChanged);
			// 
			// optPlayersPlayerVsComputer
			// 
			this.optPlayersPlayerVsComputer.AutoSize = true;
			this.optPlayersPlayerVsComputer.Checked = true;
			this.optPlayersPlayerVsComputer.Location = new System.Drawing.Point(53, 80);
			this.optPlayersPlayerVsComputer.Name = "optPlayersPlayerVsComputer";
			this.optPlayersPlayerVsComputer.Size = new System.Drawing.Size(140, 17);
			this.optPlayersPlayerVsComputer.TabIndex = 1;
			this.optPlayersPlayerVsComputer.TabStop = true;
			this.optPlayersPlayerVsComputer.Text = "Player Against Computer";
			this.optPlayersPlayerVsComputer.UseVisualStyleBackColor = true;
			this.optPlayersPlayerVsComputer.CheckedChanged += new System.EventHandler(this.optPlayersPlayerVsComputer_CheckedChanged);
			// 
			// optPlayersTwoPeople
			// 
			this.optPlayersTwoPeople.AutoSize = true;
			this.optPlayersTwoPeople.Location = new System.Drawing.Point(53, 49);
			this.optPlayersTwoPeople.Name = "optPlayersTwoPeople";
			this.optPlayersTwoPeople.Size = new System.Drawing.Size(82, 17);
			this.optPlayersTwoPeople.TabIndex = 0;
			this.optPlayersTwoPeople.Text = "Two People";
			this.optPlayersTwoPeople.UseVisualStyleBackColor = true;
			this.optPlayersTwoPeople.CheckedChanged += new System.EventHandler(this.optPlayersTwoPeople_CheckedChanged);
			// 
			// groupBoxGame
			// 
			this.groupBoxGame.BackColor = System.Drawing.Color.Transparent;
			this.groupBoxGame.Controls.Add(this.panelGame);
			this.groupBoxGame.Location = new System.Drawing.Point(12, 12);
			this.groupBoxGame.Name = "groupBoxGame";
			this.groupBoxGame.Size = new System.Drawing.Size(552, 130);
			this.groupBoxGame.TabIndex = 4;
			this.groupBoxGame.TabStop = false;
			this.groupBoxGame.Text = "Game";
			// 
			// panelGame
			// 
			this.panelGame.BackColor = System.Drawing.Color.Khaki;
			this.panelGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelGame.Controls.Add(this.lblGameDescription2);
			this.panelGame.Controls.Add(this.lblGameDescription1);
			this.panelGame.Controls.Add(this.labelGameTitle);
			this.panelGame.Location = new System.Drawing.Point(83, 19);
			this.panelGame.Name = "panelGame";
			this.panelGame.Size = new System.Drawing.Size(376, 91);
			this.panelGame.TabIndex = 1;
			// 
			// lblGameDescription2
			// 
			this.lblGameDescription2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameDescription2.Location = new System.Drawing.Point(3, 56);
			this.lblGameDescription2.Name = "lblGameDescription2";
			this.lblGameDescription2.Size = new System.Drawing.Size(368, 23);
			this.lblGameDescription2.TabIndex = 2;
			this.lblGameDescription2.Text = "Game Description 2";
			this.lblGameDescription2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblGameDescription1
			// 
			this.lblGameDescription1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGameDescription1.Location = new System.Drawing.Point(3, 33);
			this.lblGameDescription1.Name = "lblGameDescription1";
			this.lblGameDescription1.Size = new System.Drawing.Size(368, 23);
			this.lblGameDescription1.TabIndex = 1;
			this.lblGameDescription1.Text = "Game Description 1";
			this.lblGameDescription1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelGameTitle
			// 
			this.labelGameTitle.BackColor = System.Drawing.Color.Gold;
			this.labelGameTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelGameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelGameTitle.Location = new System.Drawing.Point(-1, -1);
			this.labelGameTitle.Name = "labelGameTitle";
			this.labelGameTitle.Size = new System.Drawing.Size(376, 25);
			this.labelGameTitle.TabIndex = 0;
			this.labelGameTitle.Text = "Game Name";
			this.labelGameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// GameSettingsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(576, 555);
			this.Controls.Add(this.groupBoxGame);
			this.Controls.Add(this.groupBoxPlayers);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBoxTC);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "GameSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Game Settings";
			this.Load += new System.EventHandler(this.GameSettingsForm_Load);
			this.groupBoxTC.ResumeLayout(false);
			this.groupBoxTC.PerformLayout();
			this.panelTimePerGame.ResumeLayout(false);
			this.panelTimePerGame.PerformLayout();
			this.panelUnlimited.ResumeLayout(false);
			this.panelFixedDepth.ResumeLayout(false);
			this.panelFixedDepth.PerformLayout();
			this.panelTimePerMove.ResumeLayout(false);
			this.panelTimePerMove.PerformLayout();
			this.groupBoxPlayers.ResumeLayout(false);
			this.groupBoxPlayers.PerformLayout();
			this.panelPlayerVsComputer.ResumeLayout(false);
			this.panelPlayerVsComputer.PerformLayout();
			this.panelTwoPeople.ResumeLayout(false);
			this.panelTwoComputers.ResumeLayout(false);
			this.panelTwoComputers.PerformLayout();
			this.groupBoxGame.ResumeLayout(false);
			this.panelGame.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxTC;
		private System.Windows.Forms.RadioButton optTimeTimePerGame;
		private System.Windows.Forms.RadioButton optTimeFixedNodes;
		private System.Windows.Forms.RadioButton optTimeFixedDepth;
		private System.Windows.Forms.RadioButton optTimeFixedPerMove;
		private System.Windows.Forms.RadioButton optTimeUnlimited;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBoxPlayers;
		private System.Windows.Forms.Panel panelPlayerVsComputer;
		private System.Windows.Forms.ComboBox pickEngine;
		private System.Windows.Forms.Label labelEngine;
		private System.Windows.Forms.ComboBox pickComputerSide;
		private System.Windows.Forms.Label labelComputerPlays;
		private System.Windows.Forms.Label labelPlayerAgainstComputer;
		private System.Windows.Forms.RadioButton optPlayersTwoComputers;
		private System.Windows.Forms.RadioButton optPlayersPlayerVsComputer;
		private System.Windows.Forms.RadioButton optPlayersTwoPeople;
		private System.Windows.Forms.GroupBox groupBoxGame;
		private System.Windows.Forms.Panel panelGame;
		private System.Windows.Forms.Label lblGameDescription2;
		private System.Windows.Forms.Label lblGameDescription1;
		private System.Windows.Forms.Label labelGameTitle;
		private System.Windows.Forms.Panel panelTimePerMove;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelTimePerMove;
		private System.Windows.Forms.Label lblTimeMoveMSec;
		private System.Windows.Forms.Label lblTimeMoveSec;
		private System.Windows.Forms.Label lblTimeMoveMin;
		private System.Windows.Forms.TextBox txtTimeMoveMSec;
		private System.Windows.Forms.TextBox txtTimeMoveSec;
		private System.Windows.Forms.TextBox txtTimeMoveMin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panelTimePerGame;
		private System.Windows.Forms.Label labelTimeMSec;
		private System.Windows.Forms.Label labelTimeSec;
		private System.Windows.Forms.Label labelTimeMin;
		private System.Windows.Forms.TextBox txtTimeIncrementMSec;
		private System.Windows.Forms.TextBox txtTimeIncrementSec;
		private System.Windows.Forms.TextBox txtTimeIncrementMin;
		private System.Windows.Forms.TextBox txtTimeBaseMSec;
		private System.Windows.Forms.TextBox txtTimeBaseSec;
		private System.Windows.Forms.TextBox txtTimeBaseMin;
		private System.Windows.Forms.Label labelTimePerGameIncrement;
		private System.Windows.Forms.Label labelTimePerGameBase;
		private System.Windows.Forms.Label labelTimePerGame;
		private System.Windows.Forms.Panel panelTwoComputers;
		private System.Windows.Forms.ComboBox pickEngine2;
		private System.Windows.Forms.Label labelSecondEngine;
		private System.Windows.Forms.ComboBox pickEngine1;
		private System.Windows.Forms.Label labelFirstEngine;
		private System.Windows.Forms.Label labelTwoComputers;
		private System.Windows.Forms.Panel panelTwoPeople;
		private System.Windows.Forms.Label labelTwoPeopleTwo;
		private System.Windows.Forms.Label labelTwoPeople1;
		private System.Windows.Forms.Label labelTwoPeople;
		private System.Windows.Forms.Panel panelUnlimited;
		private System.Windows.Forms.Label labelUnlimited;
		private System.Windows.Forms.Label labelTimeUnlimited2;
		private System.Windows.Forms.Label labelTimeUnlimited1;
		private System.Windows.Forms.Panel panelFixedDepth;
		private System.Windows.Forms.TextBox txtFixedDepth;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelFixedDepth;
	}
}