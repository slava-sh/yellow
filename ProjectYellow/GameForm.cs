using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int FramesPerSecond = 60;

        private static readonly Dictionary<Keys, int> KeyRepeatDelayFrames =
            new Dictionary<Keys, int>
            {
                [Keys.Left] = GameBoy.ShiftDelayFrames,
                [Keys.Right] = GameBoy.ShiftDelayFrames,
                [Keys.Down] = GameBoy.SoftDropIntervalFrames
            };

        private static readonly Dictionary<Keys, int> KeyRepeatIntervalFrames =
            new Dictionary<Keys, int>
            {
                [Keys.Left] = GameBoy.ShiftIntervalFrames,
                [Keys.Right] = GameBoy.ShiftIntervalFrames,
                [Keys.Down] = GameBoy.SoftDropIntervalFrames
            };

        private readonly Dictionary<Keys, Action> keyPressHandlers;

        private readonly Dictionary<Keys, Scheduler.Task> keyPressTasks =
            new Dictionary<Keys, Scheduler.Task>();

        private Game game;
        private Scheduler.Task gravityTask;

        private Scheduler scheduler;

        public GameForm()
        {
            InitializeComponent();
            ClientSize = gameView.Size;
            keyPressHandlers = GetKeyPressHandlers();
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
            var tetrominoGenerator =
                new PeekableTetrominoGenerator(
                    new RandomBagTetrominoGenerator(randomSeed));
            game = new Game(FieldWidth, FieldHeight, tetrominoGenerator);
            gameView.Game = game;
            gameView.GetNextTetromino = tetrominoGenerator.Peek;
            scheduler = new Scheduler(FramesPerSecond);
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
            gravityTask?.Cancel();
            var delay = GameBoy.LevelSpeed[game.Stats.Level];
            gravityTask = scheduler.SetTimeout(delay, () =>
            {
                ApplyGravity();
                RescheduleGravity();
            });
        }

        private void GameOver()
        {
            gravityTask.Cancel();
            foreach (var task in keyPressTasks.Values)
            {
                task?.Cancel();
            }
            keyPressTasks.Clear();

            // TODO: Add a funny icon.
            var result = MessageBox.Show("Game over. Try again?", "Game Over",
                MessageBoxButtons.YesNo,
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
            if (!keyPressHandlers.ContainsKey(key) ||
                keyPressTasks.ContainsKey(key))
            {
                return;
            }

            OnKeyPress(key);

            if (KeyRepeatDelayFrames.ContainsKey(key))
            {
                keyPressTasks[key] =
                    scheduler.SetInterval(
                        KeyRepeatDelayFrames[key],
                        KeyRepeatIntervalFrames[key],
                        () => OnKeyPress(key));
            }
            else
            {
                // Prevent subsequent calls to HandleKeyDown from handling this key.
                keyPressTasks[key] = null;
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
            if (!keyPressTasks.ContainsKey(key))
            {
                return;
            }
            keyPressTasks[key]?.Cancel();
            keyPressTasks.Remove(key);
        }
    }
}
