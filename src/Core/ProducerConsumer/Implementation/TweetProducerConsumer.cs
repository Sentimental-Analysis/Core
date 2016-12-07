using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Models;
using Core.ProducerConsumer.Interfaces;

namespace Core.ProducerConsumer.Implementation
{
    public class TweetProducerConsumer : IProducerConsumer<Tweet, IEnumerable<Tweet>>
    {
        public Task ProduceAsync(ITargetBlock<Tweet> target)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tweet>> ConsumeAsync(ISourceBlock<Tweet> source)
        {
            throw new System.NotImplementedException();
        }
    }
}