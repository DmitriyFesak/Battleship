using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Battleship;

namespace GraphicalUI
{
    class GUI : IUserInterface
    {
        private Button[,] _playerField, _enemyField;
        private ListBox _logs;
        private bool _isTurnEnded;
        private ShotResult _shotRes;
        private int _x, _y;
        public Game game { get; private set; }
        public GUI(Button[,] playerField, Button[,] enemyField, ListBox logs)
        {
            _playerField = playerField;
            _enemyField = enemyField;
            _logs = logs;
            game = new Game(this);

            AutoLocation(game);
            UpdateBattlefield(game);
        }
        public void AutoLocation(Game game)
        {
            game.AutoPlaceShips(0);
        }

        public void RandomRelocate()
        {
            for (int i = 0; i < 10; i++)
            {
                game.UnsetShipLocation(0, i);
            }
            AutoLocation(game);
        }

        public ShotResult MakeMove(Game game)
        {
            _isTurnEnded = false;
            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    _enemyField[i, j].Click += new EventHandler(Shoot);
                }
            }

            while (!_isTurnEnded)
            {
                Application.DoEvents();
            }

            return _shotRes;
        }

        private void Shoot(object sender, EventArgs e)
        {
            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    _enemyField[i, j].Click -= Shoot;
                }
            }
            
            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    if (_enemyField[i, j] == sender)
                    {
                        _y = i;
                        _x = j;
                        _shotRes = game.NextTurn(_y, _x);
                        _isTurnEnded = true;
                        break;
                    }
                }
            }
        }

        public void ManualLocation()
        {
            throw new NotImplementedException();
        }

        public void PrintResult(Shot shot, string name)
        {
            _logs.Items.Add(name + " is shooting at coordinates: Y = " + shot.GetY() + "; X = " + shot.GetX());

            if (shot.GetResult() == ShotResult.Miss)
            {
                _logs.Items.Add("Miss!");
            }
            else if (shot.GetResult() == ShotResult.Destroyed)
            {
                _logs.Items.Add("Ship was destroyed!");
            }
            else if (shot.GetResult() == ShotResult.Damaged)
            {
                _logs.Items.Add("Ship was damaged!");
            }

            _logs.TopIndex = _logs.Items.Count - 1;
        }

        public void ShowAlreadyShotMsg()
        {
            MessageBox.Show("These coordinates have already been shot! Result = " + game.GetShotResult(_y, _x));
        }

        public string StartGame(Game game)
        {
            return game.Start();
        }

        public void UpdateBattlefield(Game game)
        {
            const string ship = "*";
            const string miss = "-";
            const string hit = "x";
            const string unshot = "";

            CellStatus[,] playerField = game.GetCurrentField();
            CellStatus[,] enemyField = game.GetEnemyField();

            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    if (playerField[i, j] == CellStatus.Unshot)
                    {
                        _playerField[i, j].Text = unshot;
                        _playerField[i, j].BackColor = Color.White;
                    }
                    else if (playerField[i, j] == CellStatus.Miss)
                    {
                        _playerField[i, j].Text = miss;
                        _playerField[i, j].BackColor = Color.Blue;
                    }
                    else if (playerField[i, j] == CellStatus.Hit)
                    {
                        _playerField[i, j].Text = hit;
                        _playerField[i, j].BackColor = Color.Red;
                    }
                    else
                    {
                        _playerField[i, j].Text = ship;
                        _playerField[i, j].BackColor = Color.LightGreen;
                    }

                    if (enemyField[i, j] == CellStatus.Unshot)
                    {
                        _enemyField[i, j].Text = unshot;
                        _enemyField[i, j].BackColor = Color.White;
                    }
                    else if (enemyField[i, j] == CellStatus.Miss)
                    {
                        _enemyField[i, j].Text = miss;
                        _enemyField[i, j].BackColor = Color.Blue;
                    }
                    else
                    {
                        _enemyField[i, j].Text = hit;
                        _enemyField[i, j].BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
