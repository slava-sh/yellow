using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectYellow.Game;

namespace ProjectYellowTests
{
    [TestClass]
    public class RandomBagTetrominoGeneratorTests
    {
        [TestMethod]
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
                CollectionAssert.AreEqual(expectedOutput, output);
            }
        }

        private static int CompareTetrominoes(Tetromino a, Tetromino b)
        {
            return a.GetHashCode() - b.GetHashCode();
        }
    }
}
