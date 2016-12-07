using Core.Services.Implementations;
using static Core.Tests.Builders.LearnStateBuilder;
using Core.Tests.Builders;
using Xunit;

namespace Core.Tests.Services
{
    public class LearningServiceTests
    {
        [Fact]
        public void Test_First_Get_Learn_State()
        {
            var learningService = new LearningService()
        }

        public static T A<T>(IBuilder<T> stateBuilder)
        {
            return stateBuilder.Build();
        }
    }
}