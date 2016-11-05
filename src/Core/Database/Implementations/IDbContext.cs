using System;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Implementations
{
    public interface IDbContext: IDisposable
    {
        DbSet<Tweet> Tweets { get; set; }
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}