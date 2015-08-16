using Orleans;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;

namespace TestGrains
{
    public class BatchProducerGrain : Grain ,IBatchProducerGrain
    {
        private IAsyncStream<int> _producer;
        private int _numProducedItems;
        private IDisposable _producerTimer;
        internal Logger Logger;

        public override Task OnActivateAsync()
        {
            Logger = GetLogger("BatchProducerGrain " + IdentityString);
            Logger.Info("OnActivateAsync");
            _numProducedItems = 0;
            return TaskDone.Done;            
        }

        public Task BecomeProducer(Guid streamId, string streamNamespace, string providerToUse)
        {
            Logger.Info("BecomeProducer");
            IStreamProvider streamProvider = GetStreamProvider(providerToUse);
            _producer = streamProvider.GetStream<int>(streamId, streamNamespace);
            return TaskDone.Done;
        }

        public Task StartPeriodicBatchProducing(int batchSize)
        {
            Logger.Info("StartPeriodicProducing");
            _producerTimer = RegisterTimer(TimerCallback, batchSize, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));
            return TaskDone.Done;
        }
        private Task TimerCallback(object state)
        {
            return _producerTimer != null ? Fire((int)state) : TaskDone.Done;
        }

        public Task StopPeriodicBatchProducing()
        {
            Logger.Info("StopPeriodicProducing");
            _producerTimer.Dispose();
            _producerTimer = null;
            return TaskDone.Done;
        }

        public Task<int> GetNumberProduced()
        {
            Logger.Info("GetNumberProduced");
            return Task.FromResult(_numProducedItems);
        }

        public Task ClearNumberProduced()
        {
            _numProducedItems = 0;
            return TaskDone.Done;
        }

        public Task Produce(int batchSize)
        {
            return Fire(batchSize);
        }

        private Task Fire(int batchSize,[CallerMemberName] string caller = null)
        {
            // Creating a batch
            List<int> batch = new List<int>(batchSize);

            for (int i = 0; i < batchSize; i++)
            {
                batch.Add(_numProducedItems);
                _numProducedItems++;
            }

            Logger.Info("{0} (item={1})", caller, _numProducedItems);

            return _producer.OnNextBatchAsync(batch);
        }
    }
}
