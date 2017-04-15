using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    internal static class Utils
    {
        public static bool[,] ParseMask(string[] lines)
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

        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source,
            Random random)
        {
            // Fisher-Yates shuffle from http://stackoverflow.com/a/1287572/559031
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i >= 0; i--)
            {
                var swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}
