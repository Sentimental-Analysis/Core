using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class BayesAnalysisService : ISentimentalAnalysisService
    {
        private readonly IClassifier<Score, string> _tweetClassifier;

        public BayesAnalysisService(IClassifier<Score, string> tweetClassifier)
        {
            _tweetClassifier = tweetClassifier;
        }

        public Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets)
        {
            var result = tweets.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).Select(x =>
            {
                var res = _tweetClassifier.Classify(x.Text);
                return x.WithNewSentiment(res.Sentence.Category);
            }).AsEnumerable();
            return Result<IEnumerable<Tweet>>.Wrap(result);
        }
    }
}