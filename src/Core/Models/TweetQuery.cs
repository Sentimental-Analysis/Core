using System;

namespace Core.Models
{
    public sealed class TweetQuery
    {
        public string Key { get; }
        public int MaxQuantity { get; }
        public DateTime Since { get; }

        public TweetQuery(string key, int maxQuantity = 1000)
        {
            Key = key;
            Since = DateTime.MinValue;
            MaxQuantity = maxQuantity;
        }

        public TweetQuery(string key, DateTime since, int maxQuantity = 1000)
        {
            Key = key;
            Since = since;
            MaxQuantity = maxQuantity;
        }

        private bool Equals(TweetQuery other)
        {
            return string.Equals(Key, other.Key) && MaxQuantity == other.MaxQuantity && Since.Equals(other.Since);
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
                var hashCode = (Key != null ? Key.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ MaxQuantity;
                hashCode = (hashCode * 397) ^ Since.GetHashCode();
                return hashCode;
            }
        }
    }
}