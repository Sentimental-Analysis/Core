using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class BayesAnalysisService : ISentimentalAnalysisService
    {
        public Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets)
        {
            throw new System.NotImplementedException();
        }

        public Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets)
        {
            throw new System.NotImplementedException();
        }
    }
}