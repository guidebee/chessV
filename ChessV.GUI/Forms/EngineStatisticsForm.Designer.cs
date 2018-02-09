namespace ChessV.GUI
{
	partial class EngineStatisticsForm
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
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblNodesPerSecond = new System.Windows.Forms.Label();
			this.lblNodes = new System.Windows.Forms.Label();
			this.lblQNodes = new System.Windows.Forms.Label();
			this.lblQNodePercent = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(92, 50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Total Nodes:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(75, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Total Q-Nodes:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(103, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Q-Node %:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(51, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(134, 16);
			this.label4.TabIndex = 3;
			this.label4.Text = "Nodes per Second:";
			// 
			// lblNodesPerSecond
			// 
			this.lblNodesPerSecond.AutoSize = true;
			this.lblNodesPerSecond.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNodesPerSecond.Location = new System.Drawing.Point(191, 25);
			this.lblNodesPerSecond.Name = "lblNodesPerSecond";
			this.lblNodesPerSecond.Size = new System.Drawing.Size(16, 16);
			this.lblNodesPerSecond.TabIndex = 4;
			this.lblNodesPerSecond.Text = "0";
			// 
			// lblNodes
			// 
			this.lblNodes.AutoSize = true;
			this.lblNodes.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNodes.Location = new System.Drawing.Point(191, 50);
			this.lblNodes.Name = "lblNodes";
			this.lblNodes.Size = new System.Drawing.Size(16, 16);
			this.lblNodes.TabIndex = 5;
			this.lblNodes.Text = "0";
			// 
			// lblQNodes
			// 
			this.lblQNodes.AutoSize = true;
			this.lblQNodes.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblQNodes.Location = new System.Drawing.Point(191, 75);
			this.lblQNodes.Name = "lblQNodes";
			this.lblQNodes.Size = new System.Drawing.Size(16, 16);
			this.lblQNodes.TabIndex = 6;
			this.lblQNodes.Text = "0";
			// 
			// lblQNodePercent
			// 
			this.lblQNodePercent.AutoSize = true;
			this.lblQNodePercent.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblQNodePercent.Location = new System.Drawing.Point(191, 100);
			this.lblQNodePercent.Name = "lblQNodePercent";
			this.lblQNodePercent.Size = new System.Drawing.Size(16, 16);
			this.lblQNodePercent.TabIndex = 7;
			this.lblQNodePercent.Text = "0";
			// 
			// EngineStatisticsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 147);
			this.Controls.Add(this.lblQNodePercent);
			this.Controls.Add(this.lblQNodes);
			this.Controls.Add(this.lblNodes);
			this.Controls.Add(this.lblNodesPerSecond);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EngineStatisticsForm";
			this.Text = "Internal Engine Statistics";
			this.Load += new System.EventHandler(this.EngineStatisticsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblNodesPerSecond;
		private System.Windows.Forms.Label lblNodes;
		private System.Windows.Forms.Label lblQNodes;
		private System.Windows.Forms.Label lblQNodePercent;
	}
}