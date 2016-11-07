using System;
using Core.Cache.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITweetRepository Tweets { get; }
        ITweetRepository ApiTweets { get; }
        ICacheService Cache { get; }
        ISentimentalAnalysisService SentimentalAnalysis { get; }
        int Complete();
    }
}