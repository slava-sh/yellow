using System.Collections;
using System.Collections.Generic;
using ProjectYellow;

namespace ProjectYellowTests
{
    internal class MockTetrominoGenerator : ITetrominoGenerator,
        IEnumerable<Tetromino>
    {
        private readonly Queue<Tetromino> next = new Queue<Tetromino>();

        public MockTetrominoGenerator()
        {
        }

        public MockTetrominoGenerator(IEnumerable<Tetromino> tetrominoes)
        {
            foreach (var tetromino in tetrominoes)
            {
                next.Enqueue(tetromino);
            }
        }

        public IEnumerator<Tetromino> GetEnumerator()
        {
            return next.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Tetromino Next()
        {
            return next.Dequeue();
        }

        public void Add(Tetromino tetromino)
        {
            next.Enqueue(tetromino);
        }
    }
}
