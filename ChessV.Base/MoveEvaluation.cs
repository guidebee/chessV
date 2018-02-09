
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG
  
  THIS FILE DERIVED FROM CUTE CHESS BY ILARI PIHLAJISTO AND ARTO JONSSON

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

namespace ChessV
{
	public class MoveEvaluation
	{
		//	Constructs an empty MoveEvaluation object
		public MoveEvaluation()
		{
			IsBookEval = false;
			Depth = 0;
			Score = 0;
			Time = 0;
			NodeCount = 0;
			PV = "";
		}

		//	Returns true if the evaluation is empty
		public bool IsEmpty
		{ get { return Depth == 0 && Score == 0 && Time < 500 && NodeCount == 0; } }
		
		//	Returns true if the evaluation points to a book move.
		public bool IsBookEval { get; set; }

		//	How many plies were searched?
		//	For human players this is always 0.
		public int Depth { get; set; }

		//	Score in centipawns from the player's point of view.
		//	For human player this always 0.
		public int Score { get; set; }

		//	Move time in milliseconds
		public long Time { get; set; }

		//	How many nodes were searched?
		//	For human players this is always 0.
		public long NodeCount { get; set; }

		//	Principal variation (PV) - the sequence of 
		//	moves that an engine expects to be played next
		//	(because these are the best moves by its calculation.)
		//	For human players this is always empty.
		public string PV { get; set; }

		public void Clear()
		{
			IsBookEval = false;
			Depth = 0;
			Score = 0;
			Time = 0;
			NodeCount = 0;
			PV = "";
		}
	}
}
