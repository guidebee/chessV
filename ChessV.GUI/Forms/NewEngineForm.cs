
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
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class NewEngineForm: Form
	{
		public NewEngineForm()
		{
			InitializeComponent();
		}

		private void NewEngineForm_Load( object sender, EventArgs e )
		{
			showingMore = false;
			Size = new Size( 582, 196 );
			arguments = new List<string>();
		}

		private void btnMoreLess_Click( object sender, EventArgs e )
		{
			if( !showingMore )
			{
				Size = new Size( 582, 364 );
				btnMoreLess.Text = "    Less";
				showingMore = true;
				btnMoreLess.Image = global::ChessV.GUI.Properties.Resources.icon_less;
				label5.Visible = true;
				listArguments.Visible = true;
				btnNewArgument.Visible = true;
				btnEditArgument.Visible = true;
				btnRemoveArgument.Visible = true;
			}
			else
			{
				Size = new Size( 582, 196 );
				btnMoreLess.Text = "    More";
				showingMore = false;
				btnMoreLess.Image = global::ChessV.GUI.Properties.Resources.icon_more;
				label5.Visible = false;
				listArguments.Visible = false;
				btnNewArgument.Visible = false;
				btnEditArgument.Visible = false;
				btnRemoveArgument.Visible = false;
			}
		}

		private void btnBrowseExecutable_Click( object sender, EventArgs e )
		{
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				txtExecutable.Text = openFileDialog.FileName;
				txtWorkingDirectory.Text = Path.GetDirectoryName( openFileDialog.FileName );
			}
		}

		private void btnBrowseWorkingDirectory_Click( object sender, EventArgs e )
		{
			if( folderBrowserDialog.ShowDialog() == DialogResult.OK )
			{
				txtWorkingDirectory.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnDiscoverEngine_Click( object sender, EventArgs e )
		{
			Program.Manager.EngineLibrary.ManualEngineDiscover( txtWorkingDirectory.Text, txtExecutable.Text, arguments );
			timer.Start();
		}

		private void timer_Tick( object sender, EventArgs e )
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		//	new argument button click event handler
		private void btnNewArgument_Click( object sender, EventArgs e )
		{
			StringEditForm form = new StringEditForm();
			form.StringName = "Command Line Argument";
			if( form.ShowDialog() == DialogResult.OK )
				if( form.StringValue.Length > 0 )
					listArguments.Items.Add( form.StringValue );
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
					int index = listArguments.SelectedIndex;
					listArguments.Items.Remove( argument );
					if( form.StringValue.Length > 0 )
						listArguments.Items.Insert( index, form.StringValue );
				}
			}
		}

		//	remove argument button click event handler
		private void btnRemoveArgument_Click( object sender, EventArgs e )
		{
			string argument = (string) listArguments.SelectedItem;
			if( argument != null )
				listArguments.Items.Remove( argument );
		}

		private bool showingMore;
		private List<string> arguments;
	}
}
