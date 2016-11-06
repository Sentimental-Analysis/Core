using System.Collections.Generic;
using Core.Models;
using Core.Services.Implementations;
using Core.Tests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace Core.Tests.Services
{
    public class TweetServiceTests
    {
        [Fact]
        public void Test_Get_Tweets_When_Key_In_Db_Not_Exists()
        {
            var apiTweetRepositoryMock = new TweetRepositoryMock().Mock();
            var dbTweetRepositoryMock = new TweetRepositoryMock().Mock();
            var sentimentalAnalyzeServiceMocks = new SentimentalAnalysisServiceMock().Mock();
            var unitOfWorkMock = new UnitOfWorkMock(apiTweetRepositoryMock.Object, dbTweetRepositoryMock.Object,  new MemoryCacheFakeService(), sentimentalAnalyzeServiceMocks.Object).Mock();
            var service = new TweetService(unitOfWorkMock.Object);

            var tweets = service.GetTweetByKey(TweetRepositoryMock.DbKeyWithNull);
            dbTweetRepositoryMock.Verify(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == TweetRepositoryMock.DbKeyWithNull)), Times.Once);
            apiTweetRepositoryMock.Verify(x => x.FindByKey(It.IsAny<TweetQuery>()), Times.Once);
            sentimentalAnalyzeServiceMocks.Verify(x => x.Analyze(It.IsAny<IEnumerable<Tweet>>()), Times.Once);
        }

        [Fact]
        public void Test_Get_Tweets_When_Key_In_Db_Exists()
        {
            var apiTweetRepositoryMock = new TweetRepositoryMock().Mock();
            var dbTweetRepositoryMock = new TweetRepositoryMock().Mock();
            var sentimentalAnalyzeServiceMocks = new SentimentalAnalysisServiceMock().Mock();
            var unitOfWorkMock = new UnitOfWorkMock(apiTweetRepositoryMock.Object, dbTweetRepositoryMock.Object,  new MemoryCacheFakeService(), sentimentalAnalyzeServiceMocks.Object).Mock();
            var service = new TweetService(unitOfWorkMock.Object);

            var testResult = service.GetTweetByKey(TweetRepositoryMock.DbKeyWithList);
            dbTweetRepositoryMock.Verify(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == TweetRepositoryMock.DbKeyWithList)), Times.Once);
            apiTweetRepositoryMock.Verify(x => x.FindByKey(It.IsAny<TweetQuery>()), Times.Never);
            sentimentalAnalyzeServiceMocks.Verify(x => x.Analyze(It.IsAny<IEnumerable<Tweet>>()), Times.Never);
        }
    }
}