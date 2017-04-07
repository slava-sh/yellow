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

        public override Shape New(int centerX, int centerY, ShapeRotation rotation)
        {
            return new LShape(centerX, centerY, rotation);
        }

        public override Position[] GetPositions()
        {
            var x = CenterX;
            var y = CenterY;
            switch (Rotation.Number % 4)
            {
                case 0:
                    return new Position[] {
                        new Position(x - 1, y),
                        new Position(x - 1, y + 1),
                        new Position(x, y + 1),
                        new Position(x + 1, y + 1),
                    };
                case 1:
                    return new Position[] {
                        new Position(x, y - 1),
                        new Position(x - 1, y - 1),
                        new Position(x - 1, y),
                        new Position(x - 1, y + 1),
                    };
                case 2:
                    return new Position[] {
                        new Position(x + 1, y),
                        new Position(x + 1, y - 1),
                        new Position(x, y - 1),
                        new Position(x - 1, y - 1),
                    };
                case 3:
                    return new Position[] {
                        new Position(x, y + 1),
                        new Position(x + 1, y + 1),
                        new Position(x + 1, y),
                        new Position(x + 1, y - 1),
                    };
                default:
                    throw new ArgumentException();
            }
        }
    }
}
