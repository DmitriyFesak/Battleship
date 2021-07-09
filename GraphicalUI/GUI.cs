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
        private GUICell[,] _playerField, _enemyField;
        private ListBox _logs;
        private bool _isTurnEnded;
        private ShotResult _shotRes;
        public Game game { get; private set; }
        public GUI(GUICell[,] playerField, GUICell[,] enemyField, ListBox logs)
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
                    _enemyField[i, j].button.Click += new EventHandler(Shoot);
                }
            }

            while(!_isTurnEnded)
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
                    _enemyField[i, j].button.Click -= Shoot;
                }
            }
            
            for (int i = 0; i < game.GetRows(); i++)
            {
                for (int j = 0; j < game.GetCols(); j++)
                {
                    if (_enemyField[i, j].button == sender)
                    {
                        _shotRes = game.NextTurn(i, j);
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
        }

        public void SetPlayerName(int playerId, Game game)
        {
            throw new NotImplementedException();
        }

        public void ShowAlreadyShotMsg()
        {
            throw new NotImplementedException();
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
                        _playerField[i, j].button.Text = unshot;
                        _playerField[i, j].button.BackColor = Color.White;
                    }
                    else if (playerField[i, j] == CellStatus.Miss)
                    {
                        _playerField[i, j].button.Text = miss;
                        _playerField[i, j].button.BackColor = Color.Blue;
                    }
                    else if (playerField[i, j] == CellStatus.Hit)
                    {
                        _playerField[i, j].button.Text = hit;
                        _playerField[i, j].button.BackColor = Color.Red;
                    }
                    else
                    {
                        _playerField[i, j].button.Text = ship;
                        _playerField[i, j].button.BackColor = Color.LightGreen;
                    }

                    if (enemyField[i, j] == CellStatus.Unshot)
                    {
                        _enemyField[i, j].button.Text = unshot;
                        _enemyField[i, j].button.BackColor = Color.White;
                    }
                    else if (enemyField[i, j] == CellStatus.Miss)
                    {
                        _enemyField[i, j].button.Text = miss;
                        _enemyField[i, j].button.BackColor = Color.Blue;
                    }
                    else
                    {
                        _enemyField[i, j].button.Text = hit;
                        _enemyField[i, j].button.BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
