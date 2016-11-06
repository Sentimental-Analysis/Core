using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Core.ProducerConsumer.Interfaces
{
    public interface IProducerConsumer<T>
    {
        Task ProduceAsync(ITargetBlock<T> target);
        Task<TConsume> ConsumeAsync<TConsume>(ISourceBlock<T> source);
    }
}