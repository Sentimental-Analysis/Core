using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Utils
{
    public class LinqTests
    {
        [Fact]
        public void Test_DistinctBy_Extension_Method_When_List_Has_No_Duplicates()
        {
            var testEnumerable = Enumerable.Range(1,10);
            var testResult = testEnumerable.DistinctBy(x => x);
            testResult.Count().Should().Be(10);
        }

        [Fact]
        public void Test_DistinctBy_Extension_Method_When_List_Has_Duplicates()
        {
            var testEnumerable = new List<int>
            {
                1,2,3,4,1,2,3,4
            };
            var testResult = testEnumerable.DistinctBy(x => x);
            testResult.Count().Should().Be(4);
        }

        [Fact]
        public void Test_Filter_Short_Word_Extension_Method()
        {
            var words = new[] { null, "1", "22", "333", "4444" };
            var testResult = words.FilterShortWord();
            testResult.Should().Contain("4444").And.NotContain("1", "22", "333").And.NotContainNulls();
        }
    }
}