
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
using System.Reflection;
using System.Windows.Forms;

namespace ChessV.GUI
{
	public partial class GenerateMobilityStatisticsForm: Form
	{
		public GenerateMobilityStatisticsForm()
		{
			InitializeComponent();
		}

		private void GenerateMobilityStatisticsForm_Load( object sender, EventArgs e )
		{
			densities = new int[] { 50, 45, 40, 35, 30, 25, 20, 15, 10 }; 
		}

		private void btnCalculate_Click( object sender, EventArgs e )
		{
			int nFiles = Convert.ToInt32( txtNumberOfFiles.Text );
			int nRanks = Convert.ToInt32( txtNumberOfRanks.Text );
			GameAttribute attr = new GameAttribute( "", typeof(Geometry.Rectangular), nFiles, nRanks );
			Games.Abstract.UndefinedGame.PieceTypeList = new List<PieceType>();
			types = new List<PieceType>();

			addPieceType( typeof( Queen ), "Q" );
			addPieceType( typeof( Rook ), "R" );
			addPieceType( typeof( Bishop ), "B" );
			addPieceType( typeof( Knight ), "N" );
			addPieceType( typeof( Amazon ), "QN" );
			addPieceType( typeof( Chancellor ), "RN" );
			addPieceType( typeof( Archbishop ), "BN" );
			addPieceType( typeof( Knightrider ), "NN" );
			addPieceType( typeof( Unicorn ), "BNN" );
			addPieceType( typeof( Lion ), "HFD" );
			addPieceType( typeof( WarElephant ), "FAD" );
			addPieceType( typeof( Phoenix ), "WA" );
			addPieceType( typeof( Cleric ), "BD" );
			addPieceType( typeof( ShortRook ), "R4" );
			addPieceType( typeof( Bowman ), "WD" );
			addPieceType( typeof( NarrowKnight ), "fbNF" );
			addPieceType( typeof( ChargingRook ), "frlRbK" );
			addPieceType( typeof( ChargingKnight ), "fhNrlbK" );
			addPieceType( typeof( Colonel ), "fhNfrlRbK" );

			ChessV.Games.Abstract.UndefinedGame game = new Games.Abstract.UndefinedGame( nFiles, nRanks );

			StringBuilder str = new StringBuilder();
			foreach( PieceType type in types )
			{
				type.CalculateMobilityStatistics( game, (double) 1.0 - ((double) densities[0] / 100.0) );
				str.Append( type.InternalName );
				str.Append( '\t' );
				str.Append( type.Notation );
				str.Append( '\t' );
				str.Append( type.AverageDirectionsAttacked.ToString( "F2" ) );
				str.Append( '\t' );
				str.Append( type.AverageSafeChecks.ToString( "F2" ) );
				str.Append( '\t' );
				str.Append( type.AverageMobility.ToString( "F2" ) );
				for( int x = 1; x < densities.Length; x++ )
				{
					type.CalculateMobilityStatistics( game, (double) 1.0 - ((double) densities[x] / 100.0) );
					str.Append( '\t' );
					str.Append( type.AverageMobility.ToString( "F2" ) );
				}
				str.Append( '\n' );
			}
			Clipboard.SetText( str.ToString() );
		}

		protected void addPieceType( Type pieceTypeType, string notation )
		{
			ConstructorInfo ci = pieceTypeType.GetConstructor( new Type[] { typeof(string), typeof(string), typeof(int), typeof(int), typeof(string) } );
			object pieceType = ci.Invoke( new object[] { "", notation, 0, 0, "" } );
			Games.Abstract.UndefinedGame.PieceTypeList.Add( (PieceType) pieceType );
			types.Add( (PieceType) pieceType );
		}

		protected int[] densities;
		protected List<PieceType> types;
	}
}
