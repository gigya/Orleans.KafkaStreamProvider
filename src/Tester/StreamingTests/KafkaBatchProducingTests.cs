using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("ClientConfiguration.xml")]

    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaBatchProducingTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml

        private readonly BatchProducingTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaBatchProducingTests()
            : base(new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml")
            })
        {
            _runner = new BatchProducingTestRunner(KafkaStreamProviderName, Client.Logger, "OrleansConfigurationForStreamingUnitTests.xml");
            _host = this;
            GrainClient.Initialize("ClientConfiguration.xml");
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task SimpleBatchTesting()
        {
            Client.Logger.Info("************************ SimpleBatchTesting *********************************");
            await _runner.SimpleBatchTesting();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleConsumersOneStream()
        {
            Client.Logger.Info("************************ BatchTestMultipleConsumersOneStream *********************************");
            await _runner.BatchTestMultipleConsumersOneStream();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleStreamsOneConsumer()
        {
            Client.Logger.Info("************************ BatchTestMultipleStreamsOneConsumer *********************************");
            await _runner.BatchTestMultipleStreamsOneConsumer();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestLoadTesting()
        {
            Client.Logger.Info("************************ BatchTestLoadTesting ************************");
            await _runner.BatchTestLoadTesting();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestLoadTestingWithImplicitSubscription()
        {
            Client.Logger.Info("************************ BatchTestLoadTesting ************************");
            await _runner.BatchTestLoadTestingWithImplicitSubscription();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("KafkaStreamProvider"), TestCategory("Streaming")]
        public async Task BatchTestMultipleProducers()
        {
            Client.Logger.Info("************************ BatchTestMultipleProducers ************************");
            await _runner.BatchTestMultipleProducers(10, 10);
        }

        [TestMethod]
        [Ignore]
        public async Task InfiniteTest()
        {
            Client.Logger.Info("************************ InfiniteTest ************************");
            await _runner.InfiniteTest();
        }
    }
}
