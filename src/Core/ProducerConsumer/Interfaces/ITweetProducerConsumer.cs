using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Models;

namespace Core.ProducerConsumer.Interfaces
{
    public interface ITweetProducerConsumer
    {
        Task ProduceAsync(ITargetBlock<Tweet> target, IEnumerable<Tweet> values);
        Tweet Consume(Tweet source);
    }
}