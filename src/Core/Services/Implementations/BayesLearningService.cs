using System;
using System.Collections.Generic;
using Bayes.Data;
using Bayes.Learner.Interfaces;
using Core.Cache.Interfaces;
using Core.Services.Interfaces;
using System.Linq;

namespace Core.Services.Implementations
{
    public class BayesLearningService : ILearningService
    {
        private readonly ICacheService _cacheService;
        private readonly string _learnStateCacheKey = $"{nameof(BayesLearningService)}-learnstate";
        private readonly Lazy<IEnumerable<Sentence>> _sentences;
        private readonly ITweetLearner _learner;

        public BayesLearningService(ICacheService cacheService, ITweetLearner learner,
            Lazy<IEnumerable<Sentence>> initSentences)
        {
            _cacheService = cacheService;
            _sentences = initSentences;
            _learner = learner;
        }

        public LearnerState Get()
        {
            var serializableState =  _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return _sentences.Value.Aggregate(LearnerState.Empty, (state, sentence) => _learner.Learn(state, sentence)).Builder;
            }, TimeSpan.FromDays(1));

            return serializableState.Build();
        }

        public LearnerState Learn(IEnumerable<Sentence> sentences)
        {
            var oldState = Get();
            _cacheService.Clear(_learnStateCacheKey);
            var learnerStateBuilder =  _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return sentences.Aggregate(oldState, (state, sentence) => _learner.Learn(state, sentence)).Builder;
            }, TimeSpan.FromDays(1));
            return learnerStateBuilder.Build();
        }

        public LearnerState LearnOne(Sentence sentence)
        {
            var oldState = Get();
            _cacheService.Clear(_learnStateCacheKey);
            var builder = _cacheService.GetOrStore(_learnStateCacheKey, () => _learner.Learn(oldState, sentence).Builder, TimeSpan.FromDays(1));
            return builder.Build();
        }
    }
}