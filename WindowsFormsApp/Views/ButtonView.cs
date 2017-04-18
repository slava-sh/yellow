using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Game.Interaction;
using static System.Drawing.ColorTranslator;

namespace WindowsFormsApp.Views
{
    [Designer(typeof(Designer))]
    internal class ButtonView : CustomView
    {
        private const int BorderSize = 2;
        private const int SmoothingMargin = 1;
        private static readonly Color BorderColor = Color.Black;
        private static readonly Color NormalColor = FromHtml("#399bef");
        private static readonly Color PressedColor = FromHtml("#2b7ccf");

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
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics.FillEllipse(new SolidBrush(BorderColor),
                SmoothingMargin,
                SmoothingMargin,
                rectangle.Width - 2 * SmoothingMargin,
                rectangle.Height - 2 * SmoothingMargin);
            const int margin = BorderSize + SmoothingMargin;
            Graphics.FillEllipse(
                new SolidBrush(Key.IsPressed ? PressedColor : NormalColor),
                margin,
                margin,
                rectangle.Width - 2 * margin,
                rectangle.Height - 2 * margin);
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
