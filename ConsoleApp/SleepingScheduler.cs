using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Game.Interaction;

namespace ConsoleApp
{
    internal class SleepingScheduler : Scheduler
    {
        private readonly List<Task> tasks = new List<Task>();
        private readonly TimeSpan timePerFrame;
        private bool isRunning;

        public SleepingScheduler(int framesPerSecond)
        {
            timePerFrame =
                new TimeSpan(TimeSpan.TicksPerSecond / framesPerSecond);
        }

        public override ITask SetInterval(int delayFrames, int intervalFrames,
            Action tick)
        {
            var task = new Task(delayFrames, intervalFrames, tick);
            tasks.Add(task);
            return task;
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Run()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var nextFrame = new TimeSpan(0);
            isRunning = true;
            while (isRunning)
            {
                var delay = nextFrame - stopwatch.Elapsed;
                if (delay.Ticks > 0)
                {
                    Thread.Sleep(delay);
                }
                nextFrame += timePerFrame;
                ProcessFrame();
            }
        }

        private void ProcessFrame()
        {
            var currentTasks = tasks.ToArray();
            tasks.Clear();
            foreach (var task in currentTasks)
            {
                if (task.IsCancelled)
                {
                    continue;
                }
                tasks.Add(task);
                --task.DelayFrames;
                if (task.DelayFrames <= 0)
                {
                    task.Tick();
                    task.DelayFrames = task.IntervalFrames;
                }
            }
        }

        private class Task : ITask
        {
            public readonly int IntervalFrames;
            public readonly Action Tick;
            public int DelayFrames;

            public Task(int delayFrames, int intervalFrames, Action tick)
            {
                DelayFrames = delayFrames;
                IntervalFrames = intervalFrames;
                Tick = tick;
            }

            public bool IsCancelled { get; private set; }

            public void Cancel()
            {
                IsCancelled = true;
            }
        }
    }
}
