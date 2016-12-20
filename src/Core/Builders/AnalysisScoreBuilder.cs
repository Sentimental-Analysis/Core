using System;
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
        private readonly string _key;
        private readonly IEnumerable<Tweet> _tweets;


        public static AnalysisScoreBuilder AnalysisScore(IEnumerable<Tweet> tweets, string key)
            => new AnalysisScoreBuilder(tweets, key);

        public AnalysisScoreBuilder(IEnumerable<Tweet> tweets, string key)
        {
            _key = key;
            _tweets = tweets;
        }

        private AnalysisScore FromTweets(IEnumerable<Tweet> tweets, string key)
        {
            var tweetsList = tweets.ToList();
            var keywords =
                tweetsList?.SelectMany(x => x.Text.Tokenize())
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
                        }).Select(x => new KeyWord(x.Key, x.Value)).OrderByDescending(x => x.Quantity).Take(50).ToList() ?? new List<KeyWord>();

            var negativeQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Negative);
            var positiveQuantity = tweetsList.Count(tweet => tweet.Sentiment == WordCategory.Positive);
            var sentimentResult = negativeQuantity > positiveQuantity
                ? GeneralSentiment.Negative
                : negativeQuantity == positiveQuantity ? GeneralSentiment.Neutral : GeneralSentiment.Positive;

            var localizations =
                tweetsList.Where(
                        tweet =>
                            Math.Abs(tweet.Longitude - default(double)) > double.Epsilon &&
                            Math.Abs(tweet.Latitude - default(double)) > double.Epsilon)
                    .Select(tweet => new Localization(tweet.Longitude, tweet.Latitude));

            return new AnalysisScore
            {
                Sentiment = sentimentResult,
                Key = key,
                PositiveTweetsQuantity = positiveQuantity,
                NegativeTweetsQuantity = negativeQuantity,
                Localizations = localizations,
                KeyWords = keywords,
            };
        }

        public AnalysisScore Build()
        {
            return FromTweets(_tweets, _key);
        }
    }
}