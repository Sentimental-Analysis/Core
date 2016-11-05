using System.Collections.Generic;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface ITweetService : IService
    {
        Result<IEnumerable<Tweet>> GetTweetByKey(string key);
        Result<IEnumerable<Tweet>> GetTweetByKeyAsync(string key);
    }
}