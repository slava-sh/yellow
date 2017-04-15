using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public virtual void BeginInit()
        {
        }

        public virtual void EndInit()
        {
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

        protected TranslateTransformHelper Translate(int dx, int dy)
        {
            return new TranslateTransformHelper(Graphics, dx, dy);
        }

        /// <summary>
        ///     Wrapper for <see cref="Graphics.TranslateTransform(float,float)" />
        ///     for use in a <c>using</c> statement.
        /// </summary>
        internal class TranslateTransformHelper : IDisposable
        {
            private readonly GraphicsContainer container;
            private readonly Graphics graphics;

            public TranslateTransformHelper(Graphics graphics, int dx, int dy)
            {
                this.graphics = graphics;
                container = graphics.BeginContainer();
                graphics.TranslateTransform(dx, dy);
            }

            public void Dispose()
            {
                graphics.EndContainer(container);
            }
        }
    }
}
