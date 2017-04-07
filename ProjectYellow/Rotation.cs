using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectYellow
{
    public struct Rotation
    {
        public readonly int Number;

        public Rotation(int number)
        {
            this.Number = number;
        }

        public Rotation Next()
        {
            return new Rotation(Number + 1);
        }
    }
}
