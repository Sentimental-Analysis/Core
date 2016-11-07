﻿using System.Collections.Generic;
using System.Linq;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Abstractions;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Implementations
{
    public class DatabaseTweetRepository : Repository<Tweet>, ITweetRepository
    {
        public DatabaseTweetRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Tweet> FindByKey(TweetQuery query)
        {
            return DbContext.Tweets.Where(tweet => tweet.Key == query.Key).Take(query.MaxQuantity).AsNoTracking().ToList();
        }

        public override void Dispose()
        {

        }
    }
}