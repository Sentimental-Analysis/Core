using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;
using Core.Utils;

namespace Core.Services.Implementations
{
    public class SimpleAnalysisService : ISentimentalAnalysisService
    {
        private readonly ImmutableDictionary<string, int> _sentiments;

        public SimpleAnalysisService(ImmutableDictionary<string, int> sentiments)
        {
            _sentiments = sentiments;
        }

        public Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets)
        {
            var result =
                tweets.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).Select(AnalyzeOne).ToList();
            return Result<IEnumerable<Tweet>>.Wrap(result?.AsEnumerable());
        }

        private Tweet AnalyzeOne(Tweet tweet)
        {
            var result =
                tweet.Text.Split(' ').Select(x => Regex.Replace(x, @"\W", "").ToLower()).Aggregate(Tuple.Create(0, ""),
                    (acc, x) =>
                    {
                        int score;
                        if (_sentiments.TryGetValue(x, out score))
                        {
                            int finalscore = Text.IsNegate(acc.Item2) ? -score : score;
                            return Tuple.Create(acc.Item1 + finalscore, x);
                        }
                        return Tuple.Create(acc.Item1, x);
                    });
            return tweet.WithNewSentiment(result.Item1);
        }
    }
}