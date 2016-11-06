using System.Collections.Generic;
using Core.Models;
using Core.Services.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class SentimentalAnalysisServiceMock : IMock<ISentimentalAnalysisService>
    {
        public Mock<ISentimentalAnalysisService> Mock()
        {
            var mock = new Mock<ISentimentalAnalysisService>();
            mock.Setup(x => x.Analyze(It.IsAny<IEnumerable<Tweet>>())).Returns<IEnumerable<Tweet>>(x => Result<IEnumerable<Tweet>>.Wrap(x));
            return mock;
        }
    }
}