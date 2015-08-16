using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans.TestingHost;
using TestGrainInterfaces;
using UnitTests.GrainInterfaces;

namespace Tester.StreamingTests
{
    public class BatchProducingTestRunner
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(45);
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public BatchProducingTestRunner(string streamProviderName, Logger logger)
        {
            if (streamProviderName == null) throw new ArgumentNullException("streamProviderName");

            _streamProviderName = streamProviderName;
            _logger = logger;   
        }

        public async Task SimpleBatchTesting()
        {
            var proudcer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ConsumerGrain>(Guid.NewGuid());
            var batchSize = 10;

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "BatchTestStream";

            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.StartPeriodicBatchProducing(batchSize);
            
            await Task.Delay(TimeSpan.FromSeconds(3));

            await proudcer.StopPeriodicBatchProducing();
            
            _logger.Info("Producer produced {0} messages", await proudcer.GetNumberProduced());

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounter(proudcer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        public async Task BatchTestMultipleConsumersOneStream()
        {
            var proudcer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());
            var batchSize = 10;

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "BatchTestStream";

            var firstHandler = await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);
            var secondHandler = await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.StartPeriodicBatchProducing(batchSize);

            await Task.Delay(TimeSpan.FromSeconds(3));

            await proudcer.StopPeriodicBatchProducing();

            _logger.Info("Producer produced {0} messages", await proudcer.GetNumberProduced());

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounter(proudcer, consumer, 2, lastTry), Timeout);

