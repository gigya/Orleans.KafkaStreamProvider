using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans.TestingHost;
using Orleans.TestingHost.Utils;
using TestGrainInterfaces;
using UnitTests.GrainInterfaces;

namespace Tester.StreamingTests
{
    public class MultipleStreamsTestRunner
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public MultipleStreamsTestRunner(string streamProviderName, Logger logger)
        {
            if (string.IsNullOrEmpty(streamProviderName)) throw new ArgumentNullException("streamProviderName");

            _streamProviderName = streamProviderName;
            _logger = logger;
        }

        public async Task MultipleStreamsSameNamespaceTest(string streamNamespace)
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var secondProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await consumer.BecomeConsumer(firstStream, streamNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await consumer.BecomeConsumer(secondStream, streamNamespace, _streamProviderName);
            
            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);
            await secondProducer.BecomeProducer(secondStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            await Task.WhenAll(firstProducer.StartPeriodicProducing(), secondProducer.StartPeriodicProducing());

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(1000));

            // Stopping
            await Task.WhenAll(firstProducer.StopPeriodicProducing(), secondProducer.StopPeriodicProducing());

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(firstProducer, consumer, 2, firstHandle,lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(secondProducer, consumer, 2, secondHandle, lastTry), Timeout));

            // unsubscribe
            await consumer.StopConsuming(firstHandle);
            await consumer.StopConsuming(secondHandle);
        }

        public async Task MultipleStreamsDifferentNamespaceTest()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var secondProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            var firstNamespace = "MultipleStreamsDifferentNamepsaceFirst";
            var secondNamespace = "MultipleStreamsDifferentNamepsaceSecond";

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await consumer.BecomeConsumer(firstStream, firstNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await consumer.BecomeConsumer(secondStream, secondNamespace, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, firstNamespace, _streamProviderName);
            await secondProducer.BecomeProducer(secondStream, secondNamespace, _streamProviderName);

            // Starting to produce!
            //await Task.WhenAll(firstProducer.StartPeriodicProducing(), secondProducer.StartPeriodicProducing());
            await firstProducer.StartPeriodicProducing();
            await secondProducer.StartPeriodicProducing();

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            // Stopping
            //await Task.WhenAll(firstProducer.StopPeriodicProducing(), secondProducer.StopPeriodicProducing());

            await firstProducer.StopPeriodicProducing();
            await secondProducer.StopPeriodicProducing();

            var firstProduced = await firstProducer.GetNumberProduced();
            var secondProduced = await secondProducer.GetNumberProduced();

            _logger.Info("Producer number 1 produced {0} messages", firstProduced);
            _logger.Info("Producer number 2 produced {0} messages", secondProduced);

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(firstProducer, consumer, 2, firstHandle, lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(secondProducer, consumer, 2, secondHandle, lastTry), Timeout));

            // unsubscribe
            await consumer.StopConsuming(firstHandle);
            await consumer.StopConsuming(secondHandle);
        }

        public async Task MultipleStreamsDifferentNamespcaeDifferentConsumers()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var secondProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var firstConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());
            var secondConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            var firstNamespace = "MultipleStreamsDifferentNamepsaceFirst";
            var secondNamespace = "MultipleStreamsDifferentNamepsaceSecond";

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await firstConsumer.BecomeConsumer(firstStream, firstNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await secondConsumer.BecomeConsumer(secondStream, secondNamespace, _streamProviderName);

            // Becoming a producer
            Task.WaitAll(firstProducer.BecomeProducer(firstStream, firstNamespace, _streamProviderName), secondProducer.BecomeProducer(secondStream, secondNamespace, _streamProviderName));

            // Starting to produce!
            await Task.WhenAll(firstProducer.StartPeriodicProducing(), secondProducer.StartPeriodicProducing());

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            // Stopping
            await Task.WhenAll(firstProducer.StopPeriodicProducing(), secondProducer.StopPeriodicProducing());

            _logger.Info("Producer number 1 produced {0} messages", await firstProducer.GetNumberProduced());
            _logger.Info("Producer number 2 produced {0} messages", await secondProducer.GetNumberProduced());

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(firstProducer, firstConsumer, 1, null, lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(secondProducer, secondConsumer, 1, null, lastTry), Timeout));

            // unsubscribe
            await firstConsumer.StopConsuming(firstHandle);
            await secondConsumer.StopConsuming(secondHandle);
        }

        public async Task MultipleProducersSameGrainSameNamespaceDifferentConsumers()
        {
            // get producer and consumer
            var producer = GrainClient.GrainFactory.GetGrain<IMultipleProducingProducerGrain>(Guid.NewGuid());
            var firstConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());
            var secondConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            var streamNamespace = "MultipleProducersSameGrainSameNamespaceDifferentConsumers";

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await firstConsumer.BecomeConsumer(firstStream, streamNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await secondConsumer.BecomeConsumer(secondStream, streamNamespace, _streamProviderName);

            // Becoming a producer
            var firstKey = await producer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);
            var secondKey = await producer.BecomeProducer(secondStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            await producer.StartPeriodicProducing(firstKey);
            await producer.StartPeriodicProducing(secondKey);

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            // Stopping
            await producer.StopPeriodicProducing(firstKey);
            await producer.StopPeriodicProducing(secondKey);

            var numProduced = await producer.GetNumberProduced();

            _logger.Info("Producer number 1 produced {0} messages", numProduced.Values.First());
            _logger.Info("Producer number 2 produced {0} messages", numProduced.Values.Last());

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, firstConsumer, 2, 1, lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, secondConsumer, 2, 1, lastTry), Timeout));

            // unsubscribe
            await firstConsumer.StopConsuming(firstHandle);
            await secondConsumer.StopConsuming(secondHandle);
        }

        public async Task MultipleProducersSameGrainDifferentNamespaceDifferentConsumers()
        {
            // get producer and consumer
            var producer = GrainClient.GrainFactory.GetGrain<IMultipleProducingProducerGrain>(Guid.NewGuid());
            var firstConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());
            var secondConsumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            var streamFirstNamespace = "FirstMultipleProducersSameGrainSameNamespaceDifferentConsumers";
            var streamSecondNamespace = "SecondMultipleProducersSameGrainSameNamespaceDifferentConsumers";

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await firstConsumer.BecomeConsumer(firstStream, streamFirstNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await secondConsumer.BecomeConsumer(secondStream, streamSecondNamespace, _streamProviderName);

            // Becoming a producer
            var firstKey = await producer.BecomeProducer(firstStream, streamFirstNamespace, _streamProviderName);
            var secondKey = await producer.BecomeProducer(secondStream, streamSecondNamespace, _streamProviderName);

            // Starting to produce!
            await producer.StartPeriodicProducing(firstKey);
            await producer.StartPeriodicProducing(secondKey);

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            // Stopping
            await producer.StopPeriodicProducing(firstKey);
            await producer.StopPeriodicProducing(secondKey);

            var numProduced = await producer.GetNumberProduced();

            _logger.Info("Producer number 1 produced {0} messages", numProduced.Values.First());
            _logger.Info("Producer number 2 produced {0} messages", numProduced.Values.Last());

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, firstConsumer, 2, 1, lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, secondConsumer, 2, 1, lastTry), Timeout));

            // unsubscribe
            await firstConsumer.StopConsuming(firstHandle);
            await secondConsumer.StopConsuming(secondHandle);
        }

        public async Task MultipleProducersSameGrainDifferentNamespaceSameConsumer()
        {
            // get producer and consumer
            var producer = GrainClient.GrainFactory.GetGrain<IMultipleProducingProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<IMultipleSubscriptionConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            Guid secondStream = Guid.NewGuid();

            var streamFirstNamespace = "FirstMultipleProducersSameGrainSameNamespaceDifferentConsumers";
            var streamSecondNamespace = "SecondMultipleProducersSameGrainSameNamespaceDifferentConsumers";

            // subscribing to different streams on the same namespace
            StreamSubscriptionHandle<int> firstHandle = await consumer.BecomeConsumer(firstStream, streamFirstNamespace, _streamProviderName);
            StreamSubscriptionHandle<int> secondHandle = await consumer.BecomeConsumer(secondStream, streamSecondNamespace, _streamProviderName);

            // Becoming a producer
            var firstKey = await producer.BecomeProducer(firstStream, streamFirstNamespace, _streamProviderName);
            var secondKey = await producer.BecomeProducer(secondStream, streamSecondNamespace, _streamProviderName);

            // Starting to produce!
            await producer.StartPeriodicProducing(firstKey);
            await producer.StartPeriodicProducing(secondKey);

            // Waiting a bit..
            await Task.Delay(TimeSpan.FromMilliseconds(3000));

            // Stopping
            await producer.StopPeriodicProducing(firstKey);
            await producer.StopPeriodicProducing(secondKey);

            var numProduced = await producer.GetNumberProduced();

            _logger.Info("Producer number 1 produced {0} messages", numProduced.Values.First());
            _logger.Info("Producer number 2 produced {0} messages", numProduced.Values.Last());

            // Checking
            await
                Task.WhenAll(
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, consumer, 2, 2, lastTry), Timeout),
                    TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, consumer, 2, 2, lastTry), Timeout));

            // unsubscribe
            await consumer.StopConsuming(firstHandle);
            await consumer.StopConsuming(secondHandle);
        }

        private async Task<bool> CheckCounters(ISampleStreaming_ProducerGrain producer, IMultipleSubscriptionConsumerGrain consumer, int consumerCount, StreamSubscriptionHandle<int> handler, bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();
            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced > 0, "Events were not produced");
                Assert.AreEqual(consumerCount, numConsumed.Count, "Incorrect number of consumers");
                if (handler != null)
                {
                    var numHandlerConsumed = numConsumed[handler];
                    Assert.AreEqual(numProduced, numHandlerConsumed, "Producer and consumer do not match");
                }
                else
                {
                    foreach (int consumed in numConsumed.Values)
                    {
                        Assert.AreEqual(numProduced, consumed, "Produced and consumed counts do not match");
                    }
                }
            }
            else if (numProduced <= 0 || // no events produced?
                     consumerCount != numConsumed.Count || // subscription counts are wrong?
                     handler == null ? numConsumed.Values.All(consumedCount => consumedCount != numProduced) : numConsumed[handler] != numProduced) // consumed events don't match produced events for any subscription or specific handler (if sent)?
            {
                if (numProduced <= 0)
                {
                    _logger.Info("numProduced <= 0: Events were not produced");
                }
                if (consumerCount != numConsumed.Count)
                {
                    _logger.Info("consumerCount != numConsumed.Count: Incorrect number of consumers. consumerCount = {0}, numConsumed.Count = {1}",
                        consumerCount, numConsumed.Count);
                }
                foreach (int consumed in numConsumed.Values)
                {
                    if (numProduced != consumed)
                    {
                        _logger.Info("numProduced != consumed: Produced and consumed counts do not match. numProduced = {0}, consumed = {1}",
                            numProduced, consumed);
                        //numProduced, Utils.DictionaryToString(numConsumed));
                    }
                }
                return false;
            }

            if (handler != null)
            {
                _logger.Info("All counts are equal. numProduced = {0}, numHandlerConsumed = {1}, consumersCount = {2}", numProduced, numConsumed[handler], consumerCount); //Utils.DictionaryToString(numConsumed));    
            }
            else
            {
                _logger.Info("All counts are equal. numProduced = {0}, consumersCount = {1}", numProduced, consumerCount); //Utils.DictionaryToString(numConsumed));    
            }
         
            return true;
        }

        private async Task<bool> CheckCounters(IMultipleProducingProducerGrain producer, IMultipleSubscriptionConsumerGrain consumer, int producerCount, int consumerCount, bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();

            var consumed = numConsumed.First().Value;

            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced.Any(pair => pair.Value > 0), "Events were not produced");
                Assert.AreEqual(consumerCount, numConsumed.Count, "Incorrect number of consumers");
                Assert.AreEqual(producerCount, numProduced.Count, "Incorrect number of producers");

                foreach (int produced in numProduced.Values)
                {
                    Assert.AreEqual(produced, consumed, "Produced and Consumed counts do not match");
                }
            }
            else if (numProduced.Any(pair => pair.Value <= 0) || // no events produced?
                     consumerCount != numConsumed.Count || // subscription counts are wrong?
                     producerCount != numProduced.Count ||
                     numProduced.Values.All(producedCount => producedCount != consumed)) // consumed events don't match produced events for any subscription or specific handler (if sent)?
            {
                if (numProduced.Any(pair => pair.Value <= 0))
                {
                    _logger.Info("numProduced <= 0: Events were not produced");
                }
                if (consumerCount != numConsumed.Count)
                {
                    _logger.Info("consumerCount != numConsumed.Count: Incorrect number of consumers. consumerCount = {0}, numConsumed.Count = {1}",
                        consumerCount, numConsumed.Count);
                }
                if (producerCount != numProduced.Count)
                {
                    _logger.Info("producerCount != numProduced.Count: Incorrect number of producer. producerCount = {0}, numProduced.Count = {1}",
                        producerCount, numProduced.Count);
                }

                foreach (var produced in numProduced.Values)
                {
                    if (produced != consumed)
                    {
                        _logger.Info("numProduced != consumed: Produced and consumed counts do not match. numProduced = {0}, consumed = {1}",
                            produced, consumed);
                        //numProduced, Utils.DictionaryToString(numConsumed));
                    }
                }
                return false;
            }

            _logger.Info("All counts are equal. numProduced = {0}, consumersCount = {1}", numProduced, consumerCount); //Utils.DictionaryToString(numConsumed));    

            return true;
        }
    }
}
