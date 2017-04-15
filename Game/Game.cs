using System;

namespace Game
{
    public class Game
    {
        public readonly Field Field;

        public readonly Statistics Stats = new Statistics();
        private readonly ITetrominoGenerator tetrominoGenerator;
        private Piece activePiece;

        public Game(int fieldWidth, int fieldHeight,
            ITetrominoGenerator tetrominoGenerator)
        {
            if (fieldWidth % 2 != 0)
            {
                throw new ArgumentException("fieldWidth must be even.");
            }
            this.tetrominoGenerator = tetrominoGenerator;
            Field = new Field(fieldWidth, fieldHeight);
            activePiece = NewPiece();
        }

        public bool IsOver { get; private set; }

        private Piece NewPiece()
        {
            var tetromino = tetrominoGenerator.Next();
            var origin = new Cell((Field.Width - 1) / 2, 0);
            return new Piece(origin, tetromino);
        }

        public void ApplyGravity()
        {
            CheckGameState();
            var nextPiece = activePiece.MoveDown();
            if (Field.CanPlace(nextPiece))
            {
                activePiece = nextPiece;
            }
            else if (!Field.Contains(activePiece))
            {
                // Note that this is not a Tetris Guideline compatible "lock out" condition.
                // In our case, the game ends even if the piece is partially visible.
                // See https://tetris.wiki/Top_out
                IsOver = true;
            }
            else
            {
                Field.Place(activePiece);
                var numClearedLines = Field.ClearLines();
                if (numClearedLines > 0)
                {
                    Stats.Clear(numClearedLines);
                }
                nextPiece = NewPiece();
                if (Field.CanPlace(nextPiece))
                {
                    activePiece = nextPiece;
                }
                else
                {
                    // The "block out" condition.
                    IsOver = true;
                }
            }
        }

        private void CheckGameState()
        {
            if (IsOver)
            {
                throw new InvalidOperationException("The game is over.");
            }
        }

        public bool Rotate()
        {
            return MaybeSetActivePiece(activePiece.Rotate());
        }

        public bool ShiftLeft()
        {
            return MaybeSetActivePiece(activePiece.MoveLeft());
        }

        public bool ShiftRight()
        {
            return MaybeSetActivePiece(activePiece.MoveRight());
        }

        public bool SoftDrop()
        {
            var moved = MaybeSetActivePiece(activePiece.MoveDown());
            if (moved)
            {
                Stats.SoftDrop();
            }
            return moved;
        }

        public bool HardDrop()
        {
            CheckGameState();
            var newPiece = activePiece;
            var numLines = 0;
            while (true)
            {
                var nextPiece = newPiece.MoveDown();
                if (!Field.CanPlace(nextPiece))
                {
                    break;
                }
                newPiece = nextPiece;
                ++numLines;
            }
            if (numLines == 0)
            {
                return false;
            }
            activePiece = newPiece;
            Stats.HardDrop(numLines);
            return true;
        }

        private bool MaybeSetActivePiece(Piece newPiece)
        {
            CheckGameState();
            if (!Field.CanPlace(newPiece))
            {
                return false;
            }
            activePiece = newPiece;
            return true;
        }

        public bool[,] GetFieldMask()
        {
            var mask = new bool[Field.Width, Field.Height];
            for (var x = 0; x < Field.Width; ++x)
            {
                for (var y = 0; y < Field.Height; ++y)
                {
                    if (Field.IsOccupied(new Cell(x, y)))
                    {
                        mask[x, y] = true;
                    }
                }
            }
            foreach (var cell in activePiece.GetCells())
            {
                if (Field.Contains(cell))
                {
                    mask[cell.X, cell.Y] = true;
                }
            }
            return mask;
        }
    }
}
