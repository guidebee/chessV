
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
using System.IO;

namespace ChessV.Games
{
	[Game("Eurasian Chess", typeof(Geometry.Rectangular), 10, 10,
		  Invented = "2003",
		  InventedBy = "Fergus Duniho",
		  Tags = "Chess Variant",
		  GameDescription1 = "A synthesis of European and Asian forms of Chess",
		  GameDescription2 = "Features the Cannon from Chinese Chess and elements from Grand Chess")]
	[Appearance(ColorScheme="Buckingham Green", PieceSet="Eurasian")]
	public class EurasianChess: GrandChess
	{
		// *** PIECE TYPES *** //

		public PieceType Cannon;
		public PieceType Vao;


		// *** CONSTRUCTION *** //

		public EurasianChess()
		{
			boardImageFile = "Graphics" +
				Path.DirectorySeparatorChar + "Themes" +
				Path.DirectorySeparatorChar + "Eurasian Chess" +
				Path.DirectorySeparatorChar + "EurasianBoard.jpg";
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "r1c4c1r/1nbvqkvbn1/pppppppppp/10/10/10/10/PPPPPPPPPP/1NBVQKVBN1/R1C4C1R";
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			Chancellor.Enabled = false;
			Archbishop.Enabled = false;
			AddPieceType( Cannon = new Cannon( "Cannon", "C", 400, 275 ) );
			AddPieceType( Vao = new Vao( "Vao", "V", 300, 175 ) );
		}
		#endregion

		#region AddRules
		public override void AddRules()
		{
			base.AddRules();
			AddRule( new Rules.Xiangqi.KingFacingRule( King ) );
			AddRule( new Rules.PieceLocationRestrictionRule( King, loc => loc.Rank >= 5 ) );
		}
		#endregion


		// *** CUSTOM APPEARANCE *** //

		#region GetCustomThemes
		public override List<string> GetCustomThemes()
		{
			if( File.Exists( boardImageFile ) )
				return new List<string> { "Eurasian" };
			return null;
		}
		#endregion

		#region GetThemeSquareSize
		public override void GetThemeSquareSize( string themeName, ref int squareSize )
		{
			squareSize = 54;
		}
		#endregion

		#region GetDefaultCustomTheme
		public override string GetDefaultCustomTheme()
		{
			return "Eurasian";
		}
		#endregion

		#region RenderCustomThemeBoard
		public override void RenderCustomThemeBoard( System.Drawing.Graphics gr, int borderWidth, string customThemeName )
		{
			if( boardImage == null && File.Exists( boardImageFile ) )
				boardImage = Image.FromFile( boardImageFile );
			if( boardImage != null )
				gr.DrawImage( boardImage, new Rectangle( borderWidth, borderWidth, 540,  540 ) );
		}
		#endregion


		// *** PROTECTED DATA MEMBERS *** //

		protected string boardImageFile;
		protected Image boardImage;
	}
}
