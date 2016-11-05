using System.Collections.Generic;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface ITweetService : IService
    {
        IEnumerable<Tweet> GetTweetByKey(string key);
    }
}