using System;
using Core.Cache.Implementations;
using Core.Cache.Interfaces;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Implementations;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Core.UnitOfWork.Implementations
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private bool _shouldBeDisposed = true;

        public DefaultUnitOfWork(IDbContext context, IMemoryCache cache, TwitterApiCredentials credentials, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _dbContext = context;
            ApiTweets = new ApiTweetRepository(credentials);
            Tweets = new DatabaseTweetRepository(context);
            Cache = new InMemoryCacheService(cache);
            SentimentalAnalysis = sentimentalAnalysisService;
        }

        public ITweetRepository Tweets { get; }
        public ITweetRepository ApiTweets { get; }
        public ICacheService Cache { get; }
        public ISentimentalAnalysisService SentimentalAnalysis { get; }

        public void Dispose()
        {
            if (_shouldBeDisposed)
            {
                _shouldBeDisposed = false;
                _dbContext?.Dispose();
                Cache?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}