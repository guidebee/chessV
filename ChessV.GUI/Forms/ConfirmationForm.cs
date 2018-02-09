using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class ConfirmationForm: Form
	{
		public ConfirmationForm()
		{
			InitializeComponent();
		}

		private void ConfirmationForm_Load( object sender, EventArgs e )
		{
			lblConfirmationMessage.Text = ConfirmationMessage;
		}

		public string ConfirmationMessage;
	}
}
