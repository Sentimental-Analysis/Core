using System.Collections.Generic;
using Core.Models;
using Core.Utils;
using Xunit;

namespace Core.Tests.Utils
{
    public class StatisticsUtilsTests
    {
        [Fact]
        public void Test_Rating_Trend_When_Trend_Is_Stable()
        {
            var l = new List<int> {1, 1, 1};
            var trend = Statistics.RateTrend(l);
            Assert.Equal(Trend.Stable, trend);
        }

        [Fact]
        public void Test_Rating_Trend_When_Trend_Is_Increasing()
        {
            var l = new List<int> {0, 0, 1, 1, 1};
            var trend = Statistics.RateTrend(l);
            Assert.Equal(Trend.Increasing, trend);
        }

        [Fact]
        public void Test_Rating_Trend_When_Trend_Is_Decreasing()
        {
            var l = new List<int> {1, 1, 0, 0, 0};
            var trend = Statistics.RateTrend(l);
            Assert.Equal(Trend.Decreasing, trend);
        }
    }
}