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
        private readonly IMemoryCache _memoryCache;
        private readonly ISentimentalAnalysisService _sentimentalAnalysisService;

        public UnitOfWorkMock(ITweetRepository apiTweetRepository, ITweetRepository dbTweetRepository, IMemoryCache memoryCache, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _apiTweetRepository = apiTweetRepository;
            _dbTweetRepository = dbTweetRepository;
            _memoryCache = memoryCache;
            _sentimentalAnalysisService = sentimentalAnalysisService;
        }


        public Mock<IUnitOfWork> Mock()
        {
            var mock = new Mock<IUnitOfWork>();
            return mock;
        }
    }
}