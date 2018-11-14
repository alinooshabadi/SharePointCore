using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePointCore.Extensions
{
    public static class EnumerableExtensiosn
    {
        public static IEnumerable<double> CumulativeSum(this IEnumerable<double> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum.Round(2);
            }
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target)
        {
            var r = new Random();

            return target.OrderBy(x => (r.Next()));
        }

        public static string ToListString<T>(this IEnumerable<T> target)
        {
            if (target != null && target.Any())
            {
                return string.Join(", ", target.Select(x => x.ToString()));
            }

            return string.Empty;
        }
    }
}