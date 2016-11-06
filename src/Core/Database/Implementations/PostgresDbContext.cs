using Core.Database.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Implementations
{
    public class PostgresDbContext : DbContext, IDbContext
    {
        public DbSet<Tweet> Tweets { get; set; }
    }
}