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
using UnitTests.StreamingTests;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForStreamingUnitTests.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaSubscriptionMultiplicityTests : UnitTestSiloHost
    {
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml
        private const string StreamNamespace = "KafkaSubscriptionMultiplicityTestsNamespace";

        private readonly SubscriptionMultiplicityTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaSubscriptionMultiplicityTests()
            : base(new TestingSiloOptions
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForStreamingUnitTests.xml"),
            })
        {
            _runner = new SubscriptionMultiplicityTestRunner(KafkaStreamProviderName, GrainClient.Logger);
            _host = this;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Kafka"), TestCategory("Storage"), TestCategory("Streaming")]
        public async Task KafkaMultipleSubscriptionTest()
        {
            logger.Info("************************ KafkaMultipleSubscriptionTest *********************************");
            await _runner.MultipleSubscriptionTest(Guid.NewGuid(), StreamNamespace);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Kafka"), TestCategory("Storage"), TestCategory("Streaming")]
        public async Task KafkaAddAndRemoveSubscriptionTest()
        {
            logger.Info("************************ KafkaAddAndRemoveSubscriptionTest *********************************");
            await _runner.AddAndRemoveSubscriptionTest(Guid.NewGuid(), StreamNamespace);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Kafka"), TestCategory("Storage"), TestCategory("Streaming")]
        public async Task KafkaResubscriptionTest()
        {
            logger.Info("************************ KafkaResubscriptionTest *********************************");
            await _runner.ResubscriptionTest(Guid.NewGuid(), StreamNamespace);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Kafka"), TestCategory("Storage"), TestCategory("Streaming")]
        public async Task KafkaResubscriptionAfterDeactivationTest()
        {
            logger.Info("************************ KafkaResubscriptionAfterDeactivationTest *********************************");
            await _runner.ResubscriptionAfterDeactivationTest(Guid.NewGuid(), StreamNamespace);
        }

        [TestMethod, TestCategory("Functional"), TestCategory("Kafka"), TestCategory("Storage"), TestCategory("Streaming")]
        public async Task KafkaActiveSubscriptionTest()
        {
            logger.Info("************************ KafkaActiveSubscriptionTest *********************************");
            await _runner.ActiveSubscriptionTest(Guid.NewGuid(), StreamNamespace);
        }
    }
}
