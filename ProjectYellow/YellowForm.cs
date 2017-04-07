using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int CellSize = 30;
        private static Color EmptyCellColor = Color.Yellow;
        private static Color OccupiedCellColor = Color.Black;

        private Button[,] buttons;
        private Field field;
        private Shape currentShape;
        private Random random = new Random(2017);
        private Timer ticker = new Timer();

        public YellowForm()
        {
            InitializeComponent();
            field = new Field();
            currentShape = new LShape(3, 3, new ShapeRotation(0));
            buttons = new Button[Field.FieldWidth, Field.FieldHeight];
            for (int x = 0; x < Field.FieldWidth; ++x)
            {
                for (int y = 0; y < Field.FieldHeight; ++y)
                {
                    var button = new Button();
                    button.Size = new Size(CellSize, CellSize);
                    button.Location = new Point(x * CellSize, y * CellSize);
                    button.Enabled = false;
                    Controls.Add(button);
                    buttons[x, y] = button;
                }
            }
        }

        private void YellowForm_Load(object sender, EventArgs e)
        {
            Tick(null, null);
            ticker.Tick += Tick;
            ticker.Interval = 300;
            ticker.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            var nextShape = currentShape.MoveDown();
            if (field.CanPlace(nextShape))
            {
                currentShape = nextShape;
            }
            else
            {
                field.Add(currentShape);
                ShapeRotation rotation = new ShapeRotation(random.Next() % 4);
                nextShape = new LShape(3, 3, rotation);
                if (!field.CanPlace(nextShape))
                {
                    ticker.Stop();
                    return;
                }
                currentShape = nextShape;
            }
            Render();
        }

        private void Render()
        {
            for (int x = 0; x < Field.FieldWidth; ++x)
            {
                for (int y = 0; y < Field.FieldHeight; ++y)
                {
                    var button = buttons[x, y];
                    button.BackColor = field.GetCell(new Position(x, y)).IsOccupied ? OccupiedCellColor : EmptyCellColor;
                }
            }
            foreach (var pos in currentShape.GetPositions())
            {
                buttons[pos.X, pos.Y].BackColor = OccupiedCellColor;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                currentShape = currentShape.Rotate();
                Render();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
