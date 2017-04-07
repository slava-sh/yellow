using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    class LShape : Shape
    {
        public LShape(int x, int y, ShapeRotation rotation) : base(x, y, rotation)
        {
        }

        protected override Cell[] GetCells(Field field, int x, int y)
        {
            switch (Rotation.Number % 4)
            {
                case 0:
                    return new Cell[] {
                        field.GetCell(x - 1, y),
                        field.GetCell(x - 1, y + 1),
                        field.GetCell(x, y + 1),
                        field.GetCell(x + 1, y + 1),
                    };
                case 1:
                    return new Cell[] {
                        field.GetCell(x, y - 1),
                        field.GetCell(x - 1, y - 1),
                        field.GetCell(x - 1, y),
                        field.GetCell(x - 1, y + 1),
                    };
                case 2:
                    return new Cell[] {
                        field.GetCell(x + 1, y),
                        field.GetCell(x + 1, y - 1),
                        field.GetCell(x, y - 1),
                        field.GetCell(x - 1, y - 1),
                    };
                case 3:
                    return new Cell[] {
                        field.GetCell(x, y + 1),
                        field.GetCell(x + 1, y + 1),
                        field.GetCell(x + 1, y),
                        field.GetCell(x + 1, y - 1),
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}
