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
        private Game game;
        private GUICell[,] _playerField, _enemyField;
        private const int CELLSIZE = 30;
        private const int SPACEBETWEENFIELDS = 380;
        private const int ROWS = 10;
        private const int COLS = 10;
        public MainForm()
        {
            InitializeComponent();

            InitializeFields();

            ListBox logs = new ListBox();
            logs.Location = new Point(0, CELLSIZE * ROWS + 140);
            logs.Width = 710;
            this.Controls.Add(logs);

            GUI gui = new GUI(_playerField, _enemyField, logs);
            game = gui.game;

            Button btnAutoPLace = new Button();
            btnAutoPLace.Text = "Random";
            btnAutoPLace.AutoSize = true;
            btnAutoPLace.Font = new Font("Arial", 15, FontStyle.Regular);
            btnAutoPLace.Location = new Point(0, CELLSIZE * ROWS + 90);
            btnAutoPLace.Click += new EventHandler((object sender, EventArgs e) => 
            {
                gui.RandomRelocate();
                gui.UpdateBattlefield(game);
            });
            this.Controls.Add(btnAutoPLace);

            Button btnStartGame = new Button();
            btnStartGame.Text = "Start Game";
            btnStartGame.AutoSize = true;
            btnStartGame.Font = new Font("Arial", 15, FontStyle.Regular);
            btnStartGame.Location = new Point(100, CELLSIZE * ROWS + 90);
            this.Controls.Add(btnStartGame);
            btnStartGame.Click += new EventHandler((object sender, EventArgs e) =>
            {
                btnAutoPLace.Enabled = false;
                btnStartGame.Enabled = false;
                MessageBox.Show(gui.StartGame(game) + " won!");
            });
        }

        private void InitializeFields()
        {
            PlayerFieldInit();
            EnemyFieldInit();
        }

        private void PlayerFieldInit()
        {
            _playerField = new GUICell[ROWS, COLS];

            Label lbl = new Label();
            lbl.Text = "Your field";
            lbl.AutoSize = true;
            lbl.Font = new Font("Arial", 24, FontStyle.Regular);
            lbl.Location = new Point(115, 10);
            this.Controls.Add(lbl);

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
                    _playerField[i, j] = new GUICell(btn);
                }
            }
        }

        private void EnemyFieldInit()
        {
            _enemyField = new GUICell[ROWS, COLS];

            Label lbl = new Label();
            lbl.Text = "Enemy field";
            lbl.AutoSize = true;
            lbl.Font = new Font("Arial", 24, FontStyle.Regular);
            lbl.Location = new Point(SPACEBETWEENFIELDS + 90, 10);
            this.Controls.Add(lbl);

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
                    _enemyField[i, j] = new GUICell(btn);
                }
            }
        }
    }
}
