﻿using System;
using System.Collections.Generic;
using static ProjectYellow.Utils;

namespace ProjectYellow
{
    /// <summary>
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
            foreach (var tetromino in Shuffle(Tetromino.All, random))
            {
                bag.Enqueue(tetromino);
            }
        }
    }
}
