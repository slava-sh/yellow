using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProjectYellow.Game;

namespace ProjectYellow
{
    internal class TimerBasedScheduler : Scheduler
    {
        private readonly int framesPerSecond;
        private readonly HashSet<Task> tasks = new HashSet<Task>();

        public TimerBasedScheduler(int framesPerSecond)
        {
            this.framesPerSecond = framesPerSecond;
        }

        public void Stop()
        {
            var tasksToCancel = tasks.ToArray();
            tasks.Clear();
            foreach (var task in tasksToCancel)
            {
                task.Cancel();
            }
        }

        public override ITask SetInterval(int frames, Action tick)
        {
            var timer = new Timer
            {
                Interval = FramesToMilliseconds(frames)
            };
            timer.Tick += (sender, e) => tick();
            timer.Start();
            Task task = null;
            task = new Task(() =>
            {
                timer.Stop();
                // ReSharper disable once AccessToModifiedClosure
                tasks.Remove(task);
            });
            tasks.Add(task);
            return task;
        }

        public override ITask SetInterval(int delayFrames, int intervalFrames,
            Action tick)
        {
            ITask task = null;
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

        public class Task : ITask
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
