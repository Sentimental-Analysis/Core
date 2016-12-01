using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Core.Models;
using Core.Services.Implementations;
using Core.Tests.Builders;
using FluentAssertions;
using Xunit;
using static Core.Tests.Builders.ClassifierBuilder;

namespace Core.Tests.Services
{
    public class BayesAnalysisServiceTests
    {
        [Fact]
        public void Test_Classifier_When_All_Tweets_Are_Negative()
        {
            var testClassifier = A(Classifier().WithLearnData(ImmutableDictionary<string, int>.Empty.Add("fuck", -1).Add("love", 4).Add("suck", -5).Add("sun", 5)));
            var service = new BayesAnalysisService(testClassifier);
            var testResult = service.Analyze(new List<Tweet>()
            {
                new Tweet()
                {
                    Text = "fuck and suck"
                }
            });

            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.First().Sentiment.Should().Be(WordCategory.Negative);
        }

        [Fact]
        public void Test_Classifier_When_All_Tweets_Are_Positive()
        {
            var testClassifier = A(Classifier().WithLearnData(ImmutableDictionary<string, int>.Empty.Add("fuck", -1).Add("love", 4).Add("suck", -5).Add("sun", 5)));
            var service = new BayesAnalysisService(testClassifier);
            var testResult = service.Analyze(new List<Tweet>()
            {
                new Tweet()
                {
                    Text = "love and sun"
                }
            });

            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.First().Sentiment.Should().Be(WordCategory.Positive);
        }

        public T A<T>(IBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}