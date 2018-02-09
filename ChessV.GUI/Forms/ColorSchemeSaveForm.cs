
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
using Microsoft.Win32;
using ChessV;

namespace ChessV.GUI
{
	public partial class ColorSchemeSaveForm: Form
	{
		// *** PROPERTIES *** //

		//	Returns whether the scheme is saved as a new scheme
		//	(as opposed to overwriting the existing scheme)
		public bool NewScheme { get; private set; }


		// *** CONSTRUCTION *** //

		public ColorSchemeSaveForm( ColorScheme colorScheme )
		{
			this.colorScheme = colorScheme;

			InitializeComponent();
		}


		// *** EVENT HANDLERS *** //

		//	Form load event handler
		private void ColorSchemeSaveForm_Load( object sender, EventArgs e )
		{
			if( colorScheme.Name == "(custom)" )
			{
				//	handling for a "custom" (i.e., nameless) scheme - 
				//	we can only Save As, not Overwrite
				optSaveAs.Checked = true;
				optOverwrite.Enabled = false;
			}
			else
			{
				//	remove the (modified) from the end of the scheme name
				//	since if we save it, it will no longer be modified
				//	(and we shouldn't save it with "modified" in the name)
				string schemeName = colorScheme.Name.Replace( " (modified)", "" );
				optOverwrite.Text += " " + schemeName;
				//	disable the new scheme name text box until the 
				//	user selects the Save As radio button
				txtNewSchemeName.Enabled = false;
			}
		}

		//	Save As radio button check changed event handler
		private void optSaveAs_CheckedChanged( object sender, EventArgs e )
		{
			if( optSaveAs.Checked )
				//	enable the scheme name text box if Save As is selected
				txtNewSchemeName.Enabled = true;
		}

		//	Overwrite radio button check changed event handler
		private void optOverwrite_CheckedChanged( object sender, EventArgs e )
		{
			if( optOverwrite.Checked )
				//	disable the scheme name text box if Save As is not selected
				txtNewSchemeName.Enabled = false;
		}

		//	Cancel button clicked event handler
		private void btnCancel_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		//	OK button clicked event handler
		private void btnOK_Click( object sender, EventArgs e )
		{
			if( optSaveAs.Checked )
			{
				//	If we're saving this as a new scheme ...
				if( txtNewSchemeName.Text.Length < 1 )
				{
					MessageBox.Show( "You must provide a name for the new color scheme" );
					return;
				}
				if( ColorSchemeLibrary.Contains( txtNewSchemeName.Text ) )
				{
					MessageBox.Show( "A color scheme with that name already exists" );
					return;
				}
				else
				{
					colorScheme.Name = txtNewSchemeName.Text;
					colorScheme.Modified = false;
					ColorSchemeLibrary.NewScheme( colorScheme );
					NewScheme = true;
				}
			}
			else
			{
				//	We're overwriting the existing color scheme (if it's been modified)
				//	NOTE: we shouldn't be here if the scheme has not been modified; in 
				//	that case, the Save button on the AppearanceSettingsForm should 
				//	have been disabled.
				if( colorScheme.Modified )
				{
					//	since we're now saving it, it's no longer modified
					colorScheme.Modified = false;
					//	remove the (modified) from the name
					colorScheme.Name = colorScheme.Name.Replace( " (modified)", "" );
					//	update the color scheme in the library (which will 
					//	update it in the registry)
					ColorSchemeLibrary.UpdateScheme( colorScheme );
				}
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		
		// *** PRIVATE DATA MEMBERS *** //

		private ColorScheme colorScheme;
	}
}
