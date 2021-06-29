using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Battlefield
    {
        private const int ROWS_COUNT = 10;
        private const int COLS_COUNT = 10;
        private const int DESTROYERS_COUNT = 4;
        private const int CRUISERS_COUNT = 3;
        private const int SUBMARINES_COUNT = 2;
        private const int BATTLESHIPS_COUNT = 1;
        private Cell[,] cells;
        private List<Ship> ships;

        public Battlefield()
        {
            InitializeCells();
            InitializeShips();
        }

        public int [,] GetBattlefield()
        {
            int [,] currentField = new int [ROWS_COUNT, COLS_COUNT];
            int cellValue;

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < COLS_COUNT; j++)
                {
                    if (cells[i, j].isShot)
                    {
                        if (cells[i, j].isShipPlaced)
                        {
                            cellValue = ((int)Enumerables.CellStatus.hit);
                        }
                        else
                        {
                            cellValue = ((int)Enumerables.CellStatus.miss);
                        }
                    }
                    else
                    {
                        if (cells[i, j].isShipPlaced)
                        {
                            cellValue = ((int)Enumerables.CellStatus.unshotShip);
                        }
                        else
                        {
                            cellValue = ((int)Enumerables.CellStatus.unshot);
                        }
                    }
                    currentField[i, j] = cellValue;
                }
            }
            return currentField;
        }

        public bool CheckCell(int x, int y)
        {
            return cells[y, x].TakeShot();
        }

        // TODO: placing ships methods
        
        private void InitializeCells()
        {
            cells = new Cell[ROWS_COUNT, COLS_COUNT];

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < COLS_COUNT; j++)
                {
                    cells[i, j] = new Cell(i, j);
                }
            }
        }

        private void InitializeShips()
        {
            int totalShipsCount = DESTROYERS_COUNT + CRUISERS_COUNT + SUBMARINES_COUNT + BATTLESHIPS_COUNT;

            for (int i = 0; i < totalShipsCount; i++)
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
                ships.Add(ship);
            }
        }
    }
}
