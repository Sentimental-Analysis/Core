using Bayes.Data;
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
            var tweet = new Tweet() {Sentiment = WordCategory.Positive};
            var testResult = tweet.WithNewSentiment(WordCategory.Negative);
            testResult.Sentiment.Should().Be(WordCategory.Negative);
        }
    }
}