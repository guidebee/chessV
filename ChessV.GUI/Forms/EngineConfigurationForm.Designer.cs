
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
	partial class EngineConfigurationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EngineConfigurationForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRemoveInitString = new System.Windows.Forms.Button();
			this.btnEditInitString = new System.Windows.Forms.Button();
			this.btnNewInitString = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.listInitStrings = new System.Windows.Forms.ListBox();
			this.btnRemoveArgument = new System.Windows.Forms.Button();
			this.btnEditArgument = new System.Windows.Forms.Button();
			this.btnNewArgument = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.listArguments = new System.Windows.Forms.ListBox();
			this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
			this.txtExecutable = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pickProtocol = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkValidateClaims = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.listSupportedVariants = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.listSupportedFeatures = new System.Windows.Forms.ListBox();
			this.pickRestartMode = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblEngineName = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.txtFriendlyName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRemoveInitString);
			this.groupBox1.Controls.Add(this.btnEditInitString);
			this.groupBox1.Controls.Add(this.btnNewInitString);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.listInitStrings);
			this.groupBox1.Controls.Add(this.btnRemoveArgument);
			this.groupBox1.Controls.Add(this.btnEditArgument);
			this.groupBox1.Controls.Add(this.btnNewArgument);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.listArguments);
			this.groupBox1.Controls.Add(this.txtWorkingDirectory);
			this.groupBox1.Controls.Add(this.txtExecutable);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 164);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(705, 270);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Execution";
			// 
			// btnRemoveInitString
			// 
			this.btnRemoveInitString.Location = new System.Drawing.Point(540, 227);
			this.btnRemoveInitString.Name = "btnRemoveInitString";
			this.btnRemoveInitString.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveInitString.TabIndex = 11;
			this.btnRemoveInitString.Text = "Remove";
			this.btnRemoveInitString.UseVisualStyleBackColor = true;
			this.btnRemoveInitString.Click += new System.EventHandler(this.btnRemoveInitString_Click);
			// 
			// btnEditInitString
			// 
			this.btnEditInitString.Location = new System.Drawing.Point(457, 227);
			this.btnEditInitString.Name = "btnEditInitString";
			this.btnEditInitString.Size = new System.Drawing.Size(75, 23);
			this.btnEditInitString.TabIndex = 10;
			this.btnEditInitString.Text = "Edit";
			this.btnEditInitString.UseVisualStyleBackColor = true;
			this.btnEditInitString.Click += new System.EventHandler(this.btnEditInitString_Click);
			// 
			// btnNewInitString
			// 
			this.btnNewInitString.Location = new System.Drawing.Point(374, 227);
			this.btnNewInitString.Name = "btnNewInitString";
			this.btnNewInitString.Size = new System.Drawing.Size(75, 23);
			this.btnNewInitString.TabIndex = 9;
			this.btnNewInitString.Text = "New";
			this.btnNewInitString.UseVisualStyleBackColor = true;
			this.btnNewInitString.Click += new System.EventHandler(this.btnNewInitString_Click);
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Khaki;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(374, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(241, 23);
			this.label6.TabIndex = 11;
			this.label6.Text = "Initialization Strings";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listInitStrings
			// 
			this.listInitStrings.FormattingEnabled = true;
			this.listInitStrings.Location = new System.Drawing.Point(374, 126);
			this.listInitStrings.Name = "listInitStrings";
			this.listInitStrings.Size = new System.Drawing.Size(241, 95);
			this.listInitStrings.TabIndex = 8;
			// 
			// btnRemoveArgument
			// 
			this.btnRemoveArgument.Location = new System.Drawing.Point(244, 227);
			this.btnRemoveArgument.Name = "btnRemoveArgument";
			this.btnRemoveArgument.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveArgument.TabIndex = 7;
			this.btnRemoveArgument.Text = "Remove";
			this.btnRemoveArgument.UseVisualStyleBackColor = true;
			this.btnRemoveArgument.Click += new System.EventHandler(this.btnRemoveArgument_Click);
			// 
			// btnEditArgument
			// 
			this.btnEditArgument.Location = new System.Drawing.Point(161, 227);
			this.btnEditArgument.Name = "btnEditArgument";
			this.btnEditArgument.Size = new System.Drawing.Size(75, 23);
			this.btnEditArgument.TabIndex = 6;
			this.btnEditArgument.Text = "Edit";
			this.btnEditArgument.UseVisualStyleBackColor = true;
			this.btnEditArgument.Click += new System.EventHandler(this.btnEditArgument_Click);
			// 
			// btnNewArgument
			// 
			this.btnNewArgument.Location = new System.Drawing.Point(78, 227);
			this.btnNewArgument.Name = "btnNewArgument";
			this.btnNewArgument.Size = new System.Drawing.Size(75, 23);
			this.btnNewArgument.TabIndex = 5;
			this.btnNewArgument.Text = "New";
			this.btnNewArgument.UseVisualStyleBackColor = true;
			this.btnNewArgument.Click += new System.EventHandler(this.btnNewArgument_Click);
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Khaki;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(78, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(241, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Command Line Arguments";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listArguments
			// 
			this.listArguments.FormattingEnabled = true;
			this.listArguments.Location = new System.Drawing.Point(78, 126);
			this.listArguments.Name = "listArguments";
			this.listArguments.Size = new System.Drawing.Size(241, 95);
			this.listArguments.TabIndex = 4;
			// 
			// txtWorkingDirectory
			// 
			this.txtWorkingDirectory.Location = new System.Drawing.Point(183, 62);
			this.txtWorkingDirectory.Name = "txtWorkingDirectory";
			this.txtWorkingDirectory.ReadOnly = true;
			this.txtWorkingDirectory.Size = new System.Drawing.Size(388, 20);
			this.txtWorkingDirectory.TabIndex = 2;
			// 
			// txtExecutable
			// 
			this.txtExecutable.Location = new System.Drawing.Point(183, 22);
			this.txtExecutable.Name = "txtExecutable";
			this.txtExecutable.ReadOnly = true;
			this.txtExecutable.Size = new System.Drawing.Size(388, 20);
			this.txtExecutable.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(82, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Working Directory:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(114, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Executable:";
			// 
			// pickProtocol
			// 
			this.pickProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickProtocol.FormattingEnabled = true;
			this.pickProtocol.Items.AddRange(new object[] {
            "xboard"});
			this.pickProtocol.Location = new System.Drawing.Point(175, 22);
			this.pickProtocol.Name = "pickProtocol";
			this.pickProtocol.Size = new System.Drawing.Size(128, 21);
			this.pickProtocol.TabIndex = 0;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(120, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(49, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Protocol:";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.LemonChiffon;
			this.groupBox2.Controls.Add(this.chkValidateClaims);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.listSupportedVariants);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.listSupportedFeatures);
			this.groupBox2.Controls.Add(this.pickRestartMode);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.pickProtocol);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(12, 440);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(705, 241);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Capabilities";
			// 
			// chkValidateClaims
			// 
			this.chkValidateClaims.AutoSize = true;
			this.chkValidateClaims.Location = new System.Drawing.Point(299, 207);
			this.chkValidateClaims.Name = "chkValidateClaims";
			this.chkValidateClaims.Size = new System.Drawing.Size(97, 17);
			this.chkValidateClaims.TabIndex = 4;
			this.chkValidateClaims.Text = "Validate Claims";
			this.chkValidateClaims.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Khaki;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(374, 67);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(241, 23);
			this.label10.TabIndex = 6;
			this.label10.Text = "Supported Variants";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listSupportedVariants
			// 
			this.listSupportedVariants.FormattingEnabled = true;
			this.listSupportedVariants.Location = new System.Drawing.Point(374, 89);
			this.listSupportedVariants.Name = "listSupportedVariants";
			this.listSupportedVariants.Size = new System.Drawing.Size(241, 95);
			this.listSupportedVariants.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Khaki;
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(78, 67);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(241, 23);
			this.label9.TabIndex = 4;
			this.label9.Text = "Supported Features";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listSupportedFeatures
			// 
			this.listSupportedFeatures.FormattingEnabled = true;
			this.listSupportedFeatures.Location = new System.Drawing.Point(78, 89);
			this.listSupportedFeatures.Name = "listSupportedFeatures";
			this.listSupportedFeatures.Size = new System.Drawing.Size(241, 95);
			this.listSupportedFeatures.TabIndex = 1;
			// 
			// pickRestartMode
			// 
			this.pickRestartMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pickRestartMode.FormattingEnabled = true;
			this.pickRestartMode.Items.AddRange(new object[] {
            "Automatic",
            "Always Restart",
            "Never Restart"});
			this.pickRestartMode.Location = new System.Drawing.Point(433, 22);
			this.pickRestartMode.Name = "pickRestartMode";
			this.pickRestartMode.Size = new System.Drawing.Size(128, 21);
			this.pickRestartMode.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(353, 25);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(74, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Restart Mode:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(304, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(193, 24);
			this.label1.TabIndex = 9;
			this.label1.Text = "Settings for Engine:";
			// 
			// lblEngineName
			// 
			this.lblEngineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEngineName.Location = new System.Drawing.Point(304, 63);
			this.lblEngineName.Name = "lblEngineName";
			this.lblEngineName.Size = new System.Drawing.Size(395, 25);
			this.lblEngineName.TabIndex = 10;
			this.lblEngineName.Text = "Engine Name";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(216, 695);
			this.btnOK.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnOK.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(123, 32);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "     &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
			this.btnCancel.Location = new System.Drawing.Point(380, 695);
			this.btnCancel.MaximumSize = new System.Drawing.Size(123, 32);
			this.btnCancel.MinimumSize = new System.Drawing.Size(123, 32);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(123, 32);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "    &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Image = global::ChessV.GUI.Properties.Resources.EngineIcon;
			this.pictureBox1.ImageLocation = "";
			this.pictureBox1.Location = new System.Drawing.Point(205, 10);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(84, 100);
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			// 
			// txtFriendlyName
			// 
			this.txtFriendlyName.Location = new System.Drawing.Point(315, 128);
			this.txtFriendlyName.Name = "txtFriendlyName";
			this.txtFriendlyName.Size = new System.Drawing.Size(180, 20);
			this.txtFriendlyName.TabIndex = 0;
			this.txtFriendlyName.TextChanged += new System.EventHandler(this.txtFriendlyName_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(234, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 13);
			this.label2.TabIndex = 12;
			this.label2.Text = "Display Name:";
			// 
			// EngineConfigurationForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(729, 739);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtFriendlyName);
			this.Controls.Add(this.lblEngineName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EngineConfigurationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Engine Configuration";
			this.Load += new System.EventHandler(this.EngineConfigurationForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtWorkingDirectory;
		private System.Windows.Forms.TextBox txtExecutable;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnRemoveInitString;
		private System.Windows.Forms.Button btnEditInitString;
		private System.Windows.Forms.Button btnNewInitString;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ListBox listInitStrings;
		private System.Windows.Forms.Button btnRemoveArgument;
		private System.Windows.Forms.Button btnEditArgument;
		private System.Windows.Forms.Button btnNewArgument;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listArguments;
		private System.Windows.Forms.ComboBox pickProtocol;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox pickRestartMode;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListBox listSupportedVariants;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listSupportedFeatures;
		private System.Windows.Forms.CheckBox chkValidateClaims;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblEngineName;
		private System.Windows.Forms.TextBox txtFriendlyName;
		private System.Windows.Forms.Label label2;
	}
}