using System;

namespace Qapo.DeFi.Bot.Core.Extensions
{
    public static class ObjectExtensions
    {
        #nullable enable

        public static T? ThrowIfNull<T>(this T? @instance, string paramName) where T : class
        {
            return @instance ?? throw new ArgumentNullException(paramName, "Instance of type '" + typeof(T).Name + "' is null");
        }

        public static T ThrowIfDefault<T>(this T @instance, string paramName) where T : class
        {
            if (@instance.Equals(default(T)))
            {
                throw new ArgumentException("Instance of type '" + typeof(T).Name + "' is default", paramName);
            }

            return @instance;
        }

        public static T? ThrowIfNullOrDefault<T>(this T? @instance, string paramName) where T : class
        {
            return @instance.ThrowIfNull(paramName)?.ThrowIfDefault(paramName);
        }

        #nullable disable
    }
}
