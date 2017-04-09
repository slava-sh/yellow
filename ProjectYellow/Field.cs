using System;
using System.Linq;

namespace ProjectYellow
{
    internal class Field
    {
        public readonly int Height;
        public readonly int Width;
        private bool[,] cells;

        public Field(int width, int height)
        {
            Width = width;
            Height = height;
            cells = new bool[Width, Height];
        }

        public bool IsOccupied(Cell cell)
        {
            return Contains(cell) ? cells[cell.X, cell.Y] : cell.Y >= 0;
        }

        public bool Contains(Cell cell)
        {
            return 0 <= cell.X && cell.X < Width && 0 <= cell.Y && cell.Y < Height;
        }

        public bool Contains(Block block)
        {
            return block.GetCells().All(Contains);
        }

        public bool CanPlace(Block block)
        {
            return block.GetCells().All(cell => !IsOccupied(cell));
        }

        public void Place(Block block)
        {
            foreach (var cell in block.GetCells())
            {
                if (Contains(cell))
                {
                    if (cells[cell.X, cell.Y])
                    {
                        throw new ArgumentException("The cell is already occupied.");
                    }
                    cells[cell.X, cell.Y] = true;
                }
            }
        }

        public void RemoveFullLines()
        {
            var lineIsFull = new bool[Height];
            for (var y = 0; y < Height; ++y)
            {
                lineIsFull[y] = true;
                for (var x = 0; x < Width; ++x)
                {
                    if (!cells[x, y])
                    {
                        lineIsFull[y] = false;
                        break;
                    }
                }
            }
            if (!lineIsFull.Any(x => x))
            {
                return;
            }
            var newCells = new bool[Width, Height];
            var numFullLines = 0;
            for (var y = Height - 1; y >= 0; --y)
            {
                if (lineIsFull[y])
                {
                    ++numFullLines;
                    continue;
                }
                for (var x = 0; x < Width; ++x)
                {
                    newCells[x, y + numFullLines] = cells[x, y];
                }
            }
            cells = newCells;
        }
    }
}