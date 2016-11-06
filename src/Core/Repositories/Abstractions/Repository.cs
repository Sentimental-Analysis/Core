using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Database.Interfaces;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Abstractions
{
    public abstract class Repository<TEntity>: IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IDbContext DbManager;

        protected Repository(IDbContext dbManager)
        {
            DbManager = dbManager;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbManager.Set<TEntity>().Where(predicate).AsNoTracking().ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbManager.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> FindAll()
        {
            return DbManager.Set<TEntity>().AsNoTracking().ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbManager.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public OperationStatus Add(TEntity entity)
        {
            var result = DbManager.Set<TEntity>().Add(entity);
            return result.IsKeySet ? OperationStatus.Succeed : OperationStatus.Error;
        }

        public OperationStatus AddRange(IEnumerable<TEntity> entities)
        {
            DbManager.Set<TEntity>().AddRange(entities);
            return OperationStatus.Succeed;
        }

        public OperationStatus Remove(TEntity entity)
        {
            var result = DbManager.Set<TEntity>().Remove(entity);
            return result.IsKeySet ? OperationStatus.Succeed : OperationStatus.Error;
        }

        public OperationStatus RemoveRange(IEnumerable<TEntity> entities)
        {
            DbManager.Set<TEntity>().RemoveRange(entities);
            return  OperationStatus.Succeed;
        }
    }
}