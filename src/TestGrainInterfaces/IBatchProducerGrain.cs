using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace TestGrainInterfaces
{
    public interface IBatchProducerGrain : IGrainWithGuidKey
    {
        Task BecomeProducer(Guid streamId, string streamNamespace, string providerToUse);

        Task StartPeriodicBatchProducing(int batchSize);

        Task StopPeriodicBatchProducing();

        Task<int> GetNumberProduced();

        Task ClearNumberProduced();

        Task Produce(int batchSize);
    }
}
