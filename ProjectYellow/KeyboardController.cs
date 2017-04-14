using System.Collections.Generic;
using System.Windows.Forms;
using ProjectYellow.Game;

namespace ProjectYellow
{
    internal class KeyboardController
    {
        public readonly KeyController HardDrop;
        public readonly KeyController Rotate;
        public readonly KeyController ShiftLeft;
        public readonly KeyController ShiftRight;
        public readonly KeyController SoftDrop;

        private readonly Dictionary<Keys, KeyController> keyMap;
        
        public KeyboardController(Scheduler scheduler)
        {
            Rotate = new KeyController();
            ShiftLeft = new RepeatingKeyController(scheduler,
                GameBoy.ShiftDelayFrames, GameBoy.ShiftIntervalFrames);
            ShiftRight = new RepeatingKeyController(scheduler,
                GameBoy.ShiftDelayFrames, GameBoy.ShiftIntervalFrames);
            SoftDrop = new RepeatingKeyController(scheduler,
                GameBoy.SoftDropIntervalFrames, GameBoy.SoftDropIntervalFrames);
            HardDrop = new KeyController();
            keyMap = new Dictionary<Keys, KeyController>
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
