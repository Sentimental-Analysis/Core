using System.Collections.Generic;
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

        public short Predict(string word)
        {
            return 0;
        }
    }
}