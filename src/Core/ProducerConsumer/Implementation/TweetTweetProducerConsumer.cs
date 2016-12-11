using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Bayes.Classifiers.Implementations;
using Core.Models;
using Core.ProducerConsumer.Interfaces;
using Core.Services.Interfaces;
using System.Linq;
using Bayes.Data;
using Core.Repositories.Interfaces;

namespace Core.ProducerConsumer.Implementation
{
    public class TweetTweetProducerConsumer : ITweetProducerConsumer
    {
        private readonly ILearningService _learningService;

        public TweetTweetProducerConsumer(ITweetApiRepository _apiRepository, ILearningService learningService)
        {
            _learningService = learningService;
        }

        public async Task ProduceAsync(ITargetBlock<Tweet> target, string key)
        {
            var apiResult = _learningService.,
        }

        public Tweet Consume(Tweet source)
        {
            var classifier = new TweetClassifier(_learningService.Get());
            var score = classifier.Classify(source.Text);
            return source.WithNewSentiment(score.Sentence.Category);
        }
    }
}