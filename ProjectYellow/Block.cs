using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ProjectYellowTests")]
namespace ProjectYellow
{
    struct Block
    {
        public readonly Cell Center;
        public readonly Shape Shape;
        public readonly Rotation Rotation;

        public Block(Shape shape, Rotation rotation)
        {
            Center = new Cell(0, 0);
            this.Shape = shape;
            this.Rotation = rotation;
        }

        public Block(Cell center, Shape shape, Rotation rotation)
        {
            this.Center = center;
            this.Shape = shape;
            this.Rotation = rotation;
        }

        public Cell[] GetCells()
        {
            var x = Center.X;
            var y = Center.Y;
            switch (Rotation.Number % 4)
            {
                case 0:
                    return new Cell[] {
                        new Cell(x - 1, y),
                        new Cell(x - 1, y + 1),
                        new Cell(x, y + 1),
                        new Cell(x + 1, y + 1),
                    };
                case 1:
                    return new Cell[] {
                        new Cell(x, y - 1),
                        new Cell(x - 1, y - 1),
                        new Cell(x - 1, y),
                        new Cell(x - 1, y + 1),
                    };
                case 2:
                    return new Cell[] {
                        new Cell(x + 1, y),
                        new Cell(x + 1, y - 1),
                        new Cell(x, y - 1),
                        new Cell(x - 1, y - 1),
                    };
                case 3:
                    return new Cell[] {
                        new Cell(x, y + 1),
                        new Cell(x + 1, y + 1),
                        new Cell(x + 1, y),
                        new Cell(x + 1, y - 1),
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public Block Rotate()
        {
            return new Block(Center, Shape, Rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(new Cell(Center.X, Center.Y + 1), Shape, Rotation);
        }

        public Block MoveLeft()
        {
            return new Block(new Cell(Center.X - 1, Center.Y), Shape, Rotation);
        }

        public Block MoveRight()
        {
            return new Block(new Cell(Center.X + 1, Center.Y), Shape, Rotation);
        }

        internal Block MoveTo(Cell newCenter)
        {
            return new Block(newCenter, Shape, Rotation);
        }
    }
}
