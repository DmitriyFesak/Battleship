using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Destroyer : Ship
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

        public override void UnsetLocation()
        {
            _firstSegment.RemoveShipSegment();

            _firstSegment = null;
        }
    }
}
