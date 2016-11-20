using Core.Bayes.Data;
using Core.Models;

namespace Core.Bayes.Interfaces
{
    public interface IBayesClassifier
    {
        Analysis Classify(Tweet tweet);
    }
}