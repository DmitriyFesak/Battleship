using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Cruiser : Destroyer
    {
        protected Cell secondSegment;

        public override int DamageSegment()
        {
            damagedSegments++;

            if (damagedSegments == 1)
            {
                return ((int)Enumerables.ShotResult.damaged);
            }
            else
            {
                return ((int)Enumerables.ShotResult.destroyed);
            }
        }

        public override void SetLocation(List<Cell> shipSegments)
        {
            firstSegment = shipSegments[0];
            secondSegment = shipSegments[1];
        }
    }
}
