using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Abstractions;
using Core.Repositories.Interfaces;

namespace Core.Repositories.Implementations
{
    public class DatabaseTweetRepository : Repository<Tweet>, ITweetRepository
    {
        public DatabaseTweetRepository(IDbContext dbManager) : base(dbManager)
        {
        }
    }
}