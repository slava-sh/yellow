using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp.Views;
using Game;
using Game.Interaction;

namespace WindowsFormsApp
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int FramesPerSecond = 60;

        private readonly TimerBasedScheduler scheduler =
            new TimerBasedScheduler(FramesPerSecond);

        private Dictionary<Keys, Key> keyMap;

        public GameForm()
        {
            InitializeComponent();
            ClientSize = gameView.Size;
            Load += (sender, e) => NewGame();
            KeyDown += HandleKeyDown;
            KeyUp += HandleKeyUp;
        }

        private void NewGame()
        {
            var randomSeed = new Random().Next();
            var tetrominoGenerator =
                new PeekableTetrominoGenerator(
                    new RandomBagTetrominoGenerator(randomSeed));
            var game = new Game.Game(FieldWidth, FieldHeight,
                tetrominoGenerator);

            gameView.Game = game;
            gameView.GetNextTetromino = tetrominoGenerator.Peek;

            var gameController = new GameController(game, scheduler);
            gameController.Update += gameView.Invalidate;
            gameController.GameOver += HandleGameOver;

            var keyboard = new Keyboard(scheduler);
            keyMap = new Dictionary<Keys, Key>();
            Bind(Keys.Up, rotateButton, keyboard.Rotate,
                gameController.HandleRotate);
            Bind(Keys.Left, shiftLeftButton, keyboard.ShiftLeft,
                gameController.HandleShiftLeft);
            Bind(Keys.Right, shiftRightButton, keyboard.ShiftRight,
                gameController.HandleShiftRight);
            Bind(Keys.Down, softDropButton, keyboard.SoftDrop,
                gameController.HandleSoftDrop);
            Bind(Keys.Space, hardDropButton, keyboard.HardDrop,
                gameController.HandleHardDrop);

            Invalidate(true);
        }

        private void Bind(Keys keyCode, ButtonView button, Key key,
            Action keyPressHandler)
        {
            keyMap[keyCode] = key;
            button.Key = key;
            button.MouseDown += (sender, e) => key.HandleKeyDown();
            button.MouseUp += (sender, e) => key.HandleKeyUp();
            key.KeyDown += button.Invalidate;
            key.KeyPress += keyPressHandler;
            key.KeyUp += button.Invalidate;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (keyMap.ContainsKey(key))
            {
                keyMap[key].HandleKeyDown();
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.KeyData;
            if (keyMap.ContainsKey(key))
            {
                keyMap[key].HandleKeyUp();
            }
        }

        private void HandleGameOver()
        {
            scheduler.Stop();

            // TODO: Add a funny icon.
            var result = MessageBox.Show("Game over. Try again?", "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }

            // TODO: Clean up reset event handlers.
            NewGame();
        }
    }
}
