using System.Collections.Generic;
using Core.Models;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;

namespace Core.Services.Implementations
{
    public class TweetService: ITweetService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TweetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Tweet> GetTweetByKey(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}