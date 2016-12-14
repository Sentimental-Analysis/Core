using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Bayes.Classifiers.Implementations;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Core.Models;
using Core.Services.Implementations;
using Core.Tests.Builders;
using Core.Tests.Stubs;
using FluentAssertions;
using Xunit;
using static Core.Tests.Builders.TestLearnStateBuilder;
using static Core.Builders.LearningServiceBuilder;

namespace Core.Tests.Services
{
    public class BayesAnalysisServiceTests
    {
        [Fact]
        public void Test_Classifier_When_All_Tweets_Are_Negative()
        {
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());
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
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());

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

        [Fact]
        public async Task Test_Async_Classifier_When_All_Tweets_Are_Negative()
        {        
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());
            var testResult = await service.AnalyzeAsync(new List<Tweet>()
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
        public async Task Test_Async_Classifier_When_All_Tweets_Are_Positive()
        {
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());

            var testResult = await service.AnalyzeAsync(new List<Tweet>()
            {
                new Tweet()
                {
                    Text = "love and sun"
                }
            });

            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.First().Sentiment.Should().Be(WordCategory.Positive);
        }

        [Fact]
        public async Task Test_Async_Classifier_With_Many_Tweets_When_All_Tweets_Are_Positive()
        {
            var testClassifier = A(TestLearnState().WithSentence(ImmutableDictionary<string, int>.Empty.Add("fuck", -1).Add("love", 4).Add("suck", -5).Add("sun", 5)));
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());

            var testResult = await service.AnalyzeAsync(Enumerable.Range(0, 10000).Select(x => new Tweet()
            {
                Text = "love and sun"
            }).ToList());

            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.First().Sentiment.Should().Be(WordCategory.Positive);
        }


        [Fact]
        public void Test_Classifier_With_Many_Tweets_When_All_Tweets_Are_Positive()
        {
            var initSentences = new[]
              {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();

            var service = new BayesAnalysisService(learningService, new TweetClassifier());

            var testResult = service.Analyze(Enumerable.Range(0, 10000).Select(x => new Tweet()
            {
                Text = "love and sun"
            }).ToList());

            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.First().Sentiment.Should().Be(WordCategory.Positive);
        }

        public T A<T>(IBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}