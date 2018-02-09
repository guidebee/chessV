
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	public partial class AppearanceSettingsForm: Form
	{
		// *** CONSTRUCTION *** //

		public AppearanceSettingsForm( Theme theme )
		{
			this.theme = theme;
			InitializeComponent();
		}


		// *** EVENT HANDLERS *** //

		private void AppearanceSettingsForm_Load( object sender, EventArgs e )
		{
			//	copy Theme settings into temporary variables so that we don't change 
			//	the actual Theme object unless the user actually applies the settings
			originalSchemeBeingEdited = theme.ColorScheme;
			colorScheme = theme.ColorScheme.Clone();
			nSquareColors = theme.NSquareColors;

			//	initialize the color schemes pick list
			bool colorSchemeInList = false;
			foreach( KeyValuePair<string, ColorScheme> scheme in ColorSchemeLibrary.ColorSchemes )
			{
				pickColorScheme.Items.Add( scheme.Value );
				if( scheme.Value == theme.ColorScheme )
					colorSchemeInList = true;
			}
			if( !colorSchemeInList )
				pickColorScheme.Items.Add( theme.ColorScheme );
			pickColorScheme.SelectedItem = theme.ColorScheme;

			//	initialize color controls for scheme
			UpdateColorScheme();

			//	if this is an existing scheme and hasn't been 
			//	modified yet, disable the Save button
			if( colorScheme.Name != "(custom)" )
				btnSaveScheme.Enabled = false;

			//	populate list of piece sets
			foreach( KeyValuePair<string, PieceSet> pair in PieceSetLibrary.PieceSets )
			{
				PieceSet pieceset = pair.Value;
				pickPieceSet.Items.Add( pieceset );
			}
			pickPieceSet.SelectedItem = theme.PieceSet;
		}

		private void pickColorScheme_SelectedIndexChanged( object sender, EventArgs e )
		{
			colorScheme = ((ColorScheme) pickColorScheme.SelectedItem).Clone();
			UpdateColorScheme();
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			if( colorScheme.Modified )
			{
				colorScheme.Name = "(custom)";
				theme.ColorScheme = colorScheme;
			}
			else
				theme.ColorScheme = (ColorScheme) pickColorScheme.SelectedItem;
			theme.PieceSet = (PieceSet) pickPieceSet.SelectedItem;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void panelBorderColor_Click( object sender, EventArgs e )
		{
			colorDialog.Color = colorScheme.BorderColor;
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				panelBorderColor.BackColor = colorDialog.Color;
				colorScheme.BorderColor = colorDialog.Color;
				SchemeModified();
			}
		}

		private void panelHighlightColor_Click( object sender, EventArgs e )
		{
			colorDialog.Color = colorScheme.HighlightColor;
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				panelHighlightColor.BackColor = colorDialog.Color;
				colorScheme.HighlightColor = colorDialog.Color;
				SchemeModified();
			}
		}

		private void panelTextColor_Click( object sender, EventArgs e )
		{
			colorDialog.Color = colorScheme.TextColor;
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				panelTextColor.BackColor = colorDialog.Color;
				colorScheme.TextColor = colorDialog.Color;
				SchemeModified();
			}
		}

		private void panelSquareColor1_Click( object sender, EventArgs e )
		{
			SquareColorForm form = new SquareColorForm();
			if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 0 ) )
				form.Texture = colorScheme.SquareTextures[0];
			else
				form.Color = colorScheme.SquareColors[0];
			if( form.ShowDialog() == DialogResult.OK )
			{
				if( form.Texture != null )
				{
					if( colorScheme.SquareTextures == null )
						colorScheme.SquareTextures = new Dictionary<int, Texture>();
					colorScheme.SquareTextures[0] = form.Texture;
					colorScheme.SquareColors[0] = form.Texture.SubstituteColor;
					panelSquareColor1.BackgroundImage = form.Texture.Images[0];
				}
				else
				{
					if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 0 ) )
						colorScheme.SquareTextures.Remove( 0 );
					colorScheme.SquareColors[0] = form.Color;
					panelSquareColor1.BackColor = colorDialog.Color;
					panelSquareColor1.BackgroundImage = null;
				}
				SchemeModified();
			}
		}

		private void panelSquareColor2_Click( object sender, EventArgs e )
		{
			SquareColorForm form = new SquareColorForm();
			if( colorScheme.NumberOfColors < 2 )
			{
				form.NotUsedEnabled = true;
				form.NotUsed = true;
			}
			else
			{
				if( colorScheme.NumberOfColors > 2 )
					form.NotUsedEnabled = false;
				else
					form.NotUsedEnabled = true;
				if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 1 ) )
					form.Texture = colorScheme.SquareTextures[1];
				else
					form.Color = colorScheme.SquareColors[1];
			}
			if( form.ShowDialog() == DialogResult.OK )
			{
				if( form.NotUsed )
				{
					colorScheme.NumberOfColors = 1;
					if( colorScheme.SquareColors.ContainsKey( 1 ) )
						colorScheme.SquareColors.Remove( 1 );
					if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 1 ) )
						colorScheme.SquareTextures.Remove( 1 );
					panelSquareColor2.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
				}
				else
				{
					if( colorScheme.NumberOfColors == 1 )
						colorScheme.NumberOfColors = 2;
					if( form.Texture != null )
					{
						if( colorScheme.SquareTextures == null )
							colorScheme.SquareTextures = new Dictionary<int, Texture>();
						colorScheme.SquareTextures[1] = form.Texture;
						colorScheme.SquareColors[1] = form.Texture.SubstituteColor;
						panelSquareColor2.BackgroundImage = form.Texture.Images[0];
					}
					else
					{
						if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 1 ) )
							colorScheme.SquareTextures.Remove( 1 );
						colorScheme.SquareColors[1] = form.Color;
						panelSquareColor2.BackColor = form.Color;
						panelSquareColor2.BackgroundImage = null;
					}
				}
				SchemeModified();
			}
		}

		private void panelSquareColor3_Click( object sender, EventArgs e )
		{
			SquareColorForm form = new SquareColorForm();
			if( colorScheme.NumberOfColors < 3 )
			{
				if( colorScheme.NumberOfColors < 2 )
					return;
				form.NotUsedEnabled = true;
				form.NotUsed = true;
			}
			else
			{
				if( colorScheme.NumberOfColors > 3 )
					form.NotUsedEnabled = false;
				else
					form.NotUsedEnabled = true;
				if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 2 ) )
					form.Texture = colorScheme.SquareTextures[2];
				else
					form.Color = colorScheme.SquareColors[2];
			}
			if( form.ShowDialog() == DialogResult.OK )
			{
				if( form.NotUsed )
				{
					colorScheme.NumberOfColors = 2;
					if( colorScheme.SquareColors.ContainsKey( 2 ) )
						colorScheme.SquareColors.Remove( 2 );
					if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 2 ) )
						colorScheme.SquareTextures.Remove( 2 );
					panelSquareColor3.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
				}
				else
				{
					if( colorScheme.NumberOfColors == 2 )
						colorScheme.NumberOfColors = 3;
					if( form.Texture != null )
					{
						if( colorScheme.SquareTextures == null )
							colorScheme.SquareTextures = new Dictionary<int, Texture>();
						colorScheme.SquareTextures[2] = form.Texture;
						colorScheme.SquareColors[2] = form.Texture.SubstituteColor;
						panelSquareColor3.BackgroundImage = form.Texture.Images[0];
					}
					else
					{
						if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 2 ) )
							colorScheme.SquareTextures.Remove( 2 );
						colorScheme.SquareColors[2] = form.Color;
						panelSquareColor3.BackColor = form.Color;
						panelSquareColor3.BackgroundImage = null;
					}
				}
				SchemeModified();
			}
		}

		private void panelPlayerColor1_Click( object sender, EventArgs e )
		{
			colorDialog.Color = colorScheme.PlayerColors[0];
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				panelPlayerColor1.BackColor = colorDialog.Color;
				colorScheme.PlayerColors[0] = colorDialog.Color;
				SchemeModified();
			}
		}

		private void panelPlayerColor2_Click( object sender, EventArgs e )
		{
			colorDialog.Color = colorScheme.PlayerColors[1];
			if( colorDialog.ShowDialog() == DialogResult.OK )
			{
				panelPlayerColor2.BackColor = colorDialog.Color;
				colorScheme.PlayerColors[1] = colorDialog.Color;
				SchemeModified();
			}
		}

		private void btnSaveScheme_Click( object sender, EventArgs e )
		{
			ColorSchemeSaveForm form = new ColorSchemeSaveForm( colorScheme );
			if( form.ShowDialog() == DialogResult.OK )
			{
				if( form.NewScheme )
				{
					pickColorScheme.Items.RemoveAt( pickColorScheme.SelectedIndex );
					int index = pickColorScheme.Items.Add( colorScheme );
					pickColorScheme.SelectedIndex = index;
					colorScheme = colorScheme.Clone();
				}
				else
				{
					pickColorScheme.Items.Remove( originalSchemeBeingEdited );

				}
			}
		}


		// *** HELPER FUNCTIONS *** //

		//	SchemeModified called whenever anything about "colorScheme" 
		//	is changed.  If this is the first modification, it sets 
		//	the Modified flag and adds (modified) to the name and enters 
		//	it into the color scheme pick-list as a new entry.  Until 
		//	the first modification, although the "colorScheme" property 
		//	is really a clone, since it is unmodified it is still 
		//	presented to the user in the pick list as the original scheme
		#region SchemeModified
		private void SchemeModified()
		{
			if( !colorScheme.Modified )
			{
				btnSaveScheme.Enabled = true;
				originalSchemeBeingEdited = (ColorScheme) pickColorScheme.SelectedItem;
				colorScheme.Modified = true;
				if( colorScheme.Name != "(custom)" )
					colorScheme.Name += " (modified)";
				int index = pickColorScheme.Items.Add( colorScheme );
				pickColorScheme.SelectedIndex = index;
			}
		}
		#endregion

		//	UpdateColorScheme called whenever any colors are changed.
		//	It is responsible for re-synching all of the controls 
		//	in the form to match property "colorScheme"
		#region UpdateColorScheme
		private void UpdateColorScheme()
		{
			// *** BASIC COLORS *** //

			//	initialize the border color
			panelBorderColor.BackColor = colorScheme.BorderColor;

			//	initialize the square highlight color
			panelHighlightColor.BackColor = colorScheme.HighlightColor;

			//	initialize the text color
			panelTextColor.BackColor = colorScheme.TextColor;


			// *** SQUARE COLORS *** //

			//	Square Color 1 (light squares)
			if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 0 ) )
				//	first square color is a texture
				panelSquareColor1.BackgroundImage = colorScheme.SquareTextures[0].Images[0];
			else
			{
				//	first square color is a solid color
				panelSquareColor1.BackColor = colorScheme.SquareColors[0];
				//	since it's not a texture, disable background image
				panelSquareColor1.BackgroundImage = null;
			}
			//	Do we have more than one color of squares?
			if( colorScheme.NumberOfColors > 1 )
			{
				//	Square Color 2 (dark squares)
				if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 1 ) )
					//	second square color is a texture
					panelSquareColor2.BackgroundImage = colorScheme.SquareTextures[1].Images[0];
				else
				{
					//	second square color is a solid color
					panelSquareColor2.BackColor = colorScheme.SquareColors[1];
					//	since it's not a texture, disable background image
					panelSquareColor2.BackgroundImage = null;
				}
				//	Do we have more than two colors of squares?
				if( colorScheme.NumberOfColors > 2 )
				{
					//	Square Color 3 (typically alternate dark, but also 
					//	for special uses on custom boards, e.g., Brouhaha)
					if( colorScheme.SquareTextures != null && colorScheme.SquareTextures.ContainsKey( 2 ) )
						//	third square color is a texture
						panelSquareColor3.BackgroundImage = colorScheme.SquareTextures[2].Images[0];
					else
					{
						//	third square color is a solid color
						panelSquareColor3.BackColor = colorScheme.SquareColors[2];
						//	since it's not a texture, disable background image
						panelSquareColor3.BackgroundImage = null;
					}
				}
				else
					//	no third square color so draw it as a big red X
					panelSquareColor3.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
			}
			else
			{
				//	no second or third square colors so draw them as red X'es
				panelSquareColor2.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
				panelSquareColor3.BackgroundImage = global::ChessV.GUI.Properties.Resources.RedX;
			}


			// *** PLAYER COLORS *** //

			panelPlayerColor1.BackColor = colorScheme.PlayerColors[0];
			panelPlayerColor2.BackColor = colorScheme.PlayerColors[1];
		}
		#endregion


		// *** PRIVATE PROPERTIES *** //

		//	actual Theme object for game; update only on exit with save
		private Theme theme { get; set; }

		//	temporary Theme values being edited
		private ColorScheme colorScheme { get; set; }
		private int nSquareColors { get; set; }
		private ColorScheme originalSchemeBeingEdited { get; set; }
	}
}
