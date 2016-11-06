using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Models
{
    public class TweetTests
    {
        [Fact]
        public void Test_Creation_New_Twwet_With_Sentiment()
        {
            var tweet = new Tweet() {Sentiment = 0};
            var testResult = tweet.WithNewSentiment(5);
            testResult.Sentiment.Should().Be(5);
        }
    }
}