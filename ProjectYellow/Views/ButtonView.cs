using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ProjectYellow.Views
{
    [Designer(typeof(Designer))]
    internal class ButtonView : CustomView, ISupportInitialize
    {
        private bool pressed;
        private Rectangle rectangle;

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            rectangle = new Rectangle(0, 0, Size.Width, Size.Height);
            var path = new GraphicsPath();
            path.AddEllipse(rectangle);
            Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics.FillEllipse(pressed ? Brushes.Red : Brushes.Blue,
                rectangle);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            pressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        private class Designer : ControlDesigner
        {
        }
    }
}
