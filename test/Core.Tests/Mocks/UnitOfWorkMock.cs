using Core.UnitOfWork.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class UnitOfWorkMock : IMock<IUnitOfWork>
    {
        public Mock<IUnitOfWork> Mock()
        {
            var mock = new Mock<IUnitOfWork>();
            return mock;
        }
    }
}