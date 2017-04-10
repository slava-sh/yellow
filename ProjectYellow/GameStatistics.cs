using System;

namespace ProjectYellow
{
    public class GameStatistics
    {
        public readonly int Level;
        public readonly int Score;

        public GameStatistics()
        {
            Level = 1;
            Score = 0;
        }

        private GameStatistics(int level, int score)
        {
            Level = level;
            Score = score;
        }

        public GameStatistics Clear(int numLines)
        {
            // Scoring according to https://tetris.wiki/Scoring#Recent_guideline_compatible_games
            switch (numLines)
            {
                case 1:
                    return new GameStatistics(Level, Score + 100 * Level);
                case 2:
                    return new GameStatistics(Level, Score + 300 * Level);
                case 3:
                    return new GameStatistics(Level, Score + 500 * Level);
                case 4:
                    return new GameStatistics(Level, Score + 800 * Level);
                default:
                    throw new ArgumentException("Incorrect number of cleared lines.");
            }
        }
    }
}