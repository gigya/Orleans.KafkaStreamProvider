using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using KafkaNet.Interfaces;
using KafkaNet.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orleans.KafkaStreamProvider.KafkaQueue;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProviderTest
{
    [TestClass]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class KafkaQueueAdapterReceiverUnitTests
    {
        private readonly Logger _logger;
        private readonly KafkaStreamProviderOptions _options;
        private readonly QueueId _id;

        public KafkaQueueAdapterReceiverUnitTests()
        {
            Mock<Logger> loggerMock = new Mock<Logger>();
            _logger = loggerMock.Object;

            var connectionStrings = new List<Uri> {new Uri("http://192.168.10.27:9092")};
            var topicName = "TestTopic";
            var consumerGroupName = "TestConsumerGroup";
            _options = new KafkaStreamProviderOptions(connectionStrings.ToArray(), topicName, consumerGroupName);
            _id = QueueId.GetQueueId("test", 0, 0);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof (ArgumentNullException), "Didn't get ArgumentNull exception for ManualConsumer")]
        public void CtorManualConsumerNullTest()
        {
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, null, _options, factoryMock.Object, _logger);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof(ArgumentNullException), "Didn't get ArgumentNull exception for QueueId")]
        public void CtorQueueIdNullTest()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(null, consumerMock.Object, _options, factoryMock.Object, _logger);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof(ArgumentNullException), "Didn't get ArgumentNull exception for KafkaStreamProviderOptions")]
        public void CtorOptionsNullTest()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, null, factoryMock.Object, _logger);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof(ArgumentNullException), "Didn't get ArgumentNull exception for KafkaBatchFactory")]
        public void CtorFactoryNullTest()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, null, _logger);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof(ArgumentNullException), "Didn't get ArgumentNull exception for Logger")]
        public void CtorLoggerNullTest()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, null);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task InitializeGetLastOffsetTest()
        {
            var wantedOffset = 10;

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            consumerMock.Setup(x => x.FetchLastOffset()).ReturnsAsync(wantedOffset);

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            Assert.AreEqual(wantedOffset, adapterReceiver.CurrentOffset);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task InitializeGetOffsetFromConsumerGroupTest()
        {
            var wantedOffset = 10;

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();
            KafkaStreamProviderOptions mockOptions = new KafkaStreamProviderOptions(_options.ConnectionStrings,
                _options.TopicName, _options.ConsumerGroupName) {ShouldInitWithLastOffset = false};

            consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).ReturnsAsync(wantedOffset);

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, mockOptions, factoryMock.Object, _logger);

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            Assert.AreEqual(wantedOffset, adapterReceiver.CurrentOffset);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task InitializeConsumerGroupDoesntExistTest()
        {
            var wantedOffset = 10;

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();
            KafkaStreamProviderOptions mockOptions = new KafkaStreamProviderOptions(_options.ConnectionStrings,
                _options.TopicName, _options.ConsumerGroupName) {ShouldInitWithLastOffset = false};

            consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).Throws(new KafkaApplicationException("Test") { ErrorCode = (int)ErrorResponseCode.UnknownTopicOrPartition });

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);
            consumerMock.Setup(x => x.FetchLastOffset()).ReturnsAsync(wantedOffset);

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            Assert.AreEqual(wantedOffset, adapterReceiver.CurrentOffset);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task InitializeConsumerGroupDoesntExistRecoveryTest()
        {
            var wantedOffset = 10;
            var savedNewOffset = false;

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();
            KafkaStreamProviderOptions mockOptions = new KafkaStreamProviderOptions(_options.ConnectionStrings,
                _options.TopicName, _options.ConsumerGroupName) {ShouldInitWithLastOffset = false};

            consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).Throws(new KafkaApplicationException("Test") { ErrorCode = (int)ErrorResponseCode.UnknownTopicOrPartition });

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);
            consumerMock.Setup(x => x.FetchLastOffset()).ReturnsAsync(wantedOffset).Callback(() => savedNewOffset=true);            

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            if (savedNewOffset)
            {
                consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).ReturnsAsync(wantedOffset);    
            }

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            Assert.AreEqual(wantedOffset, adapterReceiver.CurrentOffset);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessagesQueueHasOneTest()
        {
            Message message1 = new Message() { Value = new byte[] { 1, 2, 3, 4 }, Meta = new MessageMetadata() { Offset = 0, PartitionId = 0 } };

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IBatchContainer> batchContainerMock = new Mock<IBatchContainer>();
            
            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            factoryMock.Setup(x => x.FromKafkaMessage(It.IsAny<Message>(), It.IsAny<long>())).Returns(batchContainerMock.Object);

            var returnCollection = new List<Message>(){message1};
            consumerMock.Setup(x => x.FetchMessages(1, It.IsAny<long>())).ReturnsAsync(returnCollection);

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            int numToTake = 1;

            var result = await adapterReceiver.GetQueueMessagesAsync(numToTake);
            
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(batchContainerMock.Object, result.FirstOrDefault());
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessagesQueueHasFewTest()
        {
            Message message1 = new Message() { Value = new byte[] { 1, 2, 3, 4 }, Meta = new MessageMetadata() { Offset = 0, PartitionId = 0 } };
            Message message2 = new Message() { Value = new byte[] { 4, 5, 6, 7 }, Meta = new MessageMetadata() { Offset = 1, PartitionId = 0 } };
            Message message3 = new Message() { Value = new byte[] { 8, 9, 10, 11 }, Meta = new MessageMetadata() { Offset = 2, PartitionId = 0 } };

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IBatchContainer> batchContainerMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock3 = new Mock<IBatchContainer>();

            var returnCollection = new List<Message>() { message1, message2, message3 };
            consumerMock.Setup(x => x.FetchMessages(3, It.IsAny<long>())).ReturnsAsync(returnCollection);

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            factoryMock.Setup(x => x.FromKafkaMessage(message1, It.IsAny<long>()))
                .Returns(batchContainerMock1.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message2, It.IsAny<long>()))
                .Returns(batchContainerMock2.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message3, It.IsAny<long>()))
                .Returns(batchContainerMock3.Object);

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);            

            int numToTake = 3;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(batchContainerMock1.Object, result[0]);
            Assert.AreEqual(batchContainerMock2.Object, result[1]);
            Assert.AreEqual(batchContainerMock3.Object, result[2]);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessageQueueIsEmpty()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            
            var returnCollection = new List<Message>();
            consumerMock.Setup(x => x.FetchMessages(3, It.IsAny<long>())).ReturnsAsync(returnCollection);

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            int numToTake = 3;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);           

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessagesRunTwiceTest()
        {
            Message message1 = new Message() { Value = new byte[] { 1, 2, 3, 4 }, Meta = new MessageMetadata() { Offset = 0, PartitionId = 0 } };
            Message message2 = new Message() { Value = new byte[] { 4, 5, 6, 7 }, Meta = new MessageMetadata() { Offset = 1, PartitionId = 0 } };
            Message message3 = new Message() { Value = new byte[] { 8, 9, 10, 11 }, Meta = new MessageMetadata() { Offset = 2, PartitionId = 0 } };
            Message message4 = new Message() { Value = new byte[] { 12, 13, 14, 15 }, Meta = new MessageMetadata() { Offset = 3, PartitionId = 0 } };
            Message message5 = new Message() { Value = new byte[] { 16, 17, 18, 19 }, Meta = new MessageMetadata() { Offset = 4, PartitionId = 0 } };
            Message message6 = new Message() { Value = new byte[] { 21, 22, 23, 24 }, Meta = new MessageMetadata() { Offset = 5, PartitionId = 0 } };

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IBatchContainer> batchContainerMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock3 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock4 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock5 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock6 = new Mock<IBatchContainer>();

            var returnCollection1 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message1, message2, message3 });
            consumerMock.Setup(x => x.FetchMessages(6, 0)).Returns(returnCollection1);

            var returnCollection2 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message4, message5, message6 });
            consumerMock.Setup(x => x.FetchMessages(6, 3)).Returns(returnCollection2);

            consumerMock.Setup(x => x.FetchLastOffset()).Returns(Task.FromResult<long>(0));

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            factoryMock.Setup(x => x.FromKafkaMessage(message1, It.IsAny<long>()))
                .Returns(batchContainerMock1.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message2, It.IsAny<long>()))
                .Returns(batchContainerMock2.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message3, It.IsAny<long>()))
                .Returns(batchContainerMock3.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message4, It.IsAny<long>()))
            .Returns(batchContainerMock4.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message5, It.IsAny<long>()))
                .Returns(batchContainerMock5.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message6, It.IsAny<long>()))
                .Returns(batchContainerMock6.Object);

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            await adapterReceiverMock.Initialize(TimeSpan.MaxValue);

            int numToTake = 6;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(batchContainerMock1.Object, result[0]);
            Assert.AreEqual(batchContainerMock2.Object, result[1]);
            Assert.AreEqual(batchContainerMock3.Object, result[2]);

            result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(batchContainerMock4.Object, result[0]);
            Assert.AreEqual(batchContainerMock5.Object, result[1]);
            Assert.AreEqual(batchContainerMock6.Object, result[2]);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider"), Ignore]
        public async Task GetMessagesQueueSavingBatchAfterFewFetchesTest()
        {
            bool hasCommited = false;

            Message message1 = new Message() { Value = new byte[] { 1, 2, 3, 4 }, Meta = new MessageMetadata() { Offset = 0, PartitionId = 0 } };
            Message message2 = new Message() { Value = new byte[] { 4, 5, 6, 7 }, Meta = new MessageMetadata() { Offset = 1, PartitionId = 0 } };
            Message message3 = new Message() { Value = new byte[] { 8, 9, 10, 11 }, Meta = new MessageMetadata() { Offset = 2, PartitionId = 0 } };
            Message message4 = new Message() { Value = new byte[] { 12, 13, 14, 15 }, Meta = new MessageMetadata() { Offset = 3, PartitionId = 0 } };
            Message message5 = new Message() { Value = new byte[] { 16, 17, 18, 19 }, Meta = new MessageMetadata() { Offset = 4, PartitionId = 0 } };
            Message message6 = new Message() { Value = new byte[] { 21, 22, 23, 24 }, Meta = new MessageMetadata() { Offset = 5, PartitionId = 0 } };

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IBatchContainer> batchContainerMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock3 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock4 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock5 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock6 = new Mock<IBatchContainer>();

            var returnCollection1 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message1 });
            consumerMock.Setup(x => x.FetchMessages(6, 0)).Returns(returnCollection1);

            var returnCollection2 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message2, message3});
            consumerMock.Setup(x => x.FetchMessages(6, 1)).Returns(returnCollection2);

            var returnCollection3 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message4, message5, message6 });
            consumerMock.Setup(x => x.FetchMessages(6, 3)).Returns(returnCollection3);

            consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).Returns(Task.FromResult<long>(0));

            consumerMock.Setup(x => x.UpdateOrCreateOffset(_options.ConsumerGroupName, 3)).Returns(TaskDone.Done)
                .Callback(() => hasCommited = true);

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            factoryMock.Setup(x => x.FromKafkaMessage(message1, It.IsAny<long>()))
                .Returns(batchContainerMock1.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message2, It.IsAny<long>()))
                .Returns(batchContainerMock2.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message3, It.IsAny<long>()))
                .Returns(batchContainerMock3.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message4, It.IsAny<long>()))
            .Returns(batchContainerMock4.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message5, It.IsAny<long>()))
                .Returns(batchContainerMock5.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message6, It.IsAny<long>()))
                .Returns(batchContainerMock6.Object);

            var newOptions = new KafkaStreamProviderOptions(_options.ConnectionStrings, _options.TopicName, _options.ConsumerGroupName){OffsetCommitInterval = 2, ShouldInitWithLastOffset = false};

            KafkaQueueAdapterReceiver adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, newOptions, factoryMock.Object, _logger);

            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            int numToTake = 6;

            // Asking once, should return 1 message
            var result = await adapterReceiver.GetQueueMessagesAsync(6);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(batchContainerMock1.Object, result[0]);

            // Now sth has happened, restating the adapter, the offset should not be saved
            adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, newOptions, factoryMock.Object, _logger);
            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            // Asking once again, should return 1 message
            result = await adapterReceiver.GetQueueMessagesAsync(6);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(batchContainerMock1.Object, result[0]);

            // Asking second again, should return 2 messages and commit
            result = await adapterReceiver.GetQueueMessagesAsync(6);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(batchContainerMock2.Object, result[0]);
            Assert.AreEqual(batchContainerMock3.Object, result[1]);

            if (hasCommited)
            {
                consumerMock.Setup(x => x.FetchOffset(_options.ConsumerGroupName)).Returns(Task.FromResult<long>(3));
            }

            // Now sth happened, but this time the offset was committed
            adapterReceiver = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, newOptions, factoryMock.Object, _logger);
            await adapterReceiver.Initialize(TimeSpan.MaxValue);

            // This request should return the last 3 messages
            result = await adapterReceiver.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(batchContainerMock4.Object, result[0]);
            Assert.AreEqual(batchContainerMock5.Object, result[1]);
            Assert.AreEqual(batchContainerMock6.Object, result[2]);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessagesBufferUnderflowRecoveryTest()
        {
            Message message1 = new Message() { Value = new byte[] { 1, 2, 3, 4 }, Meta = new MessageMetadata(){Offset = 0, PartitionId = 0}};
            Message message2 = new Message() { Value = new byte[] { 4, 5, 6, 7 }, Meta = new MessageMetadata() { Offset = 1, PartitionId = 0 } };
            Message message3 = new Message() { Value = new byte[] { 8, 9, 10, 11 }, Meta = new MessageMetadata(){Offset = 2, PartitionId = 0}};

            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();
            Mock<IBatchContainer> batchContainerMock1 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock2 = new Mock<IBatchContainer>();
            Mock<IBatchContainer> batchContainerMock3 = new Mock<IBatchContainer>();

            var returnCollection1 = Task.FromResult<IEnumerable<Message>>(new List<Message>() { message1, message2, message3 });

            // The setup!
            consumerMock.Setup(x => x.FetchMessages(It.IsAny<int>(), 0)).Throws(new BufferUnderRunException(10,10));
            consumerMock.Setup(x => x.FetchMessages(It.IsInRange(3, Int32.MaxValue, Range.Inclusive), 1)).Returns(returnCollection1);

            consumerMock.Setup(x => x.FetchLastOffset()).ReturnsAsync(0);

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            factoryMock.Setup(x => x.FromKafkaMessage(message1, It.IsAny<long>()))
                .Returns(batchContainerMock1.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message2, It.IsAny<long>()))
                .Returns(batchContainerMock2.Object);
            factoryMock.Setup(x => x.FromKafkaMessage(message3, It.IsAny<long>()))
                .Returns(batchContainerMock3.Object);

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            await adapterReceiverMock.Initialize(TimeSpan.MaxValue);

            int numToTake = 3;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(0, result.Count);

            result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(batchContainerMock1.Object, result[0]);
            Assert.AreEqual(batchContainerMock2.Object, result[1]);
            Assert.AreEqual(batchContainerMock3.Object, result[2]);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        public async Task GetMessageFailGotNull()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();

            consumerMock.Setup(x => x.FetchMessages(It.IsAny<int>(), It.IsAny<long>())).ReturnsAsync(null);

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            int numToTake = 3;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod, TestCategory("UnitTest"), TestCategory("KafkaStreamProvider")]
        [ExpectedException(typeof(Exception))]
        public async Task GetMessageFailFetchTaskThrewException()
        {
            Mock<IManualConsumer> consumerMock = new Mock<IManualConsumer>();

            consumerMock.Setup(x => x.FetchMessages(It.IsAny<int>(), It.IsAny<long>())).Throws(new Exception("Oh noes!"));

            Mock<IKafkaBatchFactory> factoryMock = new Mock<IKafkaBatchFactory>();

            KafkaQueueAdapterReceiver adapterReceiverMock = new KafkaQueueAdapterReceiver(_id, consumerMock.Object, _options, factoryMock.Object, _logger);

            int numToTake = 3;

            var result = await adapterReceiverMock.GetQueueMessagesAsync(numToTake);
        }
    }
}
