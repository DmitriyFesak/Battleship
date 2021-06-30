using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Destroyer : Ship
    {
        protected Cell firstSegment;

        public override int DamageSegment()
        {
            damagedSegments++;
            return ((int)Enumerables.ShotResult.destroyed);
        }

        public override void SetLocation(List<Cell> shipSegments)
        {
            firstSegment = shipSegments[0];
        }
    }
}
