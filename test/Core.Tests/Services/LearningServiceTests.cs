using Bayes.Data;
using Bayes.Learner.Implementations;
using Core.Services.Implementations;
using Core.Tests.Builders;
using Core.Tests.Stubs;
using Xunit;

namespace Core.Tests.Services
{
    public class LearningServiceTests
    {
        [Fact]
        public void Test_First_Get_Learn_State()
        {
            var learningService = new LearningService(new CacheServiceForTests(), new TweetLearner(), new Sentence("hate", WordCategory.Negative), new Sentence("love", WordCategory.Positive));
        }

        public static T A<T>(IBuilder<T> stateBuilder)
        {
            return stateBuilder.Build();
        }
    }
}