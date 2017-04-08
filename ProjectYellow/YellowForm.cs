using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow
{
    public partial class YellowForm : Form
    {
        private const int CellSize = 30;
        private static Color EmptyCellColor = Color.Yellow;
        private static Color OccupiedCellColor = Color.Black;

        private Game game = new Game(randomSeed: 2017);

        private Button[,] buttons;
        private Timer ticker = new Timer();

        public YellowForm()
        {
            InitializeComponent();
            buttons = new Button[game.field.Width, game.field.Height];
            for (int x = 0; x < game.field.Width; ++x)
            {
                for (int y = 0; y < game.field.Height; ++y)
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
            for (int x = 0; x < game.field.Width; ++x)
            {
                for (int y = 0; y < game.field.Height; ++y)
                {
                    var cell = new Cell(x, y);
                    var button = buttons[x, y];
                    button.BackColor = game.field.IsOccupied(cell) ? OccupiedCellColor : EmptyCellColor;
                }
            }
            foreach (var cell in game.block.GetCells())
            {
                if (game.field.Contains(cell)) {
                    buttons[cell.X, cell.Y].BackColor = OccupiedCellColor;
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
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
