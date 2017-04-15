using System.Collections.Generic;
using Xunit;

namespace Game.Tests
{
    internal class PeekableTetrominoGeneratorTests
    {
        [Fact]
        public void CanPeek()
        {
            var generator = new PeekableTetrominoGenerator(
                new MockTetrominoGenerator
                {
                    Tetromino.I,
                    Tetromino.T
                });
            Assert.Equal(Tetromino.I, generator.Peek());
            generator.Next();
            Assert.Equal(Tetromino.T, generator.Peek());
        }

        [Fact]
        public void ProxiesOutput()
        {
            var expectedOutput = new[]
            {
                Tetromino.I, Tetromino.J, Tetromino.L, Tetromino.O, Tetromino.S,
                Tetromino.S, Tetromino.O, Tetromino.T, Tetromino.Z, Tetromino.T, 
                Tetromino.L, Tetromino.Z, Tetromino.T, Tetromino.I, Tetromino.J
            };
            var generator =
                new PeekableTetrominoGenerator(
                    new MockTetrominoGenerator(expectedOutput));
            var output = new List<Tetromino>();
            for (var i = 0; i < expectedOutput.Length; ++i)
            {
                output.Add(generator.Next());
            }
            Assert.Equal(expectedOutput, output);
        }
    }
}
