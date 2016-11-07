using System;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITweetRepository Tweets { get; }
        ITweetApiRepository ApiTweets { get; }
        int Complete();
    }
}