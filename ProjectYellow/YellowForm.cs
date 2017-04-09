using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int MillisecondsPerTick = 500;
        private const int MillisecondsPerKeyPress = 200;

        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int CellSize = 25;
        private static readonly Color EmptyCellColor = Color.Yellow;
        private static readonly Color OccupiedCellColor = Color.Black;

        private readonly Button[,] buttons;

        private readonly Dictionary<Keys, Action> keyPressHandlers;
        private readonly Dictionary<Keys, Timer> keyPressTimers = new Dictionary<Keys, Timer>();

        private Game game;
        private Timer ticker;

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
            ticker = Utils.SetIntervalAndFire(MillisecondsPerTick, Tick);
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

        protected override bool IsInputKey(Keys key)
        {
            return keyPressHandlers.ContainsKey(key);
        }

        private void YellowForm_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (!keyPressHandlers.ContainsKey(key) || keyPressTimers.ContainsKey(key))
            {
                return;
            }
            keyPressTimers[key] = Utils.SetIntervalAndFire(MillisecondsPerKeyPress, () =>
            {
                keyPressHandlers[key]();
                Render();
            });
            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void YellowForm_KeyUp(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (!keyPressTimers.ContainsKey(key))
            {
                return;
            }
            keyPressTimers[key].Stop();
            keyPressTimers.Remove(key);
        }
    }
}