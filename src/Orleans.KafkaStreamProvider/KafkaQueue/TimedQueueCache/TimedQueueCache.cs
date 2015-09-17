using System;
using System.Collections.Generic;
using System.Linq;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache
{
    internal class TimedQueueCacheBucket
    {
        // For backpressure detection we maintain a histogram of 10 buckets.
        // Every buckets records how many items are in the cache in that bucket
        // and how many cursors are pointing to an item in that bucket.
        // In the TimedCache the bucket also takes note what is the newest and oldest members timespan because buckets also 
        // has a maximum timespan they can hold (this is done for better bucket distribution)
        // We update the NumCurrentItems, NewestMemberTimespan and OldestMemberTimespan (when it's a new bucket) when we add and remove cache item (potentially opening or removing a bucket)
        // We update NumCurrentCursors every time we move a cursor
        // If the first (most outdated bucket) has at least one cursor pointing to it, we say we are under back pressure (in a full cache).
        internal int NumCurrentItems { get; private set; }
        internal int NumCurrentCursors { get; private set; }

        internal void UpdateNumItems(int val)
        {
            NumCurrentItems = NumCurrentItems + val;
        }
        internal void UpdateNumCursors(int val)
        {
            NumCurrentCursors = NumCurrentCursors + val;
        }

        internal DateTime OldestMemberTimestamp;
        internal DateTime NewestMemberTimestamp;
    }

    /// <summary>
    /// The TimedQueueCacheItems is similar to the SimpleQueueCacheItem, but it also holds a Timestamp so we will know 
    /// how long the message is in the cache.
    /// </summary>
    internal struct TimedQueueCacheItem
    {
        internal IBatchContainer Batch;
        internal StreamSequenceToken SequenceToken;
        internal TimedQueueCacheBucket CacheBucket;
        internal DateTime Timestamp;
    }

    /// <summary>
    /// The TimedQueueCache works similarly to the SimpleQueueCache but it also has a Timespan which is used as an expiration time. I.e, only items
    /// that expire their Timespan (and were consumed by all cursors of course) are allowed to be removed from the cache. That way the cache always guarantees 
    /// to hold all the items that were inserted in a certain Timespan (for example if the Timespan is 1 hour, all the messages that were inserted in the last
    /// hour will remain in the cache, with no regard if they were consumed or not).
    /// The TimedQueueCache also offers to hold a callback for when items are being removed from the cache and also allows to define an interval for how many items need to be
    /// removed before the callback is called.
    /// </summary>
    public class TimedQueueCache : IQueueCache
    {
        private readonly LinkedList<TimedQueueCacheItem> _cachedMessages;
        private readonly Logger _logger;
        private readonly List<TimedQueueCacheBucket> _cacheCursorHistogram; // for backpressure detection
        private const int NumCacheHistogramBuckets = 10;
        private readonly int _cacheSize;
        private readonly int _cacheHistogramMaxBucketSize;
        private readonly TimeSpan _cacheTimeSpan;
        private readonly TimeSpan _bucketTimeSpan;
        private readonly int _callbackInterval;
        private int _numOfRemovals;
        private int _maxNumberToAdd;
        private readonly Func<bool> _deletionCallback;

        public QueueId Id { get; private set; }

        public int Size
        {
            get { return _cachedMessages.Count; }
        }

        public int MaxAddCount
        {
            get
            {                
                // Because our bucket sizes our inconsistent (they are also dependant to time),
                // we need to make sure that the cache doesn't take more messages than it can. see the function CalculateMessagesToAdd 
                return _maxNumberToAdd;
            }
        }

        public TimedQueueCache(QueueId queueId, TimeSpan cacheTimespan, int cacheSize, Logger logger)
        {
            Id = queueId;
            _cachedMessages = new LinkedList<TimedQueueCacheItem>();

            _logger = logger;
            _cacheCursorHistogram = new List<TimedQueueCacheBucket>();

            // conceptually we have 10 buckets, although there are scenarios we'll have more than 10 buckets, but we will not exceed the max size of the cache
            _cacheHistogramMaxBucketSize = Math.Max(_cacheSize / NumCacheHistogramBuckets, 1);
            _maxNumberToAdd = _cacheHistogramMaxBucketSize;
            _cacheTimeSpan = cacheTimespan;
            _cacheSize = cacheSize;
            _bucketTimeSpan = TimeSpan.FromMilliseconds(cacheTimespan.TotalMilliseconds / NumCacheHistogramBuckets);            
        }

        public TimedQueueCache(QueueId queueId, TimeSpan cacheTimespan, int cacheSize, Func<bool> deletionCallback,
            int deletionCallbackInterval, Logger logger) : this(queueId, cacheTimespan, cacheSize, logger)
        {
            _deletionCallback = deletionCallback;
            _callbackInterval = deletionCallbackInterval;
            _numOfRemovals = 0;
        }

        public bool IsUnderPressure()
        {
            if (_cachedMessages.Count == 0) return false; // empty cache
            if (_cacheCursorHistogram.Count == 0) return false;    // no cursors yet - zero consumers basically yet.

            // If the cache still has room, no problem of adding 
            if (Size < _cacheSize)
            {
                CalculateMessagesToAdd();
                return false;
            }

            // cache is full. Need Check how many cursors we have in the oldest bucket AND that we don't break our timespan guarantee.
            var numCursorsInLastBucket = _cacheCursorHistogram[0].NumCurrentCursors;

            var currentCacheTimespan = DateTime.Now - _cacheCursorHistogram[0].NewestMemberTimestamp;
            if (numCursorsInLastBucket > 0 || currentCacheTimespan <= _cacheTimeSpan) return true;

            // Cache is full yet we can add messages, calculating how many messages we can put
            CalculateMessagesToAdd();
            return false;
        }

        private void CalculateMessagesToAdd()
        {
            _maxNumberToAdd = Size < _cacheSize ? Math.Min(_cacheSize - Size, _cacheHistogramMaxBucketSize) : _cacheCursorHistogram[0].NumCurrentItems;
        }

        public virtual void AddToCache(IList<IBatchContainer> msgs)
        {
            if (msgs == null) throw new ArgumentNullException("msgs");

            Log(_logger, "TimedQueueCache for QueueId:{0}, AddToCache: added {1} items to cache.", Id.ToString(), msgs.Count);            

            foreach (var message in msgs)
            {
                Add(message, message.SequenceToken);
            }
        }

        public virtual IQueueCacheCursor GetCacheCursor(Guid streamGuid, string streamNamespace, StreamSequenceToken token)
        {
            if (token != null && !(token is EventSequenceToken))
            {
                // Null token can come from a stream subscriber that is just interested to start consuming from latest (the most recent event added to the cache).
                throw new ArgumentOutOfRangeException("token", "token must be of type EventSequenceToken");
            }

            var cursor = new TimedQueueCacheCursor(this, streamGuid, streamNamespace, _logger);
            InitializeCursor(cursor, token);
            return cursor;
        }

        private void InitializeCursor(TimedQueueCacheCursor cursor, StreamSequenceToken sequenceToken)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, InitializeCursor: {1} to sequenceToken {2}", Id.ToString(), cursor, sequenceToken);

            if (_cachedMessages.Count == 0) // nothing in cache
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, InitializeCursor: The TimedQueueCache is empty", Id.ToString());
                ResetCursor(cursor, sequenceToken);
                return;
            }

            // if offset is not set, iterate from newest (first) message in cache, but not including the first message itself
            if (sequenceToken == null)
            {
                LinkedListNode<TimedQueueCacheItem> firstMessage = _cachedMessages.First;
                ResetCursor(cursor, ((EventSequenceToken)firstMessage.Value.SequenceToken).NextSequenceNumber());
                return;
            }

            if (sequenceToken.Newer(FirstItem.SequenceToken)) // sequenceId is too new to be in cache
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, initializing with newer token", Id.ToString());
                ResetCursor(cursor, sequenceToken);
                return;
            }
            
            // Check to see if offset is too old to be in cache
            if (sequenceToken.Older(LastItem.SequenceToken))
            {
                // We don't throw cache misses, we are more tolerant. Starting the cursor from the last message and logging the incident
                _logger.Info("TimedQueueCache for QueueId:{0}, InitializeCursor: Sequence tried to subscribe with an older token: {0}, started instead from oldest token in cache which is: {1} and was inserted on {2}", Id.ToString(), sequenceToken, LastItem.SequenceToken, LastItem.Timestamp);
                SetCursor(cursor, _cachedMessages.Last);
                return;
            }

            // Now the requested sequenceToken is set and is also within the limits of the cache.

            // Find first message at or below offset
            // Events are ordered from newest to oldest, so iterate from start of list until we hit a node at a previous offset, or the end.
            LinkedListNode<TimedQueueCacheItem> node = _cachedMessages.First;
            while (node != null && node.Value.SequenceToken.Newer(sequenceToken))
            {
                // did we get to the end?
                if (node.Next == null) // node is the last message
                    break;

                // if sequenceId is between the two, take the higher
                if (node.Next.Value.SequenceToken.Older(sequenceToken))
                    break;

                node = node.Next;
            }

            // return cursor from start.
            SetCursor(cursor, node);
        }

        /// <summary>
        /// Acquires the next message in the cache at the provided cursor
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        internal bool TryGetNextMessage(TimedQueueCacheCursor cursor, out IBatchContainer batch)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, TryGetNextMessage: {0}", Id.ToString(), cursor);

            batch = null;

            if (cursor == null) throw new ArgumentNullException("cursor");

            //if not set, try to set and then get next
            if (!cursor.IsSet)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, TryGetNextMessage: cursor was not set on a value, initializing with the cursor sequence token", Id.ToString());
                InitializeCursor(cursor, cursor.SequenceToken);
                return cursor.IsSet && TryGetNextMessage(cursor, out batch);
            }

            // has this message been purged
            if (cursor.SequenceToken.Older(LastItem.SequenceToken))
            {
                throw new QueueCacheMissException(cursor.SequenceToken, LastItem.SequenceToken, FirstItem.SequenceToken);
            }

            // Cursor now points to a valid message in the cache. Get it!
            // Capture the current element and advance to the next one.
            batch = cursor.NextElement.Value.Batch;
            Log(_logger, "TimedQueueCache for QueueId:{0}, TryGetNextMessage: retrieved an item from cache.", Id.ToString());

            // Advance to next:
            if (cursor.NextElement == _cachedMessages.First)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, TryGetNextMessage: reached end of cache, resetting the cursor to a future token.", Id.ToString());

                // If we are at the end of the cache unset cursor and move offset one forward
                ResetCursor(cursor, ((EventSequenceToken)cursor.SequenceToken).NextSequenceNumber());
            }
            else // move to next
            {
                UpdateCursor(cursor, cursor.NextElement.Previous);
            }
            return true;
        }

        private void UpdateCursor(TimedQueueCacheCursor cursor, LinkedListNode<TimedQueueCacheItem> item)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, UpdateCursor: {0} to item {1}", Id.ToString(), cursor, item.Value.Batch);

            // remove from previous bucket
            cursor.NextElement.Value.CacheBucket.UpdateNumCursors(-1); 
            cursor.Set(item);

            // add to next bucket
            cursor.NextElement.Value.CacheBucket.UpdateNumCursors(1);
        }

        internal void SetCursor(TimedQueueCacheCursor cursor, LinkedListNode<TimedQueueCacheItem> item)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, SetCursor: {0} to item {1}", Id.ToString(), cursor, item.Value.Batch);

            cursor.Set(item);

            // add to bucket
            cursor.NextElement.Value.CacheBucket.UpdateNumCursors(1);  
        }

        internal void ResetCursor(TimedQueueCacheCursor cursor, StreamSequenceToken token)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, ResetCursor: {0} to token {1}", Id.ToString(), cursor, token);

            if (cursor.IsSet)
            {
                cursor.NextElement.Value.CacheBucket.UpdateNumCursors(-1);
            }
            cursor.Reset(token);
        }

        private void Add(IBatchContainer batch, StreamSequenceToken sequenceToken)
        {
            if (batch == null) throw new ArgumentNullException("batch");

            TimedQueueCacheBucket cacheBucket;
            if (_cacheCursorHistogram.Count == 0)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: No buckets, creating the first bucket", Id.ToString());
                cacheBucket = new TimedQueueCacheBucket();
                _cacheCursorHistogram.Add(cacheBucket);
            }
            else
            {
                cacheBucket = _cacheCursorHistogram.Last(); // last one
            }

            // if last bucket is full or containing all the TimeSpan, open a new one
            if (cacheBucket.NumCurrentItems == _cacheHistogramMaxBucketSize || 
                (cacheBucket.NewestMemberTimestamp - cacheBucket.OldestMemberTimestamp) > _bucketTimeSpan) 
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: Last bucket exceeded size ", Id.ToString());
                cacheBucket = new TimedQueueCacheBucket();
                _cacheCursorHistogram.Add(cacheBucket);
            }

            cacheBucket.UpdateNumItems(1);

            // Add message to linked list
            var item = new TimedQueueCacheItem
            {
                Batch = batch,
                SequenceToken = sequenceToken,
                CacheBucket = cacheBucket,
                Timestamp = DateTime.Now
            };

            // If it's the first item, then we also update 
            if (cacheBucket.NumCurrentCursors == 1)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: The oldest timespan in the cache is {1}", Id.ToString(), item.Timestamp);
                cacheBucket.OldestMemberTimestamp = item.Timestamp;
            }

            // Setting the newest member
            cacheBucket.NewestMemberTimestamp = item.Timestamp;

            _cachedMessages.AddFirst(new LinkedListNode<TimedQueueCacheItem>(item));

            // Removing segment
            
            // If it's a size issue, then we have to remove at least one message immediately (No need to check for last bucket, cause if there was an issue the cache would be under pressure)
            if (Size > _cacheSize && _cacheCursorHistogram[0].NumCurrentCursors == 0)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: last  message because of size", Id.ToString());
                RemoveLastMessage();
            }

            // Now we are looking for old messages that needs to go away (the condition is that they are older than cache timespan and their bucket has no cursors on it, cause under pressure checks don't check time)
            while (_cacheCursorHistogram.Count > 0 && _cacheCursorHistogram[0].NumCurrentCursors == 0 && (DateTime.Now - LastItem.Timestamp) > _cacheTimeSpan)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: last  message because of time expiration", Id.ToString());
                RemoveLastMessage();
            }

            // Checking if we need to call the deletionCallback
            if (_deletionCallback != null && _numOfRemovals >= _callbackInterval)
            {
                if (!_deletionCallback()) _logger.Info("TimedQueueCache for QueueId:{0}, Add: Callback function assigned to cache failed. Function is {1}", Id.ToString(), _deletionCallback.ToString());
                _numOfRemovals = 0;
            }
        }

        private void RemoveLastMessage()
        {
            // Removing the last message
            _cachedMessages.RemoveLast();
            _numOfRemovals++;

            // Some bucket updating
            var bucket = _cacheCursorHistogram[0]; // same as:  var bucket = last.Value.CacheBucket;
            bucket.UpdateNumItems(-1);
            
            if (bucket.NumCurrentItems == 0)
            {
                _logger.Info("TimedQueueCache for QueueId:{0}, RemoveLastMessage: Last bucket is empty, removing it", Id.ToString());
                _cacheCursorHistogram.RemoveAt(0);
            }
            else
            {
                _cacheCursorHistogram[0].OldestMemberTimestamp = LastItem.Timestamp;
            }
        }

        internal TimedQueueCacheItem FirstItem
        {
            get { return _cachedMessages.First.Value; }
        }

        internal TimedQueueCacheItem LastItem
        {
            get { return _cachedMessages.Last.Value; }
        }
       
        internal static void Log(Logger logger, string format, params object[] args)
        {
            if (logger.IsVerbose)
            {
                logger.Verbose(format,  args);
            }
        }
    }
}
