using System;
using Bayes.Data;
using Core.Builders;

namespace Core.Models
{
    public class Tweet : IEntity
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
        public TweetBuilder Builder => new TweetBuilder(this);
    }
}