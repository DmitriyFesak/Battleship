using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Cell
    {
        private int x;
        private int y;
        public bool isShot { get { return isShot; } private set { } }
        public bool isShipPlaced { get { return isShipPlaced; } private set { } }
        private Ship shipPlaced;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            isShot = false;
            isShipPlaced = false;
        }

        public bool TakeShot()
        {
            isShot = true;
            if (isShipPlaced)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Ship GetShip()
        {
            return shipPlaced;
        }

        public void PlaceShipSegment(Ship ship)
        {
            isShipPlaced = true;
            shipPlaced = ship;
        }
    }
}
