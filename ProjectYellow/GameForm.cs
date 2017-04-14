using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ProjectYellow.Game;

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

        private Dictionary<Keys, Action> keyPressHandlers;

        private readonly Dictionary<Keys, ITask> keyPressTasks =
            new Dictionary<Keys, ITask>();

        private GameController gameController;
        private Scheduler scheduler;

        public GameForm()
        {
            InitializeComponent();
            ClientSize = gameView.Size;
        }

        private Dictionary<Keys, Action> GetKeyPressHandlers()
        {
            return new Dictionary<Keys, Action>
            {
                [Keys.Up] = gameController.HandleRotate,
                [Keys.Left] = gameController.HandleShiftLeft,
                [Keys.Right] = gameController.HandleShiftRight,
                [Keys.Down] = gameController.HandleSoftDrop,
                [Keys.Space] = gameController.HandleHardDrop
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
            var game = new Game.Game(FieldWidth, FieldHeight,
                tetrominoGenerator);
            scheduler = new Scheduler(FramesPerSecond);

            gameController = new GameController(game, scheduler);
            gameController.Update += ScheduleRepaint;
            gameController.GameOver += GameOver;

            keyPressHandlers = GetKeyPressHandlers();
            gameView.Game = game;
            gameView.GetNextTetromino = tetrominoGenerator.Peek;
            ScheduleRepaint();
        }

        private void GameOver()
        {
            scheduler.Stop();
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
            /* TODO: Handle this.
            if (game.IsOver)
            {
                return;
            }
            */
            keyPressHandlers[key]();
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
