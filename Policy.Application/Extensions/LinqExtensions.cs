using System;
using System.Collections.Generic;

namespace Policy.Application.Extensions
{
    internal static class LinqExtensions
    {

        internal static dynamic AsDynamic(this object value)
        {
            return (dynamic)value;
        }
    }
}