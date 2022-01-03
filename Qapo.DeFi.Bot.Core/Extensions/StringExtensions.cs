
namespace Qapo.DeFi.Bot.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @str)
        {
            return string.IsNullOrEmpty(@str);
        }

        public static int? IntRepresentation(this string @str)
        {
            if (@str.IsNullOrEmpty())
            {
                return null;
            }

            int result = 0;

            for (int i = 0; i < @str.Length; ++i)
            {
                result = result.Combine(@str[0]);
            }

            return result;
        }
    }
}
