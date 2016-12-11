using System.Collections.Generic;
using Core.Models;

namespace Core.ProducerConsumer.Interfaces
{
    public interface ITweetProducerConsumerService : ITweetProducerConsumer
    {
        Tweet Manage(IEnumerable<Tweet> values);
    }
}