using System;

namespace ProjectYellow.Game
{
    public class GameController
    {
        private readonly Game game;
        private readonly Scheduler scheduler;
        private ITask gravityTask;

        public GameController(Game game, Scheduler scheduler)
        {
            this.game = game;
            this.scheduler = scheduler;
            RescheduleGravity();
        }

        public event Action Update;
        public event Action GameOver;

        public void HandleRotate()
        {
            if (game.Rotate())
            {
                OnUpdate();
            }
        }

        public void HandleShiftLeft()
        {
            if (game.ShiftLeft())
            {
                OnUpdate();
            }
        }

        public void HandleShiftRight()
        {
            if (game.ShiftRight())
            {
                OnUpdate();
            }
        }

        public void HandleSoftDrop()
        {
            if (game.SoftDrop())
            {
                OnUpdate();
            }
            else
            {
                ApplyGravity();
            }
            RescheduleGravity();
        }

        public void HandleHardDrop()
        {
            game.HardDrop();
            ApplyGravity();
            RescheduleGravity();
        }

        private void ApplyGravity()
        {
            game.ApplyGravity();
            OnUpdate();
            if (game.IsOver)
            {
                OnGameOver();
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

        private void OnUpdate()
        {
            Update?.Invoke();
        }

        private void OnGameOver()
        {
            GameOver?.Invoke();
        }
    }
}
