using System;

namespace ProjectYellow.Game
{
    public abstract class Scheduler
    {
        public abstract ITask SetInterval(int delayFrames, int intervalFrames,
            Action tick);

        public virtual ITask SetInterval(int frames, Action tick)
        {
            return SetInterval(frames, frames, tick);
        }

        public virtual ITask SetTimeout(int frames, Action action)
        {
            ITask task = null;
            task = SetInterval(frames, () =>
            {
                // ReSharper disable once AccessToModifiedClosure
                // ReSharper disable once PossibleNullReferenceException
                task.Cancel();
                action();
            });
            return task;
        }
    }
}
