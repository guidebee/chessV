
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

namespace ChessV
{
    public class Evaluation
    {
		// *** CONSTRUCTION *** //

        public Evaluation()
		{ }


		// *** INITIALIZATION *** //

		public virtual void Initialize( Game game )
		{ this.game = game; this.board = game.Board; }

        public virtual void PostInitialize()
        { }


		// *** OVERRIDEABLE VIRTUAL FUNCTIONS *** //

        public virtual void AdjustEvaluation( ref int midgameEval, ref int endgameEval )
        { }

		public virtual void MoveBeingMade( MoveInfo move, int ply )
		{ }

		public virtual void MoveBeingUnmade( MoveInfo move, int ply )
		{ }


        // *** PROTECTED DATA MEMBERS *** //

        protected Board board;
        protected Game game;
    }
}
