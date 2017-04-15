using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Game
{
    [Pure]
    public sealed class Tetromino
    {
        // Rotations according to the Super Rotation System.
        // See https://tetris.wiki/SRS
        public static readonly Tetromino I = new Tetromino(
            "I",
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
            "J",
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
            "L",
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
            "O",
            new Cell(1, 1),
            new[]
            {
                new[]
                {
                    ".##.",
                    ".##."
                }
            });

        public static readonly Tetromino S = new Tetromino(
            "S",
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
            "T",
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
            "Z",
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

        public readonly string Name;

        /// <summary>
        ///     Left-middle cell of the first rotation.
        /// </summary>
        internal readonly Cell Pivot;

        private readonly bool[][,] rotationMasks;

        private Tetromino(string name, Cell pivot,
            IEnumerable<string[]> rotations)
        {
            Name = name;
            Pivot = pivot;
            rotationMasks = rotations.Select(ParseMask).ToArray();
        }

        public bool[,] GetMaskForRotation(Rotation rotation)
        {
            return rotationMasks[rotation.Number % rotationMasks.Length];
        }

        private static bool[,] ParseMask(string[] lines)
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

        public override string ToString()
        {
            return Name;
        }
    }
}
