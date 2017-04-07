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
        private Field field;
        private Shape shape;
        private Random random = new Random(2017);
        private Timer ticker = new Timer();

        public YellowForm()
        {
            InitializeComponent();
            field = new Field();
            shape = new LShape(3, 3, new ShapeRotation(0));
            field.Add(shape);
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
            if (!shape.MaybeFall(field))
            {
                ShapeRotation rotation = new ShapeRotation(random.Next() % 4);
                shape = new LShape(3, 3, rotation);
                if (!shape.MaybeFall(field))
                {
                    ticker.Stop();
                    return;
                }
                field.Add(shape);
            }
            Render();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                Shape nextShape = shape;
                shape.Rotation = shape.Rotation.Next();
                Render();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void Render()
        {
            field.Render(this);
        }
    }
}
