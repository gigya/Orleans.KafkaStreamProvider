using System;
using System.Collections.Generic;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache
{
    /// <summary>
    /// The TimedQueueCacheCursor is at the moment identical to SimpleQueueCache cursor. We are not using the SimpleCacheCursor in order 
    /// to be able to extend the CacheCursor without considering the SimpleQueueCache.
    /// </summary>
    public class TimedQueueCacheCursor : IQueueCacheCursor
    {
        private readonly Guid _streamGuid;
        private readonly string _streamNamespace;
        private readonly TimedQueueCache _cache;
        private readonly Logger _logger;
        private IBatchContainer _current; // this is a pointer to the current element in the cache. It is what will be returned by GetCurrent().

        // This is a pointer to the NEXT element in the cache.
        // After the cursor is first created it should be called MoveNext before the call to GetCurrent().
        // After MoveNext returns, the current points to the current element that will be returned by GetCurrent()
        // and Element will point to the next element (since MoveNext actualy advanced it to the next).
        internal LinkedListNode<TimedQueueCacheItem> NextElement { get; private set; }
        internal StreamSequenceToken SequenceToken { get; private set; }

        internal bool IsSet => NextElement != null;

        internal void Reset(StreamSequenceToken token)
        {
            NextElement = null;
            SequenceToken = token;
        }

        internal void Set(LinkedListNode<TimedQueueCacheItem> item)
        {
            NextElement = item;
            SequenceToken = item.Value.SequenceToken;
        }

        public TimedQueueCacheCursor(TimedQueueCache cache, Guid streamGuid, string streamNamespace, Logger logger)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }
            _cache = cache;
            _streamGuid = streamGuid;
            _streamNamespace = streamNamespace;
            _logger = logger;
            _current = null;
            TimedQueueCache.Log(logger, "TimedQueueCacheCursor New Cursor for {0}, {1}", streamGuid, streamNamespace);
        }

        public virtual IBatchContainer GetCurrent(out Exception exception)
        {
            TimedQueueCache.Log(_logger, "TimedQueueCacheCursor.GetCurrent: {0}", _current);

            exception = null;
            return _current;
        }

        public bool MoveNext()
        {
            IBatchContainer next;
            while (_cache.TryGetNextMessage(this, out next))
            {
                if(IsInStream(next))
                    break;
            }
            if (!IsInStream(next))
                return false;

            _current = next;
            return true;
        }

        private bool IsInStream(IBatchContainer batchContainer)
        {
            return batchContainer != null &&
                    batchContainer.StreamGuid.Equals(_streamGuid) &&
                    string.Equals(batchContainer.StreamNamespace, _streamNamespace);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cache.ResetCursor(this, null);
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("<TimedQueueCacheCursor: Element={0}, SequenceToken={1}>",
                NextElement != null ? NextElement.Value.Batch.ToString() : "null", SequenceToken?.ToString() ?? "null");
        }

        public void RecordDeliveryFailure()
        {
            _logger.Warn(0, "Delivery has failed");
        }

        public void Refresh(StreamSequenceToken token)
        {
            if (!IsSet)
            {
                _cache.InitializeCursor(this, token);
            }
        }
    }
}
