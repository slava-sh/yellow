using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    public class Game
    {
        internal Field field;
        internal Block block;
        public bool IsOver { get; private set; } = false;
        private Random random;

        public Game(int randomSeed)
        {
            random = new Random(randomSeed);
            field = new Field(6, 9);
            block = NextBlock();
        }

        public void Tick()
        {
            var nextBlock = block.MoveDown();
            if (field.CanPlace(nextBlock))
            {
                block = nextBlock;
            }
            else
            {
                field.Place(block);
                nextBlock = NextBlock();
                if (!field.CanPlace(nextBlock))
                {
                    IsOver = true;
                    return;
                }
                block = nextBlock;
            }
        }

        private Block NextBlock()
        {
            Rotation rotation = new Rotation(random.Next() % 4);
            return new Block(3, 0, Shape.L, rotation);
        }

        public bool TryRotate()
        {
            block = block.Rotate();
            return true; // TODO
        }
    }
}
