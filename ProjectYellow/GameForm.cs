using System;
using System.Windows.Forms;
using ProjectYellow.Game;

namespace ProjectYellow
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int FramesPerSecond = 60;

        private readonly TimerBasedScheduler scheduler =
            new TimerBasedScheduler(FramesPerSecond);

        public GameForm()
        {
            InitializeComponent();
            ClientSize = gameView.Size;
            Load += (sender, e) => NewGame();
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

            var keyboard = new KeyboardController(scheduler);
            keyboard.Rotate.KeyPress += gameController.HandleRotate;
            keyboard.ShiftLeft.KeyPress += gameController.HandleShiftLeft;
            keyboard.ShiftRight.KeyPress += gameController.HandleShiftRight;
            keyboard.SoftDrop.KeyPress += gameController.HandleSoftDrop;
            keyboard.HardDrop.KeyPress += gameController.HandleHardDrop;

            rotateButton.Key = keyboard.Rotate;
            shiftLeftButton.Key = keyboard.ShiftLeft;
            shiftRightButton.Key = keyboard.ShiftRight;
            softDropButton.Key = keyboard.SoftDrop;
            hardDropButton.Key = keyboard.HardDrop;

            KeyDown += (sender, e) => keyboard.HandleKeyDown(e.KeyData);
            KeyUp += (sender, e) => keyboard.HandleKeyUp(e.KeyData);

            Invalidate(true);
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
