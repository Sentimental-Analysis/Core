using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class BayesAnalysisService : ISentimentalAnalysisService
    {
        private readonly ILearningService _learningService;
        private readonly ITweetClassifier _tweetClassifier;

        public BayesAnalysisService(ILearningService learningService, ITweetClassifier classifier)
        {
            _learningService = learningService;
            _tweetClassifier = classifier;
        }

        public async Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets)
        {
            var buffer =
                new BufferBlock<Tweet>(new DataflowBlockOptions {BoundedCapacity = Environment.ProcessorCount * 5});
            var learnerState = _learningService.Get();
            var result = new List<Tweet>();
            var classifier = new ActionBlock<Tweet>(x =>
            {
                var res = _tweetClassifier.Classify(x.Text, learnerState);
                var tweetBuilder = x.Builder;
                tweetBuilder.Sentiment = res.Sentence.Category;
                result.Add(tweetBuilder.Build());
            }, new ExecutionDataflowBlockOptions {BoundedCapacity = 1});

            var classifiers = Enumerable.Range(0, Environment.ProcessorCount).Select(x => classifier).ToList();

            var linkToOptions = new DataflowLinkOptions {PropagateCompletion = true};
            classifiers.ForEach(x => buffer.LinkTo(x, linkToOptions));

            using (tweets.ToObservable()
                .Retry(5)
                .SubscribeOn(Scheduler.Default)
                .Subscribe(onNext: async tweet => { await buffer.SendAsync(tweet); },
                    onError: ex => { ((IDataflowBlock)buffer).Fault(ex); },
                    onCompleted: () => { buffer.Complete(); }))
            {
                await Task.WhenAll(buffer.Completion);
                await Task.WhenAll(classifiers.Select(x => x.Completion).ToArray());
                _learningService.Learn(result.Select(x => new Sentence(x.Text, x.Sentiment)));
                return Result<IEnumerable<Tweet>>.Wrap(result.AsEnumerable());
            }
        }

        public Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets)
        {
            var learnerState = _learningService.Get();
            var result = tweets.AsParallel().WithDegreeOfParallelism(Environment.ProcessorCount).Select(x =>
            {
                var res = _tweetClassifier.Classify(x.Text, learnerState);
                var tweetBuilder = x.Builder;
                tweetBuilder.Sentiment = res.Sentence.Category;
                return tweetBuilder.Build();
            }).ToList();
            _learningService.Learn(result.Select(x => new Sentence(x.Text, x.Sentiment)));
            return Result<IEnumerable<Tweet>>.Wrap(result.AsEnumerable());
        }
    }
}