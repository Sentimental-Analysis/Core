using Core.Repositories.Interfaces;
using Core.UnitOfWork.Interfaces;
using Moq;

namespace Core.Tests.Mocks
{
    public class UnitOfWorkMock : IMock<IUnitOfWork>
    {

        private readonly ITweetApiRepository _apiTweetRepository;
        private readonly ITweetRepository _dbTweetRepository;

        public UnitOfWorkMock(ITweetRepository dbTweetRepository, ITweetApiRepository apiTweetRepository)
        {
            _apiTweetRepository = apiTweetRepository;
            _dbTweetRepository = dbTweetRepository;
        }


        public Mock<IUnitOfWork> Mock()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ApiTweets).Returns(() => _apiTweetRepository);
            mock.Setup(x => x.Tweets).Returns(() => _dbTweetRepository);
            return mock;
        }
    }
}