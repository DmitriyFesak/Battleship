using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Cruiser : Destroyer
    {
        protected Cell _secondSegment;

        public override ShotResult DamageSegment()
        {
            _damagedSegments++;

            if (_damagedSegments == 1)
            {
                return ShotResult.Damaged;
            }
            else
            {
                return ShotResult.Destroyed;
            }
        }

        public override void SetLocation(List<Cell> shipSegments)
        {
            _firstSegment = shipSegments[0];
            _secondSegment = shipSegments[1];
        }
    }
}
