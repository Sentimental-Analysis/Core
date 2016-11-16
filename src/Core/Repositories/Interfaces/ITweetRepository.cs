using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories.Interfaces
{
    public interface ITweetRepository : IDisposable
    {
        IEnumerable<Tweet> FindByKey(TweetQuery query);
        OperationStatus AddRange(IEnumerable<Tweet> tweets);
    }
}