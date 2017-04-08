using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ProjectYellowTests")]
namespace ProjectYellow
{
    sealed class Tetromino
    {
        // Rotations according to the Super Rotation System.
        // See http://tetris.wikia.com/wiki/SRS
        public static readonly Tetromino I = new Tetromino(
    new Cell(1, 1),
    new string[][] {
                new string[] {
                    "....",
                    "####",
                    "....",
                    "....",
                },
                new string[] {
                    "..#.",
                    "..#.",
                    "..#.",
                    "..#.",
                },
                new string[] {
                    "....",
                    "....",
                    "####",
                    "....",
                },
                new string[] {
                    ".#..",
                    ".#..",
                    ".#..",
                    ".#..",
                },
    });
        public static readonly Tetromino J = new Tetromino(
    new Cell(1, 1),
    new string[][] {
                new string[] {
                    "#..",
                    "###",
                    "...",
                },
                new string[] {
                    ".##",
                    ".#.",
                    ".#.",
                },
                new string[] {
                    "...",
                    "###",
                    "..#",
                },
                new string[] {
                    ".#.",
                    ".#.",
                    "##.",
                },
    });
        public static readonly Tetromino L = new Tetromino(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    "..#",
                    "###",
                    "...",
                },
                new string[] {
                    ".#.",
                    ".#.",
                    ".##",
                },
                new string[] {
                    "...",
                    "###",
                    "#..",
                },
                new string[] {
                    "##.",
                    ".#.",
                    ".#.",
                },
            });
        public static readonly Tetromino O = new Tetromino(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    "##",
                    "##",
                },
            });
        public static readonly Tetromino S = new Tetromino(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    ".##",
                    "##.",
                    "...",
                },
                new string[] {
                    ".#.",
                    ".##",
                    "..#",
                },
                new string[] {
                    "...",
                    ".##",
                    "##.",
                },
                new string[] {
                    "#..",
                    "##.",
                    ".#.",
                },
            });
        public static readonly Tetromino T = new Tetromino(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    ".#.",
                    "###",
                    "...",
                },
                new string[] {
                    ".#.",
                    ".##",
                    ".#.",
                },
                new string[] {
                    "...",
                    "###",
                    ".#.",
                },
                new string[] {
                    ".#.",
                    "##.",
                    ".#.",
                },
            });
        public static readonly Tetromino Z = new Tetromino(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    "##.",
                    ".##",
                    "...",
                },
                new string[] {
                    "..#",
                    ".##",
                    ".#.",
                },
                new string[] {
                    "...",
                    "##.",
                    ".##",
                },
                new string[] {
                    ".#.",
                    "##.",
                    "#..",
                },
            });

        public static readonly Tetromino[] All = new Tetromino[] {
            I, J, L, O, S, T, Z,
        };

        public readonly Cell Center;
        private readonly bool[][,] rotationMasks;

        private Tetromino(Cell center, string[][] rotations)
        {
            this.Center = center;
            rotationMasks = rotations.Select(ParseMask).ToArray();
        }

        public bool[,] GetRotationMask(Rotation rotation)
        {
            return rotationMasks[rotation.Number % rotationMasks.Length];
        }

        private bool[,] ParseMask(string[] maskString)
        {
            int width = maskString[0].Length;
            int height = maskString.Length;
            bool[,] mask = new bool[width, height];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    mask[x, y] = maskString[y][x] == '#';
                }
            }
            return mask;
        }
    }
}
