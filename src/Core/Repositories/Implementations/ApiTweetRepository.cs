using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Core.Repositories.Implementations
{
    public class ApiTweetRepository : ITweetRepository
    {
        public IEnumerable<Tweet> Find(Expression<Func<Tweet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tweet>> FindAsync(Expression<Func<Tweet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tweet> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tweet>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public OperationStatus Add(Tweet entity)
        {
            throw new NotImplementedException();
        }

        public OperationStatus AddRange(IEnumerable<Tweet> entities)
        {
            throw new NotImplementedException();
        }

        public OperationStatus Remove(Tweet entity)
        {
            throw new NotImplementedException();
        }

        public OperationStatus RemoveRange(IEnumerable<Tweet> entities)
        {
            throw new NotImplementedException();
        }
    }
}