using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectYellow;

namespace ProjectYellowTests
{
    [TestClass]
    internal class PeekableTetrominoGeneratorTests
    {
        [TestMethod]
        public void ProxiesOutput()
        {
            var expectedOutput = new List<Tetromino>();
            expectedOutput.AddRange(Tetromino.All);
            expectedOutput.AddRange(Tetromino.All);
            expectedOutput = Utils.Shuffle(expectedOutput, new Random(0)).ToList();
            var generator = new PeekableTetrominoGenerator(new MockTetrominoGenerator(expectedOutput));
            var output = new List<Tetromino>();
            for (var i = 0; i < expectedOutput.Count; ++i)
            {
                output.Add(generator.Next());
            }
            CollectionAssert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void CanPeek()
        {
            var generator = new PeekableTetrominoGenerator(new MockTetrominoGenerator
            {
                Tetromino.I,
                Tetromino.T
            });
            Assert.AreEqual(Tetromino.I, generator.Peek());
            generator.Next();
            Assert.AreEqual(Tetromino.T, generator.Peek());
        }
    }
}