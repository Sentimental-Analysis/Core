using System.Collections.Generic;
using Core.Models;
using Core.Repositories.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class TweetRepositoryMock : IMock<ITweetRepository>
    {
        public const string DbKeyWithNull = "dbKeyWithNull";
        public const string ApiKeyWithNull = "apiKeyWithNull";
        public const string DbKeyWithList = "dbKeyWithList";
        public const string ApiKeyWithList = "apiKeyWithList";

        public TweetRepositoryMock()
        {

        }

        public Mock<ITweetRepository> Mock()
        {
            var mock = new Mock<ITweetRepository>();
            mock.Setup(x => x.FindByKey(new TweetQuery(DbKeyWithNull, It.IsAny<int>()))).Returns(() => null);
            mock.Setup(x => x.FindByKey(new TweetQuery(DbKeyWithList, It.IsAny<int>()))).Returns(() => new List<Tweet>());
            mock.Setup(x => x.FindByKey(new TweetQuery(ApiKeyWithNull, It.IsAny<int>()))).Returns(() => null);
            mock.Setup(x => x.FindByKey(new TweetQuery(ApiKeyWithList, It.IsAny<int>()))).Returns(() => new List<Tweet>());
            return mock;
        }
    }
}