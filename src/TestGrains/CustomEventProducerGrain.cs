using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;

namespace TestGrains
{
    public class CustomEventProducerGrain<T> : Grain, ICustomEventProducerGrain<T>
    {
        private IAsyncStream<T> _producer;
        private int _numProducedItems;
        private IDisposable _producerTimer;
        private T _lastProducedItem;
        internal Logger Logger;

        public override Task OnActivateAsync()
        {
            Logger = GetLogger("CustomEventProducerGrain " + IdentityString);
            Logger.Info("OnActivateAsync");
            _numProducedItems = 0;
            return TaskDone.Done;
        }

        public Task BecomeProducer(Guid streamId, string streamNamespace, string providerToUse)
        {
            Logger.Info("BecomeProducer");
            IStreamProvider streamProvider = GetStreamProvider(providerToUse);
            _producer = streamProvider.GetStream<T>(streamId, streamNamespace);
            return TaskDone.Done;
        }

        public Task StartPeriodicBatchProducing(int batchSize, T valueToProduce)
        {
            Logger.Info("StartPeriodicProducing");
            Tuple<int, T> batchValueTuple = new Tuple<int, T>(batchSize, valueToProduce);
            _producerTimer = RegisterTimer(TimerCallback, batchValueTuple, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));
            return TaskDone.Done;
        }

        private Task TimerCallback(object state)
        {
            return _producerTimer != null ? Fire((Tuple<int,T>)state) : TaskDone.Done;
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

        public Task Produce(int batchSize, T valueToProduce)
        {
            return Fire(new Tuple<int, T>(batchSize, valueToProduce));
        }

        private Task Fire(Tuple<int, T> batchValueTuple, [CallerMemberName] string caller = null)
        {
            var batchSize = batchValueTuple.Item1;
            var item = batchValueTuple.Item2;

            // Creating a batch
            List<T> batch = new List<T>(batchSize);

            for (int i = 0; i < batchSize; i++)
            {
                batch.Add(item);
                _numProducedItems++;
            }

            Logger.Info("{0} (item={1}, serialNum={2})", caller, item.ToString(),_numProducedItems);

            _lastProducedItem = item;
            return _producer.OnNextBatchAsync(batch);
        }


        public Task<T> GetLastProducedItem()
        {
            return Task.FromResult(_lastProducedItem);
        }
    }
}
