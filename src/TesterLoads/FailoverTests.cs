using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime;
using Orleans.TestingHost;

namespace TesterLoads
{
    [DeploymentItem("OrleansConfigurationForTesting.xml")]
    [DeploymentItem(@"FailoverConfigs\OrleansConfigurationForPressureTesting.xml")]
    [DeploymentItem(@"FailoverConfigs\OrleansConfigurationForMainKafkaDownTest.xml")]
    [DeploymentItem(@"FailoverConfigs\OrleansConfigurationForAllKafkaDownTest.xml")]        
    [DeploymentItem("ClientConfigurationForTesting.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class FailoverTests
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
            TestingSiloOptions options = new TestingSiloOptions() { StartFreshOrleans = true, SiloConfigFile = new FileInfo(configFileName) };
            _currentTestSiloHost = new UnitTestSiloHost(options);
            _runner = new LoadTestRunner(KafkaStreamProviderName, Logger);
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [TestCleanup]
        public void MyClassCleanup()
        {
            TestingSiloHost.StopAllSilos();
        }

        [TestMethod]
        public async Task StreamPressureTest()
        {
            SetUpSiloForTest("OrleansConfigurationForPressureTesting.xml");
            Logger.Info("************************ StreamPressureTest *********************************");
            await _runner.RunStreams(10, 1, 1, 100);
        }

        [TestMethod]
        public async Task MainKafkaDownTest()
        {
            SetUpSiloForTest("OrleansConfigurationForMainKafkaDownTest.xml");
            Logger.Info("************************ MainKafkaDownTest *********************************");
            await _runner.RunStreams(1, 1, 1, 1);
        }

        [TestMethod]
        public async Task AllKafkaDownTest()
        {
            SetUpSiloForTest("OrleansConfigurationForAllKafkaDownTest.xml");
            Logger.Info("************************ MainKafkaDownTest *********************************");
            await _runner.RunStreams(10, 1, 10, 10);
        }
    }
}
