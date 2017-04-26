using System;
using System.Collections.Generic;

namespace Application.Extensions
{
    internal static class LinqExtensions
    {
        internal static void ForEach<TCollection>(this IEnumerable<TCollection> values, Action<TCollection> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }

        internal static dynamic AsDynamic(this object value)
        {
            return (dynamic)value;
        }
    }
}