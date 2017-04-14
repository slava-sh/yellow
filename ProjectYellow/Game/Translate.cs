using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectYellow.Game
{
    /// <summary>
    ///     Wrapper for <see cref="Graphics.TranslateTransform(float,float)" />
    ///     for use in a <c>using</c> statement.
    /// </summary>
    internal class Translate : IDisposable
    {
        private readonly GraphicsContainer container;
        private readonly Graphics graphics;

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
