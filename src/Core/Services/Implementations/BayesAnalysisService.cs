using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Core.Models;
using Core.Services.Interfaces;
using Core.Utils;

namespace Core.Services.Implementations
{
    public class BayesAnalysisService : ISentimentalAnalysisService
    {
        private readonly IClassifier<Sentence, string> _tweetClassifier;

        public BayesAnalysisService(IClassifier<Sentence, string> tweetClassifier)
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
                return x.WithNewSentiment(res.Category);
            }).AsEnumerable();
            return Result<IEnumerable<Tweet>>.Wrap(result);
        }
    }
}