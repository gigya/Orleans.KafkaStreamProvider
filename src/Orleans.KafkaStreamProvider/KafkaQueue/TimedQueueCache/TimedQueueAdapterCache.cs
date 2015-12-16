using System;
using System.Collections.Concurrent;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache
{
    public class TimedQueueAdapterCache : IQueueAdapterCache
    {
        private readonly TimeSpan _cacheTimeSpan;
        private readonly int _cacheSize;
        private readonly int _cacheNumOfBuckets;
        private readonly Logger _logger;
        
        public TimedQueueAdapterCache(IQueueAdapterFactory factory, TimeSpan cacheTimeSpan, int cacheSize, int cacheNumOfBuckets, Logger logger)
        {
            if (cacheTimeSpan == TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("cacheTimeSpan", "cacheTimeSpan must be larger than zero TimeSpan.");
            _cacheTimeSpan = cacheTimeSpan;
            _cacheSize = cacheSize;
            _cacheNumOfBuckets = cacheNumOfBuckets;
            _logger = logger;
        }

        public IQueueCache CreateQueueCache(QueueId queueId)
        {
            return new TimedQueueCache(queueId, _cacheTimeSpan, _cacheSize, _cacheNumOfBuckets, _logger);
        }

        public int Size
        {
            get { return _cacheSize; }
        }
    }
}
