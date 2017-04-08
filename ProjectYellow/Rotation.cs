using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("ProjectYellowTests")]
namespace ProjectYellow
{
    struct Rotation
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
