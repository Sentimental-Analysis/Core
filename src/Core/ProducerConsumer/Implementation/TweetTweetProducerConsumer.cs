using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Bayes.Classifiers.Implementations;
using Bayes.Learner.Implementations;
using Core.Models;
using Core.ProducerConsumer.Interfaces;
using Core.Services.Interfaces;
using System.Linq;
using Bayes.Data;

namespace Core.ProducerConsumer.Implementation
{
    public class TweetTweetProducerConsumer : ITweetProducerConsumer
    {
        private readonly ILearningService _learningService;

        public TweetTweetProducerConsumer(ILearningService learningService)
        {
            _learningService = learningService;
        }

        public async Task ProduceAsync(ITargetBlock<Tweet> target, IEnumerable<Tweet> values)
        {
            var personList = values.ToList();
            var size = (int) Math.Ceiling(personList.Count / (double) Environment.ProcessorCount);
            var producers =
                Enumerable.Range(0, Environment.ProcessorCount)
                    .Select(x => ProduceAsyncPart(target, personList.Skip(x * size).Take(size)))
                    .ToArray();
            await Task.WhenAll(producers);
            target.Complete();
        }

        private async Task ProduceAsyncPart(ITargetBlock<Tweet> target, IEnumerable<Tweet> values)
        {
            foreach (var tweet in values)
            {
                var classifier = new TweetClassifier(_learningService.Get());
                var score = classifier.Classify(tweet.Text);
                await target.SendAsync(tweet.WithNewSentiment(score.Sentence.Category));
            }
        }

        public Tweet ConsumeAsync(Tweet source)
        {
            _learningService.LearnOne(new Sentence(source.Text, source.Sentiment));
            return source;
        }
    }
}