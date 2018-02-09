
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
	[Game("Corridor Chess", typeof(Geometry.Rectangular), 8, 8, 
		  InventedBy = "Tony Paletta",
		  Invented = "1980",
		  Tags = "Chess Variant",
		  GameDescription1 = "Standard Chess with a different setup and no castling",
		  GameDescription2 = "Players often fight for control of the outside files")]
	class CorridorChess: Chess
	{
		// *** INITIALIZATION *** //

		#region SetGameVariables
		public override void SetGameVariables()
		{
			base.SetGameVariables();
			Array = "1nrqkrn1/2b2b2/1pppppp1/8/8/1PPPPPP1/2B2B2/1NRQKRN1";
			Castling.Value = "None";
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
