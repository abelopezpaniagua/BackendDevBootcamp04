using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class MyLinqExtensions
    {
        public static IEnumerable<T> ProcessSequence<T>(this IEnumerable<T> sequence)
        {
            return sequence;
        }

        public static int? Median(this IEnumerable<int?> sequence)
        {
            var ordered = sequence.OrderBy(item => item);
            int middlePosition = ordered.Count() / 2;

            return ordered.ElementAt(middlePosition);
        }

        public static int? Median<T>(this IEnumerable<T> sequence, Func<T, int?> selector)
        {
            return sequence.Select(selector).Median();
        }

        public static int? Mode(this IEnumerable<int?> sequence)
        {
            var mostCommon = sequence
                .GroupBy(item => item)
                .OrderByDescending(item => item.Count())
                .First()
                .Key;

            return mostCommon;
        }

        public static int? Mode<T>(this IEnumerable<T> sequence, Func<T, int?> selector)
        {
            return sequence.Select(selector).Mode();
        }

        public static int? LessCommon(this IEnumerable<int?> sequence)
        {
            var lessCommon = sequence
                .GroupBy(item => item)
                .OrderBy(item => item.Count())
                .First()
                .Key;

            return lessCommon;
        }

        public static int? LessCommon<T>(this IEnumerable<T> sequence, Func<T, int?> selector)
        {
            return sequence.Select(selector).LessCommon();
        }
    }
}
