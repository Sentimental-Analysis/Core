using System.Collections.Generic;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Repositories.Interfaces
{
    public interface ITweetApiRepository : IApiRepository<TweetQuery, IEnumerable<Tweet>>
    {

    }
}