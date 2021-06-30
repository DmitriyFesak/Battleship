using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Game
    {
        private Player[] player;
        private int turn = 0;
        private IUserInterface userInterface;

        public Game()
        {
            player = new Player[2];
            player[0] = new Player("player1");
            player[1] = new Player("player2");
            userInterface = new ConsoleUI();
        }

        public void SetPlayerName(int playerId, string name)
        {
            player[playerId].SetName(name);
        }

        public string GetPlayerName(int playerId)
        {
            return player[playerId].GetName();
        }

        public string GetLastShot()
        {
            int playerId = ChoosePlayer();
            int res = player[playerId].GetLastShotResult();
            int x = player[playerId].GetLastShotX();
            int y = player[playerId].GetLastShotY();

            string lastShot;
            if (res == ((int)Enumerables.ShotResult.miss))
            {
                lastShot = Enumerables.ShotResult.miss.ToString();
            }
            else if (res == ((int)Enumerables.ShotResult.damaged))
            {
                lastShot = Enumerables.ShotResult.damaged.ToString();
            }
            else
            {
                lastShot = Enumerables.ShotResult.destroyed.ToString();
            }
            return lastShot + " " + y + " " + x;
        }

        public bool PlaceShipManually(int playerId, int shipId, int y, int x, int orientation)
        {
            return player[playerId].PlaceShipManually(shipId, y, x, orientation);
        }

        public void AutoPlaceShips(int playerId)
        {
            player[playerId].AutoPlaceShip();
        }

        public int[,] GetCurrentField()
        {
            int playerId = ChoosePlayer();
            return player[playerId].GetCurrentField();
        }

        public int[,] GetEnemyField()
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
            return player[playerId].GetEnemyField(player[enemyId]);
        }

        public void StartGame()
        {
            AutoPlaceShips(1);

            bool gameFinished = false;
            while (!gameFinished)
            {
                turn++;
                if (ChoosePlayer() == 0)
                {
                    userInterface.MakeMove();
                    gameFinished = checkForVictory(player[0]);
                }
                else
                {
                    NextTurn();
                    gameFinished = checkForVictory(player[1]);
                }
            }
        }

        public int NextTurn(int y, int x)
        {
            Player currentPlayer = player[ChoosePlayer()];
            if (currentPlayer.MakeShot(y, x))
            {
                CheckShotResult(currentPlayer);
                return currentPlayer.GetLastShotResult();
            }
            else
            {
                turn--;
                return ((int)Enumerables.ShotResult.alreadyShot);
            }
        }

        public int NextTurn()
        {
            Player currentPlayer = player[ChoosePlayer()];
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
                turn--;
                return ((int)Enumerables.ShotResult.alreadyShot);
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
            if (turn % 2 == 1)
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
            int shotResult = currentPlayer.GetLastShotResult();
            if (shotResult == ((int)Enumerables.ShotResult.damaged) || shotResult == ((int)Enumerables.ShotResult.destroyed))
            {
                turn--;
            }
        }
    }
}
