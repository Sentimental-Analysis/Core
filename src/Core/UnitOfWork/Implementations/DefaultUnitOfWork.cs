using System;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Implementations;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;

namespace Core.UnitOfWork.Implementations
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private bool _shouldBeDisposed = true;

        public DefaultUnitOfWork(IDbContext context, TwitterApiCredentials credentials)
        {
            _dbContext = context;
            Tweets = new DatabaseTweetRepository(_dbContext);
            ApiTweets = new ApiTweetRepository(credentials);
        }

        public ITweetRepository Tweets { get; }
        public ITweetApiRepository ApiTweets { get; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_shouldBeDisposed)
            {
                _shouldBeDisposed = false;
                _dbContext?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}