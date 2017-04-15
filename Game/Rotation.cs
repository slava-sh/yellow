using System.Diagnostics.Contracts;

namespace Game
{
    [Pure]
    public class Rotation
    {
        public static readonly Rotation Default = new Rotation(0);

        public readonly int Number;

        private Rotation(int number)
        {
            Number = number;
        }

        public Rotation Next()
        {
            return new Rotation(Number + 1);
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
