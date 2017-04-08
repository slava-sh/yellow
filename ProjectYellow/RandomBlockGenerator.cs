using System;

namespace ProjectYellow
{
    internal class RandomBlockGenerator : IBlockGenerator
    {
        private readonly Random random;

        public RandomBlockGenerator(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public Block NextBlock()
        {
            var tetrimono = Tetromino.All[random.Next() % Tetromino.All.Length];
            return new Block(tetrimono);
        }
    }
}