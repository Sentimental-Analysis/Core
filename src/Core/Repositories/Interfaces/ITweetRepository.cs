using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Models;

namespace Core.Repositories.Interfaces
{
    public interface ITweetRepository : IDisposable
    {
        IEnumerable<Tweet> FindByKey(TweetQuery query);
        OperationStatus AddRange(IEnumerable<Tweet> tweets);
        IDictionary<string, long> QuantityByKey();
        IEnumerable<string> GetUniqueKeys();
    }
}