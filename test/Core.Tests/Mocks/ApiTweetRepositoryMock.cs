using System.Collections.Generic;
using Core.Models;
using Core.Repositories.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class ApiTweetRepositoryMock : IMock<ITweetApiRepository>
    {
        public Mock<ITweetApiRepository> Mock()
        {
            var mock = new Mock<ITweetApiRepository>();
            mock.Setup(x => x.Get(It.IsAny<TweetQuery>())).Returns(new List<Tweet>() {new Tweet() {Text = "AAA aaa"}});
            return mock;
        }
    }
}