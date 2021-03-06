using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Player
    {
        private string _name;
        private Battlefield _field;
        private int _shipsLeft;
        private List<Shot> _shotsHistory;

        public Player(string name)
        {
            SetName(name);
            _field = new Battlefield();
            _shipsLeft = _field.GetTotalShipsCount();
            _shotsHistory = new List<Shot>();
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public Shot GetLastShot()
        {
            return _shotsHistory[_shotsHistory.Count - 1];
        }

        public ShotResult GetShotResult(int y, int x)
        {
            ShotResult res = ShotResult.AlreadyShot;
            for (int i = 0; i < _shotsHistory.Count; i++)
            {
                if (_shotsHistory[i].GetY() == y && _shotsHistory[i].GetX() == x)
                {
                    res = _shotsHistory[i].GetResult();
                    break;
                }
            }
            return res;
        }

        public bool PlaceShipManually(int shipId, int y, int x, int orientation)
        {
            return _field.PlaceShipManually(shipId, y, x, orientation);
        }

        public void AutoPlaceShip()
        {
            _field.AutoPlaceShips();
        }

        public CellStatus[,] GetCurrentField()
        {
            return _field.GetBattlefield();
        }

        public bool MakeShot(int y, int x, Player enemy)
        {
            Shot shot = new Shot(y, x);
            shot.SetResult(enemy._field.CheckCell(y, x));

            if (shot.GetResult() == ShotResult.Destroyed)
            {
                _shipsLeft--;
            }
            else if (shot.GetResult() == ShotResult.AlreadyShot)
            {
                return false;
            }
            _shotsHistory.Add(shot);

            return true;
        }

        public void UnsetShipLocation(int shipId)
        {
            _field.UnsetShipLocation(shipId);
        }

        public int GetAmountOfShips()
        {
            return _shipsLeft;
        }

        public int GetRowsOfField()
        {
            return _field.GetRows();
        }

        public int GetColsOfField()
        {
            return _field.GetCols();
        }
    }
}
