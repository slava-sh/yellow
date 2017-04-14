namespace ProjectYellow.Game
{
    public class PeekableTetrominoGenerator : ITetrominoGenerator
    {
        private readonly ITetrominoGenerator baseGenerator;
        private Tetromino next;

        public PeekableTetrominoGenerator(ITetrominoGenerator baseGenerator)
        {
            this.baseGenerator = baseGenerator;
            next = baseGenerator.Next();
        }

        public Tetromino Next()
        {
            var current = next;
            next = baseGenerator.Next();
            return current;
        }

        public Tetromino Peek()
        {
            return next;
        }
    }
}
