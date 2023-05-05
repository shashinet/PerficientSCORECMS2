using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Perficient.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddIfNotNull<T>(this List<T> list, T value)
        {
            if (value == null)
            {
                return;
            }

            list.Add(value);
        }

        public static void AddIfNotNull<T>(this ConcurrentBag<T> list, T value)
        {
            if (value == null)
            {
                return;
            }

            list.Add(value);
        }

        public static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> value)
        {
            if (value == null)
            {
                return;
            }

            list.AddRange(value);
        }

        public static void AddRangeIfNotNull<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            if (source == null)
            {
                return;
            }

            foreach (T item in source)
            {
                destination.Add(item);
            }
        }

        public static bool Any(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AnyInternal();
        }

        public static int Count(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var enumerator = source.GetEnumerator();
            var result = 0;

            while (enumerator.MoveNext())
            {
                result++;
            }

            return result;
        }

        public static bool IsEmpty(this IEnumerable source)
        {
            return source == null || !source.Any();
        }

        public static bool IsNotEmpty(this IEnumerable source)
        {
            return source != null && source.Any();
        }

        private static bool AnyInternal(this IEnumerable source)
        {
            return source.GetEnumerator().MoveNext();
        }

        public static bool CheckContainsIfHasValue<T>(this IEnumerable<T> source, T value)
        {
            return source == null || source.Contains(value);
        }
    }
}