using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Cassandra.Data.Linq;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Utils;
using Tweetinvi.Core.Extensions;

namespace Core.Repositories.Implementations
{
    public class DatabaseTweetRepository : ITweetRepository
    {
        private readonly ICassandraConnection _connection;
        private bool _shouldBeDisposed = true;

        public DatabaseTweetRepository(ICassandraConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<Tweet> FindByKey(TweetQuery query)
        {
            return _connection.Mapper.Fetch<Tweet>().Where(tweet => tweet.Key == query.Key).Take(query.MaxQuantity);
        }

        public OperationStatus AddRange(IEnumerable<Tweet> entities)
        {
            try
            {
                var table = _connection.Session.GetTable<Tweet>();             
                entities.ToList().Windowed(100).ForEach(tweets =>
                {
                    var batch = _connection.Session.CreateBatch();
                    foreach (var tweet in tweets)
                    {
                        batch.Append(table.Insert(tweet));
                    }
                    batch.Execute();
                });
                return OperationStatus.Succeed;
            }
            catch (Exception)
            {
                return OperationStatus.Error;
            }
        }

        public IEnumerable<string> GetUniqueKeys()
        {
            return _connection.Mapper.Fetch<Tweet>().Select(x => x.Key).Distinct();
        }

        public IDictionary<string, long> QuantityByKey(string key)
        {
            var all = _connection.Mapper.Fetch<Tweet>();
            return all.Aggregate(ImmutableDictionary<string, long>.Empty, (acc, tweet) =>
            {
                long quantity;
                if (acc.TryGetValue(tweet.Key, out quantity))
                {
                    return acc.SetItem(tweet.Key, quantity + 1);
                }
                return acc.Add(tweet.Key, 1);
            });
        }

        public void Dispose()
        {
            if (_shouldBeDisposed)
            {
                _shouldBeDisposed = false;
                _connection.Dispose();
            }
        }
    }
}