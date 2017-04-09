using System.Collections.Generic;

namespace ProjectYellow
{
    internal struct Block
    {
        private readonly Tetromino tetromino;
        private readonly Rotation rotation;

        /// <summary>
        ///     Position of the tetromino's pivot on the field.
        /// </summary>
        private readonly Cell pivot;

        public Block(Cell pivot, Tetromino tetromino) : this(pivot, tetromino, new Rotation())
        {
        }

        private Block(Cell pivot, Tetromino tetromino, Rotation rotation)
        {
            this.pivot = pivot;
            this.tetromino = tetromino;
            this.rotation = rotation;
        }

        public Block Rotate()
        {
            return new Block(pivot, tetromino, rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(new Cell(pivot.X, pivot.Y + 1), tetromino, rotation);
        }

        public Block MoveLeft()
        {
            return new Block(new Cell(pivot.X - 1, pivot.Y), tetromino, rotation);
        }

        public Block MoveRight()
        {
            return new Block(new Cell(pivot.X + 1, pivot.Y), tetromino, rotation);
        }

        public IEnumerable<Cell> GetCells()
        {
            var cells = new List<Cell>();
            var origin = new Cell(pivot.X - tetromino.Pivot.X, pivot.Y - tetromino.Pivot.Y);
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