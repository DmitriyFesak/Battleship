using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Game
    {
        private Player[] _player;
        private int _turn = 0;
        private IUserInterface _userInterface;

        public Game(IUserInterface userInterface)
        {
            _player = new Player[2];
            _player[0] = new Player("player1");
            _player[1] = new Player("player2");
            _userInterface = userInterface;
        }

        public void SetPlayerName(int playerId, string name)
        {
            _player[playerId].SetName(name);
        }

        public string GetPlayerName(int playerId)
        {
            return _player[playerId].GetName();
        }

        public Shot GetLastShot()
        { 
            int playerId = ChoosePlayer();

            return _player[playerId].GetLastShot();
        }

        public bool PlaceShipManually(int playerId, int shipId, int y, int x, int orientation)
        {
            return _player[playerId].PlaceShipManually(shipId, y, x, orientation);
        }

        public void AutoPlaceShips(int playerId)
        {
            _player[playerId].AutoPlaceShip();
        }

        public CellStatus[,] GetCurrentField()
        {
            return _player[0].GetCurrentField();
        }

        public CellStatus[,] GetEnemyField()
        {
            CellStatus[,] enemyField = _player[1].GetCurrentField();

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
                    } while (shotRes == ShotResult.AlreadyShot);

                    gameFinished = checkForVictory(_player[0]);
                }
                else
                {
                    do
                    {
                        shotRes = NextTurn();
                    } while (shotRes == ShotResult.AlreadyShot);

                    gameFinished = checkForVictory(_player[1]);
                }
                _userInterface.UpdateBattlefield(this);
            }

            if (checkForVictory(_player[0]))
            {
                return _player[0].GetName();
            }
            else
            {
                return _player[1].GetName();
            }
        }

        public ShotResult NextTurn(int y, int x)
        {
            Player currentPlayer = _player[ChoosePlayer()];

            if (currentPlayer.MakeShot(y, x, _player[1]))
            {
                CheckShotResult(currentPlayer);

                return currentPlayer.GetLastShot().GetResult();
            }
            else
            {
                _turn--;

                return ShotResult.AlreadyShot;
            }
        }

        public ShotResult NextTurn()
        {
            Player currentPlayer = _player[ChoosePlayer()];
            Random rnd = new Random();
            int y = rnd.Next(0, currentPlayer.GetRowsOfField());
            int x = rnd.Next(0, currentPlayer.GetColsOfField());

            if (currentPlayer.MakeShot(y, x, _player[0]))
            {
                CheckShotResult(currentPlayer);

                return currentPlayer.GetLastShot().GetResult();
            }
            else
            {
                _turn--;

                return ShotResult.AlreadyShot;
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
            return _player[0].GetRowsOfField();
        }

        public int GetCols()
        {
            return _player[0].GetColsOfField();
        }
    }
}
