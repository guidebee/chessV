
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
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class GameIndexFilterForm: Form
	{
		public GameIndexFilterForm()
		{
			InitializeComponent();
		}

		private void GameIndexFilterForm_Load( object sender, EventArgs e )
		{
			LoadRequiredTags();
			if( ListIsFiltered )
				chkFilterGameIndex.Checked = true;
			EnableOrDisableFilterControls( ListIsFiltered );
		}

		private void chkFilterGameIndex_CheckedChanged( object sender, EventArgs e )
		{
			EnableOrDisableFilterControls( chkFilterGameIndex.Checked );
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			ListIsFiltered = chkFilterGameIndex.Checked;
			SaveRequiredTags();
			Close();
		}

		private void EnableOrDisableFilterControls( bool enable )
		{
			chkTagChessVariant.Enabled = enable;
			chkTagDifferentArmies.Enabled = enable;
			chkTagHistoric.Enabled = enable;
			chkTagMultipleBoards.Enabled = enable;
			chkTagPopular.Enabled = enable;
			chkTagRandomArray.Enabled = enable;
			chkTagRegional.Enabled = enable;
		}

		private void LoadRequiredTags()
		{
			chkTagChessVariant.Checked = RequiredTags.Contains( "Chess Variant" );
			chkTagDifferentArmies.Checked = RequiredTags.Contains( "Different Armies" );
			chkTagHistoric.Checked = RequiredTags.Contains( "Historic" );
			chkTagMultipleBoards.Checked = RequiredTags.Contains( "Multiple Boards" );
			chkTagPopular.Checked = RequiredTags.Contains( "Popular" );
			chkTagRandomArray.Checked = RequiredTags.Contains( "Random Array" );
			chkTagRegional.Checked = RequiredTags.Contains( "Regional" );
		}

		private void SaveRequiredTags()
		{
			RequiredTags.Clear();
			if( chkTagChessVariant.Checked )
				RequiredTags.Add( "Chess Variant" );
			if( chkTagDifferentArmies.Checked )
				RequiredTags.Add( "Different Armies" );
			if( chkTagHistoric.Checked )
				RequiredTags.Add( "Historic" );
			if( chkTagMultipleBoards.Checked )
				RequiredTags.Add( "Multiple Boards" );
			if( chkTagPopular.Checked )
				RequiredTags.Add( "Popular" );
			if( chkTagRandomArray.Checked )
				RequiredTags.Add( "Random Array" );
			if( chkTagRegional.Checked )
				RequiredTags.Add( "Regional" );
		}

		public List<string> RequiredTags { get; set; }
		public bool ListIsFiltered { get; set; }
	}
}
