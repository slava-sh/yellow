using System.Collections.Generic;

namespace ProjectYellow
{
    /// <summary>
    ///     See https://tetris.wiki/Tetris_(Game_Boy)
    /// </summary>
    public static class GameBoy
    {
        public const int ShiftDelayFrames = 24;
        public const int ShiftIntervalFrames = 9;
        public const int SoftDropIntervalFrames = 3;

        public static readonly Dictionary<int, int> LevelSpeed =
            new Dictionary<int, int>
            {
                [0] = 53,
                [1] = 49,
                [2] = 45,
                [3] = 41,
                [4] = 37,
                [5] = 33,
                [6] = 28,
                [7] = 22,
                [8] = 17,
                [9] = 11,
                [10] = 10,
                [11] = 9,
                [12] = 8,
                [13] = 7,
                [14] = 6,
                [15] = 6,
                [16] = 5,
                [17] = 5,
                [18] = 4,
                [19] = 4,
                [20] = 3
            };
    }
}
