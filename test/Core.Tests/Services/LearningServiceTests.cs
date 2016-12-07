using Bayes.Data;
using Bayes.Learner.Implementations;
using Core.Tests.Stubs;
using Xunit;
using static Core.Builders.LearningServiceBuilder;

namespace Core.Tests.Services
{
    public class LearningServiceTests
    {
        [Fact]
        public void Test_First_Get_Learn_State()
        {
            var learningService = LearningService.WithCacheService(new CacheServiceForTests()).WithLearner(new TweetLearner()).WithSentences(new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive));
        }

        public static T A<T>(Builders.IBuilder<T> stateBuilder)
        {
            return stateBuilder.Build();
        }
    }
}