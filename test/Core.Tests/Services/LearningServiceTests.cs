using System.Linq;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Core.Tests.Stubs;
using FluentAssertions;
using Xunit;
using static Core.Builders.LearningServiceBuilder;

namespace Core.Tests.Services
{
    public class LearningServiceTests
    {
        [Fact]
        public void Test_First_Get_Learn_State()
        {
            var initSentences = new[]
                {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();
            var learnerState = learningService.Get();
            learnerState.Should().NotBeNull();
            learnerState.WordPerQuantity.Keys.Count().Should().Be(2);
            learnerState.CategoryPerQuantity.Keys.Count().Should().Be(2);
        }


        [Fact]
        public void Test_Learn_Method()
        {
            var initSentences = new[]
                {new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive)};
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(initSentences).Build();
            var learnerState = learningService.Get();
            learnerState.Should().NotBeNull();
            learnerState.CategoryPerQuantity.Keys.Count().Should().Be(2);
            learnerState.WordPerQuantity.Keys.Count().Should().Be(2);
            var newSentences = new[]
                {new Sentence("rain", WordCategory.Negative), new Sentence("sun", WordCategory.Positive)};
            var storeRes = learningService.Learn(newSentences);

            storeRes.CategoryPerQuantity.Keys.Count().Should().Be(2);
            storeRes.WordPerQuantity.Keys.Count().Should().Be(4);
        }

        public static T A<T>(Builders.IBuilder<T> stateBuilder)
        {
            return stateBuilder.Build();
        }
    }
}