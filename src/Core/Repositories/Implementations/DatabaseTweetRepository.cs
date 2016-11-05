using System.Collections.Generic;
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
        public DatabaseTweetRepository(IDbContext dbManager) : base(dbManager)
        {
        }

        public IEnumerable<Tweet> FindByKey(string query, int count)
        {
            return DbManager.Tweets.Where(tweet => tweet.Key == query).Take(count).AsNoTracking().ToList();
        }
    }
}