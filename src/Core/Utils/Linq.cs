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

        public static IEnumerable<string> FilterShortWord(this IEnumerable<string> source)
        {
            return source.Where(word => !string.IsNullOrEmpty(word) && word.Length > 3);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            var seenKeys = new HashSet<TKey>();
            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new ArgumentNullException("Seqence conatins no elements");
                }
                var current = sourceIterator.Current;
                var element = keySelector.Invoke(current);
                if (seenKeys.Add(element))
                {
                    yield return current;
                }

                while (sourceIterator.MoveNext())
                {
                    current = sourceIterator.Current;
                    element = keySelector.Invoke(current);
                    if (seenKeys.Add(element))
                    {
                        yield return current;
                    }
                }


            }
        }
    }
}
