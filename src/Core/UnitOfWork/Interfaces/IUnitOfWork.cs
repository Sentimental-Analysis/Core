using System;
using Core.Repositories.Interfaces;

namespace Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITweetRepository Tweets { get; }
        ITweetApiRepository ApiTweets { get; }
    }
}