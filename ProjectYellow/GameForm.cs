using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProjectYellow.Properties;

namespace ProjectYellow
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;

        private static readonly Dictionary<int, int> LevelSpeed = new Dictionary<int, int>
        {
            // Game Boy speeds. See https://tetris.wiki/Tetris_(Game_Boy)
            [1] = 49,
            [2] = 45,
            [3] = 41,
            [4] = 37,
            [5] = 33,
            [6] = 28,
            [7] = 22,
            [8] = 17,
            [9] = 11,
            [10] = 10,
            [11] = 9,
            [12] = 8,
            [13] = 7,
            [14] = 6,
            [15] = 6,
            [16] = 5,
            [17] = 5,
            [18] = 4,
            [19] = 4,
            [20] = 3
        };

        private static readonly Dictionary<Keys, int> KeyRepeatDelayFrames = new Dictionary<Keys, int>
        {
            [Keys.Left] = 23,
            [Keys.Right] = 23,
            [Keys.Down] = 9
        };

        private static readonly Dictionary<Keys, int> KeyRepeatIntervalFrames = new Dictionary<Keys, int>
        {
            [Keys.Left] = 9,
            [Keys.Right] = 9,
            [Keys.Down] = 9
        };

        private readonly Dictionary<Keys, Action> keyPressHandlers;
        private readonly Dictionary<Keys, Timer> keyPressTimers = new Dictionary<Keys, Timer>();

        private Game game;
        private Timer gravityTimer;

        public GameForm()
        {
            InitializeComponent();
            keyPressHandlers = GetKeyPressHandlers();
            ClientSize = Resources.Background.Size;
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
                    if (!game.SoftDrop())
                    {
                        ApplyGravity();
                    }
                    RescheduleGravity();
                },
                [Keys.Space] = () =>
                {
                    game.HardDrop();
                    ApplyGravity();
                    RescheduleGravity();
                }
            };
        }

        private void HandleFormLoad(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            var randomSeed = new Random().Next();
            var tetrominoGenerator = new PeekableTetrominoGenerator(new RandomBagTetrominoGenerator(randomSeed));
            game = new Game(FieldWidth, FieldHeight, tetrominoGenerator);
            gameView.Game = game;
            gameView.GetNextTetromino = tetrominoGenerator.Peek;
            ScheduleRepaint();
            RescheduleGravity();
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

        private void RescheduleGravity()
        {
            gravityTimer?.Stop();
            var delay = Utils.FramesToMilliseconds(LevelSpeed[game.Stats.Level]);
            gravityTimer = Utils.SetTimeout(delay, () =>
            {
                ApplyGravity();
                RescheduleGravity();
            });
        }

        private void GameOver()
        {
            gravityTimer.Stop();
            foreach (var timer in keyPressTimers.Values)
            {
                timer?.Stop();
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
            gameView.Invalidate();
        }

        protected override bool IsInputKey(Keys key)
        {
            return keyPressHandlers.ContainsKey(key);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (!keyPressHandlers.ContainsKey(key) || keyPressTimers.ContainsKey(key))
            {
                return;
            }

            OnKeyPress(key);

            if (KeyRepeatDelayFrames.ContainsKey(key))
            {
                var delay = Utils.FramesToMilliseconds(KeyRepeatDelayFrames[key]);
                keyPressTimers[key] = Utils.SetTimeout(delay, () =>
                {
                    var interval = Utils.FramesToMilliseconds(KeyRepeatIntervalFrames[key]);
                    keyPressTimers[key] = Utils.SetIntervalAndFire(interval, () => OnKeyPress(key));
                });
            }
            else
            {
                // Prevent subsequent calls to HandleKeyDown from handling this key.
                keyPressTimers[key] = null;
            }

            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void OnKeyPress(Keys key)
        {
            if (game.IsOver)
            {
                return;
            }
            keyPressHandlers[key]();
            ScheduleRepaint();
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (!keyPressTimers.ContainsKey(key))
            {
                return;
            }
            keyPressTimers[key]?.Stop();
            keyPressTimers.Remove(key);
        }
    }
}