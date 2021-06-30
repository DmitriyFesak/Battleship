using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Battleship : Submarine
    {
        protected Cell fourthSegment;

        public override int DamageSegment()
        {
            damagedSegments++;

            if (damagedSegments < 4)
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
            fourthSegment = shipSegments[3];
        }
    }
}
