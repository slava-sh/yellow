using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
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

        public static string MaskToString(bool[,] mask)
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

        // Fisher-Yates shuffle.
        // See http://stackoverflow.com/a/1287572/559031
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source, Random random)
        {
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i >= 0; i--)
            {
                var swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        public static Timer SetInterval(int milliseconds, Action tick)
        {
            var timer = new Timer
            {
                Interval = milliseconds
            };
            timer.Tick += (sender, e) => tick();
            timer.Start();
            return timer;
        }

        public static Timer SetIntervalAndFire(int milliseconds, Action tick)
        {
            var timer = SetInterval(milliseconds, tick);
            tick();
            return timer;
        }

        public static Timer SetTimeout(int milliseconds, Action action)
        {
            var timer = new Timer
            {
                Interval = milliseconds
            };
            timer.Tick += (sender, e) =>
            {
                action();
                timer.Stop();
            };
            timer.Start();
            return timer;
        }

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

        public static int FramesToMilliseconds(int frames, int framesPerSecond = 60)
        {
            return frames * 1000 / framesPerSecond;
        }
    }
}