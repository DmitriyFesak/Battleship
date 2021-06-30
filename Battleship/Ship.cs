using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    abstract class Ship
    {
        protected int damagedSegments = 0;
        public abstract void SetLocation(List<Cell> shipSegments);
        public abstract int DamageSegment();
    }
}
