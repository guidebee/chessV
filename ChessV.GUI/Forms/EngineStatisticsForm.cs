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
	public partial class EngineStatisticsForm: Form
	{
		public EngineStatisticsForm( Statistics statistics )
		{
			Statistics = statistics;

			InitializeComponent();
		}

		private void EngineStatisticsForm_Load( object sender, EventArgs e )
		{
		}

		public void UpdateStatistics()
		{
			lblNodes.Text = Statistics.Nodes.ToString( "N0" );
			lblQNodes.Text = Statistics.QNodes.ToString( "N0" );
			lblQNodePercent.Text = ((double) Statistics.QNodes / (double) Statistics.Nodes * 100.0).ToString( "N2" ) + "%";
			TimeSpan elapsedTime = DateTime.Now - Statistics.SearchStartTime;
			lblNodesPerSecond.Text = (Statistics.Nodes / elapsedTime.TotalSeconds / 1000).ToString( "N" ) + "k";
		}

		public Statistics Statistics { get; private set; }
	}
}
