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
        public readonly Tetromino Tetromino;
        public readonly Rotation Rotation;

        public Block(Tetromino shape)
        {
            Center = new Cell(0, 0);
            this.Tetromino = shape;
            this.Rotation = new Rotation();
        }

        private Block(Cell center, Tetromino shape, Rotation rotation)
        {
            this.Center = center;
            this.Tetromino = shape;
            this.Rotation = rotation;
        }

        public Block Rotate()
        {
            return new Block(Center, this.Tetromino, Rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(new Cell(Center.X, Center.Y + 1), this.Tetromino, Rotation);
        }

        public Block MoveLeft()
        {
            return new Block(new Cell(Center.X - 1, Center.Y), this.Tetromino, Rotation);
        }

        public Block MoveRight()
        {
            return new Block(new Cell(Center.X + 1, Center.Y), this.Tetromino, Rotation);
        }

        internal Block MoveTo(Cell newCenter)
        {
            return new Block(newCenter, this.Tetromino, Rotation);
        }

        public IEnumerable<Cell> GetCells()
        {
            var cells = new List<Cell>();
            var origin = new Cell(Center.X - this.Tetromino.Center.X, Center.Y - this.Tetromino.Center.Y);
            bool[,] rotationMask = this.Tetromino.GetRotationMask(Rotation);
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
