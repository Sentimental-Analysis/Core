using System.Collections.Generic;
using Bayes.Data;

namespace Core.Models
{
    public class AnalysisScore
    {
        public WordCategory Sentiment { get; set; }
        public int PositiveTweetsQuantity { get; set; }
        public int NegativeTweetsQuantity { get; set; }
        public IEnumerable<string> KeyWords { get; set; }
    }
}