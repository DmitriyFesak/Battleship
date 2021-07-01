using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Destroyer : Ship
    {
        protected Cell _firstSegment;

        public override ShotResult DamageSegment()
        {
            _damagedSegments++;

            return ShotResult.Destroyed;
        }

        public override void SetLocation(List<Cell> shipSegments)
        {
            _firstSegment = shipSegments[0];
        }
    }
}
