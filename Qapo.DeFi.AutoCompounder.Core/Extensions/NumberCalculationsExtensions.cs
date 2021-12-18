using System.Numerics;

namespace Qapo.DeFi.AutoCompounder.Core.Extensions
{
    public static class NumberCalculationsExtensions
    {
        public static int IncreasePercentage(this int @num, float percentage)
        {
            return @num;
        }

        public static BigInteger IncreasePercentage(this BigInteger @num, float percentage)
        {
            return @num;
        }
    }
}
