using Tweetinvi.Models;

namespace Core.Models
{
    public class TweetApiCredentials : ITwitterCredentials
    {
        public string ConsumerKey { get; set; }
        public string AccessToken { get; set; }
        public string ConsumnerSecret { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}