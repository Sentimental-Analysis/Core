using System;
using System.Collections.Immutable;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Learner.Interfaces;

namespace Core.Tests.Builders
{
    public class ClassifierBuilder
    {
        private IClassifier<Score, string> _classifier;
        private ILearner<Sentence, LearnerState> _learner;
        public ClassifierBuilder WithLearnData(ImmutableDictionary<string, int> words)
        {

        }


    }
}