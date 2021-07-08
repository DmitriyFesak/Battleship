using System;
using System.Collections.Generic;
using System.Text;
using Battleship;

namespace BattleshipConsoleUI
{
    public class BattleshipConsoleUI : IUserInterface
    {
        private static BattleshipConsoleUI _consoleUI = new BattleshipConsoleUI();
        private static bool _isStarted = false;

        public static void Main()
        {
            Game game = new Game(_consoleUI);
            string winner;

            while (!_isStarted)
            {
                int choice = _consoleUI.ShowPreStartUI();
                
                switch(choice)
                {
                    case 1:
                        _consoleUI.SetPlayerName(0, game);
                        break;
                    case 2:
                        _consoleUI.SetPlayerName(1, game);
                        break;
                    case 3:
                        _isStarted = true;
                        break;
                    case 4:
                        _isStarted = true;
                        _consoleUI.AutoLocation(game);
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }
            }

            winner = _consoleUI.StartGame(game);
            Console.WriteLine("Congratulations, " + winner + "! You won!");
        }

        public int ShowPreStartUI()
        {
            Console.WriteLine("Wellcome to battleship!");
            int choice = 0;
            bool success = false;

            do
            {
                Console.WriteLine("Input 1 to change your name;");
                Console.WriteLine("Input 2 to change enemy's name;");
                Console.WriteLine("Input 3 to place ships manually;");
                Console.WriteLine("Input 4 to place ships randomly;");
                Console.WriteLine("Input 0 to exit;");
                success = Int32.TryParse(Console.ReadLine(), out choice);
                if (!success)
                {
                    choice = -1;
                }
            } while (!success || choice < 0 || choice > 4);

            return choice;
        }
        public void AutoLocation(Game game)
        {
            game.AutoPlaceShips(0);
        }

        public ShotResult MakeMove(Game game)
        {
            bool correctInput = false;
            int x, y;

            Console.WriteLine("Your turn!");

            do
            {
                Console.Write("Input Y:");
                correctInput = Int32.TryParse(Console.ReadLine(), out y);
            } while (!correctInput || y < 0 || y >= game.GetRows());

            do
            {
                Console.Write("Input X:");
                correctInput = Int32.TryParse(Console.ReadLine(), out x);
            } while (!correctInput || x < 0 || x >= game.GetCols());

            return game.NextTurn(y, x);
        }

        public void ManualLocation()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerName(int playerId, Game game)
        {
            if (playerId == 0)
            {
                Console.Write("Input your new name: ");
                game.SetPlayerName(0, Console.ReadLine());
            }
            else
            {
                Console.Write("Input your enemy's new name: ");
                game.SetPlayerName(1, Console.ReadLine());
            }
        }

        public void UpdateBattlefield(Game game)
        {
            const string ship = "*";
            const string miss = "-";
            const string hit = "x";
            const string unshot = " ";

            string[,] playerFieldToShow = new string[game.GetRows(), game.GetCols()];
            string[,] enemyFieldToShow = new string[game.GetRows(), game.GetCols()];

            CellStatus[,] playerField = game.GetCurrentField();
            CellStatus[,] enemyField = game.GetEnemyField();

            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    if (playerField[i, j] == CellStatus.Unshot)
                    {
                        playerFieldToShow[i, j] = unshot;
                    }
                    else if (playerField[i, j] == CellStatus.Miss)
                    {
                        playerFieldToShow[i, j] = miss;
                    }
                    else if (playerField[i, j] == CellStatus.Hit)
                    {
                        playerFieldToShow[i, j] = hit;
                    }
                    else
                    {
                        playerFieldToShow[i, j] = ship;
                    }

                    if (enemyField[i, j] == CellStatus.Unshot)
                    {
                        enemyFieldToShow[i, j] = unshot;
                    }
                    else if (enemyField[i, j] == CellStatus.Miss)
                    {
                        enemyFieldToShow[i, j] = miss;
                    }
                    else
                    {
                        enemyFieldToShow[i, j] = hit;
                    }
                }
            }

            PrintBattlefield(playerFieldToShow, enemyFieldToShow);
            Console.ReadKey();
        }

        public string StartGame(Game game)
        {
            return game.Start();
        }

        private void PrintBattlefield(string [,] battlefield, string[,] enemyBattlefield)
        {
            Console.WriteLine("Your field                       Enemy field");
            Console.WriteLine("———————————————————————          ———————————————————————");
            Console.WriteLine("| |0|1|2|3|4|5|6|7|8|9|          | |0|1|2|3|4|5|6|7|8|9|");
            Console.WriteLine("———————————————————————          ———————————————————————");

            for (int i = 0; i < battlefield.GetLength(0); i++)
            {
                Console.Write("|" + i);

                for (int j = 0; j < battlefield.GetLength(1); j++)
                {
                    Console.Write("|" + battlefield[i, j]);
                }

                Console.Write("|          |" + i);

                for (int j = 0; j < enemyBattlefield.GetLength(1); j++)
                {
                    Console.Write("|" + enemyBattlefield[i, j]);
                }

                Console.Write("|\n");
            }

            Console.WriteLine("———————————————————————          ———————————————————————");
        }

        public void PrintResult(Shot shot, string name)
        {
            Console.WriteLine(name + " is shooting at coordinates: Y = " + shot.GetY() + "; X = " + shot.GetX());

            if (shot.GetResult() == ShotResult.Miss)
            {
                Console.WriteLine("Miss!");
            }
            else if (shot.GetResult() == ShotResult.Destroyed)
            {
                Console.WriteLine("Ship was destroyed!");
            }
            else if (shot.GetResult() == ShotResult.Damaged)
            {
                Console.WriteLine("Ship was damaged!");
            }
        }

        public void ShowAlreadyShotMsg()
        {
            Console.WriteLine("These coordinates have already been shot!");
        }
    }
}
