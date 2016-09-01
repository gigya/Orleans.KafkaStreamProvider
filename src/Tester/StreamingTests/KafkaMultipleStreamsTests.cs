using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime;
using Orleans.TestingHost;
using UnitTests.StreamingTests;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaMultipleStreamsTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml
        private const string StreamNamespace = "KafkaSubscriptionMultipleStreamsTestsNamespace";

        private readonly MultipleStreamsTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaMultipleStreamsTests()
            : base( new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml"),
            })
        {
            _runner = new MultipleStreamsTestRunner(KafkaStreamProviderName, logger);
            _host = this;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleStreamsSameNamespaceTest()
        {
            logger.Info("************************ KafkaMultipleStreamsSameNamespace *********************************");
            await _runner.MultipleStreamsSameNamespaceTest(StreamNamespace);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleStreamsDifferentNamespaceTest()
        {
            // TODO: EranO you want to look at this
            logger.Info("************************ KafkaMultipleStreamsDifferentNamespace *********************************");
            await _runner.MultipleStreamsDifferentNamespaceTest();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleStreamsDifferentNamespcaeDifferentConsumers()
        {
            logger.Info("************************ MultipleStreamsDifferentNamespcaeDifferentConsumers *********************************");
            await _runner.MultipleStreamsDifferentNamespcaeDifferentConsumers();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleProducersSameGrainSameNamespaceDifferentConsumers()
        {
            logger.Info("************************ MultipleProducersSameGrainSameNamespaceDifferentConsumers *********************************");
            await _runner.MultipleProducersSameGrainSameNamespaceDifferentConsumers();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleProducersSameGrainDifferentNamespaceDifferentConsumers()
        {
            logger.Info("************************ MultipleProducersSameGrainDifferentNamespaceDifferentConsumers *********************************");
            await _runner.MultipleProducersSameGrainDifferentNamespaceDifferentConsumers();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task MultipleProducersSameGrainDifferentNamespaceSameConsumer()
        {
            logger.Info("************************ MultipleProducersSameGrainDifferentNamespaceSameConsumer *********************************");
            await _runner.MultipleProducersSameGrainDifferentNamespaceSameConsumer();
        }
    }
}
