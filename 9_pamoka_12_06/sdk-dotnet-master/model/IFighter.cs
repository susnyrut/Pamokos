using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFights.model
{
    interface IFighter
    {
        Move MakeNextMove(Move opponentsLastMove, int myLastScore, int opponentsLastScore);
    }
}
