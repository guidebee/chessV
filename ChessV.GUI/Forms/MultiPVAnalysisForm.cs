using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class MultiPVAnalysisForm: Form
	{
		public MultiPVAnalysisForm()
		{
			InitializeComponent();
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.OK;
			NumVariations = (int) numericVariations.Value;
			NumDepth = (int) numericFixedDepth.Value;
			Close();
		}

		public int NumVariations { get; private set; }
		public int NumDepth { get; private set; }
	}
}
