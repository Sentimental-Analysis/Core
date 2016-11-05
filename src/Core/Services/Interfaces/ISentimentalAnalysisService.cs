using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi.Logic;

namespace Core.Services.Interfaces
{
    public interface ISentimentalAnalysisService
    {
        Task<IEnumerable<Tweet>> AnalyzeAsync(IEnumerable<Tweet> tweets);
        IEnumerable<Tweet> Analyze(IEnumerable<Tweet> tweets);
    }
}