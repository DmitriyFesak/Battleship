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

        public string GetLastShot()
        { // TODO: String to ui
            int playerId = ChoosePlayer();
            ShotResult res = _player[playerId].GetLastShotResult();
            int x = _player[playerId].GetLastShotX();
            int y = _player[playerId].GetLastShotY();

            string lastShot;

            if (res == ShotResult.Miss)
            {
                lastShot = ShotResult.Miss.ToString();
            }
            else if (res == ((int)ShotResult.Damaged))
            {
                lastShot = ShotResult.Damaged.ToString();
            }
            else
            {
                lastShot = ShotResult.Destroyed.ToString();
            }

            return lastShot + " " + y + " " + x;
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
            int playerId = ChoosePlayer();

            return _player[playerId].GetCurrentField();
        }

        public CellStatus[,] GetEnemyField()
        {
            int playerId = ChoosePlayer();
            int enemyId;

            if (playerId == 0)
            {
                enemyId = 1;
            }
            else
            {
                enemyId = 0;
            }

            return _player[playerId].GetEnemyField(_player[enemyId]);
        }

        public void Start()
        {
            AutoPlaceShips(1);

            bool gameFinished = false;

            while (!gameFinished)
            {
                _turn++;
                if (ChoosePlayer() == 0)
                {
                    _userInterface.MakeMove();
                    gameFinished = checkForVictory(_player[0]);
                }
                else
                {
                    NextTurn();
                    gameFinished = checkForVictory(_player[1]);
                }
            }
        }

        public ShotResult NextTurn(int y, int x)
        {
            Player currentPlayer = _player[ChoosePlayer()];

            if (currentPlayer.MakeShot(y, x))
            {
                CheckShotResult(currentPlayer);

                return currentPlayer.GetLastShotResult();
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

            if (currentPlayer.MakeShot(y, x))
            {
                CheckShotResult(currentPlayer);

                return currentPlayer.GetLastShotResult();
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
            ShotResult shotResult = currentPlayer.GetLastShotResult();

            if (shotResult == ShotResult.Damaged || shotResult == ShotResult.Destroyed)
            {
                _turn--;
            }
        }
    }
}
