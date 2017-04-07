using System;
using System.Windows.Forms;

namespace ProjectYellow
{
    internal class Cell
    {
        public const int CellSize = 30;

        public readonly int X;
        public readonly int Y;
        public Shape Occupier = null;

        private Button button;

        public CellState State
        {
            get
            {
                return Occupier == null ? CellState.Empty : CellState.Occupied;
            }
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            button = new System.Windows.Forms.Button();
            button.Size = new System.Drawing.Size(CellSize, CellSize);
            button.Location = new System.Drawing.Point(X * CellSize, Y * CellSize);
            button.Enabled = false;
        }

        public void Render(Form form)
        {
            if (State == CellState.Occupied)
            {
                button.BackColor = System.Drawing.Color.Black;
            }
            else
            {
                button.BackColor = System.Drawing.Color.Yellow;
            }
            form.Controls.Add(button);
        }
    }

    public enum CellState
    {
        Empty,
        Occupied,
    }
}