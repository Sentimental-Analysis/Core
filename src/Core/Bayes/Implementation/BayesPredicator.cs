using System.Collections.Generic;
using Core.Bayes.Data;
using Core.Bayes.Interfaces;

namespace Core.Bayes.Implementation
{
    public class BayesPredicator : IPredicator
    {
        private readonly Dictionary<string, int> _words;
        public BayesPredicator(Dictionary<string, int> words)
        {
            _words = words;
        }

        public Score Predict(string word)
        {
            return new Score();
        }
    }
}