using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Battlefield
    {
        public const int ROWS_COUNT = 10;
        public const int COLS_COUNT = 10;
        public const int DESTROYERS_COUNT = 4;
        public const int CRUISERS_COUNT = 3;
        public const int SUBMARINES_COUNT = 2;
        public const int BATTLESHIPS_COUNT = 1;

        private Cell[,] _cells;
        private List<Ship> _ships;

        public Battlefield()
        {
            InitializeCells();
            InitializeShips();
        }

        public CellStatus [,] GetBattlefield()
        {
            CellStatus[,] currentField = new CellStatus[ROWS_COUNT, COLS_COUNT];
            CellStatus cellValue;

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < COLS_COUNT; j++)
                {
                    if (_cells[i, j].isShot)
                    {
                        if (_cells[i, j].isShipPlaced)
                        {
                            cellValue = CellStatus.Hit;
                        }
                        else
                        {
                            cellValue = CellStatus.Miss;
                        }
                    }
                    else
                    {
                        if (_cells[i, j].isShipPlaced)
                        {
                            cellValue = CellStatus.UnshotShip;
                        }
                        else
                        {
                            cellValue = CellStatus.Unshot;
                        }
                    }
                    currentField[i, j] = cellValue;
                }
            }

            return currentField;
        }

        public int GetTotalShipsCount()
        {
            return DESTROYERS_COUNT + CRUISERS_COUNT + SUBMARINES_COUNT + BATTLESHIPS_COUNT;
        }

        public int GetRows()
        {
            return ROWS_COUNT;
        }

        public int GetCols()
        {
            return COLS_COUNT;
        }

        public ShotResult CheckCell(int y, int x)
        {
            if (_cells[y, x].isShot)
            {
                return ShotResult.AlreadyShot;
            }
            if (_cells[y, x].TakeShot())
            {
                return _cells[y, x].GetShip().DamageSegment();
            }
            else
            {
                return ShotResult.Miss;
            }
        }

        public bool PlaceShipManually(int shipId, int y, int x, int orientation)
        {
            if (shipId >= _ships.Count || y >= ROWS_COUNT || x >= COLS_COUNT)
            {
                return false;
            }

            int size = GetShipSize(shipId);

            List<Cell> shipSegments = new List<Cell>();

            if (orientation == ((int)ShipOrientation.Horizontal))
            {
                if (x + size >= COLS_COUNT)
                {
                    return false;
                }

                for (int i = x; i < x + size; i++)
                {
                    if (_cells[y, i].isShipPlaced)
                    {
                        return false;
                    }
                    else
                    {
                        shipSegments.Add(_cells[y, i]);
                    }
                }
            }
            else
            {
                if (y + size >= ROWS_COUNT)
                {
                    return false;
                }

                for (int i = y; i < y + size; i++)
                {
                    if (_cells[i, x].isShipPlaced)
                    {
                        return false;
                    }
                    else
                    {
                        shipSegments.Add(_cells[y, i]);
                    }
                }
            }

            return SaveShipLocation(shipId, size, shipSegments);
        }

        public void AutoPlaceShips()
        {
            Random rnd = new Random();
            int i = 0;
            int y, x, orientation;

            while(i < _ships.Count)
            {
                int size = GetShipSize(i);
                orientation = rnd.Next(0, 2);
                if (orientation == ((int)ShipOrientation.Horizontal))
                {
                    y = rnd.Next(0, ROWS_COUNT);
                    x = rnd.Next(0, COLS_COUNT - size);
                }
                else
                {
                    y = rnd.Next(0, ROWS_COUNT - size);
                    x = rnd.Next(0, COLS_COUNT);
                }
                if (PlaceShipManually(i, y, x, orientation) == true)
                {
                    i++;
                }
            }
        }
        
        private void InitializeCells()
        {
            _cells = new Cell[ROWS_COUNT, COLS_COUNT];

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < COLS_COUNT; j++)
                {
                    _cells[i, j] = new Cell(i, j);
                }
            }
        }

        private void InitializeShips()
        {
            _ships = new List<Ship>();

            for (int i = 0; i < GetTotalShipsCount(); i++)
            {
                Ship ship;
                if (i < DESTROYERS_COUNT)
                {
                    ship = new Destroyer();
                }
                else if (i >= DESTROYERS_COUNT && i < DESTROYERS_COUNT + CRUISERS_COUNT)
                {
                    ship = new Cruiser();
                }
                else if (i >= DESTROYERS_COUNT + CRUISERS_COUNT && i < DESTROYERS_COUNT + CRUISERS_COUNT + SUBMARINES_COUNT)
                {
                    ship = new Submarine();
                }
                else
                {
                    ship = new Battleship();
                }
                _ships.Add(ship);
            }
        }

        private int GetShipSize (int shipId)
        {
            int size = 0;

            if (_ships[shipId].GetType() == typeof(Destroyer))
            {
                size = 1;
            }
            else if (_ships[shipId].GetType() == typeof(Cruiser))
            {
                size = 2;
            }
            else if (_ships[shipId].GetType() == typeof(Submarine))
            {
                size = 3;
            }
            else if (_ships[shipId].GetType() == typeof(Battleship))
            {
                size = 4;
            }

            return size;
        }

        private bool SaveShipLocation(int shipId, int size, List<Cell> shipSegments)
        {
            if (shipSegments.Count != size)
            {
                return false;
            }
            for (int i = 0; i < size; i++)
            {
                shipSegments[i].PlaceShipSegment(_ships[shipId]);
            }
            _ships[shipId].SetLocation(shipSegments);

            return true;
        }
    }
}
