using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ProjectYellow.Properties;
using static ProjectYellow.Utils;

namespace ProjectYellow
{
    [Designer(typeof(Designer))]
    internal class GameView : PictureBox
    {
        private const int PixelSize = 2;
        private const int InnerCellSize = 7 * PixelSize;
        private const int CellSize = InnerCellSize + 4 * PixelSize;
        private const int GridStep = CellSize + PixelSize;

        private static readonly Color InactiveCellColor =
            ColorTranslator.FromHtml("#879372");

        private static readonly Color ActiveCellColor = Color.Black;

        private static readonly Color BackgroundColor =
            ColorTranslator.FromHtml("#9ead86");

        private static readonly Color WindowColor =
            ColorTranslator.FromHtml("#efcc19");

        public Game Game;
        public Func<Tetromino> GetNextTetromino;
        private Graphics graphics;

        public GameView()
        {
            Size = Resources.Background.Size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Game == null)
            {
                return;
            }
            graphics = e.Graphics;
            DrawBackground();
            using (Translate(50, 50))
            {
                FillRectangle(BackgroundColor, -5, -5, 380, 500);
                DrawField();
                using (Translate(270, 50))
                {
                    DrawPreview();
                    DrawStats(Game.Stats);
                }
            }
        }

        private Translate Translate(int dx, int dy)
        {
            return new Translate(graphics, dx, dy);
        }

        private void DrawBackground()
        {
            FillRectangle(WindowColor, 0, 0, Size.Width, Size.Height);
            //graphics.DrawImage(new Bitmap(Resources.Background), 0, 0);
        }

        private void DrawField()
        {
            const int margin = PixelSize * 3 / 2;
            const int outer = PixelSize + margin;
            DrawFrame(ActiveCellColor, 0, 0,
                outer + Game.Field.Width * GridStep - PixelSize + outer,
                outer + Game.Field.Height * GridStep - PixelSize + outer);
            using (Translate(outer, outer))
            {
                DrawMask(Game.GetFieldMask());
            }
        }

        private void DrawPreview()
        {
            if (GetNextTetromino == null)
            {
                return;
            }
            var nextTetromino = GetNextTetromino();
            var mask = nextTetromino.GetMask(Rotation.Default);
            DrawMask(Crop(mask, 4, 2));
        }

        private void DrawStats(GameStatistics stats)
        {
            var font = new Font("Consolas", 16, FontStyle.Bold);
            var brush = Brushes.Black;
            graphics.DrawString($"Score\n{stats.Score,5:00000}", font, brush,
                0, 5 * GridStep);
            graphics.DrawString($"Level\n{stats.Level,5:00}", font, brush,
                0, 8 * GridStep);
            graphics.DrawString($"Lines\n{stats.LinesCleared,5:000}", font,
                brush, 0, 11 * GridStep);
        }

        private void DrawMask(bool[,] mask)
        {
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    var cellColor = mask[x, y]
                        ? ActiveCellColor
                        : InactiveCellColor;
                    DrawCell(cellColor, x * GridStep, y * GridStep);
                }
            }
        }

        private void DrawCell(Color color, int x, int y)
        {
            DrawFrame(color, x, y, CellSize, CellSize);
            FillRectangle(color,
                x + 2 * PixelSize,
                y + 2 * PixelSize,
                InnerCellSize,
                InnerCellSize);
        }

        private void DrawFrame(Color color, int x, int y, int width, int height)
        {
            var pen = new Pen(color, PixelSize)
            {
                Alignment = PenAlignment.Inset
            };
            graphics.DrawRectangle(pen, x, y, width, height);
        }

        private void FillRectangle(Color color, int x, int y, int width,
            int height)
        {
            graphics.FillRectangle(new SolidBrush(color), x, y, width, height);
        }

        private class Designer : ControlDesigner
        {
        }
    }
}
