using System;
using System.Collections.Generic;
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