using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.Interfaces;
using Tweetinvi;
using Tweetinvi.Models;
using Tweet = Core.Models.Tweet;

namespace Core.Repositories.Implementations
{
    public class ApiTweetRepository : ITweetRepository
    {
        private readonly ITwitterCredentials _credentials;

        public ApiTweetRepository(TwitterCredentials credentials)
        {
            _credentials = Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret,
                credentials.AccessToken, credentials.AccessTokenSecret);
        }

        public IEnumerable<Tweet> Find(Expression<Func<Tweet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tweet>> FindAsync(Expression<Func<Tweet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tweet> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tweet>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public OperationStatus Add(Tweet entity)
        {
            throw new NotImplementedException();
        }

        public OperationStatus AddRange(IEnumerable<Tweet> entities)
        {
            throw new NotImplementedException();
        }

        public OperationStatus Remove(Tweet entity)
        {
            throw new NotImplementedException();
        }

        public OperationStatus RemoveRange(IEnumerable<Tweet> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tweet> FindByKey(string query)
        {
            throw new NotImplementedException();
        }
    }
}