using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Bayes.Utils;
using Core.Models;
using Core.Utils;

namespace Core.Builders
{
    public class AnalysisScoreBuilder : IBuilder<AnalysisScore>
    {
        private readonly GeneralSentiment _sentiment;
        private readonly int _positiveTweetsQuantity;
        private readonly int _negativeTweetsQuantity;
        private readonly IEnumerable<KeyWord> _keyWords;
        private readonly string _key;

        public static AnalysisScoreBuilder AnalysisScore(IEnumerable<Tweet> tweets, string key) => new AnalysisScoreBuilder(tweets, key);

        public AnalysisScoreBuilder(IEnumerable<Tweet> tweets, string key)
        {
            var tweetsList = tweets.ToList();
            var keywords =
                tweetsList.SelectMany(x => x.Text.Tokenize())
                    .FilterShortWord()
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

            _keyWords = keywords;
            _negativeTweetsQuantity = negativeQuantity;
            _positiveTweetsQuantity = positiveQuantity;
            _sentiment = sentimentResult;
            _key = key;
        }

        public AnalysisScore Build()
        {
            return new AnalysisScore
            {
                Sentiment = _sentiment,
                Key = _key,
                PositiveTweetsQuantity = _positiveTweetsQuantity,
                NegativeTweetsQuantity = _negativeTweetsQuantity,
                KeyWords = _keyWords,
            };
        }
    }
}
