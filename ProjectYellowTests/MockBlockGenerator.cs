using ProjectYellow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ProjectYellowTests
{
    class MockBlockGenerator : IBlockGenerator, IEnumerable<Block>
    {
        private Queue<Block> blocks = new Queue<Block>();

        public Block NextBlock()
        {
            return blocks.Dequeue();
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return blocks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Block block)
        {
            blocks.Enqueue(block);
        }
    }
}
