using System;
using System.Collections.Generic;

namespace MarsImages.Internal.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ObjectExtensions
    {
        public static IEnumerable<T> Traverse<O, T>(this O obj, Func<O, O> path, Func<O, T> selector)
        {
            if (selector == null) yield break;

            var value = selector(obj);
            yield return value;
            if (path == null) yield break;

            var next = path(obj);
            if (next == null) yield break;

            foreach (var r in Traverse(next, path, selector))
            {
                yield return r;
            }

            yield break;
        }
    }
}