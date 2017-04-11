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
        private const int CanvasWidth = FieldWidth + 6;
        private const int CanvasHeight = FieldHeight;
        private const int CellSize = 25;
        private static readonly Color EmptyCellColor = Color.Yellow;
        private static readonly Color OccupiedCellColor = Color.Black;

        private readonly Dictionary<Keys, Action> keyPressHandlers;
        private readonly Dictionary<Keys, Timer> keyPressTimers = new Dictionary<Keys, Timer>();

        private Game game;
        private Timer gravityTimer;
        private PeekableTetrominoGenerator tetrominoGenerator;

        public YellowForm()
        {
            InitializeComponent();
            keyPressHandlers = GetKeyPressHandlers();
            canvas.Size = new Size(CanvasWidth * CellSize, CanvasHeight * CellSize);
            ClientSize = canvas.Size;
        }

        private Dictionary<Keys, Action> GetKeyPressHandlers()
        {
            return new Dictionary<Keys, Action>
            {
                [Keys.Up] = () => game.Rotate(),
                [Keys.Left] = () => game.ShiftLeft(),
                [Keys.Right] = () => game.ShiftRight(),
                [Keys.Down] = () =>
                {
                    if (game.SoftDrop())
                    {
                        ResetGravityTimer();
                    }
                },
                [Keys.Space] = () =>
                {
                    if (game.HardDrop())
                    {
                        ResetGravityTimer();
                    }
                }
            };
        }

        private void ResetGravityTimer()
        {
            gravityTimer.Stop();
            gravityTimer.Start();
        }

        private void YellowForm_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            var randomSeed = new Random().Next();
            tetrominoGenerator = new PeekableTetrominoGenerator(
                new RandomBagTetrominoGenerator(randomSeed));
            game = new Game(FieldWidth, FieldHeight, tetrominoGenerator);
            ScheduleRepaint();
            gravityTimer = Utils.SetInterval(MillisecondsPerTick, ApplyGravity);
        }

        private void ApplyGravity()
        {
            game.ApplyGravity();
            ScheduleRepaint();
            if (game.IsOver)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            gravityTimer.Stop();
            foreach (var timer in keyPressTimers.Values)
            {
                timer.Stop();
            }
            keyPressTimers.Clear();

            // TODO: Add a funny icon.
            var result = MessageBox.Show("Game over. Try again?", "Game Over", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }

            NewGame();
        }

        private void ScheduleRepaint()
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
                ScheduleRepaint();
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
            DrawBackground(graphics);
            DrawField(graphics);
            DrawNextPiecePreview(graphics);
            DrawStats(graphics);
        }

        private static void DrawBackground(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.WhiteSmoke,
                new Rectangle(0, 0, CanvasWidth * CellSize, CanvasHeight * CellSize));
        }

        private void DrawField(Graphics graphics)
        {
            DrawMask(graphics, game.GetFieldMask(), new Cell(0, 0));
        }

        private void DrawNextPiecePreview(Graphics graphics)
        {
            var nextTetromino = tetrominoGenerator.Peek();
            var mask = nextTetromino.GetRotationMask(new Rotation());
            DrawMask(graphics, Utils.Crop(mask, 4, 2), new Cell(FieldWidth + 1, 1));
        }

        private void DrawStats(Graphics graphics)
        {
            var stats = game.Stats;
            var font = new Font("Consolas", 16, FontStyle.Bold);
            var brush = Brushes.Black;
            graphics.DrawString($"Score\n{stats.Score,5:00000}", font, brush,
                (FieldWidth + 1) * CellSize, 5 * CellSize);
            graphics.DrawString($"Level\n{stats.Level,5:00}", font, brush,
                (FieldWidth + 1) * CellSize, 8 * CellSize);
            graphics.DrawString($"Lines\n{stats.LinesCleared,5:000}", font, brush,
                (FieldWidth + 1) * CellSize, 11 * CellSize);
        }

        private static void DrawMask(Graphics graphics, bool[,] mask, Cell origin)
        {
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    var rectangle = new Rectangle(
                        (origin.X + x) * CellSize,
                        (origin.Y + y) * CellSize,
                        CellSize, CellSize);
                    var fillColor = mask[x, y] ? OccupiedCellColor : EmptyCellColor;
                    graphics.FillRectangle(new SolidBrush(fillColor), rectangle);
                }
            }
        }
    }
}