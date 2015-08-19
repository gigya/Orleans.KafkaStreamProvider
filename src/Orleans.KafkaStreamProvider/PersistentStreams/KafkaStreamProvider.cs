using Orleans.KafkaStreamProvider.KafkaQueue;
using Orleans.Providers.Streams.Common;

namespace Orleans.KafkaStreamProvider.PersistentStreams
{
    public class KafkaStreamProvider : PersistentStreamProvider<KafkaQueueAdapterFactory>
    {
    }
}
