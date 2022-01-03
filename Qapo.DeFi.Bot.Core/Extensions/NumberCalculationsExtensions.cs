using System;
using System.Numerics;

namespace Qapo.DeFi.Bot.Core.Extensions
{
    public static class NumberCalculationsExtensions
    {
        /// <summary>
        /// Adds a percentage to the original number.
        /// This method floors the final value, like the EVM.
        ///
        /// </summary>
        /// <param name="num"></param>
        /// <param name="percentage"> E.g.: 0.1 = 10% </param>
        public static BigInteger IncreasePercentage(this BigInteger @num, float percentage)
        {
            return new BigInteger(Math.Floor((decimal)@num + ((decimal)@num * (decimal)percentage)));
        }

        /// <summary>
        /// Adds a percentage to the original number.
        ///
        /// </summary>
        /// <param name="num"></param>
        /// <param name="percentage"> E.g.: 0.1 = 10% </param>
        /// <returns></returns>
        public static decimal IncreasePercentage(this decimal @num, float percentage)
        {
            return @num + (@num * (decimal)percentage);
        }

        public static int Combine(this int @leftNumber, int rightNumber)
        {
            int rigthLength = 1;

            while(rigthLength <= rightNumber)
            {
                rigthLength *= 10;
            }

            return (leftNumber * rigthLength) + rightNumber;
        }
    }
}
