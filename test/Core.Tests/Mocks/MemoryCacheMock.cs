using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Core.Tests.Mocks
{
    public class MemoryCacheMock : IMock<IMemoryCache>
    {
        public Mock<IMemoryCache> Mock()
        {
            var mock = new Mock<IMemoryCache>();
            return mock;
        }
    }
}