using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    static class Enumerables
    {
        public enum CellStatus
        {
            unshot = 0,
            miss = -1,
            hit = 1,
            unshotShip = 2
        }
    }
}
