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
        public readonly Tetrimono Tetrimono;
        public readonly Rotation Rotation;

        public Block(Tetrimono shape)
        {
            Center = new Cell(0, 0);
            this.Tetrimono = shape;
            this.Rotation = new Rotation();
        }

        private Block(Cell center, Tetrimono shape, Rotation rotation)
        {
            this.Center = center;
            this.Tetrimono = shape;
            this.Rotation = rotation;
        }

        public Block Rotate()
        {
            return new Block(Center, Tetrimono, Rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(new Cell(Center.X, Center.Y + 1), Tetrimono, Rotation);
        }

        public Block MoveLeft()
        {
            return new Block(new Cell(Center.X - 1, Center.Y), Tetrimono, Rotation);
        }

        public Block MoveRight()
        {
            return new Block(new Cell(Center.X + 1, Center.Y), Tetrimono, Rotation);
        }

        internal Block MoveTo(Cell newCenter)
        {
            return new Block(newCenter, Tetrimono, Rotation);
        }

        public IEnumerable<Cell> GetCells()
        {
            var cells = new List<Cell>();
            var origin = new Cell(Center.X - Tetrimono.Center.X, Center.Y - Tetrimono.Center.Y);
            bool[,] rotationMask = Tetrimono.GetRotationMask(Rotation);
            int width = rotationMask.GetLength(0);
            int height = rotationMask.GetLength(1);
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    if (rotationMask[x, y])
                    {
                        cells.Add(new Cell(origin.X + x, origin.Y + y));
                    }
                }
            }
            return cells;
        }
    }
}
