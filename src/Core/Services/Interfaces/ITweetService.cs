using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Interfaces
{
    public interface ITweetService : IService
    {
        Result<AnalysisScore> GetTweetScoreByKey(string key);
        Task<Result<AnalysisScore>> GetTweetScoreByKeyAsync(string key);
        Result<IDictionary<string, long>> GetQuantityByTweetKey();
    }
}