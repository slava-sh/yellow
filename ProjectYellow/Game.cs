using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    public class Game
    {
        public Field field;
        public Block block;
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
            var nextShape = block.MoveDown();
            if (field.CanPlace(nextShape))
            {
                block = nextShape;
            }
            else
            {
                field.Place(block);
                nextShape = NextBlock();
                if (!field.CanPlace(nextShape))
                {
                    IsOver = true;
                    return;
                }
                block = nextShape;
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
