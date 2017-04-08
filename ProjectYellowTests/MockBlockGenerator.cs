using System.Collections;
using System.Collections.Generic;
using ProjectYellow;

namespace ProjectYellowTests
{
    internal class MockBlockGenerator : IBlockGenerator, IEnumerable<Block>
    {
        private readonly Queue<Block> blocks = new Queue<Block>();

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