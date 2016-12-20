using System.Collections.Generic;

namespace Core.Models
{
    public class AnalysisScore
    {
        public GeneralSentiment Sentiment { get; set; }
        public int PositiveTweetsQuantity { get; set; }
        public int NegativeTweetsQuantity { get; set; }
        public IEnumerable<KeyWord> KeyWords { get; set; }
        public string Key { get; set; }
    }
}