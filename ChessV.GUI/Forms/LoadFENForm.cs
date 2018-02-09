using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class LoadFENForm: Form
	{
		public Game Game { get; private set; }

		public LoadFENForm( Game game )
		{
			Game = game;

			InitializeComponent();
		}

		private void LoadFENForm_Load( object sender, EventArgs e )
		{
			lblGameName.Text = Game.Name;
			lblFENFormat.Text = Game.FENFormat;
			lblGameStartFEN.Text = Game.FENStart;
			currentFEN = Game.FEN.ToString();
			txtCurrentFEN.Text = currentFEN;

			PieceType[] pieceTypes;
			int nPieceTypes = Game.GetPieceTypes( out pieceTypes );
			for( int x = 0; x < nPieceTypes; x++ )
			{
				PieceType type = pieceTypes[x];
				//	determine if notation requires _ prefix
				bool requiresPrefix = false;
				if( type.Notation.Length == 2 &&
					type.Notation[1] >= 'A' && type.Notation[1] <= 'Z' )
				{
					//	see if the first character of this two-character notation
					//	conflicts with a single-character notation
					for( int y = 0; y < nPieceTypes; y++ )
						if( pieceTypes[y].Notation == type.Notation[0].ToString() )
							requiresPrefix = true;
				}
				ListViewItem lvi = new ListViewItem( (requiresPrefix ? "_" : "") + type.Notation );
				lvi.SubItems.Add( type.Name );
				lvi.SubItems.Add( type.InternalName );
				listPieceTypes.Items.Add( lvi );
			}
		}

		private void btnOK_Click( object sender, EventArgs e )
		{
			if( txtCurrentFEN.Text != currentFEN )
			{
				Game.ClearGameState();
				Game.LoadFEN( txtCurrentFEN.Text );
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click( object sender, EventArgs e )
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private string currentFEN;
	}
}
