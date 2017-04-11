﻿using System;

namespace ProjectYellow
{
    public class GameStatistics
    {
        private const int MaxLevel = 20;
        private const int LinesPerLevel = 10;

        public int Level => Math.Min(MaxLevel, 1 + LinesCleared / LinesPerLevel);

        /// <summary>
        ///     Score according to https://tetris.wiki/Tetris_DS#Scoring_tables
        ///     TODO: Implement T-spins and B2B.
        /// </summary>
        public int Score { get; private set; }

        public int LinesCleared { get; private set; }

        public void Clear(int numLines)
        {
            switch (numLines)
            {
                case 1:
                    Score += 100 * Level;
                    break;
                case 2:
                    Score += 300 * Level;
                    break;
                case 3:
                    Score += 500 * Level;
                    break;
                case 4:
                    Score += 800 * Level;
                    break;
                default:
                    throw new ArgumentException("Incorrect number of cleared lines.");
            }
            LinesCleared += numLines;
        }

        public void SoftDrop()
        {
            Score += 1;
        }

        public void HardDrop(int numLines)
        {
            Score += 2 * numLines;
        }
    }
}