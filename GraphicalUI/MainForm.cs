using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Battleship;

namespace GraphicalUI
{
    public partial class MainForm : Form
    {
        private Game _game;
        private GUI _gui;
        private Button[,] _playerField, _enemyField;
        private ListBox _logs;
        private Button _btnAutoPLace, _btnStartGame, _btnNewGame;
        TextBox _playerName, _enemyName;

        private const int CELLSIZE = 30;
        private const int SPACEBETWEENFIELDS = 380;
        private const int ROWS = 10;
        private const int COLS = 10;
        public MainForm()
        {
            InitializeComponent();
            
            _logs = new ListBox();
            _logs.Location = new Point(0, CELLSIZE * ROWS + 140);
            _logs.Width = 710;
            this.Controls.Add(_logs);

            _btnAutoPLace = new Button();
            _btnAutoPLace.Text = "Random";
            _btnAutoPLace.AutoSize = true;
            _btnAutoPLace.Font = new Font("Arial", 15, FontStyle.Regular);
            _btnAutoPLace.Location = new Point(0, CELLSIZE * ROWS + 90);
            _btnAutoPLace.Enabled = false;
            _btnAutoPLace.Click += new EventHandler((object sender, EventArgs e) =>
            {
                _gui.RandomRelocate();
                _gui.UpdateBattlefield(_game);
            });
            this.Controls.Add(_btnAutoPLace);

            _btnStartGame = new Button();
            _btnStartGame.Text = "Start Game";
            _btnStartGame.AutoSize = true;
            _btnStartGame.Font = new Font("Arial", 15, FontStyle.Regular);
            _btnStartGame.Location = new Point(100, CELLSIZE * ROWS + 90);
            _btnStartGame.Enabled = false;
            _btnStartGame.Click += new EventHandler((object sender, EventArgs e) =>
            {
                _btnAutoPLace.Enabled = false;
                _btnStartGame.Enabled = false;
                _btnNewGame.Enabled = true;
                _playerName.Enabled = false;
                _enemyName.Enabled = false;

                _game.SetPlayerName(0, _playerName.Text);
                _game.SetPlayerName(1, _enemyName.Text);

                MessageBox.Show("Congratulations, " + _gui.StartGame(_game) + "! You won!", "Victory!");
                if (MessageBox.Show("Restart?", "new game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NewGame();
                }
            });
            this.Controls.Add(_btnStartGame);

            _btnNewGame = new Button();
            _btnNewGame.Text = "New Game";
            _btnNewGame.AutoSize = true;
            _btnNewGame.Font = new Font("Arial", 15, FontStyle.Regular);
            _btnNewGame.Location = new Point(230, CELLSIZE * ROWS + 90);
            _btnNewGame.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (MessageBox.Show("Restart?", "new game", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NewGame();
                }
            });
            this.Controls.Add(_btnNewGame);

            NewGame();
        }

        public void NewGame()
        {
            if (_enemyField != null && _playerField != null)
            {
                for (int i = 0; i < ROWS; i++)
                {
                    for (int j = 0; j < COLS; j++)
                    {
                        this.Controls.Remove(_playerField[i, j]);
                        this.Controls.Remove(_enemyField[i, j]);
                    }
                }
            }

            this.Controls.Remove(_playerName);
            this.Controls.Remove(_enemyName);
           
            InitializeFields();

            _logs.Items.Clear();
            _btnAutoPLace.Enabled = true;
            _btnStartGame.Enabled = true;
            _btnNewGame.Enabled = false;
            _playerName.Enabled = true;
            _enemyName.Enabled = true;

            _gui = new GUI(_playerField, _enemyField, _logs);
            _game = _gui.game;
        }

        private void InitializeFields()
        {
            PlayerFieldInit();
            EnemyFieldInit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PlayerFieldInit()
        {
            _playerField = new Button[ROWS, COLS];

            _playerName = new TextBox();
            _playerName.Width = 240;
            _playerName.Text = "player1";
            _playerName.Font = new Font("Arial", 22, FontStyle.Regular);
            _playerName.Location = new Point(60, 5);
            this.Controls.Add(_playerName);

            for (int i = 0; i < COLS; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(CELLSIZE + i * CELLSIZE, 50);
                btn.Size = new Size(CELLSIZE, CELLSIZE);
                btn.Text = i.ToString();
                this.Controls.Add(btn);
            }

            for (int i = 0; i < ROWS; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(0, 50 + CELLSIZE + i * CELLSIZE);
                btn.Size = new Size(CELLSIZE, CELLSIZE);
                btn.Text = i.ToString();
                this.Controls.Add(btn);
            }

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    Button btn = new Button();
                    btn.Location = new Point(CELLSIZE + j * CELLSIZE, 80 + i * CELLSIZE);
                    btn.Size = new Size(CELLSIZE, CELLSIZE);
                    this.Controls.Add(btn);
                    _playerField[i, j] = btn;
                }
            }
        }

        private void EnemyFieldInit()
        {
            _enemyField = new Button[ROWS, COLS];

            _enemyName = new TextBox();
            _enemyName.Width = 240;
            _enemyName.Text = "player2";
            _enemyName.Font = new Font("Arial", 22, FontStyle.Regular);
            _enemyName.Location = new Point(SPACEBETWEENFIELDS + 60, 5);
            this.Controls.Add(_enemyName);

            for (int i = 0; i < COLS; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(SPACEBETWEENFIELDS + CELLSIZE + i * CELLSIZE, 50);
                btn.Size = new Size(CELLSIZE, CELLSIZE);
                btn.Text = i.ToString();
                this.Controls.Add(btn);
            }

            for (int i = 0; i < ROWS; i++)
            {
                Button btn = new Button();
                btn.Location = new Point(SPACEBETWEENFIELDS, 50 + CELLSIZE + i * CELLSIZE);
                btn.Size = new Size(CELLSIZE, CELLSIZE);
                btn.Text = i.ToString();
                this.Controls.Add(btn);
            }

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    Button btn = new Button();
                    btn.Location = new Point(SPACEBETWEENFIELDS + CELLSIZE + j * CELLSIZE, 80 + i * CELLSIZE);
                    btn.Size = new Size(CELLSIZE, CELLSIZE);
                    this.Controls.Add(btn);
                    _enemyField[i, j] = btn;
                }
            }
        }
    }
}
