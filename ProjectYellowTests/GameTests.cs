using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectYellow;
using ProjectYellowTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void NewGameIsNotOver()
        {
            var game = new Game(6, 6, randomSeed: 0);
            Assert.IsFalse(game.IsOver);
        }

        [TestMethod()]
        public void SimpleGame()
        {
            var game = new Game(7, 6, blockGenerator: new MockBlockGenerator
            {
                new Block(Tetrimono.J),
                new Block(Tetrimono.J),
                new Block(Tetrimono.J),
                new Block(Tetrimono.J),
            });
            for (int i = 0; i < 12; ++i)
            {
                Assert.IsFalse(game.IsOver);
                game.Tick();
            }
            Assert.IsTrue(game.IsOver);
            AssertFieldMask(game,
                "__c____",
                "__ccc__",
                "__b____",
                "__bbb__",
                "__a____",
                "__aaa__");
        }

        [TestMethod()]
        public void ArrowKeys()
        {
            var game = new Game(7, 6, blockGenerator: new MockBlockGenerator
            {
                new Block(Tetrimono.J),
                new Block(Tetrimono.J),
            });
            AssertFieldMask(game,
                "__###__",
                "_______",
                "_______",
                "_______",
                "_______",
                "_______");
            game.Tick();
            game.Tick();
            AssertFieldMask(game,
                "_______",
                "__#____",
                "__###__",
                "_______",
                "_______",
                "_______");
            game.TryMoveLeft();
            game.Tick();
            AssertFieldMask(game,
                "_______",
                "_______",
                "_#_____",
                "_###___",
                "_______",
                "_______");
            game.Tick();
            game.TryMoveLeft();
            AssertFieldMask(game,
                "_______",
                "_______",
                "_______",
                "#______",
                "###____",
                "_______");
            game.Tick();
            game.TryMoveLeft();
            AssertFieldMask(game,
                "_______",
                "_______",
                "_______",
                "_______",
                "#______",
                "###____");
            game.Tick();
            game.TryMoveRight();
            game.TryMoveRight();
            AssertFieldMask(game,
                "____###",
                "_______",
                "_______",
                "_______",
                "#______",
                "###____");
            game.Tick();
            AssertFieldMask(game,
                "____#__",
                "____###",
                "_______",
                "_______",
                "#______",
                "###____");
            game.TryRotate();
            game.TryRotate();
            game.TryRotate();
            AssertFieldMask(game,
                "_____#_",
                "_____#_",
                "____##_",
                "_______",
                "#______",
                "###____");
            game.Tick();
            game.TryMoveRight();
            AssertFieldMask(game,
                "_______",
                "______#",
                "______#",
                "_____##",
                "#______",
                "###____");
            game.TryRotate();
            AssertFieldMask(game,
                "_______",
                "______#",
                "______#",
                "_____##",
                "#______",
                "###____");
        }

        private void AssertFieldMask(Game game, params string[] verboseFieldMaskLines)
        {
            var verboseFieldMask = String.Join(Environment.NewLine, verboseFieldMaskLines);
            var fieldMask = verboseFieldMask;
            foreach (var c in "abcdefghijklmopqrstuvwxyz0123456789")
            {
                fieldMask = fieldMask.Replace(c, '#');
            }
            Assert.AreEqual(Environment.NewLine + fieldMask + Environment.NewLine,
                Environment.NewLine + GetFieldMaskString(game) + Environment.NewLine);
        }

        private string GetFieldMaskString(Game game)
        {
            bool[,] mask = game.GetFieldMask();
            int width = mask.GetLength(0);
            int height = mask.GetLength(1);
            var fieldMaskLines = new string[height];
            for (int y = 0; y < height; ++y)
            {
                var line = new char[width];
                for (int x = 0; x < width; ++x)
                {
                    line[x] = mask[x, y] ? '#' : '_';
                }
                fieldMaskLines[y] = new string(line);
            }
            return String.Join(Environment.NewLine, fieldMaskLines);
        }
    }
}