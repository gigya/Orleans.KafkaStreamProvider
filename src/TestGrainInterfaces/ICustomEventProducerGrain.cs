using System;
using System.Threading.Tasks;
using Orleans;

namespace TestGrainInterfaces
{
    public interface ICustomEventProducerGrain<T> : IGrainWithGuidKey
    {
        Task BecomeProducer(Guid streamId, string streamNamespace, string providerToUse);

        Task StartPeriodicBatchProducing(int batchSize, T itemToProduce);

        Task StopPeriodicBatchProducing();

        Task<int> GetNumberProduced();

        Task ClearNumberProduced();

        Task Produce(int batchSize, T valueToProduce);

        Task<T> GetLastProducedItem();
    }
}
