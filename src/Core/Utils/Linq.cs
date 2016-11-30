using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utils
{
    public static class Linq
    {
        public static IEnumerable<IList<T>> Windowed<T>(this IList<T> source, int size)
        {
            for (int i = 0; i < Math.Ceiling(source.Count / (double)size); i++)
            {
                yield return source.Skip(size * i).Take(size).ToList();
            }
        }
    }
}
