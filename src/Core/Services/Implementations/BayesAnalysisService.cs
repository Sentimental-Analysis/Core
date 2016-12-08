using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Bayes.Classifiers.Implementations;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class BayesAnalysisService : ISentimentalAnalysisService
    {
        private readonly IClassifier<Score, string> _tweetClassifier;
        private readonly ILearningService _learningService;

        public BayesAnalysisService(IClassifier<Score, string> tweetClassifier, ILearningService learningService)
        {
            _tweetClassifier = tweetClassifier;
            _learningService = learningService;
        }

        public Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets)
        {
            var buffer = new BufferBlock<Tweet>(new DataflowBlockOptions(){ BoundedCapacity = Environment.ProcessorCount * 5});
            var classifyTransformBlock = new TransformBlock<Tweet, Tuple<Tweet, LearnerState>>(tweet =>
            {
                var learnState = _learningService.Get();
                var score = new TweetClassifier(learnState).Classify(tweet.Text);
                return Tuple.Create(tweet.WithNewSentiment(score.Sentence.Category), learnState);
            });
            var result = tweets.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).Select(x =>
            {
                var res = _tweetClassifier.Classify(x.Text);
                return x.WithNewSentiment(res.Sentence.Category);
            }).AsEnumerable();
            return Result<IEnumerable<Tweet>>.Wrap(result);
        }
    }
}