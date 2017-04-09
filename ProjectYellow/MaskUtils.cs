using System;

namespace ProjectYellow
{
    public static class MaskUtils
    {
        public static bool[,] Parse(string[] lines)
        {
            var width = lines[0].Length;
            var height = lines.Length;
            var mask = new bool[width, height];
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    mask[x, y] = lines[y][x] != '.';
                }
            }
            return mask;
        }

        public static string ToString(bool[,] mask)
        {
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            var lines = new string[height];
            for (var y = 0; y < height; ++y)
            {
                var line = new char[width];
                for (var x = 0; x < width; ++x)
                {
                    line[x] = mask[x, y] ? '#' : '.';
                }
                lines[y] = new string(line);
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}