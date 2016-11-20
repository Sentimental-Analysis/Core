using Core.Bayes.Data;
using Core.Bayes.Interfaces;
using Core.Models;

namespace Core.Bayes.Implementation
{
    public class BayesClasifier : IBayesClassifier
    {
        public Analysis Classify(Tweet tweet)
        {
            var result = new Analysis();
        }
    }
}