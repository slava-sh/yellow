using System.Diagnostics.Contracts;

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
