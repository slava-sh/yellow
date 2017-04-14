using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Scheduler
    {
        private readonly int framesPerSecond;
        private readonly HashSet<Task> tasks = new HashSet<Task>();

        public Scheduler(int framesPerSecond)
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

        public Task SetInterval(int frames, Action tick)
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
