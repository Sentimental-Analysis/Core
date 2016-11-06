using Core.Services.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class SentimentalAnalysisServiceMock : IMock<ISentimentalAnalysisService>
    {
        public Mock<ISentimentalAnalysisService> Mock()
        {
            var mock = new Mock<ISentimentalAnalysisService>();
            return mock;
        }
    }
}