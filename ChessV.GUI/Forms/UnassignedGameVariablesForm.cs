
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
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	public partial class UnassignedGameVariablesForm: Form
	{
		private IWin32Window owner;
		private Game game;
		private bool finishedLoading;
		private PropertyInfo choiceProperty1;
		private PropertyInfo choiceProperty2;
		private PropertyInfo intProperty1;
		private PropertyInfo intProperty2;

		public UnassignedGameVariablesForm( IWin32Window owner )
		{
			this.owner = owner;
			finishedLoading = false;
			InitializeComponent();
		}

		public void Helper( Game game, object helperObject )
		{
			this.game = game;
			ShowDialog( owner );
		}

		protected string ConvertVariableNameToDisplay( string variableName )
		{
			StringBuilder display = new StringBuilder( 100 );
			int x = 0;
			bool upper = Char.IsUpper( variableName[x] );
			display.Append( Char.ToUpper( variableName[x] ) );
			x++;
			while( x < variableName.Length )
			{
				if( Char.IsUpper( variableName[x] ) && !upper )
					display.Append( ' ' );
				display.Append( variableName[x] );
				upper = Char.IsUpper( variableName[x] );
				x++;
			}
			display.Append( ':' );
			return display.ToString();
		}

		protected void PreviewHelper( Game game, object helperObject )
		{
			int unassignedCount = 0;
			PropertyInfo[] properties = game.GetType().GetProperties();
			foreach( PropertyInfo property in properties )
			{
				object[] attributes = property.GetCustomAttributes( typeof(GameVariableAttribute), false );
				if( attributes.Length > 0 )
				{
					if( property.PropertyType == typeof(ChoiceVariable) )
					{
						ChoiceVariable choice = (ChoiceVariable) property.GetValue( game, null );
						if( choice.Choices.Count > 0 && choice.Value == null )
						{
							//	this property is unassigned
							if( unassignedCount == 0 )
								choice.Value = (string) pickVariable1.SelectedItem;
							else if( unassignedCount == 1 )
								choice.Value = (string) pickVariable2.SelectedItem;
							unassignedCount++;
						}
					}
					else if( property.PropertyType == typeof(IntVariable) )
					{
						IntVariable val = (IntVariable) property.GetValue( game, null );
						if( val.Value == null )
						{
							//	this property is unassigned
							if( unassignedCount == 0 )
							{
								int textvalue;
								val.Value = null;
								if( Int32.TryParse( txtVariable1.Text, out textvalue ) )
									if( textvalue >= val.MinValue && textvalue <= val.MaxValue )
										val.Value = textvalue;
							}
							else if( unassignedCount == 1 )
							{
								int textvalue;
								val.Value = null;
								if( Int32.TryParse( txtVariable2.Text, out textvalue ) )
									if( textvalue >= val.MinValue && textvalue <= val.MaxValue )
										val.Value = textvalue;
							}
							unassignedCount++;
						}
					}
				}
			}
		}

		private void GameVariablesForm_Load( object sender, EventArgs e )
		{
			//	Determine background color - this is typically the 
			//	first square color (light square color) but if that 
			//	color isn't light enough, we will scale it up to make 
			//	it lighter.  Things look bad if the backgrounds for 
			//	all the tool windows aren't fairly light
			Theme theme = ThemeFactory.CreateTheme( game );
			Color WindowBackgroundColor = theme.ColorScheme.SquareColors[0];
			int brightness =
				(WindowBackgroundColor.R +
				 WindowBackgroundColor.G +
				 WindowBackgroundColor.B) / 3;
			if( brightness < 250 )
			{
				double scaleFactor = 250.0 / (double) brightness;
				WindowBackgroundColor = Color.FromArgb(
					(int) (WindowBackgroundColor.R + ((255 - WindowBackgroundColor.R) * (scaleFactor - 1.0))),
					(int) (WindowBackgroundColor.G + ((255 - WindowBackgroundColor.G) * (scaleFactor - 1.0))),
					(int) (WindowBackgroundColor.B + ((255 - WindowBackgroundColor.B) * (scaleFactor - 1.0))) );
			}
			BackColor = WindowBackgroundColor;

			int unassignedCount = 0;
			PropertyInfo[] properties = game.GetType().GetProperties();
			foreach( PropertyInfo property in properties )
			{
				object[] attributes = property.GetCustomAttributes( typeof(GameVariableAttribute), false );
				if( attributes.Length > 0 )
				{
					if( property.PropertyType == typeof(ChoiceVariable) )
					{
						ChoiceVariable choice = (ChoiceVariable) property.GetValue( game, null );
						if( choice.Choices.Count > 0 && choice.Value == null )
						{
							//	this property is unassigned
							if( unassignedCount == 0 )
							{
								choiceProperty1 = property;
								lblGameVariable1.Visible = true;
								lblGameVariable1.Text = ConvertVariableNameToDisplay( property.Name );
								pickVariable1.Visible = true;
								foreach( string choicename in choice.Choices )
									pickVariable1.Items.Add( choicename );
								pickVariable1.SelectedIndex = Program.Random.Next( pickVariable1.Items.Count );
							}
							else if( unassignedCount == 1 )
							{
								choiceProperty2 = property;
								lblGameVariable2.Visible = true;
								lblGameVariable2.Text = ConvertVariableNameToDisplay( property.Name );
								pickVariable2.Visible = true;
								foreach( string choicename in choice.Choices )
									pickVariable2.Items.Add( choicename );
								pickVariable2.SelectedIndex = Program.Random.Next( pickVariable2.Items.Count );
							}
							else
								throw new Exception( "Not supported - too many unassigned variables" );
							unassignedCount++;
						}
					}
					else if( property.PropertyType == typeof(IntVariable) )
					{
						IntVariable val = (IntVariable) property.GetValue( game, null );
						if( val.Value == null )
						{
							//	this property is unassigned
							if( unassignedCount == 0 )
							{
								intProperty1 = property;
								lblGameVariable1.Visible = true;
								lblGameVariable1.Text = ConvertVariableNameToDisplay( property.Name );
								txtVariable1.Visible = true;
								val.Value = Program.Random.Next( (int) val.MaxValue - (int) val.MinValue + 1 ) + (int) val.MinValue;
								txtVariable1.Text = val.Value.ToString();
								unassignedCount++;
							}
						}
					}
				}
			}

			if( unassignedCount == 1 )
			{
				lblGameVariable1.Location = new Point( lblGameVariable1.Location.X, lblGameVariable1.Location.Y + 18 );
				txtVariable1.Location = new Point( txtVariable1.Location.X, txtVariable1.Location.Y + 18 );
				pickVariable1.Location = new Point( pickVariable1.Location.X, pickVariable1.Location.Y + 18 );
			}

			finishedLoading = true;
			UpdatePreviewImage();
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			int intPropertyNumber = 0;
			try
			{
				if( intProperty1 != null )
				{
					intPropertyNumber = 1;
					((IntVariable) intProperty1.GetValue( game, null )).Value = Convert.ToInt32( txtVariable1.Text );
				}
				if( intProperty2 != null )
				{
					intPropertyNumber = 2;
					((IntVariable) intProperty2.GetValue( game, null )).Value = Convert.ToInt32( txtVariable2.Text );
				}
				if( choiceProperty1 != null )
					((ChoiceVariable) choiceProperty1.GetValue( game, null )).Value = (string) pickVariable1.SelectedItem;
				if( choiceProperty2 != null )
					((ChoiceVariable) choiceProperty2.GetValue( game, null )).Value = (string) pickVariable2.SelectedItem;
			}
			catch
			{
				PropertyInfo property = intPropertyNumber == 1 ? intProperty1 : intProperty2;
				IntVariable variable = (IntVariable) property.GetValue( game, null );
				string displayName = ConvertVariableNameToDisplay( property.Name );
				MessageBox.Show( "Parameter '" + displayName.Substring( 0, displayName.Length - 1 ) + 
					"' must be between " + variable.MinValue.ToString() + " and " + variable.MaxValue.ToString() );
				DialogResult = DialogResult.None;
				return;
			}
			Close();
		}

		private void UpdatePreviewImage()
		{
			try
			{
				Type gameClass = Program.Manager.GameClasses[game.GameAttribute.GameName];
				GameAttribute gameAttribute = game.GameAttribute;
				ConstructorInfo ci = gameClass.GetConstructor( new Type[] { } );
				Game newgame;
				InitializationHelper initHelper = PreviewHelper;
				newgame = (Game) ci.Invoke( null );
				FieldInfo environmentField = newgame.GetType().GetField( "Environment" );
				if( environmentField != null )
					environmentField.SetValue( newgame, new Compiler.Environment( Program.Manager.Environment ) );
				newgame.Initialize( gameAttribute, null, initHelper );
				BoardPresentation presentation = PresentationFactory.CreatePresentation( newgame, true );
				Bitmap b = presentation.Render();
				if( b != null )
				{
					Bitmap scaled = null;
					double xscale = (double) pictSubGamePreview.Size.Width / (double) b.Width;
					xscale = xscale > 1.0 ? 1.0 : xscale;
					double yscale = (double) pictSubGamePreview.Size.Height / (double) b.Height;
					yscale = yscale > 1.0 ? 1.0 : yscale;
					double scale = xscale > yscale ? yscale : xscale;
					scaled = new Bitmap( b, (int) (b.Width * scale), (int) (b.Height * scale) );
					pictSubGamePreview.Image = scaled;
				}
			}
			catch( Exception ex )
			{
				ExceptionForm form = new ExceptionForm( ex, game );
				form.ShowDialog();
			}
		}

		private void pickVariables_SelectedIndexChanged( object sender, EventArgs e )
		{
			if( finishedLoading )
				UpdatePreviewImage();
		}

		private void txtVariables_TextChanged( object sender, EventArgs e )
		{
			if( finishedLoading )
				UpdatePreviewImage();
		}

		private void txtVariables_KeyPress( object sender, KeyPressEventArgs e )
		{
			//	Stop input of non-numeric characters.  The reason we don't 
			//	simply check to see if the entered key is a number is because 
			//	we still want arrow keys, Ctrl+V, etc, to work.
			if( char.IsLetter( e.KeyChar ) ||
				char.IsSymbol( e.KeyChar ) ||
				char.IsWhiteSpace( e.KeyChar ) ||
				char.IsPunctuation( e.KeyChar ) )
				e.Handled = true;
		}
	}
}
