using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Builders;
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

            if (dbResult.Count > 0)
            {
                return Result<AnalysisScore>.Wrap(AnalysisScoreBuilder.AnalysisScore(dbResult, key).Build());
            }

            var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
            var analyzedTweets = _sentimentalAnalysisService.Analyze(apiResult);

            if (!analyzedTweets.IsSuccess)
            {
                return Result<AnalysisScore>.Error();
            }

            _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
            return Result<AnalysisScore>.Wrap(AnalysisScoreBuilder.AnalysisScore(analyzedTweets.Value, key).Build());
        }

        public async Task<Result<AnalysisScore>> GetTweetScoreByKeyAsync(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (dbResult.Count > 0)
            {
                return Result<IEnumerable<Tweet>>.Wrap(AnalysisScoreBuilder.AnalysisScore(dbResult, key).Build());
            }

            var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
            var analyzedTweets = await _sentimentalAnalysisService.AnalyzeAsync(apiResult);
            if (analyzedTweets.IsSuccess)
            {
                _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
                return Result<AnalysisScore>.Wrap(AnalysisScoreBuilder.AnalysisScore(analyzedTweets.Value, key).Build());
            }
            return Result<AnalysisScore>.Error();
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