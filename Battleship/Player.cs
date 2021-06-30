using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Player
    {
        string name;
        Battlefield field;
        int shipsLeft;
        List<Shot> shotsHistory;

        public Player(string name)
        {
            SetName(name);
            field = new Battlefield();
            shipsLeft = field.GetTotalShipsCount();
            shotsHistory = new List<Shot>();
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public int GetLastShotResult()
        {
            return shotsHistory[shotsHistory.Count - 1].GetResult();
        }

        public int GetLastShotX()
        {
            return shotsHistory[shotsHistory.Count - 1].GetX();
        }

        public int GetLastShotY()
        {
            return shotsHistory[shotsHistory.Count - 1].GetY();
        }

        public bool PlaceShipManually(int shipId, int y, int x, int orientation)
        {
            return field.PlaceShipManually(shipId, y, x, orientation);
        }

        public void AutoPlaceShip()
        {
            field.AutoPlaceShips();
        }

        public int[,] GetCurrentField()
        {
            return field.GetBattlefield();
        }

        public int[,] GetEnemyField(Player enemy)
        {
            int[,] enemyField = enemy.GetCurrentField();
            for (int i = 0; i < enemyField.GetLength(0); i++)
            {
                for (int j = 0; j < enemyField.GetLength(1); j++)
                {
                    if (enemyField[i, j] == ((int)Enumerables.CellStatus.unshotShip))
                    {
                        enemyField[i, j] = ((int)Enumerables.CellStatus.unshot);
                    }
                }
            }
            return enemyField;
        }

        public void MakeShot(int y, int x)
        {
            Shot shot = new Shot(y, x);
            shot.SetResult(field.CheckCell(y, x));
            shotsHistory.Add(shot);
        }

        public int GetAmountOfShips()
        {
            return shipsLeft;
        }
    }
}
