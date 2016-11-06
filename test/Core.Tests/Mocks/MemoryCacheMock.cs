using System;
using System.Collections.Generic;
using Core.Cache.Interfaces;
using Moq;
using Tweetinvi.Logic;

namespace Core.Tests.Mocks
{
    public class MemoryCacheMock : IMock<ICacheService>
    {
        public Mock<ICacheService> Mock()
        {
            var mock = new Mock<ICacheService>();
            mock.Setup(x => x.GetOrStore(It.IsAny<string>(), It.IsAny<Func<IEnumerable<Tweet>>>(), It.IsAny<TimeSpan>()))
                .Returns<IEnumerable<Tweet>>(x => x);
            return mock;
        }
    }
}