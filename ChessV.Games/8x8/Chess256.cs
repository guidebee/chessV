
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

namespace ChessV.Games
{
	//**********************************************************************
	//
	//                               Chess256
	//

	[Game("Chess256", typeof(Geometry.Rectangular), 8, 8, 
		  InventedBy = "Mats Winther",
		  Invented = "2006",
		  Tags = "Chess Variant,Random Array",
		  GameDescription1 = "A Chess derivative where the opening pawn formation is",
		  GameDescription2 = "randomized to eliminate the memorization of openings")]
	class Chess256: Chess
	{
		// *** GAME VARIABLES *** //

		[GameVariable] public IntVariable PositionNumber { get; set; }


		// *** CONSTRUCTION *** //
		public Chess256()
		{
		}


		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			PositionNumber = new IntVariable( 1, 256 );
			Array = "rnbqkbnr/#{BlackPawns}/8/8/#{WhitePawns}/RNBQKBNR";
		}
		#endregion

		#region SetOtherVariables
		public override void SetOtherVariables()
		{
			base.SetOtherVariables();
			if( PositionNumber.Value == null )
			{
				SetCustomProperty( "BlackPawns", "8/8" );
				SetCustomProperty( "WhitePawns", "8/8" );
				return;
			}

			//	determine Black's second-row pawns
			int position = (int) PositionNumber.Value - 1;
			int bitNumber = 0;
			string notation2ndRank = "";

			while( bitNumber < 8 )
			{
				if( (position & (1 << bitNumber)) == 0 )
				{
					notation2ndRank += 'p';
					bitNumber++;
				}
				else
				{
					int emptySpaceCount = 1;
					bitNumber++;
					while( bitNumber < 8 && (position & (1 << bitNumber)) != 0 )
					{
						emptySpaceCount++;
						bitNumber++;
					}
					notation2ndRank += Convert.ToChar( '0' + emptySpaceCount );
				}
			}

			//	determine Black's third-row pawns
			bitNumber = 0;
			string notation3rdRank = "";

			while( bitNumber < 8 )
			{
				if( (position & (1 << bitNumber)) != 0 )
				{
					notation3rdRank += 'p';
					bitNumber++;
				}
				else
				{
					int emptySpaceCount = 1;
					bitNumber++;
					while( bitNumber < 8 && (position & (1 << bitNumber)) == 0 )
					{
						emptySpaceCount++;
						bitNumber++;
					}
					notation3rdRank += Convert.ToChar( '0' + emptySpaceCount );
				}
			}

			SetCustomProperty( "BlackPawns", notation2ndRank + "/" + notation3rdRank );
			SetCustomProperty( "WhitePawns", notation3rdRank.ToUpper() + "/" + notation2ndRank.ToUpper() );
		}
		#endregion


		// *** WINBOARD ENGINE SUPPORT *** //

		#region TryCreateAdaptor
		public override EngineGameAdaptor TryCreateAdaptor( EngineConfiguration config )
		{
			if( config.SupportedVariants.Contains( "normal" ) &&
			    config.SupportedFeatures.Contains( "setboard" ) )
			{
				EngineGameAdaptor adaptor = new EngineGameAdaptor( "normal" );
				adaptor.IssueSetboard = true;
				return adaptor;
			}
			return null;
		}
		#endregion
	}
}
