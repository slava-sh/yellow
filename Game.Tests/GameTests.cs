using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Game.Tests
{
    public class GameTests
    {
        private static void AssertFieldMask(Game game,
            params string[] verboseLines)
        {
            var expectedLines = verboseLines
                .Select(line => Regex.Replace(line, @"[^.\s]", "#"))
                .ToArray();
            var lines = MaskToLines(game.GetFieldMask()).ToArray();
            Assert.Equal(expectedLines, lines);
        }

        private static IEnumerable<string> MaskToLines(bool[,] mask)
        {
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            for (var y = 0; y < height; ++y)
            {
                var line = new char[width];
                for (var x = 0; x < width; ++x)
                {
                    line[x] = mask[x, y] ? '#' : '.';
                }
                yield return new string(line);
            }
        }

        [Fact]
        public void ArrowKeys()
        {
            var game = new Game(8, 6, new MockTetrominoGenerator
            {
                Tetromino.J,
                Tetromino.J
            });
            AssertFieldMask(game,
                "..###...",
                "........",
                "........",
                "........",
                "........",
                "........");
            game.ApplyGravity();
            game.ApplyGravity();
            AssertFieldMask(game,
                "........",
                "..#.....",
                "..###...",
                "........",
                "........",
                "........");
            game.ShiftLeft();
            game.ApplyGravity();
            AssertFieldMask(game,
                "........",
                "........",
                ".#......",
                ".###....",
                "........",
                "........");
            game.ApplyGravity();
            game.ShiftLeft();
            AssertFieldMask(game,
                "........",
                "........",
                "........",
                "#.......",
                "###.....",
                "........");
            game.ApplyGravity();
            game.ShiftLeft();
            AssertFieldMask(game,
                "........",
                "........",
                "........",
                "........",
                "#.......",
                "###.....");
            game.ApplyGravity();
            game.ShiftRight();
            game.ShiftRight();
            game.ShiftRight();
            AssertFieldMask(game,
                ".....###",
                "........",
                "........",
                "........",
                "#.......",
                "###.....");
            game.ApplyGravity();
            AssertFieldMask(game,
                ".....#..",
                ".....###",
                "........",
                "........",
                "#.......",
                "###.....");
            game.Rotate();
            game.Rotate();
            game.Rotate();
            AssertFieldMask(game,
                "......#.",
                "......#.",
                ".....##.",
                "........",
                "#.......",
                "###.....");
            game.ApplyGravity();
            game.ShiftRight();
            AssertFieldMask(game,
                "........",
                ".......#",
                ".......#",
                "......##",
                "#.......",
                "###.....");
            game.Rotate();
            AssertFieldMask(game,
                "........",
                ".......#",
                ".......#",
                "......##",
                "#.......",
                "###.....");
        }

        [Fact]
        public void GameOver1()
        {
            var game = new Game(4, 4, new MockTetrominoGenerator
            {
                Tetromino.I,
                Tetromino.O
            });
            game.Rotate();
            game.HardDrop();
            AssertFieldMask(game,
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            Assert.False(game.IsOver);
            game.ApplyGravity();
            Assert.True(game.IsOver);
        }

        [Fact]
        public void GameOver2()
        {
            var game = new Game(4, 5, new MockTetrominoGenerator
            {
                Tetromino.I,
                Tetromino.O
            });
            game.Rotate();
            game.HardDrop();
            AssertFieldMask(game,
                "....",
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            game.ApplyGravity();
            AssertFieldMask(game,
                ".OO.",
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            Assert.False(game.IsOver);
            game.ApplyGravity();
            AssertFieldMask(game,
                ".OO.",
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            Assert.True(game.IsOver);
        }

        [Fact]
        public void HardDrop()
        {
            var game = new Game(6, 4, new MockTetrominoGenerator
            {
                Tetromino.Z
            });
            game.ApplyGravity();
            AssertFieldMask(game,
                ".##...",
                "..##..",
                "......",
                "......");
            game.HardDrop();
            AssertFieldMask(game,
                "......",
                "......",
                ".##...",
                "..##..");
        }

        [Fact]
        public void NewGameIsNotOver()
        {
            var game = new Game(6, 6, new MockTetrominoGenerator
            {
                Tetromino.Z
            });
            Assert.False(game.IsOver);
        }

        [Fact]
        public void NoMovesAfterGameOver()
        {
            var game = new Game(4, 2, new MockTetrominoGenerator
            {
                Tetromino.O,
                Tetromino.O
            });
            game.HardDrop();
            game.ApplyGravity();
            Assert.True(game.IsOver);
            var actions = new Action[]
            {
                game.ApplyGravity,
                () => game.Rotate(),
                () => game.ShiftLeft(),
                () => game.ShiftRight(),
                () => game.SoftDrop(),
                () => game.HardDrop()
            };
            foreach (var action in actions)
            {
                Assert.Throws<InvalidOperationException>(action);
            }
        }

        [Fact]
        public void PiecesSpawnInTheCenter()
        {
            var game = new Game(10, 20, new MockTetrominoGenerator
            {
                Tetromino.I,
                Tetromino.J,
                Tetromino.L,
                Tetromino.T,
                Tetromino.O,
                Tetromino.S,
                Tetromino.Z
            });
            for (var i = 0; i < 7; ++i)
            {
                game.ApplyGravity();
                game.HardDrop();
            }
            AssertFieldMask(game,
                "..........",
                "..........",
                "..........",
                "..........",
                "..........",
                "..........",
                "..........",
                "...ZZ.....",
                "....ZZ....",
                "....SS....",
                "...SS.....",
                "....OO....",
                "....OO....",
                "....T.....",
                "...TTT....",
                ".....L....",
                "...LLL....",
                "...J......",
                "...JJJ....",
                "...IIII...");
        }

        [Fact]
        public void RemoveFullLines()
        {
            var game = new Game(6, 4, new MockTetrominoGenerator
            {
                Tetromino.O,
                Tetromino.O,
                Tetromino.O,
                Tetromino.O,
                Tetromino.O
            });
            game.ApplyGravity();
            AssertFieldMask(game,
                "..OO..",
                "..OO..",
                "......",
                "......");
            game.ShiftLeft();
            game.ShiftLeft();
            game.ApplyGravity();
            game.ApplyGravity();
            game.ApplyGravity();
            game.ApplyGravity();
            AssertFieldMask(game,
                "..OO..",
                "..OO..",
                "##....",
                "##....");
            game.ShiftRight();
            game.ShiftRight();
            game.ApplyGravity();
            game.ApplyGravity();
            game.ApplyGravity();
            game.ApplyGravity();
            AssertFieldMask(game,
                "..OO..",
                "..OO..",
                "##..##",
                "##..##");
            game.ShiftRight();
            game.ShiftRight();
            game.ApplyGravity();
            game.ApplyGravity();
            AssertFieldMask(game,
                "..OO##",
                "..OO##",
                "##..##",
                "##..##");
            game.ApplyGravity();
            game.ApplyGravity();
            AssertFieldMask(game,
                "....##",
                "....##",
                "##OO##",
                "##OO##");
            game.ApplyGravity();
            AssertFieldMask(game,
                "..OO..",
                "......",
                "....##",
                "....##");
        }

        [Fact]
        public void SimpleGame()
        {
            var game = new Game(8, 6, new MockTetrominoGenerator
            {
                Tetromino.J,
                Tetromino.J,
                Tetromino.J,
                Tetromino.J
            });
            for (var i = 0; i < 12; ++i)
            {
                Assert.False(game.IsOver);
                game.ApplyGravity();
            }
            Assert.True(game.IsOver);
            AssertFieldMask(game,
                "..c.....",
                "..ccc...",
                "..b.....",
                "..bbb...",
                "..a.....",
                "..aaa...");
        }

        [Fact]
        public void Spin()
        {
            var game = new Game(8, 6, new MockTetrominoGenerator
            {
                Tetromino.Z,
                Tetromino.I,
                Tetromino.Z,
                Tetromino.T,
                Tetromino.O
            });
            game.ShiftLeft();
            game.ShiftLeft();
            game.HardDrop();
            game.ApplyGravity();
            game.ShiftRight();
            game.ShiftRight();
            game.HardDrop();
            game.ApplyGravity();
            game.ShiftRight();
            game.ShiftRight();
            game.HardDrop();
            game.ApplyGravity();
            game.ApplyGravity();
            game.Rotate();
            game.Rotate();
            game.Rotate();
            AssertFieldMask(game,
                "...T....",
                "..TT....",
                "...T....",
                "....ZZ..",
                "ZZ...ZZ.",
                ".ZZ.IIII");
            game.HardDrop();
            game.Rotate();
            game.Rotate();
            game.Rotate();
            game.ApplyGravity();
            AssertFieldMask(game,
                "...OO...",
                "........",
                "........",
                "....ZZ..",
                "ZZTTTZZ.",
                ".ZZTIIII");
        }
    }
}
