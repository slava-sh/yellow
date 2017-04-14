using System.Collections.Generic;
using System.Windows.Forms;
using ProjectYellow.Game;

namespace ProjectYellow
{
    internal class KeyboardController
    {
        public readonly Key HardDrop;
        public readonly Key Rotate;
        public readonly Key ShiftLeft;
        public readonly Key ShiftRight;
        public readonly Key SoftDrop;

        private readonly Dictionary<Keys, Key> keyMap;
        
        public KeyboardController(Scheduler scheduler)
        {
            Rotate = new Key();
            ShiftLeft = new RepeatingKey(scheduler,
                GameBoy.ShiftDelayFrames, GameBoy.ShiftIntervalFrames);
            ShiftRight = new RepeatingKey(scheduler,
                GameBoy.ShiftDelayFrames, GameBoy.ShiftIntervalFrames);
            SoftDrop = new RepeatingKey(scheduler,
                GameBoy.SoftDropIntervalFrames, GameBoy.SoftDropIntervalFrames);
            HardDrop = new Key();
            keyMap = new Dictionary<Keys, Key>
            {
                [Keys.Up] = Rotate,
                [Keys.Left] = ShiftLeft,
                [Keys.Right] = ShiftRight,
                [Keys.Down] = SoftDrop,
                [Keys.Space] = HardDrop
            };
        }

        public void HandleKeyDown(Keys key)
        {
            if (keyMap.ContainsKey(key))
            {
                keyMap[key].HandleKeyDown();
            }
        }

        public void HandleKeyUp(Keys key)
        {
            if (keyMap.ContainsKey(key))
            {
                keyMap[key].HandleKeyUp();
            }
        }
    }
}
