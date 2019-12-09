using System;
using System.Collections.Generic;
using System.Linq;

namespace Cybatica.Utilities
{
    public static class LinqExtensions
    {
        public static float StdDev(this IEnumerable<float> source)
        {
            var enumerable = source.ToArray();
            var count = enumerable.Length;

            if (count < 2) return 0;
            var mean = enumerable.Average();
            var variance = enumerable.Sum(x => (x - mean) * (x - mean)) / (count - 1);

            return (float) Math.Sqrt(variance);
        }

        public static float StdDev<TSource>(this IEnumerable<TSource> source,
            Func<TSource, float> selector)
        {
            return StdDev(source.Select(selector));
        }

            public static float AverageEx(this IEnumerable<float> source)
            {
                var result = 0f;
                try
                {
                    result = source.Average();
                }
                catch (InvalidOperationException)
                {
                    return result;
                }

                return result;
            }

            public static float AverageEx<TSource>(this IEnumerable<TSource> source,
                Func<TSource, float> selector)
            {
                return AverageEx(source.Select(selector));
            }

            public static float MaxEx(this IEnumerable<float> source)
            {
                var result = 0f;
                try
                {
                    result = source.Max();
                }
                catch (InvalidOperationException)
                {
                    return result;
                }

                return result;
            }

            public static float MaxEx<TSource>(this IEnumerable<TSource> source,
                Func<TSource, float> selector)
            {
                return MaxEx(source.Select(selector));
            }
    }
}