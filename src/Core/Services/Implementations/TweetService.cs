using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;

namespace Core.Services.Implementations
{
    public class TweetService : ITweetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISentimentalAnalysisService _sentimentalAnalysisService;
        private bool _shouldBeDisposed = true;

        public TweetService(IUnitOfWork unitOfWork, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _unitOfWork = unitOfWork;
            _sentimentalAnalysisService = sentimentalAnalysisService;
        }


        public Result<AnalysisScore> GetTweetScoreByKey(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (dbResult.Any())
            {
                return Result<AnalysisScore>.Wrap(AnalysisScore.FromTweets(dbResult, key));
            }

            var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
            var analyzedTweets = _sentimentalAnalysisService.Analyze(apiResult);

            if (!analyzedTweets.IsSuccess)
            {
                return Result<AnalysisScore>.Error();
            }

            _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
            return Result<AnalysisScore>.Wrap(AnalysisScore.FromTweets(analyzedTweets.Value, key));
        }

        public async Task<Result<AnalysisScore>> GetTweetScoreByKeyAsync(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (!dbResult.Any())
            {
                var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
                var analyzedTweets = await _sentimentalAnalysisService.AnalyzeAsync(apiResult);
                if (analyzedTweets.IsSuccess)
                {
                    _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
                    return Result<AnalysisScore>.Wrap(AnalysisScore.FromTweets(analyzedTweets.Value, key));
                }
                return Result<AnalysisScore>.Error();
            }
            return Result<IEnumerable<Tweet>>.Wrap(AnalysisScore.FromTweets(dbResult, key));
        }

        public void Dispose()
        {
            if (_shouldBeDisposed)
            {
                _shouldBeDisposed = false;
                _unitOfWork?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}