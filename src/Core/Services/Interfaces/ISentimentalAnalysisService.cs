using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;


namespace Core.Services.Interfaces
{
    public interface ISentimentalAnalysisService
    {
        Task<Result<IEnumerable<Tweet>>> AnalyzeAsync(IEnumerable<Tweet> tweets);
        Result<IEnumerable<Tweet>> Analyze(IEnumerable<Tweet> tweets);
    }
}