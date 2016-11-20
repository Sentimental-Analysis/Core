using System.Collections.Generic;

namespace Core.Bayes.Interfaces
{
    public interface IBayesClassifier
    {
        void Classify(HashSet<string> words);
    }
}