
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

namespace ChessV.GUI
{
	public partial class SquareColorForm: Form
	{
		// *** PROPERTIES *** //

		public Texture Texture { get; set; }
		public Color Color { get; set; }
		public bool NotUsed { get; set; }
		public bool NotUsedEnabled { get; set; }


		// *** CONSTRUCTION *** //

		public SquareColorForm()
		{
			NotUsedEnabled = false;
			InitializeComponent();
		}


		// *** EVENT HANDLERS *** //

		private void TextureOrColorForm_Load( object sender, EventArgs e )
		{
			if( !NotUsedEnabled )
				optNotUsed.Enabled = false;

			//	Populate list of textures
			foreach( KeyValuePair<string, Texture> pair in TextureLibrary.Textures )
				pickTexture.Items.Add( pair.Value );

			//	Set controls to match current settings
			if( NotUsed )
			{
				optNotUsed.Checked = true;
				pickTexture.Enabled = false;
				panelSquareColor.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
			}
			else if( Texture != null )
			{
				pickTexture.SelectedItem = Texture;
				panelSquareColor.BackgroundImage = Texture.Images[0];
				optTexture.Checked = true;
			}
			else
			{
				pickTexture.Enabled = false;
				panelSquareColor.BackgroundImage = null;
				panelSquareColor.BackColor = Color;
				optColor.Checked = true;
			}
		}

		private void options_CheckedChanged( object sender, EventArgs e )
		{
			pickTexture.Enabled = optTexture.Checked;
			if( optNotUsed.Checked )
			{
				NotUsed = true;
				panelSquareColor.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
			}
			else if( optTexture.Checked )
			{
				NotUsed = false;
				if( pickTexture.SelectedItem == null )
					pickTexture.SelectedIndex = 0;
				Texture = (Texture) pickTexture.SelectedItem;
				panelSquareColor.BackgroundImage = Texture.Images[0];
			}
			else
			{
				NotUsed = false;
				Texture = null;
				panelSquareColor.BackgroundImage = null;
				panelSquareColor.BackColor = Color;
			}
		}

		private void pickTexture_SelectedIndexChanged( object sender, EventArgs e )
		{
			Texture = (Texture) pickTexture.SelectedItem;
			panelSquareColor.BackgroundImage = Texture.Images[0];
		}

		private void linkChooseColor_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
		{
			colorDialog.SolidColorOnly = true;
			colorDialog.AllowFullOpen = true;
			colorDialog.AnyColor = true;
			colorDialog.Color = Color;
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				Color = colorDialog.Color;
				panelSquareColor.BackColor = Color;
				optColor.Checked = true;
			}
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
