using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectYellow.Views
{
    internal abstract class CustomView : Control, ISupportInitialize
    {
        protected Graphics Graphics;

        protected CustomView()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics = e.Graphics;
        }

        protected void FillRectangle(Color color, int x, int y, int width,
            int height)
        {
            Graphics.FillRectangle(new SolidBrush(color), x, y, width, height);
        }

        public virtual void BeginInit()
        {
        }

        public virtual void EndInit()
        {
        }
    }
}
