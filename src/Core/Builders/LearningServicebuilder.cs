﻿using System;
using System.Collections.Generic;
using System.Threading;
using Bayes.Data;
using Bayes.Learner.Interfaces;
using Core.Cache.Interfaces;
using Core.Services.Implementations;
using Core.Services.Interfaces;

namespace Core.Builders
{
    public class LearningServiceBuilder : IBuilder<ILearningService>
    {
        private Lazy<IEnumerable<Sentence>> _sentences;
        private ITweetLearner _tweetLearner;
        private ICacheService _cacheService;

        public static LearningServiceBuilder LearningService => new LearningServiceBuilder();

        public LearningServiceBuilder WithSentences(params Sentence[] sentences)
        {
            _sentences = new Lazy<IEnumerable<Sentence>>(() => sentences);
            return this;
        }

        public LearningServiceBuilder WithLazySentences(Lazy<IEnumerable<Sentence>> sentences)
        {
            _sentences = sentences;
            return this;
        }

        public LearningServiceBuilder WithLearner(ITweetLearner learner)
        {
            _tweetLearner = learner;
            return this;
        }

        public LearningServiceBuilder WithCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
            return this;
        }

        public ILearningService Build()
        {
            return new BayesLearningService(_cacheService, _tweetLearner, _sentences);
        }
    }
}