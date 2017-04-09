using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectYellow;

namespace ProjectYellowTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void NewGameIsNotOver()
        {
            var game = new Game(6, 6, 0);
            Assert.IsFalse(game.IsOver);
        }

        [TestMethod]
        public void SimpleGame()
        {
            var game = new Game(7, 6, new MockBlockGenerator
            {
                new Block(Tetromino.J),
                new Block(Tetromino.J),
                new Block(Tetromino.J),
                new Block(Tetromino.J)
            });
            for (var i = 0; i < 12; ++i)
            {
                Assert.IsFalse(game.IsOver);
                game.Tick();
            }
            Assert.IsTrue(game.IsOver);
            AssertFieldMask(game,
                "..c....",
                "..ccc..",
                "..b....",
                "..bbb..",
                "..a....",
                "..aaa..");
        }

        [TestMethod]
        public void ArrowKeys()
        {
            var game = new Game(7, 6, new MockBlockGenerator
            {
                new Block(Tetromino.J),
                new Block(Tetromino.J)
            });
            AssertFieldMask(game,
                "..###..",
                ".......",
                ".......",
                ".......",
                ".......",
                ".......");
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                ".......",
                "..#....",
                "..###..",
                ".......",
                ".......",
                ".......");
            game.ShiftLeft();
            game.Tick();
            AssertFieldMask(game,
                ".......",
                ".......",
                ".#.....",
                ".###...",
                ".......",
                ".......");
            game.Tick();
            game.ShiftLeft();
            AssertFieldMask(game,
                ".......",
                ".......",
                ".......",
                "#......",
                "###....",
                ".......");
            game.Tick();
            game.ShiftLeft();
            AssertFieldMask(game,
                ".......",
                ".......",
                ".......",
                ".......",
                "#......",
                "###....");
            game.Tick();
            game.ShiftRight();
            game.ShiftRight();
            AssertFieldMask(game,
                "....###",
                ".......",
                ".......",
                ".......",
                "#......",
                "###....");
            game.Tick();
            AssertFieldMask(game,
                "....#..",
                "....###",
                ".......",
                ".......",
                "#......",
                "###....");
            game.Rotate();
            game.Rotate();
            game.Rotate();
            AssertFieldMask(game,
                ".....#.",
                ".....#.",
                "....##.",
                ".......",
                "#......",
                "###....");
            game.Tick();
            game.ShiftRight();
            AssertFieldMask(game,
                ".......",
                "......#",
                "......#",
                ".....##",
                "#......",
                "###....");
            game.Rotate();
            AssertFieldMask(game,
                ".......",
                "......#",
                "......#",
                ".....##",
                "#......",
                "###....");
        }

        [TestMethod]
        public void FullLineAreRemoved()
        {
            var game = new Game(6, 4, new MockBlockGenerator
            {
                new Block(Tetromino.O),
                new Block(Tetromino.O),
                new Block(Tetromino.O),
                new Block(Tetromino.O)
            });
            game.Tick();
            AssertFieldMask(game,
                "..##..",
                "..##..",
                "......",
                "......");
            game.ShiftLeft();
            game.ShiftLeft();
            game.Tick();
            game.Tick();
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                "..##..",
                "..##..",
                "##....",
                "##....");
            game.ShiftRight();
            game.ShiftRight();
            game.Tick();
            game.Tick();
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                "..##..",
                "..##..",
                "##..##",
                "##..##");
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                "......",
                "......",
                "######",
                "######");
            game.Tick();
            AssertFieldMask(game,
                "..##..",
                "......",
                "......",
                "......");
        }

        [TestMethod]
        public void HardDrop()
        {
            var game = new Game(6, 4, new MockBlockGenerator
            {
                new Block(Tetromino.Z)
            });
            game.Tick();
            AssertFieldMask(game,
                "..##..",
                "...##.",
                "......",
                "......");
            game.HardDrop();
            AssertFieldMask(game,
                "......",
                "......",
                "..##..",
                "...##.");
        }

        private static void AssertFieldMask(Game game, params string[] verboseFieldMaskLines)
        {
            var verboseFieldMask = string.Join(Environment.NewLine, verboseFieldMaskLines);
            var fieldMask = verboseFieldMask;
            foreach (var c in "abcdefghijklmopqrstuvwxyz0123456789")
            {
                fieldMask = fieldMask.Replace(c, '#');
            }
            Assert.AreEqual(
                Environment.NewLine + fieldMask + Environment.NewLine,
                Environment.NewLine + GetFieldMaskString(game) + Environment.NewLine);
        }

        private static string GetFieldMaskString(Game game)
        {
            var mask = game.GetFieldMask();
            var width = mask.GetLength(0);
            var height = mask.GetLength(1);
            var fieldMaskLines = new string[height];
            for (var y = 0; y < height; ++y)
            {
                var line = new char[width];
                for (var x = 0; x < width; ++x)
                {
                    line[x] = mask[x, y] ? '#' : '.';
                }
                fieldMaskLines[y] = new string(line);
            }
            return string.Join(Environment.NewLine, fieldMaskLines);
        }
    }
}