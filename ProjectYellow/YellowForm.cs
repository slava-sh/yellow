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
                    var button = buttons[x, y];
                    var cell = game.field.GetCell(new Position(x, y));
                    button.BackColor = cell.IsOccupied ? OccupiedCellColor : EmptyCellColor;
                }
            }
            foreach (var pos in game.block.GetPositions())
            {
                if (game.field.Contains(pos)) {
                    buttons[pos.X, pos.Y].BackColor = OccupiedCellColor;
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
