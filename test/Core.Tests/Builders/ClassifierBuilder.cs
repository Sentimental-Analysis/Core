using System.Collections.Immutable;
using Bayes.Classifiers.Implementations;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Utils;

namespace Core.Tests.Builders
{
    public class ClassifierBuilder : IBuilder<IClassifier<Score, string>>
    {
        private LearnerState _learnerState;

        public ClassifierBuilder()
        {
            _learnerState = LearnerState.Empty;
        }

        public ClassifierBuilder WithLearnData(ImmutableDictionary<string, int> words)
        {
            _learnerState = Learning.FromDictionary(words);
            return this;
        }

        public ClassifierBuilder WithLearnData(Sentence sentence)
        {
            var learner = new TweetLearner();
            _learnerState = learner.Learn(_learnerState, sentence);
            return this;
        }

        public IClassifier<Score, string> Build()
        {
            return new TweetClassifier(_learnerState);
        }
    }
}