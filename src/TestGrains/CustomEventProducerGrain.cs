using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;
using TestGrains.Models;

namespace TestGrains
{
    public class CustomEventProducerGrain<T> : Grain, ICustomEventProducerGrain<T>
    {
        private IAsyncStream<T> _producer;
        private int _numProducedItems;
        private IDisposable _producerTimer;
        private IDisposable _valuesProducerTimer;
        private T _lastProducedItem;
        internal Logger Logger;
        private Stopwatch _watch;
        private TimeSpan _totalProducedTime;

        public override Task OnActivateAsync()
        {
            Logger = GetLogger("CustomEventProducerGrain " + IdentityString);
            Logger.Info("OnActivateAsync");
            _numProducedItems = 0;
            _totalProducedTime = TimeSpan.Zero;
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

        private Task ValuesTimerCallback(object state)
        {
            return _valuesProducerTimer != null ? Fire((IEnumerable<T>)state) : TaskDone.Done;
        }

        public Task StopPeriodicBatchProducing()
        {
            Logger.Info("StopPeriodicProducing");
            if (_producerTimer != null)
            {
                _producerTimer.Dispose();
                _producerTimer = null;
            }
            if (_valuesProducerTimer != null)
            {
                _valuesProducerTimer.Dispose();
                _valuesProducerTimer = null;
            }
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

        private async Task Fire(Tuple<int, T> batchValueTuple, [CallerMemberName] string caller = null)
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

            Logger.Verbose("{0} (item={1}, serialNum={2})", caller, item.ToString(),_numProducedItems);

            _lastProducedItem = item;
            _watch = Stopwatch.StartNew();            
            await _producer.OnNextBatchAsync(batch);
            _watch.Stop();

            _totalProducedTime = _totalProducedTime.Add(_watch.Elapsed);
        }

        private async Task Fire(IEnumerable<T> values, [CallerMemberName] string caller = null)
        {
            var valuesList = values.ToList();

            foreach (var value in valuesList)
            {
                _numProducedItems++;
                Logger.Verbose("{0} (item={1}, serialNum={2})", caller, value.ToString(), _numProducedItems);                
            }

            _lastProducedItem = valuesList.Last();

            if (valuesList.All(v => v is LoadTestMessage))
            {
                valuesList.ForEach(v => (v as LoadTestMessage).SentTime = DateTime.Now);
            }

            _watch = Stopwatch.StartNew();
            await _producer.OnNextBatchAsync(valuesList);
            _watch.Stop();
            _totalProducedTime = _totalProducedTime.Add(_watch.Elapsed);
        }

        public Task<T> GetLastProducedItem()
        {
            return Task.FromResult(_lastProducedItem);
        }


        public Task Produce(IEnumerable<T> values)
        {
            return Fire(values);
        }

        public Task StartPeriodicBatchProducing(IEnumerable<T> values)
        {
            Logger.Info("StartPeriodicProducing");
            _valuesProducerTimer = RegisterTimer(ValuesTimerCallback, values, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));
            return TaskDone.Done;
        }

        public Task<TimeSpan> GetTotalProducedTime()
        {
            return Task.FromResult(_totalProducedTime);
        }
    }
}
