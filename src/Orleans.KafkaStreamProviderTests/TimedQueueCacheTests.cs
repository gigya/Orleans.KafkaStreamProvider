using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrains;

namespace Orleans.KafkaStreamProviderTest
{
    public class DeletionObserver
    {
        public int TimesCalled { get; set; }

        public DeletionObserver()
        {
            TimesCalled = 0;
        }

        public bool NotifiedDeletion(EventSequenceToken token)
        {
            TimesCalled++;
            return true;
        }
    }

    [TestClass]
    public class TimedQueueCacheTests
    {
        private readonly QueueId _defaultId;
        private readonly Logger _logger;
        private readonly int _defaultCacheSize;
        private readonly int _defaultCacheBucketNum;

        public TimedQueueCacheTests()
        {
            Mock<Logger> loggerMock = new Mock<Logger>();
            _logger = loggerMock.Object;
            _defaultId = QueueId.GetQueueId("defaultQueue");
            _defaultCacheSize = 4096;
            _defaultCacheBucketNum = 10;
        }

        [TestMethod]
        public void InsertOneMessageTest()
        {
            Mock<IBatchContainer> batchMock = new Mock<IBatchContainer>();
            List<IBatchContainer> msgs = new List<IBatchContainer>(){batchMock.Object};

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Assert.AreEqual(1, cache.Size);
        }

        [TestMethod]
        public void InsertOneMessageEmptyCollection()
        {
            List<IBatchContainer> msgs = new List<IBatchContainer>();

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Assert.AreEqual(0, cache.Size);
        }

        [TestMethod]
        public void InsertManyMessagesTest()
        {
            Mock<IBatchContainer> batchMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchMock3 = new Mock<IBatchContainer>();

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Assert.AreEqual(3, cache.Size);
        }

        [TestMethod]
        public void MessageRemovalTest()
        {
            Mock<IBatchContainer> batchMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchMock3 = new Mock<IBatchContainer>();

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromSeconds(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            IList<IBatchContainer> outContainers = new List<IBatchContainer>();

            cache.TryPurgeFromCache(out outContainers);

            Assert.AreEqual(0, cache.Size);
            Assert.AreEqual(3, outContainers.Count);
        }

        [TestMethod]
        public void CursorRunningOnCacheOnlyMessagesFromOneStreamTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Exception placeholder;

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock1.Object.SequenceToken);

