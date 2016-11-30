using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class AfinnAnalysisService : ISentimentalAnalysisService
    {
        private readonly ImmutableDictionary<string, int> _sentiments;

        public AfinnAnalysisService(ImmutableDictionary<string, int> sentiments)
        {
            _sentiments = sentiments;
        }

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