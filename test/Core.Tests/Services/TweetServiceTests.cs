using System.Collections.Generic;
using Core.Cache.Interfaces;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;
using Moq;
using Xunit;

namespace Core.Tests.Services
{
    public class TweetServiceTests
    {
        private readonly ITweetService _service;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITweetRepository> _apiTweetRepositoryMock;
        private readonly Mock<ITweetRepository> _dbTweetRepositoryMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly Mock<ISentimentalAnalysisService> _sentimentalAnalyzeServiceMocks;

        public TweetServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _apiTweetRepositoryMock = new Mock<ITweetRepository>();
            _dbTweetRepositoryMock = new Mock<ITweetRepository>();
            _cacheServiceMock = new Mock<ICacheService>();
            _sentimentalAnalyzeServiceMocks = new Mock<ISentimentalAnalysisService>();
            _service = new TweetService(_unitOfWorkMock.Object);

        }

        [Fact]
        public void Test_Get_Tweets_When_Key_In_Db_Not_Exists()
        {
            var tweets = _service.GetTweetByKey("key");
        }
    }
}