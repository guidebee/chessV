namespace ChessV.GUI
{
	partial class ConfirmationForm
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblConfirmationMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::ChessV.GUI.Properties.Resources.icon_cancel;
			this.btnCancel.Location = new System.Drawing.Point(308, 109);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(113, 32);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "    &Cancel";
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Image = global::ChessV.GUI.Properties.Resources.icon_ok;
			this.btnOK.Location = new System.Drawing.Point(168, 109);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(113, 32);
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "    &OK";
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// lblConfirmationMessage
			// 
			this.lblConfirmationMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblConfirmationMessage.Location = new System.Drawing.Point(12, 9);
			this.lblConfirmationMessage.Name = "lblConfirmationMessage";
			this.lblConfirmationMessage.Size = new System.Drawing.Size(565, 83);
			this.lblConfirmationMessage.TabIndex = 10;
			this.lblConfirmationMessage.Text = "label1";
			this.lblConfirmationMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ConfirmationForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.LemonChiffon;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(589, 153);
			this.Controls.Add(this.lblConfirmationMessage);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ConfirmationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ConfirmationForm";
			this.Load += new System.EventHandler(this.ConfirmationForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblConfirmationMessage;
	}
}