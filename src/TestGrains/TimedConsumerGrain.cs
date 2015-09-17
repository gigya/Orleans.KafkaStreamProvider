using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;
using UnitTests.Grains;

namespace TestGrains
{
    internal class TimedConsumerObserver<T> : IAsyncObserver<T>
    {
        private readonly TimedConsumerGrain _hostingGrain;

        internal TimedConsumerObserver(TimedConsumerGrain hostingGrain)
        {
            this._hostingGrain = hostingGrain;
        }

        public async Task OnNextAsync(T item, StreamSequenceToken token = null)
        {
            _hostingGrain.Logger.Info("OnNextAsync(item={0}, token={1})", item, token != null ? token.ToString() : "null");
            _hostingGrain.NumConsumedItems++;
            _hostingGrain.ReceivedTokens.Add(_hostingGrain.NumConsumedItems, token);
            await Task.Delay(_hostingGrain.TimeToConsume);
        }

        public Task OnCompletedAsync()
        {
            _hostingGrain.Logger.Info("OnCompletedAsync()");
            return TaskDone.Done;
        }

        public Task OnErrorAsync(Exception ex)
        {
            _hostingGrain.Logger.Info("OnErrorAsync({0})", ex);
            return TaskDone.Done;
        }
    }

    public class TimedConsumerGrain : Grain, ITimedConsumerGrain
    {
        private IAsyncObservable<int> _consumer;
        internal int NumConsumedItems;
        internal Logger Logger;
        private IAsyncObserver<int> _consumerObserver;
        private StreamSubscriptionHandle<int> _consumerHandle;
        internal TimeSpan TimeToConsume;
        internal Dictionary<int, StreamSequenceToken> ReceivedTokens;

        public override Task OnActivateAsync()
        {
            Logger = base.GetLogger("SampleStreaming_ConsumerGrain " + base.IdentityString);
            Logger.Info("OnActivateAsync");
            NumConsumedItems = 0;
            _consumerHandle = null;
            ReceivedTokens = new Dictionary<int, StreamSequenceToken>();
            return TaskDone.Done;
        }

        public async Task<StreamSubscriptionHandle<int>> BecomeConsumer(Guid streamId, string streamNamespace, StreamSequenceToken token, TimeSpan timeToConsume, string providerToUse)
        {
            Logger.Info("BecomeConsumer");
            _consumerObserver = new TimedConsumerObserver<int>(this);
            IStreamProvider streamProvider = base.GetStreamProvider(providerToUse);
            _consumer = streamProvider.GetStream<int>(streamId, streamNamespace);
            TimeToConsume = timeToConsume;
            _consumerHandle = await _consumer.SubscribeAsync(_consumerObserver, token);
            return _consumerHandle;
        }

        public async Task StopConsuming()
        {
            Logger.Info("StopConsuming");
            if (_consumerHandle != null)
            {
                await _consumerHandle.UnsubscribeAsync();
                _consumerHandle = null;
            }
        }

        public Task<int> GetNumberConsumed()
        {
            return Task.FromResult(NumConsumedItems);
        }

        public Task<Dictionary<int, StreamSequenceToken>> GetReceivedTokens()
        {
            return Task.FromResult(ReceivedTokens);
        }

        public override Task OnDeactivateAsync()
        {
            Logger.Info("OnDeactivateAsync");
            return TaskDone.Done;
        }
    }
}