            await consumer.StopConsuming(firstHandler);
            await consumer.StopConsuming(secondHandler);
        }

        public async Task BatchTestMultipleStreamsOneConsumer()
        {
            var firstProducer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var secondProducer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());
            var batchSize = 10;

            var firstStreamGuid = Guid.NewGuid();
            var secondStreamGuid = Guid.NewGuid();
            var firstStreamNamespace = "FirstBatchTestStream";
            var secondStreamNamespace = "SecondBatchTestStream";

            var firstHandler = await consumer.BecomeConsumer(firstStreamGuid, firstStreamNamespace, _streamProviderName);
            var secondHandler = await consumer.BecomeConsumer(secondStreamGuid, secondStreamNamespace, _streamProviderName);

            Task.WaitAll(firstProducer.BecomeProducer(firstStreamGuid, firstStreamNamespace, _streamProviderName),
                         secondProducer.BecomeProducer(secondStreamGuid, secondStreamNamespace, _streamProviderName));

            Task.WaitAll(firstProducer.StartPeriodicBatchProducing(batchSize), secondProducer.StartPeriodicBatchProducing(10));

            await Task.Delay(TimeSpan.FromSeconds(3));

            Task.WaitAll(firstProducer.StopPeriodicBatchProducing(), secondProducer.StopPeriodicBatchProducing());

            _logger.Info("First Producer produced {0} messages", await firstProducer.GetNumberProduced());
            _logger.Info("Second Producer produced {0} messages", await secondProducer.GetNumberProduced());

            Task.WaitAll(TestingUtils.WaitUntilAsync(lastTry => CheckCounter(firstProducer, consumer, firstHandler, lastTry), Timeout),
                         TestingUtils.WaitUntilAsync(lastTry => CheckCounter(secondProducer, consumer, secondHandler, lastTry), Timeout));

            await consumer.StopConsuming(firstHandler);
            await consumer.StopConsuming(secondHandler);
        }

        public async Task BatchTestLoadTesting()
        {
            var proudcer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ConsumerGrain>(Guid.NewGuid());
            var batchSize = 1000;

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "BatchTestStream";

            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);

            await proudcer.StartPeriodicBatchProducing(batchSize);

            await Task.Delay(TimeSpan.FromSeconds(3));

            await proudcer.StopPeriodicBatchProducing();

            _logger.Info("Producer produced {0} messages", await proudcer.GetNumberProduced());

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounter(proudcer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        public async Task BatchTestMultipleProducers(int numProducers, int batchSize)
        {
            List<IBatchProducerGrain> producersList = new List<IBatchProducerGrain>(numProducers);

            for (int i = 0; i < numProducers; i++)
            {
                producersList.Add(GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid()));
            }

            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ConsumerGrain>(Guid.NewGuid());

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "BatchTestStream";

            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);
            
            foreach (var producer in producersList)
            {
                await producer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);
            }
            
            foreach (var producer in producersList)
            {
                await producer.StartPeriodicBatchProducing(batchSize);
            }

            _logger.Info("***************Starting to Produce!*****************");

            await Task.Delay(TimeSpan.FromSeconds(1));

            var sum = 0;

            foreach (var producer in producersList)
            {
                await producer.StopPeriodicBatchProducing();
                _logger.Info("Producer produced {0} messages", await producer.GetNumberProduced());
                var numProduced = await producer.GetNumberProduced();
                sum += numProduced;
            }          

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounter(sum, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        private async Task<bool> CheckCounter(IBatchProducerGrain producer, IMultipleSubscriptionConsumerGrain consumer,
            StreamSubscriptionHandle<int> handler, bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var consumers = await consumer.GetNumberConsumed();
            var numConsumed = consumers[handler];
            
            _logger.Info("Checking now for handler for stream: {0}", handler.StreamIdentity.ToString());

            return await CheckCounter(numProduced, numConsumed, assertIsTrue);
        }

        private async Task<bool> CheckCounter(int numProduced, ISampleStreaming_ConsumerGrain consumer, bool assertIsTrue)
        {
            var numConsumed = await consumer.GetNumberConsumed();

            return await CheckCounter(numProduced, numConsumed, assertIsTrue);
        }

        private Task<bool> CheckCounter(int numProduced, int numConsumed, bool assertIsTrue)
        {
            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced > 0, "Nothing was produced");
                Assert.AreEqual(numProduced, numConsumed,
                    "Number of messages produced for handler does not match number of messages consumed, Produced: {0}, Consumed: {1}",
                    numProduced, numConsumed);
            }
            else if (numProduced <= 0 ||
                     numConsumed != numProduced)
            {
                if (numProduced <= 0)
                {
                    _logger.Info("Producer hasn't produced yet");
                }
                if (numConsumed != numProduced)
                {
                    _logger.Info("Num consumed does not match num produced yet. Produced: {0}, Consumed: {1}", numProduced,
                        numConsumed);
                }

                return Task.FromResult(false);
            }

            _logger.Info("All counts are equal. Produced = {0}, Consumed = {1}", numProduced, numConsumed);

            return Task.FromResult(true);
        }        

        private async Task<bool> CheckCounter(IBatchProducerGrain producer, ISampleStreaming_ConsumerGrain consumer, bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();

            return await CheckCounter(numProduced, numConsumed, assertIsTrue);
        }

        private async Task<bool> CheckCounter(IBatchProducerGrain producer, IMultipleSubscriptionConsumerGrain consumer, int consumersCount,
            bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();

            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced > 0, "No messages produced");
                Assert.AreEqual(consumersCount, numConsumed.Count, "Number of consumers is incorrect" );

                foreach (int consumed in numConsumed.Values)
                {
                    Assert.AreEqual(numProduced, consumed, "Produced and consumed counts do not match");
                }
            }
            else if (numProduced <= 0 ||
                     numConsumed.Count != consumersCount ||
                     numConsumed.Values.Any(consumed => consumed != numProduced))
            {
                if (numProduced <= 0)
                {
                    _logger.Info("Noting produced yet..");
                }
                if (numConsumed.Count != consumersCount)
                {
                    _logger.Info("Consumer num doesn't match yet..");
                }
                foreach (var consumed in numConsumed.Values)
                {
                    if (consumed != numProduced)
                    {
                        _logger.Info("numProduced != consumed: Produced and consumed counts do not match. numProduced = {0}, consumed = {1}",
                            numProduced, consumed);
                    }
                }

                return false;
            }

            _logger.Info("All counts are equal. numProduced = {0}, consumersCount = {1}, Total consumed = {2}", numProduced, consumersCount, numConsumed.Values.Sum());

            return true;
        }
    }
}
