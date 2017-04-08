using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
{
    internal struct Block
    {
        private readonly Cell center;
        private readonly Tetromino tetromino;
        private readonly Rotation rotation;

        public Block(Tetromino tetromino)
        {
            center = new Cell(0, 0);
            this.tetromino = tetromino;
            rotation = new Rotation();
        }

        private Block(Cell center, Tetromino tetromino, Rotation rotation)
        {
            this.center = center;
            this.tetromino = tetromino;
            this.rotation = rotation;
        }

        public Block Rotate()
        {
            return new Block(center, tetromino, rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(new Cell(center.X, center.Y + 1), tetromino, rotation);
        }

        public Block MoveLeft()
        {
            return new Block(new Cell(center.X - 1, center.Y), tetromino, rotation);
        }

        public Block MoveRight()
        {
            return new Block(new Cell(center.X + 1, center.Y), tetromino, rotation);
        }

        internal Block MoveTo(Cell newCenter)
        {
            return new Block(newCenter, tetromino, rotation);
        }

        public IEnumerable<Cell> GetCells()
        {
            var cells = new List<Cell>();
            var origin = new Cell(center.X - tetromino.Center.X, center.Y - tetromino.Center.Y);
            var rotationMask = tetromino.GetRotationMask(rotation);
            var width = rotationMask.GetLength(0);
            var height = rotationMask.GetLength(1);
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
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