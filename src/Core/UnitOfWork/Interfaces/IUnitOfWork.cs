using Core.Cache.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        ITweetRepository Tweets { get; }
        ICacheService CacheService { get; }
        ISentimentalAnalysisService SentimentalAnalysis { get; }
    }
}