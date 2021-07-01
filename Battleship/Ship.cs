using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    abstract class Ship
    {
        protected int _damagedSegments = 0;

        public abstract void SetLocation(List<Cell> shipSegments);

        public abstract ShotResult DamageSegment();
    }
}
