using System.Collections.Generic;
using Core.Models;

namespace Core.Repositories.Interfaces
{
    public interface ITweetRepository : IRepository<Tweet>
    {
        IEnumerable<Tweet> FindByKey(string query);
    }
}