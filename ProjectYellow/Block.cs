﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    class Block
    {
        public int CenterX;
        public int CenterY;
        public Shape Shape;
        public Rotation Rotation;

        public Block(int centerX, int centerY, Shape shape, Rotation rotation)
        {
            this.CenterX = centerX;
            this.CenterY = centerY;
            this.Shape = shape;
            this.Rotation = rotation;
        }

        public Cell[] GetCells()
        {
            var x = CenterX;
            var y = CenterY;
            switch (Rotation.Number % 4)
            {
                case 0:
                    return new Cell[] {
                        new Cell(x - 1, y),
                        new Cell(x - 1, y + 1),
                        new Cell(x, y + 1),
                        new Cell(x + 1, y + 1),
                    };
                case 1:
                    return new Cell[] {
                        new Cell(x, y - 1),
                        new Cell(x - 1, y - 1),
                        new Cell(x - 1, y),
                        new Cell(x - 1, y + 1),
                    };
                case 2:
                    return new Cell[] {
                        new Cell(x + 1, y),
                        new Cell(x + 1, y - 1),
                        new Cell(x, y - 1),
                        new Cell(x - 1, y - 1),
                    };
                case 3:
                    return new Cell[] {
                        new Cell(x, y + 1),
                        new Cell(x + 1, y + 1),
                        new Cell(x + 1, y),
                        new Cell(x + 1, y - 1),
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public Block Rotate()
        {
            return new Block(CenterX, CenterY, Shape, Rotation.Next());
        }

        public Block MoveDown()
        {
            return new Block(CenterX, CenterY + 1, Shape, Rotation);
        }
    }
}
