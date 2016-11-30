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


        public Result<IEnumerable<Tweet>> GetTweetByKey(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (dbResult.Any()) return Result<IEnumerable<Tweet>>.Wrap(dbResult.AsEnumerable());

            var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
            var analyzedTweets = _sentimentalAnalysisService.Analyze(apiResult);

            if (!analyzedTweets.IsSuccess) return Result<IEnumerable<Tweet>>.Error();

            _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
            return analyzedTweets;
        }

        public async Task<Result<IEnumerable<Tweet>>> GetTweetByKeyAsync(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (!dbResult.Any())
            {
                var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(key));
                var analyzedTweets = await _sentimentalAnalysisService.AnalyzeAsync(apiResult);
                if (analyzedTweets.IsSuccess)
                {
                    _unitOfWork.Tweets.AddRange(analyzedTweets.Value);
                    return analyzedTweets;
                }
                return Result<IEnumerable<Tweet>>.Error();
            }
            return Result<IEnumerable<Tweet>>.Wrap(dbResult.AsEnumerable());
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