
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
using Microsoft.Win32;

namespace ChessV.GUI
{
	public partial class EngineListForm: Form
	{
		public EngineListForm()
		{
			InitializeComponent();
		}

		private void EngineListForm_Load( object sender, EventArgs e )
		{
			chkAutodetectEngines.Checked = RegistrySettings.AutodetectNewEngines;
			populateEngineList();
		}

		private void btnNew_Click( object sender, EventArgs e )
		{
			NewEngineForm form = new NewEngineForm();
			if( form.ShowDialog() == DialogResult.OK )
			{
				//	refresh list of engines
				lvEngineList.Items.Clear();
				populateEngineList();
			}
		}

		private void btnModify_Click( object sender, EventArgs e )
		{
			if( lvEngineList.SelectedItems.Count == 0 )
			{
				MessageBox.Show( "You must select an engine from the list" );
				return;
			}
			EngineConfiguration engine = (EngineConfiguration) lvEngineList.SelectedItems[0].Tag;
			EngineConfigurationForm engineForm = new EngineConfigurationForm( engine );
			if( engineForm.ShowDialog() == DialogResult.OK )
			{
				//	Update the entry in the list
				lvEngineList.SelectedItems[0].SubItems[0].Text = engine.FriendlyName;
				lvEngineList.SelectedItems[0].SubItems[1].Text = engine.InternalName;
				lvEngineList.SelectedItems[0].SubItems[2].Text = engine.Protocol;
				lvEngineList.SelectedItems[0].SubItems[3].Text = engine.Command;
			}
		}

		private void btnDone_Click( object sender, EventArgs e )
		{
			RegistrySettings.AutodetectNewEngines = chkAutodetectEngines.Checked;
		}

		private void btnRemove_Click( object sender, EventArgs e )
		{
			if( lvEngineList.SelectedItems.Count == 0 )
			{
				MessageBox.Show( "No engine is selected" );
				return;
			}
			ConfirmationForm form = new ConfirmationForm();
			form.ConfirmationMessage =
				"You are about to remove this engine and all configuration information from ChessV.\n" +
				"No files will be deleted, so the engine may be auto-detected next time you start ChessV.\n" + 
				"Do you wish to proceed?";
			if( form.ShowDialog() == DialogResult.OK )
			{
				EngineConfiguration engine = (EngineConfiguration) lvEngineList.SelectedItems[0].Tag;
				//	Remove the engine's registry key
				string engineKeyName = engine.RegistryKey.Name;
				engine.RegistryKey.Close();
				int first = engineKeyName.IndexOf( '\\' );
				int last = engineKeyName.LastIndexOf( '\\' );
				string parentKeyName = engineKeyName.Substring( first + 1, last - first );
				string subKeyName = engineKeyName.Substring( last + 1 );
				RegistryKey parentKey = Registry.CurrentUser.OpenSubKey( parentKeyName, true );
				parentKey.DeleteSubKeyTree( subKeyName );
				//	Remove the engine from the library
				Program.Manager.EngineLibrary.RemoveEngine( engine );
				//	Rename the numbered registry keys after this one
				//	(if any) to fill the gap
				bool keysRelocated = false;
				int keyNumber = Convert.ToInt32( subKeyName ) + 1;
				while( stringArrayContainsValue( parentKey.GetSubKeyNames(), keyNumber.ToString() ) )
				{
					//	There is no function to rename a Registry key, so 
					//	we need to make a new key, copy all the values, 
					//	and then delete the old key.  Pain in the butt.
					RegistryKey destinationKey = parentKey.CreateSubKey( (keyNumber-1).ToString() );
					RegistryKey sourceKey = parentKey.OpenSubKey( keyNumber.ToString() );
					//	Copy all values
					foreach( string valueName in sourceKey.GetValueNames() )
					{
						object objValue = sourceKey.GetValue( valueName );
						RegistryValueKind valKind = sourceKey.GetValueKind( valueName );
						destinationKey.SetValue( valueName, objValue, valKind );
					}
					sourceKey.Close();
					destinationKey.Close();
					parentKey.DeleteSubKeyTree( keyNumber.ToString() );
					Program.Manager.EngineLibrary.RegistryKeyRenamed( parentKeyName, keyNumber.ToString(), (keyNumber - 1).ToString() );
					keyNumber++;
					keysRelocated = true;
				}
				//	If this was the only sub-key in the parent key,
				//	then we'll delete the parent key
				if( keyNumber == 2 && !keysRelocated )
				{
					parentKeyName = parentKeyName.Substring( 0, parentKeyName.Length - 1 );
					last = parentKeyName.LastIndexOf( '\\' );
					string grandParentKeyName = parentKeyName.Substring( 0, last );
					parentKeyName = parentKeyName.Substring( last + 1 );
					RegistryKey grandParentKey = Registry.CurrentUser.OpenSubKey( grandParentKeyName, true );
					grandParentKey.DeleteSubKeyTree( parentKeyName );
				}
				//	Remove the entry from the list
				lvEngineList.Items.Remove( lvEngineList.SelectedItems[0] );
			}
		}

		private bool stringArrayContainsValue( string[] array, string value )
		{
			foreach( string s in array )
				if( s.ToUpper() == value.ToUpper() )
					return true;
			return false;
		}

		private void populateEngineList()
		{
			foreach( EngineConfiguration engine in Program.Manager.EngineLibrary.GetAllEngines() )
			{
				ListViewItem lvi = new ListViewItem( engine.FriendlyName );
				lvi.SubItems.Add( engine.InternalName );
				lvi.SubItems.Add( engine.Protocol );
				lvi.SubItems.Add( engine.Command );
				lvi.Tag = engine;
				lvEngineList.Items.Add( lvi );
			}
		}
	}
}
