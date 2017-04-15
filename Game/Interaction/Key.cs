using System;

namespace Game.Interaction
{
    public class Key
    {
        public bool IsPressed { get; private set; }

        public event Action KeyDown;
        public event Action KeyPress;
        public event Action KeyUp;

        public virtual void HandleKeyDown()
        {
            if (IsPressed)
            {
                return;
            }
            IsPressed = true;
            OnKeyDown();
            OnKeyPress();
        }

        public virtual void HandleKeyUp()
        {
            if (!IsPressed)
            {
                return;
            }
            IsPressed = false;
            OnKeyUp();
        }

        private void OnKeyDown()
        {
            KeyDown?.Invoke();
        }

        protected void OnKeyPress()
        {
            KeyPress?.Invoke();
        }

        private void OnKeyUp()
        {
            KeyUp?.Invoke();
        }
    }
}
