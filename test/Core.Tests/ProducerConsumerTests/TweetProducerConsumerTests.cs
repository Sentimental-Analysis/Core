using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Core.Models;
using Core.ProducerConsumer.Implementation;
using Core.Tests.Builders;
using Core.Tests.Stubs;
using FluentAssertions;
using Xunit;
using static Core.Builders.LearningServiceBuilder;
using static Core.Tests.Builders.TweetBuilder;
namespace Core.Tests.ProducerConsumerTests
{
    public class TweetProducerConsumerTests
    {
        [Fact]
        public void Test_Producer_Consumer_Manager()
        {
            var initSentences = new[]
                {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService =
                LearningService.WithCacheService(new CacheServiceForTests())
                    .WithLearner(new TweetLearner())
                    .WithSentences(initSentences)
                    .Build();
            var manager = new TweetProducerConsumerService(new TweetTweetProducerConsumer(learningService));

            var tweets = new List<Tweet>()
            {
                A(Tweet().WithText("hate")),
                A(Tweet().WithText("love"))
            };

            var testResult = manager.Manage(tweets);
            testResult.Should().NotBeEmpty();

        }

        public T A<T>(IBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}