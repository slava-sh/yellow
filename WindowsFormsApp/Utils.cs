using System;

namespace WindowsFormsApp
{
    internal static class Utils
    {
        public static bool[,] Crop(bool[,] mask, int newWidth, int newHeight)
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
    }
}
