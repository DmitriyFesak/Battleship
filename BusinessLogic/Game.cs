using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class Game
    {
        private Player[] _players;
        private int _turn = 0;
        private IUserInterface _userInterface;

        private bool _botFinishingShip = false;
        private List<Shot> _possibleShots;
        private Shot _firstHit;
        private Shot nextShot;

        public Game(IUserInterface userInterface)
        {
            _players = new Player[2];
            _players[0] = new Player("player1");
            _players[1] = new Player("player2");
            _userInterface = userInterface;
        }

        public void SetPlayerName(int playerId, string name)
        {
            _players[playerId].SetName(name);
        }

        public string GetPlayerName(int playerId)
        {
            return _players[playerId].GetName();
        }

        public Shot GetLastShot()
        { 
            int playerId = ChoosePlayer();

            return _players[playerId].GetLastShot();
        }

        public bool PlaceShipManually(int playerId, int shipId, int y, int x, int orientation)
        {
            return _players[playerId].PlaceShipManually(shipId, y, x, orientation);
        }

        public void AutoPlaceShips(int playerId)
        {
            _players[playerId].AutoPlaceShip();
        }

        public CellStatus[,] GetCurrentField()
        {
            return _players[0].GetCurrentField();
        }

        public CellStatus[,] GetEnemyField()
        {
            CellStatus[,] enemyField = _players[1].GetCurrentField();

            for (int i = 0; i < enemyField.GetLength(0); i++)
            {
                for (int j = 0; j < enemyField.GetLength(1); j++)
                {
                    if (enemyField[i, j] == CellStatus.UnshotShip)
                    {
                        enemyField[i, j] = CellStatus.Unshot;
                    }
                }
            }

            return enemyField;
        }

        public string Start()
        {
            AutoPlaceShips(1);

            bool gameFinished = false;
            ShotResult shotRes;

            _userInterface.UpdateBattlefield(this);

            while (!gameFinished)
            {
                _turn++;

                if (ChoosePlayer() == 0)
                {
                    do
                    {
                        shotRes = _userInterface.MakeMove(this);

                        if (shotRes == ShotResult.AlreadyShot)
                        {
                            _userInterface.ShowAlreadyShotMsg();
                        }
                    } while (shotRes == ShotResult.AlreadyShot);

                    _userInterface.PrintResult(_players[0].GetLastShot(), _players[0].GetName());
                    gameFinished = checkForVictory(_players[0]);
                }
                else
                {
                    do
                    {
                        shotRes = NextTurn();
                    } while (shotRes == ShotResult.AlreadyShot);

                    _userInterface.PrintResult(_players[1].GetLastShot(), _players[1].GetName());
                    gameFinished = checkForVictory(_players[1]);
                }
                _userInterface.UpdateBattlefield(this);
            }

            if (checkForVictory(_players[0]))
            {
                return _players[0].GetName();
            }
            else
            {
                return _players[1].GetName();
            }
        }

        public ShotResult NextTurn(int y, int x)
        {
            Player currentPlayer = _players[ChoosePlayer()];

            if (currentPlayer.MakeShot(y, x, _players[1]))
            {
                CheckShotResult(currentPlayer);

                return currentPlayer.GetLastShot().GetResult();
            }
            else
            {
                return ShotResult.AlreadyShot;
            }
        }

        public ShotResult NextTurn()
        {
            Player currentPlayer = _players[ChoosePlayer()];
            int y, x;

            if (_botFinishingShip)
            {
                y = nextShot.GetY();
                x = nextShot.GetX();
            }
            else
            {
                Shot preparedShot = GetOptimalShotCoords();
                y = preparedShot.GetY();
                x = preparedShot.GetX();
            }

            if (currentPlayer.MakeShot(y, x, _players[0]))
            {
                CheckShotResult(currentPlayer);

                FinishingDamagedShip();

                return currentPlayer.GetLastShot().GetResult();
            }
            else
            {
                return ShotResult.AlreadyShot;
            }
        }

        private Shot GetOptimalShotCoords()
        {
            Random rnd = new Random();

            int y, x;

            bool isOptimalShot;

            do
            {
                isOptimalShot = true;

                y = rnd.Next(0, GetRows());
                x = rnd.Next(0, GetCols());

                CellStatus[,] field = GetCurrentField();

                if (y != 0)
                {
                    if (field[y - 1, x] == CellStatus.Hit)
                    {
                        isOptimalShot = false;
                    }
                }
                if (y != GetRows() - 1)
                {
                    if (field[y + 1, x] == CellStatus.Hit)
                    {
                        isOptimalShot = false;
                    }
                }
                if (x != 0)
                {
                    if (field[y, x - 1] == CellStatus.Hit)
                    {
                        isOptimalShot = false;
                    }
                }
                if (x != GetCols() - 1)
                {
                    if (field[y, x + 1] == CellStatus.Hit)
                    {
                        isOptimalShot = false;
                    }
                }
            } while (!isOptimalShot);

            return new Shot(y, x);
        }

        private void FinishingDamagedShip()
        {
            Shot lastShot = _players[1].GetLastShot();

            if (lastShot.GetResult() == ShotResult.Damaged && _botFinishingShip == false)
            {
                _botFinishingShip = true;
                _firstHit = lastShot;
                _possibleShots = new List<Shot>();

                if (lastShot.GetY() != 0)
                {
                    if (GetCurrentField()[lastShot.GetY() - 1, lastShot.GetX()] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY() - 1, lastShot.GetX()));
                    }
                }
                if (lastShot.GetY() != GetRows() - 1)
                {
                    if (GetCurrentField()[lastShot.GetY() + 1, lastShot.GetX()] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY() + 1, lastShot.GetX()));
                    }
                }
                if (lastShot.GetX() != 0)
                {
                    if (GetCurrentField()[lastShot.GetY(), lastShot.GetX() - 1] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY(), lastShot.GetX() - 1));
                    }
                }
                if (lastShot.GetX() != GetCols() - 1)
                {
                    if (GetCurrentField()[lastShot.GetY(), lastShot.GetX() + 1] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY(), lastShot.GetX() + 1));
                    }
                }

                nextShot = _possibleShots[_possibleShots.Count - 1];
            }
            else if (lastShot.GetResult() == ShotResult.Damaged && _firstHit != lastShot)
            {
                if (_firstHit.GetY() < lastShot.GetY() && lastShot.GetY() != GetRows() - 1)
                {
                    if (GetCurrentField()[lastShot.GetY() + 1, lastShot.GetX()] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY() + 1, lastShot.GetX()));
                    }
                }
                else if (_firstHit.GetY() > lastShot.GetY() && lastShot.GetY() != 0)
                {
                    if (GetCurrentField()[lastShot.GetY() - 1, lastShot.GetX()] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY() - 1, lastShot.GetX()));
                    }
                }
                else if (_firstHit.GetX() < lastShot.GetX() && lastShot.GetX() != GetCols() - 1)
                {
                    if (GetCurrentField()[lastShot.GetY(), lastShot.GetX() + 1] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY(), lastShot.GetX() + 1));
                    }
                }
                else if (_firstHit.GetX() > lastShot.GetX() && lastShot.GetX() != 0)
                {
                    if (GetCurrentField()[lastShot.GetY(), lastShot.GetX() - 1] != CellStatus.Miss)
                    {
                        _possibleShots.Add(new Shot(lastShot.GetY(), lastShot.GetX() - 1));
                    }
                }

                nextShot = _possibleShots[_possibleShots.Count - 1];
            }
            else
            {
                if (lastShot.GetResult() == ShotResult.Destroyed && _botFinishingShip)
                {
                    _botFinishingShip = false;
                    _firstHit = null;
                    _possibleShots = null;
                }
                else
                {
                    if (_botFinishingShip)
                    {
                        do
                        {
                            nextShot = null;

                            if (GetCurrentField()[_possibleShots[_possibleShots.Count - 1].GetY(), _possibleShots[_possibleShots.Count - 1].GetX()] != CellStatus.Hit
                                && GetCurrentField()[_possibleShots[_possibleShots.Count - 1].GetY(), _possibleShots[_possibleShots.Count - 1].GetX()] != CellStatus.Miss)
                            {
                                nextShot = _possibleShots[_possibleShots.Count - 1];
                                _possibleShots.RemoveAt(_possibleShots.Count - 1);
                            }
                            else
                            {
                                _possibleShots.RemoveAt(_possibleShots.Count - 1);
                            }
                        } while (nextShot == null);
                    }
                }
            }
        }

        private bool checkForVictory(Player currentPlayer)
        {
            if (currentPlayer.GetAmountOfShips() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int ChoosePlayer()
        {
            int playerId;

            if (_turn % 2 == 1)
            {
                playerId = 0;
            }
            else
            {
                playerId = 1;
            }

            return playerId;
        }

        private void CheckShotResult(Player currentPlayer)
        {
            ShotResult shotResult = currentPlayer.GetLastShot().GetResult();

            if (shotResult == ShotResult.Damaged || shotResult == ShotResult.Destroyed)
            {
                _turn--;
            }
        }

        public int GetRows()
        {
            return _players[0].GetRowsOfField();
        }

        public int GetCols()
        {
            return _players[0].GetColsOfField();
        }
    }
}
