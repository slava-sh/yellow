using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Cell
    {
        public const int CellSize = 30;

        public readonly int X;
        public readonly int Y;
        public Shape Occupier = null;
        public bool IsOccupied => Occupier != null;

        private Button button;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            button = new Button();
            button.Size = new Size(CellSize, CellSize);
            button.Location = new Point(X * CellSize, Y * CellSize);
            button.Enabled = false;
        }

        public void Render(Form form)
        {
            button.BackColor = IsOccupied ? Color.Black : Color.Yellow;
            form.Controls.Add(button);
        }
    }
}