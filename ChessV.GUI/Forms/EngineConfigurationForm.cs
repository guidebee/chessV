
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
using System.IO;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class EngineConfigurationForm: Form
	{
		// *** CONSTRUCTION *** //

		public EngineConfigurationForm( EngineConfiguration engineConfig )
		{
			engine = engineConfig;

			InitializeComponent();
		}


		// *** EVENT HANDLERS *** //

		//	form load event handler
		private void EngineConfigurationForm_Load( object sender, EventArgs e )
		{
			lblEngineName.Text = engine.InternalName + " (" + engine.FriendlyName + ")";
			txtFriendlyName.Text = engine.FriendlyName;
			txtExecutable.Text = engine.Command;
			txtWorkingDirectory.Text = engine.WorkingDirectory;
			foreach( string argument in engine.Arguments )
				listArguments.Items.Add( argument );
			foreach( string initString in engine.InitStrings )
				listInitStrings.Items.Add( initString );
			pickProtocol.SelectedIndex = 0;
			switch( engine.Restart )
			{
				case EngineConfiguration.RestartMode.Auto:
					pickRestartMode.SelectedItem = "Automatic";
					break;

				case EngineConfiguration.RestartMode.On:
					pickRestartMode.SelectedItem = "Always Restart";
					break;

				case EngineConfiguration.RestartMode.Off:
					pickRestartMode.SelectedItem = "Never Restart";
					break;
			}
			foreach( string supportedFeature in engine.SupportedFeatures )
				listSupportedFeatures.Items.Add( supportedFeature );
			foreach( string supportedVariant in engine.SupportedVariants )
				listSupportedVariants.Items.Add( supportedVariant );
			chkValidateClaims.Checked = engine.ClaimsValidated;
		}

		//	OK button click event handler
		private void btnOK_Click( object sender, EventArgs e )
		{
			//	validate command
			if( !File.Exists( txtExecutable.Text ) )
			{
				MessageBox.Show( "The specified command is invalid\nIt must reference an existing EXE" );
				DialogResult = DialogResult.None;
				return;
			}

			//	validate working directory
			if( !Directory.Exists( txtWorkingDirectory.Text ) )
			{
				MessageBox.Show( "The specified working directory is invalid" );
				DialogResult = DialogResult.None;
				return;
			}

			engine.Command = txtExecutable.Text;
			engine.WorkingDirectory = txtWorkingDirectory.Text;
			engine.FriendlyName = txtFriendlyName.Text;
			if( (string) pickRestartMode.SelectedItem == "Automatic" )
				engine.Restart = EngineConfiguration.RestartMode.Auto;
			else if( (string) pickRestartMode.SelectedItem == "Always Restart" )
				engine.Restart = EngineConfiguration.RestartMode.On;
			else
				engine.Restart = EngineConfiguration.RestartMode.Off;
			engine.ClaimsValidated = chkValidateClaims.Checked;
			engine.SaveToRegistry();
		}

		//	cancel button click event handler
		private void btnCancel_Click( object sender, EventArgs e )
		{
			//	reload from registry to restore the settings before we changed them
			engine.LoadFromRegistry();
			//	cancel out of the dialog box
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void txtFriendlyName_TextChanged( object sender, EventArgs e )
		{
			lblEngineName.Text = engine.InternalName + " (" + txtFriendlyName.Text + ")";
		}

		//	new argument button click event handler
		private void btnNewArgument_Click( object sender, EventArgs e )
		{
			StringEditForm form = new StringEditForm();
			form.StringName = "Command Line Argument";
			if( form.ShowDialog() == DialogResult.OK )
				if( form.StringValue.Length > 0 )
				{
					engine.AddArgument( form.StringValue );
					listArguments.Items.Add( form.StringValue );
				}
		}

		//	edit argument button click event handler
		private void btnEditArgument_Click( object sender, EventArgs e )
		{
			string argument = (string) listArguments.SelectedItem;
			if( argument != null )
			{
				StringEditForm form = new StringEditForm();
				form.StringName = "Command Line Argument";
				form.StringValue = argument;
				if( form.ShowDialog() == DialogResult.OK )
				{
					engine.RemoveArgument( argument );
					int index = listArguments.SelectedIndex;
					listArguments.Items.Remove( argument );
					if( form.StringValue.Length > 0 )
					{
						engine.AddArgument( form.StringValue );
						listArguments.Items.Insert( index, form.StringValue );
					}
				}
			}
		}

		//	remove argument button click event handler
		private void btnRemoveArgument_Click( object sender, EventArgs e )
		{
			string argument = (string) listArguments.SelectedItem;
			if( argument != null )
			{
				engine.RemoveArgument( argument );
				listArguments.Items.Remove( argument );
			}
		}

		//	new init string button click event handler
		private void btnNewInitString_Click( object sender, EventArgs e )
		{
			StringEditForm form = new StringEditForm();
			form.StringName = "Initialization String";
			if( form.ShowDialog() == DialogResult.OK )
				if( form.StringValue.Length > 0 )
				{
					engine.AddInitString( form.StringValue );
					listInitStrings.Items.Add( form.StringValue );
				}
		}

		//	edit init string button click event handler
		private void btnEditInitString_Click( object sender, EventArgs e )
		{
			string initString = (string) listArguments.SelectedItem;
			if( initString != null )
			{
				StringEditForm form = new StringEditForm();
				form.StringName = "Initialization String";
				form.StringValue = initString;
				if( form.ShowDialog() == DialogResult.OK )
				{
					engine.RemoveInitString( initString );
					int index = listInitStrings.SelectedIndex;
					listInitStrings.Items.Remove( initString );
					if( form.StringValue.Length > 0 )
					{
						engine.AddInitString( form.StringValue );
						listInitStrings.Items.Insert( index, form.StringValue );
					}
				}
			}
		}

		//	remove init string button click event handler
		private void btnRemoveInitString_Click( object sender, EventArgs e )
		{
			string initString = (string) listInitStrings.SelectedItem;
			if( initString != null )
			{
				engine.RemoveInitString( initString );
				listInitStrings.Items.Remove( initString );
			}
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected EngineConfiguration engine;
	}
}
