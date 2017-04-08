namespace ProjectYellow
{
    public class Game
    {
        private readonly IBlockGenerator blockGenerator;
        private readonly Field field;
        private readonly Cell newBlockOrigin = new Cell(3, 0);
        private Block block;

        public Game(int fieldWidth, int fieldHeight, int randomSeed) : this(fieldWidth, fieldHeight,
            new RandomBlockGenerator(randomSeed))
        {
        }

        internal Game(int fieldWidth, int fieldHeight, IBlockGenerator blockGenerator)
        {
            field = new Field(fieldWidth, fieldHeight);
            this.blockGenerator = blockGenerator;
            block = blockGenerator.NextBlock().MoveTo(newBlockOrigin);
        }

        public bool IsOver { get; private set; }

        public void Tick()
        {
            var nextBlock = block.MoveDown();
            if (field.CanPlace(nextBlock))
            {
                block = nextBlock;
            }
            else
            {
                field.Place(block);
                nextBlock = blockGenerator.NextBlock().MoveTo(newBlockOrigin);
                if (!field.CanPlace(nextBlock))
                {
                    IsOver = true;
                    return;
                }
                block = nextBlock;
            }
        }

        public bool TryRotate()
        {
            return TryNewBlock(block.Rotate());
        }

        public bool TryMoveLeft()
        {
            return TryNewBlock(block.MoveLeft());
        }

        public bool TryMoveRight()
        {
            return TryNewBlock(block.MoveRight());
        }

        public bool TryMoveDown()
        {
            return TryNewBlock(block.MoveDown());
        }

        private bool TryNewBlock(Block newBlock)
        {
            if (!field.CanPlace(newBlock))
            {
                return false;
            }
            block = newBlock;
            return true;
        }

        public bool[,] GetFieldMask()
        {
            var mask = new bool[field.Width, field.Height];
            for (var x = 0; x < field.Width; ++x)
            {
                for (var y = 0; y < field.Height; ++y)
                {
                    if (field.IsOccupied(new Cell(x, y)))
                    {
                        mask[x, y] = true;
                    }
                }
            }
            foreach (var cell in block.GetCells())
            {
                if (field.Contains(cell))
                {
                    mask[cell.X, cell.Y] = true;
                }
            }
            return mask;
        }
    }
}