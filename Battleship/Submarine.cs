using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Submarine : Cruiser
    {
        protected Cell _thirdSegment;

        public override ShotResult DamageSegment()
        {
            _damagedSegments++;

            if (_damagedSegments < 3)
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
            _thirdSegment = shipSegments[2];
        }
    }
}
