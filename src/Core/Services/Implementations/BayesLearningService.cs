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
        private readonly IEnumerable<Sentence> _sentences;
        private readonly ITweetLearner _learner;

        public BayesLearningService(ICacheService cacheService, ITweetLearner learner,
            IEnumerable<Sentence> initSentences)
        {
            _cacheService = cacheService;
            _sentences = initSentences;
            _learner = learner;
        }

        public BayesLearningService(ICacheService cacheService, ITweetLearner learner, params Sentence[] initSentences)
        {
            _cacheService = cacheService;
            _sentences = initSentences;
            _learner = learner;
        }


        public LearnerState Get()
        {
            var serializableState =  _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return SerializableLearnerState.FromImmutableLearnerState(_sentences.Aggregate(LearnerState.Empty, (state, sentence) => _learner.Learn(state, sentence)));
            }, TimeSpan.FromDays(1));

            return SerializableLearnerState.ToImmutableLearnerState(serializableState);
        }

        public LearnerState Learn(IEnumerable<Sentence> sentences)
        {
            var oldState = Get();
            _cacheService.Clear(_learnStateCacheKey);
            var mutableState =  _cacheService.GetOrStore(_learnStateCacheKey, () =>
            {
                return SerializableLearnerState.FromImmutableLearnerState(sentences.Aggregate(oldState, (state, sentence) => _learner.Learn(state, sentence)));
            }, TimeSpan.FromDays(1));
            return SerializableLearnerState.ToImmutableLearnerState(mutableState);
        }

        public LearnerState LearnOne(Sentence sentence)
        {
            var oldState = Get();
            _cacheService.Clear(_learnStateCacheKey);
            var mutableState = _cacheService.GetOrStore(_learnStateCacheKey, () => SerializableLearnerState.FromImmutableLearnerState(_learner.Learn(oldState, sentence)), TimeSpan.FromDays(1));
            return SerializableLearnerState.ToImmutableLearnerState(mutableState);
        }
    }
}