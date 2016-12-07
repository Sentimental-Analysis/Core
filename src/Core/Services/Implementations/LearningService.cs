using System;
using System.Collections.Generic;
using Bayes.Data;
using Bayes.Learner.Interfaces;
using Core.Cache.Interfaces;
using Core.Services.Interfaces;
using System.Linq;

namespace Core.Services.Implementations
{
    public class LearningService : ILearningService
    {
        private readonly ICacheService _cacheService;
        private readonly string _learnStateCacheKey = $"{nameof(LearningService)}-learnstate";
        private readonly IEnumerable<Sentence> _sentences;
        private readonly ITweetLearner _learner;

        public LearningService(ICacheService cacheService, ITweetLearner learner, IEnumerable<Sentence> initSentences)
        {
            _cacheService = cacheService;
            _sentences = initSentences;
            _learner = learner;
        }

        public LearningService(ICacheService cacheService, ITweetLearner learner, params Sentence[] initSentences)
        {
            _cacheService = cacheService;
            _sentences = initSentences;
            _learner = learner;
        }


        public LearnerState Get()
        {
            return _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return _sentences.Aggregate(LearnerState.Empty, (state, sentence) => _learner.Learn(state, sentence));
            }, TimeSpan.FromDays(1));
        }

        public LearnerState Learn(IEnumerable<Sentence> sentences)
        {
            var oldState = Get();
            _cacheService.Clear(_learnStateCacheKey);
            return _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return sentences.Aggregate(oldState, (state, sentence) => _learner.Learn(state, sentence));
            }, TimeSpan.FromDays(1));
        }
    }
}