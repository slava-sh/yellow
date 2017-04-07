using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectYellow
{
    class Field
    {
        public readonly int Width;
        public readonly int Height;
        private bool[,] cells;

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
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
    }
}
