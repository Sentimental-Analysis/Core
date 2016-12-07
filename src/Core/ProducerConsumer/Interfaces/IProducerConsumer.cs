using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Core.ProducerConsumer.Interfaces
{
    public interface IProducerConsumer<TProduce, TConsume>
    {
        Task ProduceAsync(ITargetBlock<TProduce> target);
        Task<TConsume> ConsumeAsync(ISourceBlock<TProduce> source);
    }
}