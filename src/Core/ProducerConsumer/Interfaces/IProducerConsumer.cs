using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Core.ProducerConsumer.Interfaces
{
    public interface IProducerConsumer<TProduce, TConsume>
    {
        Task ProduceAsync(ITargetBlock<TProduce> target, IEnumerable<TConsume> values);
        TConsume ConsumeAsync(TProduce source);
    }
}