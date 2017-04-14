using System;
using System.Windows.Forms;
using ProjectYellow.Game;
using ProjectYellow.Views;

namespace ProjectYellow
{
    public partial class GameForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int FramesPerSecond = 60;

        private GameController gameController;
        private KeyboardController keyboard;
        private TimerBasedScheduler scheduler;

        public GameForm()
        {
            InitializeComponent();
            ClientSize = gameView.Size;
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
            scheduler = new TimerBasedScheduler(FramesPerSecond);

            gameController = new GameController(game, scheduler);
            gameController.Update += ScheduleRepaint;
            gameController.GameOver += GameOver;

            keyboard = new KeyboardController(scheduler);
            keyboard.Rotate.KeyPress += gameController.HandleRotate;
            keyboard.ShiftLeft.KeyPress += gameController.HandleShiftLeft;
            keyboard.ShiftRight.KeyPress += gameController.HandleShiftRight;
            keyboard.SoftDrop.KeyPress += gameController.HandleSoftDrop;
            keyboard.HardDrop.KeyPress += gameController.HandleHardDrop;

            Connect(rotateButton, keyboard.Rotate);
            Connect(shiftLeftButton, keyboard.ShiftLeft);
            Connect(shiftRightButton, keyboard.ShiftRight);
            Connect(softDropButton, keyboard.SoftDrop);
            Connect(hardDropButton, keyboard.HardDrop);

            gameView.Game = game;
            gameView.GetNextTetromino = tetrominoGenerator.Peek;
            ScheduleRepaint();
        }

        private void Connect(ButtonView button, Key key)
        {
            button.Key = key;
            key.KeyDown += button.Invalidate;
            key.KeyUp += button.Invalidate;
        }

        private void GameOver()
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

            NewGame();
        }

        private void ScheduleRepaint()
        {
            gameView.Invalidate();
        }

        protected override bool IsInputKey(Keys key)
        {
            return true;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            keyboard.HandleKeyDown(e.KeyData);
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            keyboard.HandleKeyUp(e.KeyData);
        }
    }
}

