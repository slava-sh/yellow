using System;

namespace ProjectYellow
{
    public class Game
    {
        private readonly Field field;
        private readonly Cell newPieceOrigin;
        private readonly ITetrominoGenerator tetrominoGenerator;
        private Piece activePiece;

        public Game(int fieldWidth, int fieldHeight, ITetrominoGenerator tetrominoGenerator)
        {
            if (fieldWidth % 2 != 0)
            {
                throw new ArgumentException("fieldWidth must be even.");
            }
            field = new Field(fieldWidth, fieldHeight);
            var centerLeftWidth = (field.Width - 1) / 2;
            newPieceOrigin = new Cell(centerLeftWidth, 0);
            this.tetrominoGenerator = tetrominoGenerator;
            activePiece = NewPiece();
        }

        public GameStatistics Stats { get; } = new GameStatistics();

        public bool IsOver { get; private set; }
        public int Level => Stats.Level;

        private Piece NewPiece()
        {
            return new Piece(newPieceOrigin, tetrominoGenerator.Next());
        }

        public void ApplyGravity()
        {
            CheckGameState();
            var nextPiece = activePiece.MoveDown();
            if (field.CanPlace(nextPiece))
            {
                activePiece = nextPiece;
            }
            else if (!field.Contains(activePiece))
            {
                // Note that this is not a Tetris Guideline compatible "lock out" condition.
                // In our case, the game ends even if the piece is partially visible.
                // See https://tetris.wiki/Top_out
                IsOver = true;
            }
            else
            {
                field.Place(activePiece);
                var numClearedLines = field.ClearLines();
                if (numClearedLines > 0)
                {
                    Stats.Clear(numClearedLines);
                }
                nextPiece = NewPiece();
                if (field.CanPlace(nextPiece))
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
                if (!field.CanPlace(nextPiece))
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
            if (!field.CanPlace(newPiece))
            {
                return false;
            }
            activePiece = newPiece;
            return true;
        }

        public bool[,] GetFieldMask()
        {
            var mask = new bool[field.Width, field.Height];
            for (var x = 0; x < field.Width; ++x)
            {
                for (var y = 0; y < field.Height; ++y)
                {
                    if (field.IsOccupied(new Cell(x, y)))
                    {
                        mask[x, y] = true;
                    }
                }
            }
            foreach (var cell in activePiece.GetCells())
            {
                if (field.Contains(cell))
                {
                    mask[cell.X, cell.Y] = true;
                }
            }
            return mask;
        }
    }
}