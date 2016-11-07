using System.Collections.Generic;
using System.Linq;
using Core.Models;
using Core.Repositories.Interfaces;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweet = Core.Models.Tweet;

namespace Core.Repositories.Implementations
{
    public class ApiTweetRepository : ITweetApiRepository
    {
        private readonly ITwitterCredentials _credentials;

        public ApiTweetRepository(TwitterApiCredentials credentials)
        {
            _credentials = Auth.SetUserCredentials(credentials.ConsumerKey, credentials.ConsumerSecret,
                credentials.AccessToken, credentials.AccessTokenSecret);
        }

        public IEnumerable<Tweet> Get(TweetQuery parameter)
        {
            var search = new SearchTweetsParameters(parameter.Key)
            {
                SearchType = SearchResultType.Recent,
                Lang = LanguageFilter.English,
                Filters = TweetSearchFilters.None,
                MaximumNumberOfResults = parameter.MaxQuantity
            };
            var result = Search.SearchTweets(search);
            return result.Select(itweet => new Tweet
            {
                Id = itweet.TweetDTO.IdStr,
                Text = itweet.TweetDTO.Text,
                Language = itweet.TweetDTO.Language.ToString(),
                Key = parameter.Key,
                Date = itweet.TweetDTO.CreatedAt,
                Latitude = itweet.TweetDTO?.Coordinates?.Latitude ?? 0.0,
                Longitude = itweet.TweetDTO?.Coordinates?.Longitude ?? 0.0,
                Sentiment = 0
            });
        }
    }
}