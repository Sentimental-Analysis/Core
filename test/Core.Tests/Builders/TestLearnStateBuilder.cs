using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Learner.Interfaces;

namespace Core.Tests.Builders
{
    public class TestLearnStateBuilder : IBuilder<LearnerState>
    {
        private LearnerState _state;
        private readonly ITweetLearner _learner;

        public static TestLearnStateBuilder TestLearnState() => new TestLearnStateBuilder();

        public TestLearnStateBuilder()
        {
            _state = LearnerState.Empty;
            _learner = new TweetLearner();
        }

        public TestLearnStateBuilder WithSentence(Sentence sentence)
        {
            _state = _learner.Learn(_state, sentence);
            return this;
        }

        public TestLearnStateBuilder WithSentence(params Sentence[] sentences)
        {
            _state = sentences.Aggregate(_state, (state, sentence) => _learner.Learn(_state, sentence));
            return this;
        }

        public TestLearnStateBuilder WithSentence(ImmutableDictionary<string, int> sentences)
        {
            _state = sentences.Aggregate(_state, (acc, x) => _learner.Learn(acc, new Sentence(x.Key, x.Value >= 0 ? WordCategory.Positive : WordCategory.Negative)));
            return this;
        }

        public LearnerState Build()
        {
            return _state;
        }
    }
}