﻿using System;
using System.Threading.Tasks;
using Orleans;

namespace TestGrainInterfaces
{
    public interface ICustomEventConsumerGrain<T> : IGrainWithGuidKey
    {
        Task BecomeConsumer(Guid streamId, string streamNamespace, string providerToUse);

        Task StopConsuming();

        Task<int> GetNumberConsumed();

        Task<T> GetLastConsumedItem();

        Task SetMessageExpectation(int value);

        Task<TimeSpan> GetConsumingTime();

        Task<bool> HasFinishedConsuming();

        Task<TimeSpan> GetTotalSendTime();
    } 
}
