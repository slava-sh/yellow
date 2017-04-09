using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
{
    internal sealed class Tetromino
    {
        // Rotations according to the Super Rotation System.
        // See https://tetris.wiki/SRS
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
            new Cell(0, 1),
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

        /// <summary>
        ///     Left-middle cell of the first rotation.
        /// </summary>
        public readonly Cell Pivot;

        private readonly bool[][,] rotationMasks;

        private Tetromino(Cell pivot, IEnumerable<string[]> rotations)
        {
            Pivot = pivot;
            rotationMasks = rotations.Select(Utils.ParseMask).ToArray();
        }

        public bool[,] GetRotationMask(Rotation rotation)
        {
            return rotationMasks[rotation.Number % rotationMasks.Length];
        }
    }
}