
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	public partial class GameSettingsForm: Form
	{
		public TimeControl TimeControl { get; private set; }
		public List<EngineConfigurationWithAdaptor> EngineConfigurations { get; private set; }
		public int ComputerPlayerCount { get; private set; }
		public int ComputerSide { get; private set; }

		protected Game game;
		protected List<EngineConfigurationWithAdaptor> engines;
		protected Dictionary<string, EngineConfigurationWithAdaptor> engineLookup;

		public GameSettingsForm( Game game )
		{
			this.game = game;
			InitializeComponent();
			labelGameTitle.Text = game.Name;
			if( game.GameAttribute.GameDescription1 != null )
				lblGameDescription1.Text = game.GameAttribute.GameDescription1;
			else if( game.InventedBy != null && game.InventedBy != "Unknown" )
				lblGameDescription1.Text = "Invented by " + game.InventedBy;
			else
				lblGameDescription1.Text = "Inventor not known";
			if( game.GameAttribute.GameDescription2 != null )
				lblGameDescription2.Text = game.GameAttribute.GameDescription2;
			else if( game.Invented != null && game.Invented != "Unknown" )
				lblGameDescription2.Text = game.Invented;
			else
				lblGameDescription2.Text = "";
			engines = Program.Manager.EngineLibrary.FindEngines( game );
			EngineConfigurations = new List<EngineConfigurationWithAdaptor>();
		}

		private void optTimeUnlimited_CheckedChanged( object sender, EventArgs e )
		{
			if( optTimeUnlimited.Checked )
				panelUnlimited.BringToFront();
		}

		private void optTimeTimePerGame_CheckedChanged( object sender, EventArgs e )
		{
			if( optTimeTimePerGame.Checked )
				panelTimePerGame.BringToFront();
		}

		private void optTimeFixedPerMove_CheckedChanged( object sender, EventArgs e )
		{
			if( optTimeFixedPerMove.Checked )
				panelTimePerMove.BringToFront();
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			game.StartMatch();
			TimeControl = new TimeControl();
			if( optTimeUnlimited.Checked )
			{
				TimeControl.Infinite = true;
			}
			else if( optTimeTimePerGame.Checked )
			{
				try
				{
					Int64 basetime =
						(txtTimeBaseMin.Text == "" ? 0 : Convert.ToInt64( txtTimeBaseMin.Text ) * 1000 * 60) +
						(txtTimeBaseSec.Text == "" ? 0 : Convert.ToInt64( txtTimeBaseSec.Text ) * 1000) +
						(txtTimeBaseMSec.Text == "" ? 0 : Convert.ToInt64( txtTimeBaseMSec.Text ));

					Int64 increment =
						(txtTimeIncrementMin.Text == "" ? 0 : Convert.ToInt64( txtTimeIncrementMin.Text ) * 1000 * 60) +
						(txtTimeIncrementSec.Text == "" ? 0 : Convert.ToInt64( txtTimeIncrementSec.Text ) * 1000) +
						(txtTimeIncrementMSec.Text == "" ? 0 : Convert.ToInt64( txtTimeIncrementMSec.Text));

					TimeControl.TimePerTC = basetime;
					if( increment > 0 )
						TimeControl.TimeIncrement = increment;
				}
				catch
				{
					MessageBox.Show( "Time control field does not contain an integer" );
					return;
				}
			}
			else if( optTimeFixedPerMove.Checked )
			{
				Int64 time = 
					(txtTimeMoveMin.Text == "" ? 0 : Convert.ToInt64( txtTimeMoveMin.Text ) * 1000 * 60) +
					(txtTimeMoveSec.Text == "" ? 0 : Convert.ToInt64( txtTimeMoveSec.Text ) * 1000) +
					(txtTimeMoveMSec.Text == "" ? 0 : Convert.ToInt64( txtTimeMoveMSec.Text ));
				TimeControl.TimePerMove = time;
			}
			else if( optTimeFixedDepth.Checked )
			{
				TimeControl.Infinite = true;
				TimeControl.PlyLimit = Convert.ToInt32( txtFixedDepth.Text );
			}
			else if( optTimeFixedNodes.Checked )
			{
				TimeControl.Infinite = true;
				TimeControl.NodeLimit = Convert.ToInt64( 200000 );
			}

			if( optPlayersPlayerVsComputer.Checked )
			{
				ComputerPlayerCount = 1;
				ComputerSide = (string) pickComputerSide.SelectedItem == "White" ? 0 : 1;
				if( (string) pickEngine.SelectedItem != "ChessV" )
					EngineConfigurations.Add( engineLookup[(string) pickEngine.SelectedItem] );
				else
					EngineConfigurations.Add( new EngineConfigurationWithAdaptor( Program.Manager.InternalEngine, null ) );
			}
			else if( optPlayersTwoComputers.Checked )
			{
				ComputerPlayerCount = 2;
				if( (string) pickEngine1.SelectedItem != "ChessV" )
					EngineConfigurations.Add( engineLookup[(string) pickEngine1.SelectedItem] );
				else
					EngineConfigurations.Add( new EngineConfigurationWithAdaptor( Program.Manager.InternalEngine, null ) );
				if( (string) pickEngine2.SelectedItem != "ChessV" )
					EngineConfigurations.Add( engineLookup[(string) pickEngine2.SelectedItem] );
				else
					EngineConfigurations.Add( new EngineConfigurationWithAdaptor( Program.Manager.InternalEngine, null ) );
			}
			else
				ComputerPlayerCount = 0;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void GameSettingsForm_Load( object sender, EventArgs e )
		{
			pickComputerSide.Items.Add( "White" );
			pickComputerSide.Items.Add( "Black" );
			pickComputerSide.SelectedItem = "Black";

			pickEngine.Items.Add( "ChessV" );
			pickEngine.SelectedItem = "ChessV";

			pickEngine1.Items.Add( "ChessV" );
			pickEngine1.SelectedItem = "ChessV";

			pickEngine2.Items.Add( "ChessV" );
			pickEngine2.SelectedItem = "ChessV";

			engineLookup = new Dictionary<string, EngineConfigurationWithAdaptor>();
			foreach( EngineConfigurationWithAdaptor engine in engines )
			{
				pickEngine.Items.Add( engine.Configuration.FriendlyName );
				pickEngine1.Items.Add( engine.Configuration.FriendlyName );
				pickEngine2.Items.Add( engine.Configuration.FriendlyName );
				if( !engineLookup.ContainsKey( engine.Configuration.FriendlyName ) )
					engineLookup.Add( engine.Configuration.FriendlyName, engine );
			}
		}

		private void numbersOnly_KeyPress( object sender, KeyPressEventArgs e )
		{
			if( e.KeyChar != '\b' && !System.Text.RegularExpressions.Regex.IsMatch( e.KeyChar.ToString(), "\\d+" ) )
				e.Handled = true;
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void optPlayersPlayerVsComputer_CheckedChanged( object sender, EventArgs e )
		{
			if( optPlayersPlayerVsComputer.Checked )
				panelPlayerVsComputer.BringToFront();
		}

		private void optPlayersTwoComputers_CheckedChanged( object sender, EventArgs e )
		{
			if( optPlayersTwoComputers.Checked )
				panelTwoComputers.BringToFront();
		}

		private void optPlayersTwoPeople_CheckedChanged( object sender, EventArgs e )
		{
			if( optPlayersTwoPeople.Checked )
				panelTwoPeople.BringToFront();
		}

		private void optTimeFixedDepth_CheckedChanged( object sender, EventArgs e )
		{
			if( optTimeFixedDepth.Checked )
				panelFixedDepth.BringToFront();
		}
	}
}
