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

            canvas.Size = new Size(FieldWidth * CellSize, FieldHeight * CellSize);
            ClientSize = canvas.Size;
        }

        private void YellowForm_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            var randomSeed = new Random().Next();
            game = new Game(FieldWidth, FieldHeight, randomSeed);
            Render();
            ticker = Utils.SetInterval(MillisecondsPerTick, Tick);
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
            foreach (var timer in keyPressTimers.Values)
            {
                timer.Stop();
            }
            keyPressTimers.Clear();
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
            canvas.Invalidate();
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
                if (game.IsOver)
                {
                    return;
                }
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

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var mask = game.GetFieldMask();
            for (var x = 0; x < FieldWidth; ++x)
            {
                for (var y = 0; y < FieldHeight; ++y)
                {
                    var rectangle = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);
                    var fillColor = mask[x, y] ? OccupiedCellColor : EmptyCellColor;
                    graphics.FillRectangle(new SolidBrush(fillColor), rectangle);
                }
            }
        }
    }
}