using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache
{
    public class TimedQueueAdapterCache : IQueueAdapterCache
    {
        private readonly TimeSpan _cacheTimeSpan;
        private readonly int _cacheSize;
        private readonly Logger _logger;
        private readonly Func<QueueId, Func<bool>> _deletionCallbackAcquirer;
        private readonly int _deletionCallbackInterval;
        private readonly ConcurrentDictionary<QueueId, IQueueCache> _caches;

        public TimedQueueAdapterCache(IQueueAdapterFactory factory, TimeSpan cacheTimeSpan, int cacheSize, Logger logger)
        {
            if (cacheTimeSpan == TimeSpan.Zero)
                throw new ArgumentOutOfRangeException("cacheTimeSpan", "cacheTimeSpan must be largeer than zero TimeSpan.");
            _cacheTimeSpan = cacheTimeSpan;
            _cacheSize = cacheSize;
            _logger = logger;
            _caches = new ConcurrentDictionary<QueueId, IQueueCache>();
        }

        public TimedQueueAdapterCache(IQueueAdapterFactory factory, TimeSpan cacheTimeSpan, int cacheSize,
            Func<QueueId, Func<bool>> deletionCallbackAcquirer, int deletionCallbackInterval, Logger logger)
            : this(factory, cacheTimeSpan, cacheSize, logger)
        {
            _deletionCallbackAcquirer = deletionCallbackAcquirer;
            _deletionCallbackInterval = deletionCallbackInterval;
        }

        public IQueueCache CreateQueueCache(QueueId queueId)
        {
            if (_deletionCallbackAcquirer != null)
            {
                var deletionCallback = _deletionCallbackAcquirer.Invoke(queueId);
                return _caches.AddOrUpdate(queueId, (id) => new TimedQueueCache(id, _cacheTimeSpan, _cacheSize, deletionCallback, _deletionCallbackInterval, _logger), (id, queueCache) => queueCache);
            }

            return _caches.AddOrUpdate(queueId, (id) => new TimedQueueCache(id, _cacheTimeSpan,_cacheSize, _logger), (id, queueCache) => queueCache);
        }

        public int Size
        {
            get { return _cacheSize; }
        }
    }
}
