﻿namespace ProjectYellow.Game
{
    internal class RepeatingKeyController : KeyController
    {
        private readonly int delayFrames;
        private readonly int intervalFrames;
        private readonly Scheduler scheduler;
        private ITask task;

        public RepeatingKeyController(Scheduler scheduler, int delayFrames,
            int intervalFrames)
        {
            this.scheduler = scheduler;
            this.delayFrames = delayFrames;
            this.intervalFrames = intervalFrames;
        }

        public override void HandleKeyDown()
        {
            if (IsPressed)
            {
                return;
            }
            task = scheduler.SetInterval(delayFrames, intervalFrames,
                OnKeyPress);
            base.HandleKeyDown();
        }

        public override void HandleKeyUp()
        {
            task?.Cancel();
            base.HandleKeyUp();
        }
    }
}
