using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    abstract class Shape
    {
        public int CenterX;
        public int CenterY;
        public ShapeRotation Rotation;

        public Shape(int centerX, int centerY, ShapeRotation rotation)
        {
            CenterX = centerX;
            CenterY = centerY;
            Rotation = rotation;
        }

        public abstract Position[] GetPositions();

        public abstract Shape New(int centerX, int centerY, ShapeRotation rotation);

        public Shape Rotate()
        {
            return New(CenterX, CenterY, Rotation.Next());
        }

        public Shape MoveDown()
        {
            return New(CenterX, CenterY + 1, Rotation);
        }
    }

    public struct ShapeRotation
    {
        public int Number { get; }

        public ShapeRotation(int number)
        {
            this.Number = number;
        }

        public ShapeRotation Next()
        {
            return new ShapeRotation(Number + 1);
        }
    }
}