            Assert.AreEqual(3, cache.Size);

            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock1.Object, "Didn't get the first object");
            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock2.Object, "Didn't get the second object");
            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock3.Object, "Didn't get the third object");
        }

        [TestMethod]
        public void CacheUnderPressureBecauseTimespanGuaranteeTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), msgs.Count, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Assert.IsTrue(cache.IsUnderPressure());
        }

        [TestMethod]
        public void CacheUnderPressureBecauseOfSlowConsumersTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromSeconds(1), msgs.Count, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock1.Object.SequenceToken);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            Assert.IsTrue(cache.IsUnderPressure());
        }

        [TestMethod]
        public void CacheIsNotUnderPressureBecauseTimespanHasPassedAndNoConsumersTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromSeconds(1), msgs.Count, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock2.Object.SequenceToken);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            Assert.IsTrue(!cache.IsUnderPressure());
        }

        [TestMethod]
        public void CacheIsNotUnderPressureBecauseTimespanHasPassedAndNoSlowConsumersTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromSeconds(1), msgs.Count, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            Assert.IsTrue(!cache.IsUnderPressure());
            Assert.AreEqual(1, cache.GetMaxAddCount());
        }

        [TestMethod]
        public void MaxAddCountIsBucketSizeBecauseCacheIsFullTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(streamGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, streamNamespace, 3);
            Mock<IBatchContainer> batchMock4 = GenerateBatchContainerMock(streamGuid, streamNamespace, 4);
            Mock<IBatchContainer> batchMock5 = GenerateBatchContainerMock(streamGuid, streamNamespace, 5);
            Mock<IBatchContainer> batchMock6 = GenerateBatchContainerMock(streamGuid, streamNamespace, 6);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object, batchMock4.Object, batchMock5.Object, batchMock6.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromSeconds(1), msgs.Count, msgs.Count / 2, _logger);
            cache.AddToCache(msgs);

            Task.Delay(TimeSpan.FromSeconds(2)).Wait();

            Assert.IsTrue(!cache.IsUnderPressure());
            Assert.AreEqual(2, cache.GetMaxAddCount());
        }

        [TestMethod]
        public void CursorRunningOnCacheMessagesFromMultipleStreamsTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Guid otherGuid = Guid.NewGuid();
            string otherNamespace = "Other";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(otherGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, otherNamespace, 3);
            Mock<IBatchContainer> batchMock4 = GenerateBatchContainerMock(otherGuid, otherNamespace, 4);
            Mock<IBatchContainer> batchMock5 = GenerateBatchContainerMock(streamGuid, streamNamespace, 5);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object, batchMock4.Object, batchMock5.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Exception placeholder;

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock1.Object.SequenceToken);

            Assert.AreEqual(5, cache.Size);

            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock1.Object, "Didn't get the first object");
            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock5.Object, "Didn't get the second object");
        }

        [TestMethod]
        public void CursorRunningOnCacheMessagesFromMultipleStreamsRunningFromMiddleTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Guid otherGuid = Guid.NewGuid();
            string otherNamespace = "Other";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(otherGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, otherNamespace, 3);
            Mock<IBatchContainer> batchMock4 = GenerateBatchContainerMock(otherGuid, otherNamespace, 4);
            Mock<IBatchContainer> batchMock5 = GenerateBatchContainerMock(streamGuid, streamNamespace, 5);
            Mock<IBatchContainer> batchMock6 = GenerateBatchContainerMock(streamGuid, streamNamespace, 6);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock1.Object, batchMock2.Object, batchMock3.Object, batchMock4.Object, batchMock5.Object, batchMock6.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Exception placeholder;

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock5.Object.SequenceToken);

            Assert.AreEqual(6, cache.Size);

            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock5.Object, "Didn't get the first object");
            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock6.Object, "Didn't get the second object");
        }

        [TestMethod]
        public void CursorRunningOnCacheMessagesFromMultipleStreamsGettingCursorWithOldTokenTest()
        {
            Guid streamGuid = Guid.NewGuid();
            string streamNamespace = "TestTimedCache";

            Guid otherGuid = Guid.NewGuid();
            string otherNamespace = "Other";

            Mock<IBatchContainer> batchMock1 = GenerateBatchContainerMock(streamGuid, streamNamespace, 1);
            Mock<IBatchContainer> batchMock2 = GenerateBatchContainerMock(otherGuid, streamNamespace, 2);
            Mock<IBatchContainer> batchMock3 = GenerateBatchContainerMock(streamGuid, otherNamespace, 3);
            Mock<IBatchContainer> batchMock4 = GenerateBatchContainerMock(otherGuid, otherNamespace, 4);
            Mock<IBatchContainer> batchMock5 = GenerateBatchContainerMock(streamGuid, streamNamespace, 5);
            Mock<IBatchContainer> batchMock6 = GenerateBatchContainerMock(streamGuid, streamNamespace, 6);

            List<IBatchContainer> msgs = new List<IBatchContainer>() { batchMock2.Object, batchMock3.Object, batchMock4.Object, batchMock5.Object, batchMock6.Object };

            var cache = new TimedQueueCache(_defaultId, TimeSpan.FromHours(1), _defaultCacheSize, _defaultCacheBucketNum, _logger);
            cache.AddToCache(msgs);

            Exception placeholder;

            var cursor = cache.GetCacheCursor(new StreamKey(streamGuid, streamNamespace), batchMock1.Object.SequenceToken);

            Assert.AreEqual(5, cache.Size);

            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock5.Object, "Didn't get the first object");
            Assert.IsTrue(cursor.MoveNext(), "Couldn't move next on cursor");
            Assert.AreEqual(cursor.GetCurrent(out placeholder), batchMock6.Object, "Didn't get the second object");
        }

        private Mock<IBatchContainer> GenerateBatchContainerMock(Guid streamGuid, string streamNamespace, int sequenceNumber)
        {
            Mock<IBatchContainer> batchMock = new Mock<IBatchContainer>();          
            EventSequenceToken seq = new EventSequenceToken(sequenceNumber);

            batchMock.SetupGet(x => x.SequenceToken).Returns(seq);
            batchMock.SetupGet(x => x.StreamGuid).Returns(streamGuid);
            batchMock.SetupGet(x => x.StreamNamespace).Returns(streamNamespace);

            return batchMock;
        }
    }
}
