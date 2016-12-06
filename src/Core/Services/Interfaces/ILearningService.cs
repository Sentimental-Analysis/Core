using Bayes.Data;

namespace Core.Services.Interfaces
{
    public interface ILearningService
    {
        LearnerState GetOrStore(LearnerState state);
        LearnerState Store(LearnerState state);
        LearnerState Update(LearnerState state);
    }
}