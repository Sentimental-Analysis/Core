using Core.Bayes.Data;

namespace Core.Bayes.Interfaces
{
    public interface IPredicator
    {
        Score Predict(string word);
    }
}