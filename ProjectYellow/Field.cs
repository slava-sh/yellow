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

        public Cell GetCell(Position pos)
        {
            if (!Contains(pos))
            {
                return null;
            }
            return cells[pos.X, pos.Y];
        }

        public bool Contains(Position pos)
        {
            return 0 <= pos.X && pos.X < FieldWidth && 0 <= pos.Y && pos.Y < FieldHeight;
        }

        public bool CanPlace(Shape shape)
        {
            return shape.GetPositions().All(CanPlace);
        }

        private bool CanPlace(Position pos)
        {
            if (Contains(pos))
            {
                return !GetCell(pos).IsOccupied;
            }
            else
            {
                return 0 <= pos.X && pos.X < FieldWidth && pos.Y < FieldHeight;
            }
        }

        public void Add(Shape shape)
        {
            foreach (var pos in shape.GetPositions())
            {
                var cell = GetCell(pos);
                if (cell != null)
                {
                    cell.Occupier = shape;
                }
            }
        }

        public void Remove(Shape shape)
        {
            foreach (var pos in shape.GetPositions())
            {
                var cell = GetCell(pos);
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
