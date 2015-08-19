using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;

namespace TestGrains
{
    internal class CustomEventConsumerObserver<T> : IAsyncObserver<T>
    {
        private readonly CustomEventConsumerGrain<T> _hostingGrain;

        internal CustomEventConsumerObserver(CustomEventConsumerGrain<T> hostingGrain)
        {
            _hostingGrain = hostingGrain;
        }

        public Task OnNextAsync(T item, StreamSequenceToken token = null)
        {
            _hostingGrain.Logger.Info("OnNextAsync(item={0}, token={1})", item.ToString(), token != null ? token.ToString() : "null");
            _hostingGrain.NumConsumedItems++;
            _hostingGrain.LastConsumedItem = item;

            return TaskDone.Done;
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

    public class CustomEventConsumerGrain<T> : Grain, ICustomEventConsumerGrain<T>
    {
        private IAsyncObservable<T> _consumer;
        internal int NumConsumedItems;
        internal Logger Logger;
        private IAsyncObserver<T> _consumerObserver;
        private StreamSubscriptionHandle<T> _consumerHandle;
        internal T LastConsumedItem;

        public override Task OnActivateAsync()
        {
            Logger = GetLogger("CustomEventConsumerGrain " + IdentityString);
            Logger.Info("OnActivateAsync");
            NumConsumedItems = 0;
            _consumerHandle = null;
            return TaskDone.Done;
        }

        public async Task BecomeConsumer(Guid streamId, string streamNamespace, string providerToUse)
        {
            Logger.Info("BecomeConsumer");
            _consumerObserver = new CustomEventConsumerObserver<T>(this);
            IStreamProvider streamProvider = GetStreamProvider(providerToUse);
            _consumer = streamProvider.GetStream<T>(streamId, streamNamespace);
            _consumerHandle = await _consumer.SubscribeAsync(_consumerObserver);
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

        public override Task OnDeactivateAsync()
        {
            Logger.Info("OnDeactivateAsync");
            return TaskDone.Done;
        }

        public Task<T> GetLastConsumedItem()
        {
            return Task.FromResult(LastConsumedItem);
        }
    }
}
