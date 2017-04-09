using System;
using System.Text.RegularExpressions;
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
            var game = new Game(8, 6, new MockTetrominoGenerator
            {
                Tetromino.J,
                Tetromino.J,
                Tetromino.J,
                Tetromino.J
            });
            for (var i = 0; i < 12; ++i)
            {
                Assert.IsFalse(game.IsOver);
                game.Tick();
            }
            Assert.IsTrue(game.IsOver);
            AssertFieldMask(game,
                "..c.....",
                "..ccc...",
                "..b.....",
                "..bbb...",
                "..a.....",
                "..aaa...");
        }

        [TestMethod]
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
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                "........",
                "..#.....",
                "..###...",
                "........",
                "........",
                "........");
            game.ShiftLeft();
            game.Tick();
            AssertFieldMask(game,
                "........",
                "........",
                ".#......",
                ".###....",
                "........",
                "........");
            game.Tick();
            game.ShiftLeft();
            AssertFieldMask(game,
                "........",
                "........",
                "........",
                "#.......",
                "###.....",
                "........");
            game.Tick();
            game.ShiftLeft();
            AssertFieldMask(game,
                "........",
                "........",
                "........",
                "........",
                "#.......",
                "###.....");
            game.Tick();
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
            game.Tick();
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
            game.Tick();
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

        [TestMethod]
        public void RemoveFullLines()
        {
            var game = new Game(6, 4, new MockTetrominoGenerator
            {
                Tetromino.O,
                Tetromino.O,
                Tetromino.O,
                Tetromino.O
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
            var game = new Game(6, 4, new MockTetrominoGenerator
            {
                Tetromino.Z
            });
            game.Tick();
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

        [TestMethod]
        public void BlocksSpawnInTheCenter()
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
                game.Tick();
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

        [TestMethod]
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
            game.Tick();
            game.ShiftRight();
            game.ShiftRight();
            game.HardDrop();
            game.Tick();
            game.ShiftRight();
            game.ShiftRight();
            game.HardDrop();
            game.Tick();
            game.Tick();
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
            game.Tick();
            AssertFieldMask(game,
                "...OO...",
                "........",
                "........",
                "....ZZ..",
                "ZZTTTZZ.",
                ".ZZTIIII");
        }

        [TestMethod]
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
            Assert.IsFalse(game.IsOver);
            game.Tick();
            Assert.IsTrue(game.IsOver);
        }

        [TestMethod]
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
            game.Tick();
            AssertFieldMask(game,
                ".OO.",
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            Assert.IsFalse(game.IsOver);
            game.Tick();
            AssertFieldMask(game,
                ".OO.",
                "..I.",
                "..I.",
                "..I.",
                "..I.");
            Assert.IsTrue(game.IsOver);
        }

        [TestMethod]
        public void NoMovesAfterGameOver()
        {
            var game = new Game(4, 2, new MockTetrominoGenerator
            {
                Tetromino.O,
                Tetromino.O
            });
            game.HardDrop();
            game.Tick();
            Assert.IsTrue(game.IsOver);
            var actions = new Action[]
            {
                game.Tick,
                () => game.Rotate(),
                () => game.ShiftLeft(),
                () => game.ShiftRight(),
                () => game.SoftDrop(),
                () => game.HardDrop()
            };
            foreach (var action in actions)
            {
                Exception exception = null;
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    exception = e;
                }
                Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            }
        }

        private static void AssertFieldMask(Game game, params string[] verboseFieldMaskLines)
        {
            var verboseFieldMask = string.Join(Environment.NewLine, verboseFieldMaskLines);
            var fieldMask = Regex.Replace(verboseFieldMask, @"[^.\s]", "#");
            Assert.AreEqual(
                Environment.NewLine + fieldMask + Environment.NewLine,
                Environment.NewLine + Utils.MaskToString(game.GetFieldMask()) + Environment.NewLine);
        }
    }
}