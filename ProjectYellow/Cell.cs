using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Cell
    {
        public readonly int X;
        public readonly int Y;
        public Shape Occupier = null;
        public bool IsOccupied => Occupier != null;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}