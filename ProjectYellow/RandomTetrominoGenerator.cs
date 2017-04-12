using System;

namespace ProjectYellow
{
    public class RandomTetrominoGenerator : ITetrominoGenerator
    {
        private readonly Random random;

        public RandomTetrominoGenerator(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public Tetromino Next()
        {
            return Tetromino.All[random.Next() % Tetromino.All.Length];
        }
    }
}
