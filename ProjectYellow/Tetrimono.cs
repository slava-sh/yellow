using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ProjectYellowTests")]
namespace ProjectYellow
{
    sealed class Tetrimono
    {
        // Rotations according to the Super Rotation System.
        // See http://tetris.wikia.com/wiki/SRS
        public static readonly Tetrimono I = new Tetrimono(
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
        public static readonly Tetrimono J = new Tetrimono(
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
        public static readonly Tetrimono L = new Tetrimono(
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
        public static readonly Tetrimono O = new Tetrimono(
            new Cell(1, 1),
            new string[][] {
                new string[] {
                    "##",
                    "##",
                },
            });
        public static readonly Tetrimono S = new Tetrimono(
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
        public static readonly Tetrimono T = new Tetrimono(
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
        public static readonly Tetrimono Z = new Tetrimono(
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

        public static readonly Tetrimono[] All = new Tetrimono[] {
            I, J, L, O, S, T, Z,
        };

        public readonly Cell Center;
        private readonly bool[][,] rotationMasks;

        private Tetrimono(Cell center, string[][] rotations)
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
