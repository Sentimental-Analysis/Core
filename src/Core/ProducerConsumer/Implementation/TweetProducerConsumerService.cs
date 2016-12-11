using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Models;
using Core.ProducerConsumer.Interfaces;

namespace Core.ProducerConsumer.Implementation
{
    public class TweetProducerConsumerService : ITweetProducerConsumerService
    {
        private readonly ITweetProducerConsumer _producerConsumer;

        public TweetProducerConsumerService(ITweetProducerConsumer producerConsumer)
        {
            _producerConsumer = producerConsumer;
        }


        public async Task<IEnumerable<Tweet>> ManageAsync(IEnumerable<Tweet> values)
        {
            var result = new List<Tweet>();
            var buffer =
                new BufferBlock<Tweet>(new DataflowBlockOptions() {BoundedCapacity = Environment.ProcessorCount * 5});
            var consumerOptions = new ExecutionDataflowBlockOptions {BoundedCapacity = 1,};
            var consumers =
                Enumerable.Range(0, Environment.ProcessorCount)
                    .Select(x => new ActionBlock<Tweet>(y => { result.Add(_producerConsumer.Consume(y)); }, consumerOptions))
                    .ToList();
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true, };
            consumers.ForEach(x => { buffer.LinkTo(x, linkOptions); });
            var producer = _producerConsumer.ProduceAsync(buffer, values);
            await Task.WhenAll(producer, buffer.Completion);
            await Task.WhenAll(consumers.Select(x => x.Completion).ToArray());
            return result;
        }

        public IEnumerable<Tweet> Manage(IEnumerable<Tweet> values)
        {
            var result = new List<Tweet>();
            var buffer =
                new BufferBlock<Tweet>(new DataflowBlockOptions() { BoundedCapacity = Environment.ProcessorCount * 5 });
            var consumerOptions = new ExecutionDataflowBlockOptions { BoundedCapacity = 1, };
            var consumers =
                Enumerable.Range(0, Environment.ProcessorCount)
                    .Select(x => new ActionBlock<Tweet>(y => { result.Add(_producerConsumer.Consume(y)); }, consumerOptions))
                    .ToList();
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true, };
            consumers.ForEach(x => { buffer.LinkTo(x, linkOptions); });
            var producer = _producerConsumer.ProduceAsync(buffer, values);
            Task.WaitAll(producer, buffer.Completion);
            Task.WaitAll(consumers.Select(x => x.Completion).ToArray());
            return result;
        }
    }
}