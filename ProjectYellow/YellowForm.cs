using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;
        private const int CellSize = 25;
        private static Color EmptyCellColor = Color.Yellow;
        private static Color OccupiedCellColor = Color.Black;

        private Game game = new Game(fieldWidth: FieldWidth, fieldHeight: FieldHeight, randomSeed: 2017);

        private Button[,] buttons;
        private Timer ticker = new Timer();

        public YellowForm()
        {
            InitializeComponent();
            buttons = new Button[FieldWidth, FieldHeight];
            for (int x = 0; x < FieldWidth; ++x)
            {
                for (int y = 0; y < FieldHeight; ++y)
                {
                    buttons[x, y] = new Button()
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(x * CellSize, y * CellSize),
                        Enabled = false,
                    };
                    Controls.Add(buttons[x, y]);
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
            game.Tick();
            if (game.IsOver)
            {
                ticker.Stop();
            }
            Render();
        }

        private void Render()
        {
            bool[,] mask = game.GetFieldMask();
            for (int x = 0; x < mask.GetLength(0); ++x)
            {
                for (int y = 0; y < mask.GetLength(1); ++y)
                {
                    buttons[x, y].BackColor = mask[x, y] ? OccupiedCellColor : EmptyCellColor;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                game.TryRotate();
                Render();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                game.TryMoveLeft();
                Render();
                return true;
            }
            else if (keyData == Keys.Right)
            {
                game.TryMoveRight();
                Render();
                return true;
            }
            else if (keyData == Keys.Down)
            {
                game.TryMoveDown();
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
