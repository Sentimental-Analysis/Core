using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Models;
using Core.ProducerConsumer.Interfaces;

namespace Core.ProducerConsumer.Implementation
{
    public class TweetProducerConsumer : IProducerConsumer<Tweet>
    {
        public Task ProduceAsync(ITargetBlock<Tweet> target)
        {
            throw new System.NotImplementedException();
        }

        public Task<TConsume> ConsumeAsync<TConsume>(ISourceBlock<Tweet> source)
        {
            throw new System.NotImplementedException();
        }
    }
}