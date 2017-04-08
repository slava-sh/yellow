using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
{
    internal struct Rotation
    {
        public readonly int Number;

        private Rotation(int number)
        {
            Number = number;
        }

        [Pure]
        public Rotation Next()
        {
            return new Rotation(Number + 1);
        }
    }
}