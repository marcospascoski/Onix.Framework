using System;
using System.Collections.Generic;

namespace Onix.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
