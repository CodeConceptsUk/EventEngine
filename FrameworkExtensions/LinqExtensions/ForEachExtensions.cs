using System;
using System.Collections.Generic;

namespace FrameworkExtensions.LinqExtensions
{
    public static class ForEachExtensions
    {
        public static void ForEach<TCollection>(this IEnumerable<TCollection> values, Action<TCollection> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

    }


}