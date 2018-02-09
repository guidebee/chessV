namespace ChessV.GUI
{
	partial class PieceInfoControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label label1;
			this.lblPieceName = new System.Windows.Forms.Label();
			this.lblBoardVisibility = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblMidgameValue = new System.Windows.Forms.Label();
			this.lblEndgameValue = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblAveMobility = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblAveDirectionsAttacked = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblAveSafeChecks = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(182, 37);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(77, 13);
			label1.TabIndex = 1;
			label1.Text = "Board Visibility:";
			// 
			// lblPieceName
			// 
			this.lblPieceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPieceName.Location = new System.Drawing.Point(182, 5);
			this.lblPieceName.Name = "lblPieceName";
			this.lblPieceName.Size = new System.Drawing.Size(212, 23);
			this.lblPieceName.TabIndex = 0;
			this.lblPieceName.Text = "Piece Type Name";
			this.lblPieceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBoardVisibility
			// 
			this.lblBoardVisibility.AutoSize = true;
			this.lblBoardVisibility.Location = new System.Drawing.Point(273, 37);
			this.lblBoardVisibility.Name = "lblBoardVisibility";
			this.lblBoardVisibility.Size = new System.Drawing.Size(34, 13);
			this.lblBoardVisibility.TabIndex = 2;
			this.lblBoardVisibility.Text = "0 of 0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(182, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Midgame Value:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(182, 77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Endgame Value:";
			// 
			// lblMidgameValue
			// 
			this.lblMidgameValue.AutoSize = true;
			this.lblMidgameValue.Location = new System.Drawing.Point(273, 57);
			this.lblMidgameValue.Name = "lblMidgameValue";
			this.lblMidgameValue.Size = new System.Drawing.Size(25, 13);
			this.lblMidgameValue.TabIndex = 5;
			this.lblMidgameValue.Text = "100";
			// 
			// lblEndgameValue
			// 
			this.lblEndgameValue.AutoSize = true;
			this.lblEndgameValue.Location = new System.Drawing.Point(273, 77);
			this.lblEndgameValue.Name = "lblEndgameValue";
			this.lblEndgameValue.Size = new System.Drawing.Size(25, 13);
			this.lblEndgameValue.TabIndex = 6;
			this.lblEndgameValue.Text = "100";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(388, 37);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Ave. Mobility:";
			// 
			// lblAveMobility
			// 
			this.lblAveMobility.AutoSize = true;
			this.lblAveMobility.Location = new System.Drawing.Point(464, 37);
			this.lblAveMobility.Name = "lblAveMobility";
			this.lblAveMobility.Size = new System.Drawing.Size(34, 13);
			this.lblAveMobility.TabIndex = 8;
			this.lblAveMobility.Text = "1.000";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(330, 57);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Ave. Directions Attacked:";
			// 
			// lblAveDirectionsAttacked
			// 
			this.lblAveDirectionsAttacked.AutoSize = true;
			this.lblAveDirectionsAttacked.Location = new System.Drawing.Point(464, 57);
			this.lblAveDirectionsAttacked.Name = "lblAveDirectionsAttacked";
			this.lblAveDirectionsAttacked.Size = new System.Drawing.Size(34, 13);
			this.lblAveDirectionsAttacked.TabIndex = 10;
			this.lblAveDirectionsAttacked.Text = "1.000";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(362, 77);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Ave. Safe Checks:";
			// 
			// lblAveSafeChecks
			// 
			this.lblAveSafeChecks.AutoSize = true;
			this.lblAveSafeChecks.Location = new System.Drawing.Point(464, 77);
			this.lblAveSafeChecks.Name = "lblAveSafeChecks";
			this.lblAveSafeChecks.Size = new System.Drawing.Size(34, 13);
			this.lblAveSafeChecks.TabIndex = 12;
			this.lblAveSafeChecks.Text = "1.000";
			// 
			// PieceInfoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblAveSafeChecks);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lblAveDirectionsAttacked);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblAveMobility);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lblEndgameValue);
			this.Controls.Add(this.lblMidgameValue);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblBoardVisibility);
			this.Controls.Add(label1);
			this.Controls.Add(this.lblPieceName);
			this.Name = "PieceInfoControl";
			this.Size = new System.Drawing.Size(527, 117);
			this.Load += new System.EventHandler(this.PieceInfoControl_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PieceInfoControl_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblPieceName;
		private System.Windows.Forms.Label lblBoardVisibility;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblMidgameValue;
		private System.Windows.Forms.Label lblEndgameValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblAveMobility;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblAveDirectionsAttacked;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblAveSafeChecks;
	}
}
