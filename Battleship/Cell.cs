﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Cell
    {
        private int _x;
        private int _y;
        public bool isShot { get { return isShot; } private set { } }
        public bool isShipPlaced { get { return isShipPlaced; } private set { } }
        private Ship _shipPlaced;

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
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
            return _shipPlaced;
        }

        public void PlaceShipSegment(Ship ship)
        {
            isShipPlaced = true;
            _shipPlaced = ship;
        }
    }
}
