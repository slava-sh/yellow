using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    abstract class Shape
    {
        public int X;
        public int Y;
        public ShapeRotation Rotation;

        public Shape(int x, int y, ShapeRotation rotation)
        {
            X = x;
            Y = y;
            Rotation = rotation;
        }

        public bool MaybeFall(Field field)
        {
            var currentCells = GetCells(field);
            if (currentCells.Any(cell => cell == null))
            {
                return false;
            }
            var nextCells = GetCells(field, X, Y + 1);
            if (nextCells.Any(cell => cell == null || (cell.State != CellState.Empty && cell.Occupier != this)))
            {
                return false;
            }
            field.Remove(this);
            ++Y;
            field.Add(this);
            return true;
        }

        public Cell[] GetCells(Field field)
        {
            return GetCells(field, X, Y);
        }

        protected abstract Cell[] GetCells(Field field, int x, int y);
    }

    public struct ShapeRotation
    {
        public int Number { get; }

        public ShapeRotation(int number)
        {
            this.Number = number;
        }

        public ShapeRotation Next() {
            return new ShapeRotation(Number + 1);
        }


    }
}
