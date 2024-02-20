using System;
using System.Collections.Generic;

namespace Rosetta.Utils
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> dict, Action<T> callback)
        {
            foreach (var pair in dict) callback?.Invoke(pair);
        }
    }
}