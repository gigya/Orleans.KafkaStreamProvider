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
        private readonly Func<QueueId, Func<EventSequenceToken, bool>> _deletionCallbackAcquirer;
        private readonly int _deletionCallbackInterval;
        private readonly ConcurrentDictionary<QueueId, IQueueCache> _caches;

        public TimedQueueAdapterCache(IQueueAdapterFactory factory, TimeSpan cacheTimeSpan, int cacheSize, int cacheNumOfBuckets, Logger logger)
        {
            if (cacheTimeSpan == TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("cacheTimeSpan", "cacheTimeSpan must be larger than zero TimeSpan.");
            _cacheTimeSpan = cacheTimeSpan;
            _cacheSize = cacheSize;
            _cacheNumOfBuckets = cacheNumOfBuckets;
            _logger = logger;
            _caches = new ConcurrentDictionary<QueueId, IQueueCache>();
        }

        public TimedQueueAdapterCache(IQueueAdapterFactory factory, TimeSpan cacheTimeSpan, int cacheSize, int cacheNumOfBuckets,
            Func<QueueId, Func<EventSequenceToken, bool>> deletionCallbackAcquirer, int deletionCallbackInterval, Logger logger)
            : this(factory, cacheTimeSpan, cacheSize, cacheNumOfBuckets, logger)
        {
            _deletionCallbackAcquirer = deletionCallbackAcquirer;
            _deletionCallbackInterval = deletionCallbackInterval;
        }

        public IQueueCache CreateQueueCache(QueueId queueId)
        {
            if (_deletionCallbackAcquirer != null)
            {
                var deletionCallback = _deletionCallbackAcquirer(queueId);
                return _caches.AddOrUpdate(queueId, id => new TimedQueueCache(id, _cacheTimeSpan, _cacheSize, _cacheNumOfBuckets, deletionCallback, _deletionCallbackInterval, _logger), (id, queueCache) => queueCache);
            }

            return _caches.AddOrUpdate(queueId, id => new TimedQueueCache(id, _cacheTimeSpan,_cacheSize, _cacheNumOfBuckets, _logger), (id, queueCache) => queueCache);
        }

        public int Size
        {
            get { return _cacheSize; }
        }
    }
}
