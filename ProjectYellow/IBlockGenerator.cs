using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProjectYellowTests")]

namespace ProjectYellow
{
    internal interface IBlockGenerator
    {
        Block NextBlock();
    }
}