using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;
using TestGrains.Models;

namespace TestGrains
{
    internal class CustomEventConsumerObserver<T> : IAsyncObserver<T>
    {
        private readonly CustomEventConsumerGrain<T> _hostingGrain;
        private DateTime _beginSending;

        internal CustomEventConsumerObserver(CustomEventConsumerGrain<T> hostingGrain)
        {
            _hostingGrain = hostingGrain;
        }

        public Task OnNextAsync(T item, StreamSequenceToken token = null)
        {
            _hostingGrain.Logger.Verbose("OnNextAsync(item={0}, token={1})", item.ToString(), token != null ? token.ToString() : "null");

            if (_hostingGrain.NumConsumedItems == 0)
            {
                _hostingGrain.Watch.Restart();

                if (item is LoadTestMessage)
                {
                    _beginSending = (item as LoadTestMessage).SentTime;
                }
                
            }

            _hostingGrain.NumConsumedItems++;
            _hostingGrain.LastConsumedItem = item;

            if (_hostingGrain.NumConsumedItems == _hostingGrain.ExpectedMessages)
            {
                _hostingGrain.Watch.Stop();
                _hostingGrain.TotalSendTime = DateTime.Now - _beginSending;
            }

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
        internal int ExpectedMessages;
        internal bool FinishedConsuming;
        internal Stopwatch Watch;
        internal Logger Logger;
        private IAsyncObserver<T> _consumerObserver;
        private StreamSubscriptionHandle<T> _consumerHandle;
        internal T LastConsumedItem;
        internal TimeSpan TotalSendTime;

        public override Task OnActivateAsync()
        {
            Logger = GetLogger("CustomEventConsumerGrain " + IdentityString);
            Logger.Info("OnActivateAsync");
            NumConsumedItems = 0;
            _consumerHandle = null;
            Watch = Stopwatch.StartNew();
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


        public Task SetMessageExpectation(int value)
        {
            ExpectedMessages = value;
            return TaskDone.Done;
        }

        public Task<TimeSpan> GetConsumingTime()
        {
            return Task.FromResult(Watch.Elapsed);
        }

        public Task<bool> HasFinishedConsuming()
        {
            return Task.FromResult(ExpectedMessages == NumConsumedItems);
        }

        public Task<TimeSpan> GetTotalSendTime()
        {
            return Task.FromResult(TotalSendTime);
        }

    }
}
