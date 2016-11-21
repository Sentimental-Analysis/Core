namespace Core.Bayes.Data
{
    public struct Probability
    {
        public double NegativeProbability { get; }
        public double PositiveProbability { get; }

        public Probability(double negativeProbability, double positiveProbability)
        {
            NegativeProbability = negativeProbability;
            PositiveProbability = positiveProbability;
        }
    }
}