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
        public void SampleGame()
        {
            var game = new Game(7, 6, blockGenerator: new MockBlockGenerator
            {
                new Block(3, 0, Shape.L, new Rotation(0)),
                new Block(3, 0, Shape.L, new Rotation(0)),
                new Block(3, 0, Shape.L, new Rotation(0)),
                new Block(3, 0, Shape.L, new Rotation(0)),
            });
            for (int i = 0; i < 9; ++i)
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

        private void AssertFieldMask(Game game, params string[] verboseFieldMaskLines)
        {
            var verboseFieldMask = String.Join(Environment.NewLine, verboseFieldMaskLines);
            var fieldMask = verboseFieldMask;
            foreach (var c in "abcdefghijklmopqrstuvwxyz0123456789")
            {
                fieldMask = fieldMask.Replace(c, '#');
            }
            Assert.AreEqual(fieldMask, GetFieldMaskString(game));
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