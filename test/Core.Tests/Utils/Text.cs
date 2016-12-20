using Core.Utils;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Utils
{
    public class Text
    {
        [Theory]
        [InlineData("no", true)]
        [InlineData("not", true)]
        [InlineData("nope", true)]
        [InlineData("dont", true)]
        [InlineData("true", false)]
        [InlineData("awesome", false)]
        [InlineData("love", false)]
        public void Test_Is_Negate_Checking(string text, bool shouldBeNegate)
        {
            var testResult = Core.Utils.Text.IsNegate(text);
            testResult.Should().Be(shouldBeNegate);
        }
    }
}