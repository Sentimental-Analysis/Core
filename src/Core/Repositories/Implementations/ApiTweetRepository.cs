using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.Interfaces;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweet = Core.Models.Tweet;

namespace Core.Repositories.Implementations
{
    public class ApiTweetRepository : ITweetRepository
    {
        private readonly ITwitterCredentials _credentials;

        public ApiTweetRepository(Core.Models.TwitterApiCredentials credentials)
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

        public IEnumerable<Tweet> FindByKey(string query, int count)
        {
            var search = new SearchTweetsParameters(query)
            {
                SearchType = SearchResultType.Recent,
                Lang = LanguageFilter.English,
                Filters = TweetSearchFilters.None,
                MaximumNumberOfResults = count
            };
            var result = Search.SearchTweets(search);
            return result.Select(itweet => new Tweet
            {
                Id = itweet.TweetDTO.IdStr,
                Text = itweet.TweetDTO.Text,
                Language = itweet.TweetDTO.Language.ToString(),
                Key = query,
                Date = itweet.TweetDTO.CreatedAt,
                Latitude = itweet.TweetDTO?.Coordinates?.Latitude ?? 0.0,
                Longitude = itweet.TweetDTO?.Coordinates?.Longitude ?? 0.0,
                Sentiment = 0
            });
        }
    }
}