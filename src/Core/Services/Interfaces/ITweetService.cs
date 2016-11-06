using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface ITweetService : IService
    {
        Result<IEnumerable<Tweet>> GetTweetByKey(string key);
        Task<Result<IEnumerable<Tweet>>> GetTweetByKeyAsync(string key);
    }
}