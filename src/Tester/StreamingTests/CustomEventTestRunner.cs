using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using Orleans.TestingHost.Utils;
using TestGrainInterfaces;

namespace Tester.StreamingTests
{
    public class CustomEventTestRunner
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
        private readonly string _streamProviderName;
        private readonly Logger _logger;

        public CustomEventTestRunner(string streamProviderName, Logger logger)
        {
            if (streamProviderName == null) throw new ArgumentNullException("streamProviderName");

            _streamProviderName = streamProviderName;
            _logger = logger;  
        }

        public async Task SimpleCustomEventTask<T>(T value)
        {
            var producer = GrainClient.GrainFactory.GetGrain<ICustomEventProducerGrain<T>>(Guid.NewGuid());
            var consumer = GrainClient.GrainFactory.GetGrain<ICustomEventConsumerGrain<T>>(Guid.NewGuid());
            var batchSize = 10;

            var streamGuid = Guid.NewGuid();
            var streamNamespace = "CustomEventTestStream";

            await consumer.BecomeConsumer(streamGuid, streamNamespace, _streamProviderName);

            await producer.BecomeProducer(streamGuid, streamNamespace, _streamProviderName);

            await producer.StartPeriodicBatchProducing(batchSize, value);

            await Task.Delay(TimeSpan.FromSeconds(3));

            await producer.StopPeriodicBatchProducing();

            _logger.Info("Producer produced {0} messages", await producer.GetNumberProduced());

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounter(producer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        private async Task<bool> CheckCounter<T>(ICustomEventProducerGrain<T> producer, ICustomEventConsumerGrain<T> consumer, bool lastTry)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();
            var producedValue = await producer.GetLastProducedItem();
            var consumedValue = await consumer.GetLastConsumedItem();

            return CheckCounter(numProduced, numConsumed, producedValue, consumedValue, lastTry);
        }

        private bool CheckCounter<T>(int numProduced, int numConsumed, T producedValue, T consumedValue, bool assertIsTrue)
        {
            if (assertIsTrue)
            {
                Assert.IsTrue(numProduced > 0, "Nothing was produced");
                Assert.AreEqual(numProduced, numConsumed,
                    "Number of messages produced for handler does not match number of messages consumed, Produced: {0}, Consumed: {1}",
                    numProduced, numConsumed);
                Assert.AreEqual(producedValue, consumedValue, "The consumed value is different than the produced value");
            }
            else if (numProduced <= 0 ||
                     numConsumed != numProduced ||
                     !producedValue.Equals(consumedValue))
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
                if (!producedValue.Equals(consumedValue))
                {
                    _logger.Info("The consumed value {0} is different than the produced value {1}", consumedValue.ToString(),
                        producedValue.ToString());
                }

                return false;
            }

            _logger.Info("All counts are equal and values are equal. Produced = {0}, Consumed = {1}", numProduced, numConsumed);

            return true;
        }
    }
}
