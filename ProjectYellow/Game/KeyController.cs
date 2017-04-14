using System;

namespace ProjectYellow.Game
{
    internal class KeyController
    {
        public bool IsPressed { get; protected set; }

        public event Action KeyPress;

        public virtual void HandleKeyDown()
        {
            if (IsPressed)
            {
                return;
            }
            IsPressed = true;
            OnKeyPress();
        }

        protected void OnKeyPress()
        {
            /* TODO: Handle this.
            if (game.IsOver)
            {
                return;
            }
            */
            KeyPress?.Invoke();
        }

        public virtual void HandleKeyUp()
        {
            IsPressed = false;
        }
    }
}
