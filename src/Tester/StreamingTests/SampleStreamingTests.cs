/*
Project Orleans Cloud Service SDK ver. 1.0
 
Copyright (c) Microsoft Corporation
 
All rights reserved.
 
MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the ""Software""), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using Orleans.TestingHost.Utils;
using UnitTests.GrainInterfaces;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class SampleStreamingTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";
        private const string StreamNamespace = "SampleStreamNamespace";
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

        private Guid _streamId;
        private string _streamProvider;

        private static TestingSiloHost _host;

        public SampleStreamingTests()
            : base(new TestingSiloOptions
            {                
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml")                
            })
        {
            _host = this;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // TODO: If we will ever have a way to clean kafka queues, do it here
        }

        [TestMethod, TestCategory("BVT"), TestCategory("Functional"), TestCategory("Streaming")]
        public async Task SampleStreamingTests_1()
        {
            logger.Info("************************ SampleStreamingTests_1 *********************************");
            _streamId = Guid.NewGuid();
            _streamProvider = KafkaStreamProviderName;
            await StreamingTests_Consumer_Producer(_streamId, _streamProvider);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Streaming")]
        public async Task SampleStreamingTests_2()
        {
            logger.Info("************************ SampleStreamingTests_2 *********************************");
            _streamId = Guid.NewGuid();
            _streamProvider = KafkaStreamProviderName;
            await StreamingTests_Producer_Consumer(_streamId, _streamProvider);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Streaming" )]
        public async Task SampleStreamingTests_3()
        {
            logger.Info("************************ SampleStreamingTests_3 *********************************" );
            _streamId = Guid.NewGuid();
            _streamProvider = KafkaStreamProviderName;
            await StreamingTests_Producer_InlineConsumer(_streamId, _streamProvider );
        }

        private async Task StreamingTests_Consumer_Producer(Guid streamId, string streamProvider)
        {
            // consumer joins first, producer later
            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ConsumerGrain>(Guid.NewGuid());
            await consumer.BecomeConsumer(streamId, StreamNamespace, streamProvider);

            var producer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            await producer.BecomeProducer(streamId, StreamNamespace, streamProvider);

            await producer.StartPeriodicProducing();

            await Task.Delay(TimeSpan.FromMilliseconds(1000));

            await producer.StopPeriodicProducing();

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
            }

        private async Task StreamingTests_Producer_Consumer(Guid streamId, string streamProvider)
        {
            // producer joins first, consumer later
            var producer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            await producer.BecomeProducer(streamId, StreamNamespace, streamProvider);

            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ConsumerGrain>(Guid.NewGuid());
            await consumer.BecomeConsumer(streamId, StreamNamespace, streamProvider);

            await producer.StartPeriodicProducing();

            await Task.Delay(TimeSpan.FromMilliseconds(1000));

            await producer.StopPeriodicProducing();
            //int numProduced = await producer.NumberProduced;

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        private async Task StreamingTests_Producer_InlineConsumer(Guid streamId, string streamProvider)
        {
            // producer joins first, consumer later
            var producer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_ProducerGrain>(Guid.NewGuid());
            await producer.BecomeProducer(streamId, StreamNamespace, streamProvider);

            var consumer = GrainClient.GrainFactory.GetGrain<ISampleStreaming_InlineConsumerGrain>(Guid.NewGuid());
            await consumer.BecomeConsumer(streamId, StreamNamespace, streamProvider);

            await producer.StartPeriodicProducing();

            await Task.Delay(TimeSpan.FromMilliseconds(1000));

            await producer.StopPeriodicProducing();
            //int numProduced = await producer.NumberProduced;

            await TestingUtils.WaitUntilAsync(lastTry => CheckCounters(producer, consumer, lastTry), Timeout);

            await consumer.StopConsuming();
        }

        private async Task<bool> CheckCounters(ISampleStreaming_ProducerGrain producer, ISampleStreaming_ConsumerGrain consumer, bool assertIsTrue)
        {
            var numProduced = await producer.GetNumberProduced();
            var numConsumed = await consumer.GetNumberConsumed();
            logger.Info("CheckCounters: numProduced = {0}, numConsumed = {1}", numProduced, numConsumed);
            if (assertIsTrue)
            {
                Assert.AreEqual(numProduced, numConsumed, String.Format("numProduced = {0}, numConsumed = {1}", numProduced, numConsumed));
                return true;
            }
            else
            {
                return numProduced == numConsumed;
            }
        }
    }
}
