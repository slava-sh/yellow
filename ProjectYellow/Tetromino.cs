using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
{
    internal sealed class Tetromino
    {
        // Rotations according to the Super Rotation System.
        // See http://tetris.wikia.com/wiki/SRS
        public static readonly Tetromino I = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    "....",
                    "####",
                    "....",
                    "...."
                },
                new[]
                {
                    "..#.",
                    "..#.",
                    "..#.",
                    "..#."
                },
                new[]
                {
                    "....",
                    "....",
                    "####",
                    "...."
                },
                new[]
                {
                    ".#..",
                    ".#..",
                    ".#..",
                    ".#.."
                }
            });

        public static readonly Tetromino J = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    "#..",
                    "###",
                    "..."
                },
                new[]
                {
                    ".##",
                    ".#.",
                    ".#."
                },
                new[]
                {
                    "...",
                    "###",
                    "..#"
                },
                new[]
                {
                    ".#.",
                    ".#.",
                    "##."
                }
            });

        public static readonly Tetromino L = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    "..#",
                    "###",
                    "..."
                },
                new[]
                {
                    ".#.",
                    ".#.",
                    ".##"
                },
                new[]
                {
                    "...",
                    "###",
                    "#.."
                },
                new[]
                {
                    "##.",
                    ".#.",
                    ".#."
                }
            });

        public static readonly Tetromino O = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    "##",
                    "##"
                }
            });

        public static readonly Tetromino S = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    ".##",
                    "##.",
                    "..."
                },
                new[]
                {
                    ".#.",
                    ".##",
                    "..#"
                },
                new[]
                {
                    "...",
                    ".##",
                    "##."
                },
                new[]
                {
                    "#..",
                    "##.",
                    ".#."
                }
            });

        public static readonly Tetromino T = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    ".#.",
                    "###",
                    "..."
                },
                new[]
                {
                    ".#.",
                    ".##",
                    ".#."
                },
                new[]
                {
                    "...",
                    "###",
                    ".#."
                },
                new[]
                {
                    ".#.",
                    "##.",
                    ".#."
                }
            });

        public static readonly Tetromino Z = new Tetromino(
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    "##.",
                    ".##",
                    "..."
                },
                new[]
                {
                    "..#",
                    ".##",
                    ".#."
                },
                new[]
                {
                    "...",
                    "##.",
                    ".##"
                },
                new[]
                {
                    ".#.",
                    "##.",
                    "#.."
                }
            });

        public static readonly Tetromino[] All = {I, J, L, O, S, T, Z};

        public readonly Cell Center;
        private readonly bool[][,] rotationMasks;

        private Tetromino(Cell center, IEnumerable<string[]> rotations)
        {
            Center = center;
            rotationMasks = rotations.Select(ParseMask).ToArray();
        }

        public bool[,] GetRotationMask(Rotation rotation)
        {
            return rotationMasks[rotation.Number % rotationMasks.Length];
        }

        private static bool[,] ParseMask(string[] maskString)
        {
            var width = maskString[0].Length;
            var height = maskString.Length;
            var mask = new bool[width, height];
            for (var x = 0; x < width; ++x)
            {
                for (var y = 0; y < height; ++y)
                {
                    mask[x, y] = maskString[y][x] == '#';
                }
            }
            return mask;
        }
    }
}