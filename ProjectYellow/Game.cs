using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    class Game
    {
        public Field field;
        public Shape currentShape;
        private Random random;

        public bool IsOver { get; private set; } = false;

        public Game(int randomSeed)
        {
            random = new Random(randomSeed);
            field = new Field();
            currentShape = new LShape(3, 3, new ShapeRotation(0));
        }

        public void Tick()
        {
            var nextShape = currentShape.MoveDown();
            if (field.CanPlace(nextShape))
            {
                currentShape = nextShape;
            }
            else
            {
                field.Add(currentShape);
                ShapeRotation rotation = new ShapeRotation(random.Next() % 4);
                nextShape = new LShape(3, 3, rotation);
                if (!field.CanPlace(nextShape))
                {
                    IsOver = true;
                    return;
                }
                currentShape = nextShape;
            }
        }

        public bool TryRotate()
        {
            currentShape = currentShape.Rotate();
            return true; // TODO
        }
    }
}
