using System.Collections.Immutable;
using Bayes.Classifiers.Implementations;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using System.Linq;
using Bayes.Learner.Implementations;
using Bayes.Utils;
using LanguageExt.UnitsOfMeasure;

namespace Core.Tests.Builders
{
    public class ClassifierBuilder : IBuilder<IClassifier<Score, string>>
    {
        private LearnerState _learnerState;

        public static ClassifierBuilder Classifier() => new ClassifierBuilder();

        public ClassifierBuilder()
        {
            _learnerState = LearnerState.Empty;
        }

        public ClassifierBuilder WithLearnData(ImmutableDictionary<string, int> words)
        {
            var learner = new TweetLearner();
            var list = words.ToList();
            _learnerState = list.Select(x => new Sentence(x.Key, x.Value >= 0 ? WordCategory.Positive : WordCategory.Negative)).Aggregate(_learnerState, (state, sentence) => learner.Learn(state, sentence));
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