using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Bayes.Utils;
using Core.Models;

namespace Core.Builders
{
    public class AnalysisScoreBuilder : IBuilder<AnalysisScore>
    {
        public GeneralSentiment Sentiment { get; set; }
        public int PositiveTweetsQuantity { get; set; }
        public int NegativeTweetsQuantity { get; set; }
        public IEnumerable<KeyWord> KeyWords { get; set; }
        public string Key { get; set; }

        public AnalysisScoreBuilder(IEnumerable<Tweet> tweets, string key)
        {
            var tweetsList = tweets.ToList();
            var keywords =
                tweetsList.SelectMany(x => x.Text.Tokenize())
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Aggregate(ImmutableDictionary<string, int>.Empty,
                        (acc, x) =>
                        {
                            int value;
                            if (acc.TryGetValue(x, out value))
                            {
                                return acc.SetItem(x, value + 1);
                            }
                            return acc.Add(x, 1);
                        }).Select(x => new KeyWord(x.Key, x.Value)).OrderByDescending(x => x.Quantity).Take(50).ToList();

            var negativeQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Negative);
            var positiveQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Positive);
            var sentimentResult = negativeQuantity > positiveQuantity
                ? GeneralSentiment.Negative
                : negativeQuantity == positiveQuantity ? GeneralSentiment.Neutral : GeneralSentiment.Positive;

            KeyWords = keywords;
            NegativeTweetsQuantity = negativeQuantity;
            PositiveTweetsQuantity = positiveQuantity;
            Sentiment = sentimentResult;
            Key = key;
        }

        public AnalysisScore Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
