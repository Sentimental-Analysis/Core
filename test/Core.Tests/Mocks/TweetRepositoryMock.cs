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
        public const string DbKeyWithList = "dbKeyWithList";

        public Mock<ITweetRepository> Mock()
        {
            var mock = new Mock<ITweetRepository>();
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == DbKeyWithNull))).Returns(Enumerable.Empty<Tweet>).Verifiable();
            mock.Setup(x => x.FindByKey(It.Is<TweetQuery>(query => query.Key == DbKeyWithList))).Returns(() => new List<Tweet>() { new Tweet() {Text = "AAA aaa"} }).Verifiable();
            return mock;
        }
    }
}