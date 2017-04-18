using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Game;
using static System.Drawing.ColorTranslator;

namespace WindowsFormsApp.Views
{
    [Designer(typeof(Designer))]
    internal class ScreenView : CustomView
    {
        private const int PixelSize = 2;
        private const int InnerCellSize = 7 * PixelSize;
        private const int CellSize = InnerCellSize + 4 * PixelSize;
        private const int GridStep = CellSize + PixelSize;

        private const int ScreenFrameSize = 2;
        private const int ScreenWidth = 380 + ScreenFrameSize * 2;
        private const int ScreenWeight = 500 + ScreenFrameSize * 2;

        private static readonly Color InactiveCellColor = FromHtml("#879372");
        private static readonly Color ActiveCellColor = Color.Black;
        private static readonly Color BackgroundColor = FromHtml("#9ead86");
        private static readonly Color ScreenFrameColor = Color.Black;

        private new static readonly Font Font =
            new Font("Consolas", 16, FontStyle.Bold);

        public Game.Game Game;
        public Func<Tetromino> GetNextTetromino;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            FillRectangle(BackgroundColor, 0, 0, ScreenWidth, ScreenWeight);
            DrawFrame(ScreenFrameSize, ScreenFrameColor, 0, 0, ScreenWidth,
                ScreenWeight);
            using (Translate(8 + ScreenFrameSize, 6 + ScreenFrameSize))
            {
                DrawField();
                using (Translate(264, 50))
                {
                    DrawPreview();
                    DrawStats(Game.Stats);
                }
            }
        }

        private void DrawField()
        {
            const int margin = PixelSize * 3 / 2;
            const int outer = PixelSize + margin;
            DrawFrame(PixelSize, ActiveCellColor, 0, 0,
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
            var mask = nextTetromino.GetMaskForRotation(Rotation.Default);
            var brush = Brushes.Black;
            Graphics.DrawString("Next", Font, brush, 0, 16);
            using (Translate(0, 2 * GridStep))
            {
                DrawMask(Crop(mask, 4, 2));
            }
        }

        private static bool[,] Crop(bool[,] mask, int newWidth, int newHeight)
        {
            var newMask = new bool[newWidth, newHeight];
            var maskWidth = mask.GetLength(0);
            var maskHeight = mask.GetLength(1);
            var minWidth = Math.Min(maskWidth, newWidth);
            var minHeight = Math.Min(maskHeight, newHeight);
            for (var x = 0; x < minWidth; ++x)
            {
                for (var y = 0; y < minHeight; ++y)
                {
                    newMask[x, y] = mask[x, y];
                }
            }
            return newMask;
        }

        private void DrawStats(Statistics stats)
        {
            var brush = Brushes.Black;
            Graphics.DrawString($"Score\n{stats.Score,5:00000}", Font, brush,
                0, 5 * GridStep);
            Graphics.DrawString($"Level\n{stats.Level,5:00}", Font, brush,
                0, 8 * GridStep);
            Graphics.DrawString($"Lines\n{stats.LinesCleared,5:000}", Font,
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
            DrawFrame(PixelSize, color, x, y, CellSize, CellSize);
            FillRectangle(color,
                x + 2 * PixelSize,
                y + 2 * PixelSize,
                InnerCellSize,
                InnerCellSize);
        }

        private void DrawFrame(int size, Color color, int x, int y, int width,
            int height)
        {
            var pen = new Pen(color, size)
            {
                Alignment = PenAlignment.Inset
            };
            Graphics.DrawRectangle(pen, x, y, width, height);
        }

        private class Designer : ControlDesigner
        {
            public override void Initialize(IComponent component)
            {
                base.Initialize(component);
                var gameView = (ScreenView) component;
                var tetrominoGenerator =
                    new PeekableTetrominoGenerator(
                        new RandomTetrominoGenerator(0));
                gameView.Game = new Game.Game(10, 20, tetrominoGenerator);
                gameView.GetNextTetromino = tetrominoGenerator.Peek;
                gameView.Game.ApplyGravity();
                gameView.Game.ApplyGravity();
                gameView.Game.ApplyGravity();
                gameView.Game.ApplyGravity();
                gameView.Game.ApplyGravity();
                gameView.Game.Rotate();
            }
        }
    }
}
