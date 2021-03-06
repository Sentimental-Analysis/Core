﻿using System;
using Cassandra;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Implementations;
using Core.Repositories.Interfaces;
using Core.UnitOfWork.Interfaces;
using static Core.Builders.CassandraConnectionBuilder;

namespace Core.UnitOfWork.Implementations
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        private readonly ICassandraConnection _connection;
        private bool _shouldBeDisposed = true;

        public DefaultUnitOfWork(Cluster cluster, TwitterApiCredentials credentials)
        {
            _connection = Connection(cluster).Build();
            Tweets = new DatabaseTweetRepository(_connection);
            ApiTweets = new ApiTweetRepository(credentials);
        }

        public ITweetRepository Tweets { get; }
        public ITweetApiRepository ApiTweets { get; }

        public void Dispose()
        {
            if (_shouldBeDisposed)
            {
                _shouldBeDisposed = false;
                _connection?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}