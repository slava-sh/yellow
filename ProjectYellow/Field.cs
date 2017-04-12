﻿using System;
using System.Linq;

namespace ProjectYellow
{
    public class Field
    {
        private readonly bool[,] cells;
        public readonly int Height;
        public readonly int Width;

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
            return 0 <= cell.X && cell.X < Width &&
                   0 <= cell.Y && cell.Y < Height;
        }

        internal bool Contains(Piece piece)
        {
            return piece.GetCells().All(Contains);
        }

        internal bool CanPlace(Piece piece)
        {
            return piece.GetCells().All(cell => !IsOccupied(cell));
        }

        internal void Place(Piece piece)
        {
            foreach (var cell in piece.GetCells())
            {
                if (Contains(cell))
                {
                    if (cells[cell.X, cell.Y])
                    {
                        throw new ArgumentException(
                            "The cell is already occupied.");
                    }
                    cells[cell.X, cell.Y] = true;
                }
            }
        }

        internal int ClearLines()
        {
            var numClearedLines = 0;
            for (var y = Height - 1; y >= 0; --y)
            {
                var lineIsFull = true;
                for (var x = 0; x < Width; ++x)
                {
                    if (!cells[x, y])
                    {
                        lineIsFull = false;
                        break;
                    }
                }
                if (lineIsFull)
                {
                    ++numClearedLines;
                    continue;
                }
                if (numClearedLines > 0)
                {
                    for (var x = 0; x < Width; ++x)
                    {
                        cells[x, y + numClearedLines] = cells[x, y];
                    }
                }
            }
            Array.Clear(cells, 0, Width * numClearedLines);
            return numClearedLines;
        }
    }
}
