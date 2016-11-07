using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Tweet : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string TweetIdentifier { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Language { get; set; }
        public string Key { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Sentiment { get; set; }

        public Tweet WithNewSentiment(int sentiment)
        {
            var clonedTweet = MemberwiseClone() as Tweet;
            if (clonedTweet != null)
            {
                clonedTweet.Sentiment = sentiment;
            }
            return clonedTweet;
        }
    }
}