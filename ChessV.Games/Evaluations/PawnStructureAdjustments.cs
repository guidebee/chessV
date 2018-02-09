
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

namespace ChessV.Evaluations
{
    public class PawnStructureAdjustments
    {
        public int IsolatedMidgame { get; set; }
        public int IsolatedEndgame { get; set; }

        public int BackwardMidgame { get; set; }
        public int BackwardEndgame { get; set; }

        public int WeakExposedMidgame { get; set; }
        public int WeakExposedEndgame { get; set; }

        public int DoubledMidgame { get; set; }
        public int DoubledEndgame { get; set; }

        public int PassedMidgame { get; set; }
        public int PassedEndgame { get; set; }

        public int PassedNotIsolatedMidgame { get; set; }
        public int PassedNotIsolatedEndgame { get; set; }

        public PawnStructureAdjustments()
        {
            IsolatedMidgame = 20;
            IsolatedEndgame = 20;
            BackwardMidgame = 15;
            BackwardEndgame = 12;
            WeakExposedMidgame = 4;
            WeakExposedEndgame = 5;
            DoubledMidgame = 8;
            DoubledEndgame = 20;
            PassedMidgame = 16;
            PassedEndgame = 25;
            PassedNotIsolatedMidgame = 4;
            PassedNotIsolatedEndgame = 10;
        }
    }
}
