using System;

namespace ProjectYellow
{
    public class GameStatistics
    {
        public int Level { get; } = 1;
        public int Score { get; private set; }

        public void Clear(int numLines)
        {
            // Scoring according to https://tetris.wiki/Scoring#Recent_guideline_compatible_games
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
        }

        public void SoftDrop()
        {
            Score += 1;
        }

        public void HardDrop(int numLines)
        {
            Score += 2 * numLines;
        }

        // TODO: Give points for T-spins.
    }
}