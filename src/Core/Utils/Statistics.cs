using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models;
using LanguageExt;

namespace Core.Utils
{
    public static class Statistics
    {
        private static double GetAParameter(IReadOnlyCollection<int> nums)
        {
            var y = nums;
            var t = Enumerable.Range(0, y.Count).ToList();
            var averageT = CountArithmeticAverage(t);
            var averageY = CountArithmeticAverage(y);
            var tmta = t.Select(x => x - averageT).ToList();
            var ymya = y.Select(x => x - averageY).ToList();
            var numerator = tmta.Zip(ymya).Select(x => x.Item1 * x.Item2).Aggregate(0d, (acc, x) => acc + x);
            var denominator = tmta.Select(x => Math.Pow(x, 2d)).Aggregate(0d, (acc, x) => acc + x);
            return numerator / denominator;
        }

        private static double CountArithmeticAverage(IReadOnlyCollection<int> nums)
        {
            var len = nums.Length();
            var sum = nums.Sum();
            return (double) sum / len;
        }

        public static Trend RateTrend(IEnumerable<int> nums)
        {
            var list = nums.ToList();
            if (list.Count > 0)
            {
                var a = GetAParameter(list);
                if (Math.Abs(a) < double.Epsilon)
                {
                    return Trend.Stable;
                }
                else if (a > 0d)
                {
                    return Trend.Increasing;

                }
                else
                {
                    return Trend.Decreasing;
                }
            }
        }
    }
}