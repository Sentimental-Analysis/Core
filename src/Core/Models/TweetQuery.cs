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

        private bool Equals(TweetQuery other)
        {
            return string.Equals(Key, other.Key) && MaxQuantity == other.MaxQuantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is TweetQuery && Equals((TweetQuery) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key?.GetHashCode() ?? 0) * 397) ^ MaxQuantity;
            }
        }
    }
}