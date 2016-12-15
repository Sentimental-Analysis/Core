using System;
using Bayes.Data;
using Core.Models;

namespace Core.Builders
{
    public class TweetBuilder : IBuilder<Tweet>
    {
        public Guid Id { get; set; }
        public string TweetIdentifier { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public string Key { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WordCategory Sentiment { get; set; }

        public TweetBuilder(Tweet tweet)
        {
            Id = tweet.Id;
            TweetIdentifier = tweet.TweetIdentifier;
            Text = tweet.Text;
            Language = tweet.Language;
            Key = tweet.Key;
            Date = tweet.Date;
            Latitude = tweet.Latitude;
            Longitude = tweet.Longitude;
            Sentiment = tweet.Sentiment;
        }

        public Tweet Build()
        {
            return new Tweet
            {
                Id = Id,
                TweetIdentifier = TweetIdentifier,
                Text = Text,
                Date = Date,
                Language = Language,
                Key = Key,
                Longitude = Longitude,
                Latitude = Latitude,
                Sentiment = Sentiment,              
            };
        }
    }
}