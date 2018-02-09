
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
using System.Windows.Forms;

namespace ChessV.GUI
{
	//**********************************************************************
	//
	//                        StringEditForm
	//
	//    This is a generic form for editing a single, named text property

	public partial class StringEditForm: Form
	{
		// *** PROPERTIES *** //

		//	the name of the string property being edited
		public string StringName { get; set; }

		//	the value of the string property being edited
		public string StringValue { get; set; }


		// *** CONSTRUCTION *** //

		public StringEditForm()
		{
			InitializeComponent();
		}


		// *** EVENT HANDLERS *** //

		//	Form load event handler
		private void StringEditForm_Load( object sender, EventArgs e )
		{
			Text = StringName;
			label.Text = StringName + ":";
			if( StringValue != null )
				txtString.Text = StringValue;
		}

		//	OK button clicked event handler
		private void btnOK_Click( object sender, EventArgs e )
		{
			StringValue = txtString.Text;
		}
	}
}
