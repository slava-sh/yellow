using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Game.Tests
{
    public class RandomBagTetrominoGeneratorTests
    {
        private static int CompareTetrominoes(Tetromino a, Tetromino b)
        {
            return a.GetHashCode() - b.GetHashCode();
        }

        [Fact]
        public void ServesAllTetrominoes()
        {
            var generator = new RandomBagTetrominoGenerator(4);
            var expectedOutput = Tetromino.All.ToList();
            expectedOutput.Sort(CompareTetrominoes);
            for (var round = 0; round < 3; ++round)
            {
                var output = new List<Tetromino>();
                for (var i = 0; i < Tetromino.All.Length; ++i)
                {
                    output.Add(generator.Next());
                }
                output.Sort(CompareTetrominoes);
                Assert.Equal(expectedOutput, output);
            }
        }
    }
}
