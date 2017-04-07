using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Field
    {
        public const int FieldHeight = 19;
        public const int FieldWidth = 7;

        private Cell[,] cells = new Cell[FieldWidth, FieldHeight];

        public Field()
        {
            for (int x = 0; x < FieldWidth; ++x)
            {
                for (int y = 0; y < FieldHeight; ++y)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
        }

        public void Render(Form form)
        {
            foreach (var cell in cells)
            {
                cell.Render(form);
            }
        }

        public Cell GetCell(int x, int y)
        {
            if (0 <= x && x < FieldWidth && 0 <= y && y < FieldHeight)
            {
                return cells[x, y];
            }
            return null;
        }

        public void Add(Shape shape)
        {
            foreach (var cell in shape.GetCells(this))
            {
                if (cell != null)
                {
                    cell.Occupier = shape;
                }
            }
        }

        public void Remove(Shape shape)
        {
            foreach (var cell in shape.GetCells(this))
            {
                if (cell != null)
                {
                    if (cell.Occupier != shape)
                    {
                        throw new ArgumentException();
                    }
                    cell.Occupier = null;
                }
            }
        }
    }
}
