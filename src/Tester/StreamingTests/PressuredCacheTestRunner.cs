using System;
using System.Linq;
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
    /// <summary>
    /// Testing for cases when the cache is under pressure due to slow consumers. The assumption here is that the Cache TimeSpan is 5sec and the size is 5 messages.
    /// </summary>
    public class PressuredCacheTestRunner
    {
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public PressuredCacheTestRunner(string streamProviderName, Logger logger)
        {
            if (string.IsNullOrEmpty(streamProviderName)) throw new ArgumentNullException("streamProviderName");

            _streamProviderName = streamProviderName;
            _logger = logger;
        }

        public async Task CacheIsUnderPressureDueToTimeSpanGuarantee()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            //var regularConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());
            var slowConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            //await regularConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);
            await slowConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.FromSeconds(1), _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 10;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                Task.WhenAll(//TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, regularConsumer, lastTry), TimeSpan.FromSeconds(60)),
                             TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, slowConsumer, lastTry), TimeSpan.FromSeconds(60)));

            await Task.WhenAll(slowConsumer.StopConsuming());//, regularConsumer.StopConsuming());
        }

        public async Task CacheIsUnderPressureDueToSlowConsumer()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var regularConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());
            var slowConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await regularConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);
            await slowConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.FromSeconds(4), _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int firstNumToProduce = 5;

            for (int i = 1; i <= firstNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await Task.Delay(TimeSpan.FromSeconds(5));

            int secondNumToProduce = 5;
            for (int i = 1; i <= secondNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                Task.WhenAll(TestingUtils.WaitUntilAsync(lastTry => CheckCounters(firstNumToProduce + secondNumToProduce, regularConsumer, lastTry), TimeSpan.FromSeconds(60)),
                             TestingUtils.WaitUntilAsync(lastTry => CheckCounters(firstNumToProduce + secondNumToProduce, slowConsumer, lastTry), TimeSpan.FromSeconds(60)));

            await Task.WhenAll(slowConsumer.StopConsuming(), regularConsumer.StopConsuming());
        }

        public async Task CacheIsUnderPressreDueToLateSubscriber()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var regularConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());
            var slowConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await regularConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);            

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 1;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, regularConsumer, lastTry),
                    TimeSpan.FromSeconds(30));

            var wantedToken = (await regularConsumer.GetReceivedTokens()).Values.First();

            int secondNumToProduce = 4;

            for (int i = 1; i <= secondNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await slowConsumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.FromSeconds(2), _streamProviderName);

            int thirdNumToProduce = 5;

            for (int i = 1; i <= thirdNumToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await
                Task.WhenAll(TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secondNumToProduce + thirdNumToProduce, regularConsumer, lastTry), TimeSpan.FromSeconds(30)),
                             TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secondNumToProduce + thirdNumToProduce, slowConsumer, lastTry), TimeSpan.FromSeconds(30)));

            await Task.WhenAll(slowConsumer.StopConsuming(), regularConsumer.StopConsuming());
        }

        public async Task CacheIsOvertimedDueToSlowConsumer()
        {
            // get producer and consumer
            var firstProducer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            var regularConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());
            var slowConsumer = GrainClient.GrainFactory.GetGrain<ITimedConsumerGrain>(Guid.NewGuid());

            Guid firstStream = Guid.NewGuid();
            string streamNamespace = "RewindingTestNamespace";

            // subscribing to different streams on the same namespace
            await regularConsumer.BecomeConsumer(firstStream, streamNamespace, null, TimeSpan.Zero, _streamProviderName);

            // time to produce
            await firstProducer.BecomeProducer(firstStream, streamNamespace, _streamProviderName);

            // Starting to produce!
            int numToProduce = 4;

            for (int i = 1; i <= numToProduce; i++)
            {
                await firstProducer.Produce();
            }

            await Task.Delay(5);

            await
                TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce, regularConsumer, lastTry),
                    TimeSpan.FromSeconds(30));

            var wantedToken = (await regularConsumer.GetReceivedTokens()).Values.First();

            await slowConsumer.BecomeConsumer(firstStream, streamNamespace, wantedToken, TimeSpan.FromSeconds(3), _streamProviderName);

            int secondNumToProduce = 1;

            for (int i = 1; i <= secondNumToProduce; i++)
            {
                await firstProducer.Produce();
            }            

            await
                Task.WhenAll(TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secondNumToProduce, regularConsumer, lastTry), TimeSpan.FromSeconds(30)),
                             TestingUtils.WaitUntilAsync(lastTry => CheckCounters(numToProduce + secondNumToProduce, slowConsumer, lastTry), TimeSpan.FromSeconds(30)));

            await Task.WhenAll(slowConsumer.StopConsuming(), regularConsumer.StopConsuming());
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
