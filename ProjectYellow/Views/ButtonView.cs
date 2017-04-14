using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ProjectYellow.Game;

namespace ProjectYellow.Views
{
    [Designer(typeof(Designer))]
    internal class ButtonView : CustomView
    {
        private Key key;

        private Rectangle rectangle;

        public Key Key
        {
            get => key;
            set
            {
                key = value;
                key.KeyDown += Invalidate;
                key.KeyUp += Invalidate;
                MouseDown += (sender, e) => key.HandleKeyDown();
                MouseUp += (sender, e) => key.HandleKeyUp();
            }
        }

        public override void EndInit()
        {
            rectangle = new Rectangle(0, 0, Size.Width, Size.Height);
            var path = new GraphicsPath();
            path.AddEllipse(rectangle);
            Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics.FillEllipse(Key.IsPressed ? Brushes.Red : Brushes.Blue,
                rectangle);
        }

        private class Designer : ControlDesigner
        {
            public override void Initialize(IComponent component)
            {
                base.Initialize(component);
                var buttonView = (ButtonView) component;
                buttonView.Key = new Key();
            }
        }
    }
}
