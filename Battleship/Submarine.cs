using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Submarine : Cruiser
    {
        protected Cell thirdSegment;

        public override int DamageSegment()
        {
            damagedSegments++;

            if (damagedSegments < 3)
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
            thirdSegment = shipSegments[2];
        }
    }
}
