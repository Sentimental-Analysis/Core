using System.Collections.Generic;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class TweetService: ITweetService
    {
        public IEnumerable<Tweet> GetTweetByKey(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}