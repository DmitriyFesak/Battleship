using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    abstract class Ship
    {
        protected int damagedSegments;
        public abstract void SetLocation(Cell[] shipSegments);
        public abstract String DamageSegment();
    }
}
