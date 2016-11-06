using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Repositories.Interfaces;
using LanguageExt;
using Moq;

namespace Core.Tests.Mocks
{
    public class TweetRepositoryMock : IMock<ITweetRepository>
    {
        public const string DbKeyWithNull = "dbKeyWithNull";
        public const string ApiKeyWithNull = "apiKeyWithNull";
        public const string DbKeyWithList = "dbKeyWithList";
        public const string ApiKeyWithList = "apiKeyWithList";

        public Mock<ITweetRepository> Mock()
        {
            var mock = new Mock<ITweetRepository>();
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == DbKeyWithNull))).Returns(Enumerable.Empty<Tweet>).Verifiable();
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == DbKeyWithList))).Returns(() => new List<Tweet>() { new Tweet() }).Verifiable();;
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == ApiKeyWithNull))).Returns(Enumerable.Empty<Tweet>).Verifiable();;
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == ApiKeyWithNull))).Returns(() => new List<Tweet>() { new Tweet() }).Verifiable();;
            return mock;
        }
    }
}