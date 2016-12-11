using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.ProducerConsumer.Interfaces
{
    public interface ITweetProducerConsumerService
    {
        IEnumerable<Tweet> Manage(IEnumerable<Tweet> values);
        Task<IEnumerable<Tweet>> ManageAsync(IEnumerable<Tweet> values);
    }
}