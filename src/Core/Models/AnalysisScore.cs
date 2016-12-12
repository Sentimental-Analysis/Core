using System.Collections.Generic;
using System.Linq;
using Bayes.Data;
using Bayes.Utils;
using Core.Utils;

namespace Core.Models
{
    public class AnalysisScore
    {
        public GeneralSentiment Sentiment { get; set; }
        public int PositiveTweetsQuantity { get; set; }
        public int NegativeTweetsQuantity { get; set; }
        public IEnumerable<string> KeyWords { get; set; }


        public static AnalysisScore FromTweets(IEnumerable<Tweet> tweets)
        {
            var tweetsList = tweets.ToList();
            var keywords = tweetsList.SelectMany(x => x.Text.Tokenize()).DistinctBy(x => x);
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
                Sentiment = sentimentResult
            };
            return score;
        }
    }
}