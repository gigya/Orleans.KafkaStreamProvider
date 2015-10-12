using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Streams;

namespace TestGrainInterfaces
{
    public interface ITimedConsumerGrain : IGrainWithGuidKey
    {
        Task<StreamSubscriptionHandle<int>>  BecomeConsumer(Guid streamId, string streamNamespace, StreamSequenceToken token, TimeSpan timeToConsume, string providerToUse);

        Task StopConsuming();

        Task<Dictionary<int, StreamSequenceToken>> GetReceivedTokens();

        Task<int> GetNumberConsumed();
    }
}
