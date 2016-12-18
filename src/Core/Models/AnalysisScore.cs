using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Bayes.Utils;

namespace Core.Models
{
    public class AnalysisScore
    {
        public GeneralSentiment Sentiment { get; set; }
        public int PositiveTweetsQuantity { get; set; }
        public int NegativeTweetsQuantity { get; set; }
        public IEnumerable<KeyWord> KeyWords { get; set; }
        public string Key { get; set; }


        public static AnalysisScore FromTweets(IEnumerable<Tweet> tweets, string key)
        {
            var tweetsList = tweets.ToList();
            var keywords = tweetsList.SelectMany(x => x.Text.Tokenize()).Aggregate(ImmutableDictionary<string, int>.Empty,
                (acc, x) =>
                {
                    int value;
                    if (acc.TryGetValue(x, out value))
                    {
                        return acc.SetItem(x, value + 1);
                    }
                    return acc.Add(x, 1);
                }).Select(x => new KeyWord(){ Key = x.Key, Quantity = x.Value}).OrderByDescending(x => x.Quantity).ToList();

            var negativeQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Negative);
            var positiveQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Positive);
            var sentimentResult = negativeQuantity > positiveQuantity
                ? GeneralSentiment.Negative
                : negativeQuantity == positiveQuantity ? GeneralSentiment.Neutral : GeneralSentiment.Positive;
            var score = new AnalysisScore
            {
                KeyWords = keywords,
                NegativeTweetsQuantity = negativeQuantity,
                PositiveTweetsQuantity = positiveQuantity,
                Sentiment = sentimentResult,
                Key = key
            };
            return score;
        }
    }
}