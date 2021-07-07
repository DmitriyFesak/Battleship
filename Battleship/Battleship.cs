using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Battleship : Submarine
    {
        protected Cell _fourthSegment;

        public override ShotResult DamageSegment()
        {
            _damagedSegments++;

            if (_damagedSegments < 4)
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
            _fourthSegment = shipSegments[3];
        }
    }
}
