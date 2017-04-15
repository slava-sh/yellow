using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Game.Interaction;

namespace WindowsFormsApp.Views
{
    [Designer(typeof(Designer))]
    internal class ButtonView : CustomView
    {
        public Key Key;
        private Rectangle rectangle;

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
