using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int MillisecondsPerTick = 500;
        private const int MillisecondsPerKeyPress = 100;

        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int CellSize = 25;
        private static readonly Color EmptyCellColor = Color.Yellow;
        private static readonly Color OccupiedCellColor = Color.Black;

        private readonly Button[,] buttons;

        private readonly Dictionary<Keys, Action> keyPressHandlers;
        private readonly Timer keyPressThrottler = new Timer();
        private readonly Timer ticker = new Timer();

        private Game game;
        private bool shouldProcessKeyPress = true;

        public YellowForm()
        {
            keyPressHandlers = new Dictionary<Keys, Action>
            {
                [Keys.Up] = () => game.Rotate(),
                [Keys.Left] = () => game.ShiftLeft(),
                [Keys.Right] = () => game.ShiftRight(),
                [Keys.Down] = () => game.SoftDrop(),
                [Keys.Space] = () => game.HardDrop()
            };

            keyPressThrottler.Interval = MillisecondsPerKeyPress;
            keyPressThrottler.Tick += (sender, e) =>
            {
                shouldProcessKeyPress = true;
                keyPressThrottler.Stop();
            };

            ticker.Interval = MillisecondsPerTick;
            ticker.Tick += (sender, e) => Tick();

            InitializeComponent();
            buttons = new Button[FieldWidth, FieldHeight];
            for (var x = 0; x < FieldWidth; ++x)
            {
                for (var y = 0; y < FieldHeight; ++y)
                {
                    buttons[x, y] = new Button
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(x * CellSize, y * CellSize),
                        Enabled = false
                    };
                    Controls.Add(buttons[x, y]);
                }
            }
        }

        private void YellowForm_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            game = new Game(FieldWidth, FieldHeight, 2017);
            Tick();
            ticker.Start();
        }

        private void Tick()
        {
            game.Tick();
            Render();
            if (game.IsOver)
            {
                HandleGameOver();
            }
        }

        private void HandleGameOver()
        {
            ticker.Stop();
            // TODO: Add funny icon.
            var result = MessageBox.Show("Game over. Try again?", "Game Over", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }
            NewGame();
        }

        private void Render()
        {
            var mask = game.GetFieldMask();
            for (var x = 0; x < mask.GetLength(0); ++x)
            {
                for (var y = 0; y < mask.GetLength(1); ++y)
                {
                    buttons[x, y].BackColor = mask[x, y] ? OccupiedCellColor : EmptyCellColor;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            if (!keyPressHandlers.ContainsKey(key))
            {
                return base.ProcessCmdKey(ref msg, key);
            }
            if (shouldProcessKeyPress)
            {
                keyPressHandlers[key]();
                Render();
                shouldProcessKeyPress = false;
                keyPressThrottler.Start();
            }
            return true;
        }
    }
}