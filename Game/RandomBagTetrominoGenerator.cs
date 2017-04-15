using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    /// <summary>
    ///     Generator that imitates taking a random tetromino from a bag without
    ///     replacement. The bag is refilled with all tetrominoes when empty.
    ///     See https://tetris.wiki/Random_Generator
    /// </summary>
    public class RandomBagTetrominoGenerator : ITetrominoGenerator
    {
        private readonly Queue<Tetromino> bag = new Queue<Tetromino>();
        private readonly Random random;

        public RandomBagTetrominoGenerator(int randomSeed)
        {
            random = new Random(randomSeed);
            MaybeRefillBag();
        }

        public Tetromino Next()
        {
            var tetromino = bag.Dequeue();
            MaybeRefillBag();
            return tetromino;
        }

        private void MaybeRefillBag()
        {
            if (bag.Count != 0)
            {
                return;
            }
            foreach (var tetromino in Shuffle(Tetromino.All))
            {
                bag.Enqueue(tetromino);
            }
        }

        private IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            // Fisher-Yates shuffle from http://stackoverflow.com/a/1287572/559031
            var elements = source.ToArray();
            for (var i = elements.Length - 1; i >= 0; i--)
            {
                var swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}
