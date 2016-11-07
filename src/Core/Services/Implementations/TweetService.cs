using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;

namespace Core.Services.Implementations
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly ITweetApiRepository _apiTweetRepository;
        private readonly ISentimentalAnalysisService _sentimentalAnalysisService;

        private bool _shouldBeDisposed = true;

        public TweetService(ITweetRepository tweetRepository, ITweetApiRepository apiTweetRepository, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _tweetRepository = tweetRepository;
            _apiTweetRepository = apiTweetRepository;
            _sentimentalAnalysisService = sentimentalAnalysisService;
        }


        public Result<IEnumerable<Tweet>> GetTweetByKey(string key)
        {
            var dbResult = _tweetRepository.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (dbResult.Any()) return Result<IEnumerable<Tweet>>.Wrap(dbResult.AsEnumerable());

            var apiResult = _apiTweetRepository.Get(new TweetQuery(key));
            var analyzedTweets = _sentimentalAnalysisService.Analyze(apiResult);

            if (!analyzedTweets.IsSuccess) return Result<IEnumerable<Tweet>>.Error();

            _tweetRepository.AddRange(analyzedTweets.Value);
            _tweetRepository.Complete();
            return analyzedTweets;
        }

        public async Task<Result<IEnumerable<Tweet>>> GetTweetByKeyAsync(string key)
        {
            var dbResult = _unitOfWork.Tweets.FindByKey(new TweetQuery(key, 100000)).ToList();

            if (!dbResult.Any())
            {
                var apiResult = _unitOfWork.ApiTweets.FindByKey(new TweetQuery(key));
                var analyzedTweets = await _unitOfWork.SentimentalAnalysis.AnalyzeAsync(apiResult);
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