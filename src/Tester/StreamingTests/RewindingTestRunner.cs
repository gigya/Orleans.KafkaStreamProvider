using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using Orleans.TestingHost.Utils;
using TestGrainInterfaces;
using UnitTests.GrainInterfaces;

namespace Tester.StreamingTests
{
    public class RewindingTestRunner
    {
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public RewindingTestRunner(string streamProviderName, Logger logger)
        {
            if (string.IsNullOrEmpty(streamProviderName)) throw new ArgumentNullException("streamProviderName");

            _streamProviderName = streamProviderName;
            _logger = logger;
        }

        public async Task SubscribingWithOldSequenceToken()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, consumer,  lastTry),
                    TimeSpan.FromSeconds(30));

            await consumer.StopConsuming();

            // Resubscribing with token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[numToProduce - 2];

            await
                consumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + 3, consumer, lastTry), TimeSpan.FromSeconds(30));
        }

        public async Task SubscribingWithOldSequenceTokenAfterBatchSendingOnlyOneBatch()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 1;
            int batchToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce(batchToProduce);
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce * batchToProduce, consumer, lastTry),
                    TimeSpan.FromSeconds(30));

            await consumer.StopConsuming();

            // Resubscribing with token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[batchToProduce - 2];

            await
                consumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters((numToProduce * batchToProduce * 2), consumer, lastTry), TimeSpan.FromSeconds(30));
        }

        public async Task SubscribingWithOldSequenceTokenAfterBatchSending()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<IBatchProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 1;
            int batchToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce(batchToProduce);
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce * batchToProduce, consumer, lastTry),
                    TimeSpan.FromSeconds(30));

            await consumer.StopConsuming();

            // Resubscribing with token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[batchToProduce - 2];

            // Producing just one more tiny message
            await firstProducer.Produce(1);

            await
                consumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters((numToProduce * batchToProduce * 2) + 1, consumer, lastTry), TimeSpan.FromSeconds(30));
        }

        public async Task SubscribingWithTooOldSequenceToken()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, consumer, lastTry),
                    TimeSpan.FromSeconds(30));

            await consumer.StopConsuming();

            // This will make sure next produce will kick the messages away
            await Task.Delay(TimeSpan.FromSeconds(6));

            // Getting the old token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[numToProduce - 2];

            // Prodcing the new message
            int secnodNumToProduce = 2;

            for (int i = 1; i <= secnodNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            // We have to wait a bit for the pulling agent to get the new messages and throw the old ones.
            await Task.Delay(TimeSpan.FromSeconds(1));

            await
                consumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secnodNumToProduce, consumer, lastTry), TimeSpan.FromSeconds(30));
        }

        public async Task SubscribingWithTooOldSequenceTokenForFirstTime()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, consumer, lastTry),
                    TimeSpan.FromSeconds(30));

            await consumer.StopConsuming();

            // Getting the old token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[numToProduce - 2];

            // Producing the new message
            int secnodNumToProduce = 2;

            for (int i = 1; i <= secnodNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            // We have to wait a bit for the pulling agent to get the new messages and throw the old ones.
            await Task.Delay(TimeSpan.FromSeconds(2));

            var newConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            await
                newConsumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce - 2 + secnodNumToProduce, newConsumer, lastTry), TimeSpan.FromSeconds(15));

            await newConsumer.StopConsuming();
        }

        public async Task SubscribingMultipleConsumersDifferentTokens()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();

            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await consumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 5;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, consumer, lastTry),
                    TimeSpan.FromSeconds(30));

            // Getting the old token
            var tokens = await consumer.GetReceivedTokens();
            var wantedToken = tokens[numToProduce - 2];

            // Prodcing the new message
            int secnodNumToProduce = 2;

            for (int i = 1; i <= secnodNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            // We have to wait a bit for the pulling agent to get the new messages and throw the old ones.
            await Task.Delay(TimeSpan.FromSeconds(2));

            var newConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            await
                newConsumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.Zero, _streamProviderName);

            await Task.WhenAll(TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce - 2 + secnodNumToProduce, newConsumer, lastTry), TimeSpan.FromSeconds(15)),
                               TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secnodNumToProduce, consumer, lastTry), TimeSpan.FromSeconds(15)));

            await Task.WhenAll(newConsumer.StopConsuming(), consumer.StopConsuming());
        }

        private async Task<bool> CheckCounters(int numProduced, ITimedConsumerGrain consumer, bool assertIsTrue)
        {
            var numConsumed = await consumer.GetNumberConsumed();

            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced > 0, "Events were not produced");
                Assert.AreEqual(numProduced, numConsumed, "Produced and Consumed counts do not match");
            }
            else if (numProduced <= 0 || // no events produced?
                     numProduced != numConsumed) // consumed events don't match produced events for any subscription or specific handler (if sent)?
            {
                if (numProduced <= 0)
                {
                    _logger.Info("numProduced <= 0: Events were not produced");
                }

                if (numProduced != numConsumed)
                {
                    _logger.Info("numProduced != consumed: Produced and consumed counts do not match. numProduced = {0}, consumed = {1}",
                        numProduced, numConsumed);
                }
                
                return false;
            }

            _logger.Info("All counts are equal. numProduced = {0}, consumersCount = {1}", numProduced, numConsumed); //Utils.DictionaryToString(numConsumed));    

            return true;
        }
    }
}
