
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

namespace ChessV.Games.Abstract
{
	//**********************************************************************
	//
	//                         UndefinedGame
	//
	//    This class is useful for when you need a functional Game object 
	//    for reasons other than playing an actual game.  This has the 
	//    minimum requirements to create a valid Game object.  It can 
	//    be useful, for example, for generating mobility statistics for 
	//    PieceType objects (which requires a Board and therefore a Game.)

	public class UndefinedGame: GenericChess
	{
        // *** CONSTRUCTION *** //

		public UndefinedGame
			( int nFiles,               // number of files on the board
			  int nRanks ):             // number of ranks on the board
                base( nFiles, nRanks, new MirrorSymmetry() )
				
        {
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "k" + (Board.NumFiles - 1).ToString();
			for( int rank = 1; rank < Board.NumRanks - 1; rank++ )
				Array += "/" + Board.NumFiles.ToString();
			Array += "/K" + (Board.NumFiles - 1).ToString();
		}
		#endregion

		#region AddPieceTypes
		public override void AddPieceTypes()
		{
			base.AddPieceTypes();
			foreach( PieceType type in PieceTypeList )
				AddPieceType( type );
		}
		#endregion


		// *** PROTECTED DATA MEMBERS *** //

		public static List<PieceType> PieceTypeList;
	}
}