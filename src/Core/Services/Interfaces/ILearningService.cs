using System.Collections.Generic;
using Bayes.Data;

namespace Core.Services.Interfaces
{
    public interface ILearningService
    {
        LearnerState Get();
        LearnerState Learn(IEnumerable<Sentence> sentences);
    }
}