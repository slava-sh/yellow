using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    class RandomBlockGenerator : IBlockGenerator
    {
        private Random random;

        public RandomBlockGenerator(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public Block NextBlock()
        {
            Rotation rotation = new Rotation(random.Next() % 4);
            return new Block(3, 0, Shape.L, rotation);
        }
    }
}
