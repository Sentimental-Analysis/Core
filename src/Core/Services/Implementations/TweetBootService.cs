using System;
using System.Linq;
using Bayes.Utils;
using Core.Models;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;

namespace Core.Services.Implementations
{
    public class TweetBootService : ITweetBotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISentimentalAnalysisService _sentimentalAnalysisService;
        private bool _shouldBeDisposed = true;

        public TweetBootService(IUnitOfWork unitOfWork, ISentimentalAnalysisService sentimentalAnalysisService)
        {
            _unitOfWork = unitOfWork;
            _sentimentalAnalysisService = sentimentalAnalysisService;
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

        public OperationStatus StoreNew()
        {
            var keys = _unitOfWork.Tweets.GetUniqueKeys();
            var keyByLastTweetDate = keys.Select(key => Tuple.Create(key, _unitOfWork.Tweets.FindByKey(new TweetQuery(key)).Select(x => x.Date).Max()));
            var tweetsToAddition = keyByLastTweetDate.Select(x =>
            {
                var apiResult = _unitOfWork.ApiTweets.Get(new TweetQuery(x.Item1, x.Item2, 10000));
                var analyzeResult = _sentimentalAnalysisService.Analyze(apiResult);
                if (analyzeResult.IsSuccess)
                {
                    return _unitOfWork.Tweets.AddRange(analyzeResult.Value);
                }
                return OperationStatus.Error;
            }).ToList();

            return tweetsToAddition.Any(x => x == OperationStatus.Error)
                ? OperationStatus.Error
                : OperationStatus.Succeed;
        }

    }
}