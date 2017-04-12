using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectYellow
{
    /// <summary>
    ///     Wrapper for `Graphics.BeginContainer` and `Graphics.EndContainer`
    ///     for use in a `using` statement.
    /// </summary>
    internal struct Translate : IDisposable
    {
        private readonly Graphics graphics;
        private readonly GraphicsContainer container;

        public Translate(Graphics graphics, int dx, int dy)
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