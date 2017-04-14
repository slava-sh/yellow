using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ProjectYellow.Game
{
    [Pure]
    internal class Piece
    {
        /// <summary>
        ///     Position of the tetromino's pivot on the field.
        /// </summary>
        private readonly Cell pivot;

        private readonly Rotation rotation;
        private readonly Tetromino tetromino;

        public Piece(Cell pivot, Tetromino tetromino) :
            this(pivot, tetromino, Rotation.Default)
        {
        }

        private Piece(Cell pivot, Tetromino tetromino, Rotation rotation)
        {
            this.pivot = pivot;
            this.tetromino = tetromino;
            this.rotation = rotation;
        }

        public Piece Rotate()
        {
            return new Piece(pivot, tetromino, rotation.Next());
        }

        public Piece MoveDown()
        {
            return new Piece(new Cell(pivot.X, pivot.Y + 1), tetromino,
                rotation);
        }

        public Piece MoveLeft()
        {
            return new Piece(new Cell(pivot.X - 1, pivot.Y), tetromino,
                rotation);
        }

        public Piece MoveRight()
        {
            return new Piece(new Cell(pivot.X + 1, pivot.Y), tetromino,
                rotation);
        }

        public IEnumerable<Cell> GetCells()
        {
            var origin = new Cell(
                pivot.X - tetromino.Pivot.X,
                pivot.Y - tetromino.Pivot.Y);
            var mask = tetromino.GetMaskForRotation(rotation);
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    if (mask[x, y])
                    {
                        yield return new Cell(origin.X + x, origin.Y + y);
                    }
                }
            }
        }
    }
}
