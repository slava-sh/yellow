using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    public class Game
    {
        public bool IsOver { get; private set; } = false;

        private Field field;
        private Block block;
        private IBlockGenerator blockGenerator;

        public Game(int fieldWidth, int fieldHeight, int randomSeed) :
            this(fieldWidth, fieldHeight, new RandomBlockGenerator(randomSeed))
        {
        }

        internal Game(int fieldWidth, int fieldHeight, IBlockGenerator blockGenerator)
        {
            field = new Field(fieldWidth, fieldHeight);
            this.blockGenerator = blockGenerator;
            block = blockGenerator.NextBlock();
        }

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
                nextBlock = blockGenerator.NextBlock();
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
            block = block.Rotate();
            return true; // TODO
        }

        public bool[,] GetFieldMask()
        {
            var mask = new bool[field.Width, field.Height];
            for (int x = 0; x < field.Width; ++x)
            {
                for (int y = 0; y < field.Height; ++y)
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
