using Core.Cache.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Core.Tests.Mocks
{
    public class UnitOfWorkMock : IMock<IUnitOfWork>
    {

        private readonly ITweetRepository _apiTweetRepository;
        private readonly ITweetRepository _dbTweetRepository;
        private readonly ICacheService _memoryCache;
        private readonly ISentimentalAnalysisService _sentimentalAnalysisService;

        public UnitOfWorkMock(ITweetRepository apiTweetRepository, ITweetRepository dbTweetRepository, ICacheService memoryCache, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _apiTweetRepository = apiTweetRepository;
            _dbTweetRepository = dbTweetRepository;
            _memoryCache = memoryCache;
            _sentimentalAnalysisService = sentimentalAnalysisService;
        }


        public Mock<IUnitOfWork> Mock()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ApiTweets).Returns(() => _apiTweetRepository);
            mock.Setup(x => x.Tweets).Returns(() => _dbTweetRepository);
            mock.Setup(x => x.Cache).Returns(() => _memoryCache);
            mock.Setup(x => x.SentimentalAnalysis).Returns(() => _sentimentalAnalysisService);
            return mock;
        }
    }
}