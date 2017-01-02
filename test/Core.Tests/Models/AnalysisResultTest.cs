using System.Collections.Generic;
using Bayes.Data;
using Core.Builders;
using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Models
{
    public class AnalysisResultTest
    {
        [Fact]
        public void Test_From_Tweets_Method_When_Most_Tweet_Are_Negative()
        {
            var tweets = new List<Tweet>
            {
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test Negative Tweet"},
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test Negative Tweet"},
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test Negative Tweet"},
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"}
            };

            var score = AnalysisScoreBuilder.AnalysisScore(tweets, "negative").Build();
            score.Sentiment.Should().Be(GeneralSentiment.Negative);
            score.NegativeTweetsQuantity.Should().Be(3);
            score.PositiveTweetsQuantity.Should().Be(1);
        }

        [Fact]
        public void Test_From_Tweets_Method_When_Most_Tweet_Are_Positive()
        {
            var tweets = new List<Tweet>
            {
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"},
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"},
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test Negative Tweet"},
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"}
            };

            var score = AnalysisScoreBuilder.AnalysisScore(tweets, "positive").Build();
            score.Sentiment.Should().Be(GeneralSentiment.Positive);
            score.NegativeTweetsQuantity.Should().Be(1);
            score.PositiveTweetsQuantity.Should().Be(3);
        }

        [Fact]
        public void Test_From_Tweets_Method_When_Positive_And_Negative_Tweets_Quantity_Is_Equal()
        {
            var tweets = new List<Tweet>
            {
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"},
                new Tweet { Sentiment = WordCategory.Positive, Text = "Test Positive Tweet"},
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test Negative Tweet"},
                new Tweet { Sentiment = WordCategory.Negative, Text = "Test negative Tweet"}
            };

            var score = AnalysisScoreBuilder.AnalysisScore(tweets, "test").Build();
            score.Sentiment.Should().Be(GeneralSentiment.Neutral);
            score.NegativeTweetsQuantity.Should().Be(2);
            score.PositiveTweetsQuantity.Should().Be(2);;
        }
    }
}