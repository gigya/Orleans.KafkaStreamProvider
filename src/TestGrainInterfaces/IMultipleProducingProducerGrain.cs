using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Streams;

namespace TestGrainInterfaces
{
    public interface IMultipleProducingProducerGrain : IGrainWithGuidKey
    {
        Task<IStreamIdentity> BecomeProducer(Guid streamId, string streamNamespace, string providerToUse);
        
        Task StartPeriodicProducing(IStreamIdentity identity);

        Task StopPeriodicProducing(IStreamIdentity identity);

        Task<Dictionary<IStreamIdentity, int>> GetNumberProduced();

        Task ClearNumberProduced();

        Task Produce(IStreamIdentity identifier);
    }
}
