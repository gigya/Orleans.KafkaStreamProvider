using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Metrics;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache
{
    /// <summary>
    ///  For backpressure detection we maintain a histogram of 10 buckets.
    ///  Every buckets records how many items are in the cache in that bucket
    ///  and how many cursors are pointing to an item in that bucket.
    ///  In the TimedCache the bucket also takes note what is the newest and oldest members 
    ///  timespan because buckets also  has a maximum timespan they 
    ///  can hold (this is done for better bucket distribution)
    ///  We update the NumCurrentItems, NewestMemberTimespan and OldestMemberTimespan 
    ///  (when it's a new bucket) when we add and 
    ///  remove cache item (potentially opening or removing a bucket)
    ///  We update NumCurrentCursors every time we move a cursor
    ///  If the first (most outdated bucket) has at least one cursor pointing to it,
    ///  we say we are under back pressure (in a full cache).
    /// </summary>
    internal class TimedQueueCacheBucket
    {

        internal int NumCurrentItems { get; private set; }
        internal int NumCurrentCursors { get; private set; }
        internal DateTime OldestMemberTimestamp;
        internal DateTime NewestMemberTimestamp;
        internal LinkedListNode<TimedQueueCacheItem> OldestMember;
        internal LinkedListNode<TimedQueueCacheItem> NewestMember;

        internal void UpdateNumItems(int val)
        {
            NumCurrentItems = NumCurrentItems + val;
        }

        internal void UpdateNumCursors(int val)
        {
            NumCurrentCursors = NumCurrentCursors + val;
        }
    }

    /// <summary>
    /// The TimedQueueCacheItems is similar to the SimpleQueueCacheItem, but it also holds 
    /// a Timestamp so we will know how long the message is in the cache.
    /// </summary>
    internal struct TimedQueueCacheItem
    {
        internal IBatchContainer Batch;
        internal StreamSequenceToken SequenceToken;
        internal TimedQueueCacheBucket CacheBucket;
        internal DateTime Timestamp;
    }

    /// <summary>
    /// The TimedQueueCache works similarly to the SimpleQueueCache but it also has a
    /// Timespan which is used as an expiration and retention time. I.e, only items
    /// that expire their Timespan (and were consumed by all cursors of course) are
    /// allowed to be removed from the cache. That way the cache always guarantees 
    /// to hold all the items that were inserted in a certain Timespan (for example 
    /// if the Timespan is 1 hour, all the messages that were inserted in the last
    /// hour will remain in the cache, with no regard if they were consumed or not).
    /// The TimedQueueCache also offers to hold a callback for when items are being 
    /// removed from the cache and also allows to define an interval for how many items 
    /// need to be removed before the callback is called.
    /// </summary>
    public class TimedQueueCache : IQueueCache
    {
        private readonly LinkedList<TimedQueueCacheItem> _cachedMessages;
        private readonly Logger _logger;
        private readonly List<TimedQueueCacheBucket> _cacheCursorHistogram; // for backpressure detection
        private readonly int _maxCacheSize;
        private readonly int _cacheHistogramMaxBucketSize;
        private readonly TimeSpan _cacheTimeSpan;
        private readonly TimeSpan _bucketTimeSpan;
        private int _maxNumberToAdd;
        private int _numOfCursorsCausingPressure;

        // Metrics
        private readonly Meter _meterCacheEvacuationsPerSecond;
        private readonly Counter _counterMessagesInCache;
        private readonly Counter _counterNumberOfCursorsCausingPressure;

        public QueueId Id { get; }

        public int Size => _cachedMessages.Count;
        
		public int MaxAddCount => _maxNumberToAdd;

        internal TimedQueueCacheItem FirstItem => _cachedMessages.First.Value;

        internal TimedQueueCacheItem LastItem => _cachedMessages.Last.Value;

        public TimedQueueCache(QueueId queueId, TimeSpan cacheTimespan, int cacheSize, int numOfBuckets, Logger logger)
        {
            _counterMessagesInCache = Metric.Context("KafkaStreamProvider").Counter($"Messages In Cache queueId:({queueId.GetNumericId()})", Unit.Items);
            _meterCacheEvacuationsPerSecond = Metric.Context("KafkaStreamProvider").Meter($"Cache Evacuations Per Second queueId:({queueId.GetNumericId()})", Unit.Items);
            _counterNumberOfCursorsCausingPressure = Metric.Context("KafkaStreamProvider").Counter($"Cursors causing pressure queueId:({queueId.GetNumericId()})", Unit.Items);

            Id = queueId;
            _cachedMessages = new LinkedList<TimedQueueCacheItem>();

            _logger = logger;
            _cacheCursorHistogram = new List<TimedQueueCacheBucket>();

            _maxCacheSize = cacheSize;
            _cacheHistogramMaxBucketSize = Math.Max(_maxCacheSize / numOfBuckets, 1);
            _maxNumberToAdd = _cacheHistogramMaxBucketSize;
            _cacheTimeSpan = cacheTimespan;
            _bucketTimeSpan = TimeSpan.FromMilliseconds(cacheTimespan.TotalMilliseconds / numOfBuckets);
        }

        ~TimedQueueCache()
        {
            if (Id == null) return;

            // We are using the destructor to update the TimedQueueCache Metrics (currently this is the only point where we can do this)
            if (_counterMessagesInCache != null && _cachedMessages != null)
            {
                int numOfMessages = _cachedMessages.Count;

                if (_logger != null)
                {
                    Log(_logger, "TimedQueueCache for QueueId:{0}, Destroying the cache with {1} messages", Id.ToString(), numOfMessages);
                }

                _counterMessagesInCache.Decrement(Id.ToString(), numOfMessages);
            }

            if (_counterNumberOfCursorsCausingPressure != null)
            {
                _counterNumberOfCursorsCausingPressure.Decrement(Id.ToString(), _numOfCursorsCausingPressure);
            }
        }
		
        /// <summary>
        /// Because our bucket sizes our inconsistent (they are also dependant to time),
        /// we need to make sure that the cache doesn't take more messages than it can. 
        /// see the function CalculateMessagesToAdd 
        /// </summary>
        public int GetMaxAddCount()
        {
            return _maxNumberToAdd;
        }
		
        public bool IsUnderPressure()
        {
            // empty cache
            if (_cachedMessages.Count == 0)
            {
                _counterNumberOfCursorsCausingPressure.Decrement(Id.ToString(), _numOfCursorsCausingPressure);
                _numOfCursorsCausingPressure = 0;
                return false;
            }

            // no cursors yet - zero consumers basically yet.
            if (_cacheCursorHistogram.Count == 0)
            {
                _counterNumberOfCursorsCausingPressure.Decrement(Id.ToString(), _numOfCursorsCausingPressure);
                _numOfCursorsCausingPressure = 0;
                return false;
            }

            // If the cache still has room, no problem of adding 
            if (Size < _maxCacheSize)
            {
                _counterNumberOfCursorsCausingPressure.Decrement(Id.ToString(), _numOfCursorsCausingPressure);
                _numOfCursorsCausingPressure = 0;
                CalculateMessagesToAdd();
                return false;
            }

            // cache is full. Need Check how many cursors we have in the oldest bucket 
            // AND that we don't break our timespan guarantee.
            var numCursorsInLastBucket = _cacheCursorHistogram[0].NumCurrentCursors;

            var currentCacheTimespan = DateTime.UtcNow - _cacheCursorHistogram[0].NewestMemberTimestamp;
            if (numCursorsInLastBucket > 0 || currentCacheTimespan <= _cacheTimeSpan)
            {
                _counterNumberOfCursorsCausingPressure.Increment(Id.ToString(), numCursorsInLastBucket - _numOfCursorsCausingPressure);
                _numOfCursorsCausingPressure = numCursorsInLastBucket;
                return true;
            }

            // Cache is full yet we can add messages, calculating how many messages we can put
            _counterNumberOfCursorsCausingPressure.Decrement(Id.ToString(), _numOfCursorsCausingPressure);
            _numOfCursorsCausingPressure = 0;

            CalculateMessagesToAdd();
            return false;
        }

        private void CalculateMessagesToAdd()
        {
            // Checking if the current size of the cache is smaller than the maximum cache size, then we can add
            // either the difference between the max size and the current size, or the 
            // maximum messages that can be put in a bucket (the minimal of the two).
            // If the cache is currently full (Size = _maxCacheSize), then we can only add as the number of the events
            // in the last bucket (since we are going to remove all the messages there)
            _maxNumberToAdd = Size < _maxCacheSize ? Math.Min(_maxCacheSize - Size, _cacheHistogramMaxBucketSize) : _cacheCursorHistogram[0].NumCurrentItems;
        }

        public virtual void AddToCache(IList<IBatchContainer> msgs)
        {
            if (msgs == null) throw new ArgumentNullException(nameof(msgs));

            Log(_logger, "TimedQueueCache for QueueId:{0}, AddToCache: added {1} items to cache.", Id.ToString(), msgs.Count);

            foreach (var message in msgs)
            {
                Add(message, message.SequenceToken);
            }
        }

        //public virtual IQueueCacheCursor GetCacheCursor(Guid streamGuid, string streamNamespace, StreamSequenceToken token)
        public virtual IQueueCacheCursor GetCacheCursor(IStreamIdentity streamIdentity, StreamSequenceToken token)
        {
            if (token != null && !(token is EventSequenceToken))
            {
                // Null token can come from a stream subscriber that is just interested to
                // start consuming from latest (the most recent event added to the cache).
                throw new ArgumentOutOfRangeException(nameof(token), "token must be of type EventSequenceToken");
            }

            var cursor = new TimedQueueCacheCursor(this, streamIdentity.Guid, streamIdentity.Namespace, _logger);
            InitializeCursor(cursor, token);
            return cursor;
        }

        internal void InitializeCursor(TimedQueueCacheCursor cursor, StreamSequenceToken sequenceToken)
        {
            Log(_logger, "TimedQueueCache for QueueId:{0}, InitializeCursor: {1} to sequenceToken {2}", Id.ToString(), cursor, sequenceToken);

            if (_cachedMessages.Count == 0) // nothing in cache
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, InitializeCursor: The TimedQueueCache is empty", Id.ToString());
                ResetCursor(cursor, sequenceToken);
                return;
            }

            // if offset is not set, iterate from newest (first) message in cache, but not 
            // including the first message itself
            if (sequenceToken == null)
            {
                LinkedListNode<TimedQueueCacheItem> firstMessage = _cachedMessages.First;
                ResetCursor(cursor, ((EventSequenceToken)firstMessage.Value.SequenceToken).NextSequenceNumber());
                return;
            }

            // Since we do not support finding a sequence of type x.y where y > 0, we round the token down
            var flooredToken = FloorSequenceToken(sequenceToken);

            if (flooredToken.Newer(FirstItem.SequenceToken)) // sequenceId is too new to be in cache
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, initializing with newer token", Id.ToString());
                ResetCursor(cursor, flooredToken);
                return;
            }

            // Check to see if offset is too old to be in cache
            if (flooredToken.Older(LastItem.SequenceToken))
            {
                // We don't throw cache misses, we are more tolerant. Starting the cursor 
                // from the last message and logging the incident
                _logger.Info("TimedQueueCache for QueueId:{0}, InitializeCursor: Sequence tried to subscribe with an older token: {0}, started instead from oldest token in cache which is: {1} and was inserted on {2}", Id.ToString(), sequenceToken, LastItem.SequenceToken, LastItem.Timestamp);
                SetCursor(cursor, _cachedMessages.Last);
                return;
            }

            // Now the requested sequenceToken is set and is also within the limits of the cache.
            var node = FindNodeBySequenceToken(flooredToken);

            // return cursor from start.
            SetCursor(cursor, node);
        }

        private LinkedListNode<TimedQueueCacheItem> FindNodeBySequenceToken(StreamSequenceToken sequenceToken)
        {
            // First we find a bucket where the node is in
            var sequenceBucket =
                _cacheCursorHistogram.First(
                    bucket =>
                        !sequenceToken.Newer(bucket.NewestMember.Value.SequenceToken) &&
                        !sequenceToken.Older(bucket.OldestMember.Value.SequenceToken));

            // Now that we have the bucket, we iterate on the members there starting from the newest in the bucket
            LinkedListNode<TimedQueueCacheItem> node = sequenceBucket.NewestMember;
            while (node != null && node.Value.SequenceToken.Newer(sequenceToken))
            {
                // did we get to the end?
                // node is the last message in the cache
                if (node.Next == null)
                    break;

                // if sequenceId is between the two, take the lower
                if (node.Next.Value.SequenceToken.Older(sequenceToken))
                {
                    node = node.Next;
                    break;
                }

                node = node.Next;
            }

            return node;
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

            if (cursor == null) throw new ArgumentNullException(nameof(cursor));

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
                Log(_logger, "TimedQueueCache for QueueId:{0}, This is a faulted state, by this point the cursor should point to an item in the cache. The cursor is {1}", Id.ToString(), cursor.ToString());
                SetCursor(cursor, _cachedMessages.Last);
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
            if (batch == null) throw new ArgumentNullException(nameof(batch));

            var cacheBucket = GetOrCreateBucket();

            cacheBucket.UpdateNumItems(1);
            // Add message to linked list
            var item = new TimedQueueCacheItem
            {
                Batch = batch,
                SequenceToken = sequenceToken,
                CacheBucket = cacheBucket,
            };

            item.Timestamp = GetTimestampForItem(batch);

            var newNode = new LinkedListNode<TimedQueueCacheItem>(item);

            // If it's the first item, then we also update 
            if (cacheBucket.NumCurrentItems == 1)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: The oldest timespan in the cache is {1}", Id.ToString(), item.Timestamp);
                cacheBucket.OldestMemberTimestamp = item.Timestamp;
                cacheBucket.OldestMember = newNode;
            }

            // Setting the newest member
            cacheBucket.NewestMemberTimestamp = item.Timestamp;
            cacheBucket.NewestMember = newNode;

            _cachedMessages.AddFirst(newNode);

            _counterMessagesInCache.Increment(Id.ToString(), 1);
        }

        private DateTime GetTimestampForItem(IBatchContainer batch)
        {
            // Here we check if the batch is a kafka stream 
            var batchAsKafkaBatch = batch as KafkaBatchContainer;
            return batchAsKafkaBatch == null ? DateTime.UtcNow : DateTime.ParseExact(batchAsKafkaBatch.Timestamp, "O", CultureInfo.InvariantCulture).ToUniversalTime();
        }

        private List<IBatchContainer> RemoveMessagesFromCache()
        {
            List<IBatchContainer> removedMessages = new List<IBatchContainer>();

            // If it's a size issue, then we have to remove at least one message immediately 
            // (No need to check for last bucket, cause if there was an issue the cache would be under pressure)
            if (Size > _maxCacheSize && _cacheCursorHistogram[0].NumCurrentCursors == 0)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: last  message because of size", Id.ToString());

                // Removing the last message in case its the last message in a bucket also causes the buckets removal                
                var lastRemovedMessage = RemoveLastMessage();

                // Adding to the removed messages
                removedMessages.Add(lastRemovedMessage);
            }

            // Now we are looking for old messages that needs to go away 
            // (the condition is that they are older than cache timespan and their bucket has no cursors on it)
            while (_cacheCursorHistogram.Count > 0 && _cacheCursorHistogram[0].NumCurrentCursors == 0 &&
                   (DateTime.UtcNow - LastItem.Timestamp) > _cacheTimeSpan)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, Add: last  message because of time expiration", Id.ToString());

                // Removing the last message in case its the last message in a bucket also causes the buckets removal
                var removedBatchContainer = RemoveLastMessage();
                removedMessages.Add(removedBatchContainer);
            }

            return removedMessages;
        }

        private IBatchContainer RemoveLastMessage()
        {
            var removedBatchContainer = LastItem.Batch;

            // Removing the last message
            _cachedMessages.RemoveLast();

            // Some bucket updating
            var bucket = _cacheCursorHistogram[0]; // same as:  var bucket = last.Value.CacheBucket;
            bucket.UpdateNumItems(-1);

            if (bucket.NumCurrentItems == 0)
            {
                Log(_logger, "TimedQueueCache for QueueId:{0}, RemoveLastMessage: Last bucket is empty, removing it", Id.ToString());
                _cacheCursorHistogram.RemoveAt(0);
            }
            else
            {
                _cacheCursorHistogram[0].OldestMemberTimestamp = LastItem.Timestamp;
            }

            _meterCacheEvacuationsPerSecond.Mark(Id.ToString(), 1);
            _counterMessagesInCache.Decrement(Id.ToString(), 1);

            return removedBatchContainer;
        }

        private TimedQueueCacheBucket GetOrCreateBucket()
        {
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

            return cacheBucket;
        }

        internal static void Log(Logger logger, string format, params object[] args)
        {
            logger.Verbose(format, args);
        }

        public bool TryPurgeFromCache(out IList<IBatchContainer> purgedItems)
        {
            purgedItems = RemoveMessagesFromCache();
            return true;
        }

        private StreamSequenceToken FloorSequenceToken(StreamSequenceToken token)
        {
            if (!(token is EventSequenceToken)) return token;
            EventSequenceToken tokenAsEventSequenceToken = (EventSequenceToken)token;
            if (tokenAsEventSequenceToken.EventIndex == 0) return token;

            EventSequenceToken flooredToken = new EventSequenceToken(tokenAsEventSequenceToken.SequenceNumber);
            return flooredToken;
        }
    }
}
