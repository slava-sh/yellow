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
            var tetrimono = Tetrimono.All[random.Next() % Tetrimono.All.Length];
            return new Block(tetrimono);
        }
    }
}
