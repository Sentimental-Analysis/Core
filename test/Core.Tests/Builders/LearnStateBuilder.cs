using System.Linq;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Learner.Interfaces;

namespace Core.Tests.Builders
{
    public class LearnStateBuilder : IBuilder<LearnerState>
    {
        private LearnerState _state;
        private readonly ITweetLearner _learner;

        public LearnStateBuilder LearnState => new LearnStateBuilder();

        public LearnStateBuilder()
        {
            _state = LearnerState.Empty;
            _learner = new TweetLearner();
        }

        public LearnStateBuilder WithSentence(Sentence sentence)
        {
            _state = _learner.Learn(_state, sentence);
            return this;
        }

        public LearnStateBuilder WithSentence(params Sentence[] sentences)
        {
            _state = sentences.Aggregate(_state, (state, sentence) => _learner.Learn(_state, sentence));
            return this;
        }

        public LearnerState Build()
        {
            return _state;
        }
    }
}