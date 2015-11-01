using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaBatchProducingTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml

        private readonly BatchProducingTestRunner _runner;

        public KafkaBatchProducingTests()
            : base(new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml"),
            })
        {
            _runner = new BatchProducingTestRunner(KafkaStreamProviderName, logger);
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            StopAllSilos();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task SimpleBatchTesting()
        {
            logger.Info("************************ SimpleBatchTesting *********************************");
            await _runner.SimpleBatchTesting();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleConsumersOneStream()
        {
            logger.Info("************************ BatchTestMultipleConsumersOneStream *********************************");
            await _runner.BatchTestMultipleConsumersOneStream();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleStreamsOneConsumer()
        {
            logger.Info("************************ BatchTestMultipleStreamsOneConsumer *********************************");
            await _runner.BatchTestMultipleStreamsOneConsumer();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestLoadTesting()
        {
            logger.Info("************************ BatchTestLoadTesting ************************");
            await _runner.BatchTestLoadTesting();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleProducers()
        {
            logger.Info("************************ BatchTestMultipleProducers ************************");
            await _runner.BatchTestMultipleProducers(10, 10);
        }

        [TestMethod]
        public async Task InfiniteTest()
        {
            logger.Info("************************ InfiniteTest ************************");
            await _runner.InfiniteTest();
        }
    }
}
