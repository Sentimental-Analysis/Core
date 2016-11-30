namespace Core.Models.Mappings
{
    public class TweetMapping : Cassandra.Mapping.Mappings
    {
        public TweetMapping()
        {
            For<Tweet>().TableName("tweets")
                .PartitionKey(x => x.Id)
                .Column(x => x.Key)
                .Column(x => x.Date)
                .Column(x => x.Language)
                .Column(x => x.Latitude)
                .Column(x => x.Longitude)
                .Column(x => x.Sentiment, cm => cm.WithDbType<int>())
                .Column(x => x.Text)
                .Column(x => x.TweetIdentifier);
        }
    }
}