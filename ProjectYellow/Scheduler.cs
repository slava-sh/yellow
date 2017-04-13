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

        private Task SetInterval(int frames, Action<Timer> tick)
        {
            var timer = new Timer
            {
                Interval = FramesToMilliseconds(frames)
            };
            timer.Tick += (sender, e) => tick(timer);
            timer.Start();
            return new Task(timer.Stop);
        }

        public Task SetInterval(int frames, Action tick)
        {
            return SetInterval(frames, timer => tick());
        }

        public Task SetTimeout(int frames, Action action)
        {
            return SetInterval(frames, timer =>
            {
                timer.Stop();
                action();
            });
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
