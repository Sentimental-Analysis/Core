using Core.Models;

namespace Core.Tests.Builders
{
    public class TweetBuilder : IBuilder<Tweet>
    {
        private string _text = string.Empty;
        private int _sentiment = 0;

        public static TweetBuilder Tweet() => new TweetBuilder();

        public TweetBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public TweetBuilder WithSentiment(int sentiment)
        {
            _sentiment = sentiment;
            return this;
        }

        public Tweet Build()
        {
            return new Tweet()
            {
                Text = _text,
                Sentiment = _sentiment
            };
        }
    }
}