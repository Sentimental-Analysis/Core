using Moq;

namespace Core.Tests.Mocks
{
    public interface IMock<T> where T : class
    {
        Mock<T> Mock();
    }
}