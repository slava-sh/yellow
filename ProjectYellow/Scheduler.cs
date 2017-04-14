using System;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Scheduler
    {
        private readonly int framesPerSecond;

        public Scheduler(int framesPerSecond)
        {
            this.framesPerSecond = framesPerSecond;
        }

        public Task SetInterval(int frames, Action tick)
        {
            var timer = new Timer
            {
                Interval = FramesToMilliseconds(frames)
            };
            timer.Tick += (sender, e) => tick();
            timer.Start();
            return new Task(timer.Stop);
        }

        public Task SetTimeout(int frames, Action action)
        {
            Task task = null;
            task = SetInterval(frames, () =>
            {
                // ReSharper disable once AccessToModifiedClosure
                // ReSharper disable once PossibleNullReferenceException
                task.Cancel();
                action();
            });
            return task;
        }

        public Task SetInterval(int delayFrames, int intervalFrames,
            Action tick)
        {
            Task task = null;
            task = SetTimeout(delayFrames, () =>
            {
                task = SetInterval(intervalFrames, tick);
                tick();
            });
            // ReSharper disable once ImplicitlyCapturedClosure
            return new Task(() => task.Cancel());
        }

        private int FramesToMilliseconds(int frames)
        {
            return frames * 1000 / framesPerSecond;
        }

        public class Task
        {
            private readonly Action cancel;

            public Task(Action cancel)
            {
                this.cancel = cancel;
            }

            public void Cancel()
            {
                cancel();
            }
        }
    }
}
