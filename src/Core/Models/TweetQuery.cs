namespace Core.Models
{
    public sealed class TweetQuery
    {
        public string Key { get; }
        public int MaxQuantity { get; }

        public TweetQuery(string key, int maxQuantity = 1000)
        {
            Key = key;
            MaxQuantity = maxQuantity;
        }
    }
}