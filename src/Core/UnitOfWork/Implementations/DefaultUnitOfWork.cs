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

        public DefaultUnitOfWork(IDbContext context, TwitterApiCredentials credentials, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _dbContext = context;
            ApiTweets = new ApiTweetRepository(credentials);
            Tweets = new DatabaseTweetRepository(context);
            SentimentalAnalysis = sentimentalAnalysisService;
        }

        public ITweetRepository Tweets { get; }
        public ITweetRepository ApiTweets { get; }
        public ISentimentalAnalysisService SentimentalAnalysis { get; }

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