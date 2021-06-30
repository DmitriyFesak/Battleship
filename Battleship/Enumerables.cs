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

        public enum ShotResult
        {
            damaged = 0,
            miss = -1,
            destroyed = 1,
        }

        public enum ShipOrientation
        {
            horizontal = 0,
            vertical = 1,
        }
    }
}
