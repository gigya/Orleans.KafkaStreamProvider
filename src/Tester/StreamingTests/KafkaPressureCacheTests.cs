using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime;
using Orleans.TestingHost;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("ClientConfiguration.xml")]
    [DeploymentItem("OrleansConfigurationForPressureTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaPressureCacheTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml
        private readonly PressuredCacheTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaPressureCacheTests() : base( new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForPressureTests.xml"),

            })

        {
            _runner = new PressuredCacheTestRunner(KafkaStreamProviderName, Client.Logger);
            _host = this;
            GrainClient.Initialize("ClientConfiguration.xml");
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestMethod]
        public async Task CacheIsUnderPressureDueToTimeSpanGuarantee()
        {
            Client.Logger.Info("************************ CacheIsUnderPressureDueToTimeSpanGuarantee *********************************");
            await _runner.CacheIsUnderPressureDueToTimeSpanGuarantee();
        }

        [TestMethod]
        public async Task CacheIsUnderPressreDueToLateSubscriber()
        {
            Client.Logger.Info("************************ CacheIsUnderPressreDueToLateSubscriber *********************************");
            await _runner.CacheIsUnderPressreDueToLateSubscriber();
        }

        [TestMethod]
        public async Task CacheIsOvertimedDueToSlowConsumer()
        {
            Client.Logger.Info("************************ CacheIsOvertimedDueToSlowConsumer *********************************");
            await _runner.CacheIsOvertimedDueToSlowConsumer();
        }

        [TestMethod]
        public async Task CacheIsUnderPressureDueToSlowConsumer()
        {
            Client.Logger.Info("************************ CacheIsOvertimedDueToSlowConsumer *********************************");
            await _runner.CacheIsUnderPressureDueToSlowConsumer();
        }
    }
}
