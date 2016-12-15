using Bayes.Data;
using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Models
{
    public class TweetTests
    {
        [Fact]
        public void Test_Creation_New_Tweet_With_Sentiment()
        {
            var tweet = new Tweet {Sentiment = WordCategory.Positive}.Builder;
            tweet.Sentiment = WordCategory.Negative;
            var testResult = tweet.Build();
            testResult.Sentiment.Should().Be(WordCategory.Negative);
        }
    }
}