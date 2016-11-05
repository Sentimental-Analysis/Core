using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Abstractions;
using Core.Repositories.Interfaces;

namespace Core.Repositories.Implementations
{
    public class TweetRepository : Repository<Tweet>, ITweetRepository
    {
        public TweetRepository(IDbContext dbManager) : base(dbManager)
        {
        }
    }
}