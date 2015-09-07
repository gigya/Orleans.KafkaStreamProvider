using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime;
using Orleans.TestingHost;

namespace TesterLoads
{
    // TODO: Add large stream numbers once we can run on a server
    [DeploymentItem("OrleansConfigurationForTesting.xml")]
    [DeploymentItem("ClientConfigurationForTesting.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class LoadTests
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml

        private UnitTestSiloHost _currentTestSiloHost;
        private LoadTestRunner _runner;

        public Logger Logger
        {
            get { return _currentTestSiloHost.logger ?? _currentTestSiloHost.logger; }
        }

        private void SetUpSiloForTest(string configFileName)
        {
            TestingSiloOptions options = new TestingSiloOptions(){StartFreshOrleans = true, SiloConfigFile = new FileInfo(configFileName)};
            _currentTestSiloHost = new UnitTestSiloHost(options);
            _runner = new LoadTestRunner(KafkaStreamProviderName, Logger);
        }

        [TestCleanup]
        private void TearDownSiloForTest()
        {
            TestingSiloHost.StopAllSilos();
        }

        [TestMethod]
        public async Task TimeToConsumeTest()
        {
            SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToConsumeTest *********************************");            
            await _runner.TimeToConsumeTest(10, 1);
            await _runner.TimeToConsumeTest(100, 1);
            await _runner.TimeToConsumeTest(10, 10);
            await _runner.TimeToConsumeTest(100, 10);
            await _runner.TimeToConsumeTest(100, 100);
        }

        [TestMethod]
        public async Task TimeToProduceTest()
        {
            SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToProduceTest *********************************");
            await _runner.TimeToProduceTest(10, 1);
            await _runner.TimeToProduceTest(100, 1);
            await _runner.TimeToProduceTest(10, 10);
            await _runner.TimeToProduceTest(100, 10);
            await _runner.TimeToProduceTest(100, 100);
        }

        [TestMethod]
        public async Task TimeToProduceAndConsumeSingleMessage()
        {
            SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToProduceAndConsumeSingleMessage *********************************");
            await _runner.RunStreams(1, 1, 1, 2);
            await _runner.RunStreams(1, 1, 1, 1);
            await _runner.RunStreams(1, 1, 1, 10);
            await _runner.RunStreams(1, 1, 1, 100);
            await _runner.RunStreams(1, 1, 1, 1000);
        }

        [TestMethod]
        public async Task TimeToProduceAndConsumeMultipleProducers()
        {
            SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToProduceAndConsumeMultipleProducers *********************************");
            await _runner.RunStreams(1, 10, 1, 1);
            await _runner.RunStreams(1, 10, 1, 10);
            await _runner.RunStreams(1, 10, 1, 100);
        }

        [TestMethod]
        public async Task TimeToProduceAndConsumeMultipleStreams()
        {
            SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToProduceAndConsumeMultipleStreams *********************************");
            await _runner.RunStreams(10, 1, 1, 10);
            await _runner.RunStreams(10, 1, 1, 100);
        }

        [TestMethod]
        public async Task TimeToProduceAndConsumeMultipleStreamsWithBatch()
        {
           SetUpSiloForTest("OrleansConfigurationForTesting.xml");
            Logger.Info("************************ TimeToProduceAndConsumeMultipleStreamsWithBatch *********************************");
            await _runner.RunStreams(10, 1, 5, 10);
            await _runner.RunStreams(10, 1, 10, 10);
            await _runner.RunStreams(10, 1, 20, 10);
            await _runner.RunStreams(10, 1, 40, 10);

            await _runner.RunStreams(20, 1, 5, 10);
            await _runner.RunStreams(20, 1, 10, 10);
            await _runner.RunStreams(20, 1, 20, 10);
            await _runner.RunStreams(20, 1, 40, 10);

            await _runner.RunStreams(100, 1, 10, 100);
        }        
    }
}