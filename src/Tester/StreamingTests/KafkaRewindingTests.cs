using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime;
using Orleans.TestingHost;
using UnitTests.Tester;

namespace Tester.StreamingTests
{
    [DeploymentItem("OrleansConfigurationForRewinding.xml")]
    [DeploymentItem("OrleansProviders.dll")]
    [DeploymentItem("Orleans.KafkaStreamProvider.dll")]
    [TestClass]
    public class KafkaRewindingTests : UnitTestSiloHost
    {        
        private const string KafkaStreamProviderName = "KafkaProvider";                 // must match what is in OrleansConfigurationForStreamingUnitTests.xml

        private readonly RewindingTestRunner _runner;
        private static TestingSiloHost _host;

        public KafkaRewindingTests()
            : base( new TestingSiloOptions()
            {
                StartFreshOrleans = false,
                StartSecondary = false,
                SiloConfigFile = new FileInfo("OrleansConfigurationForRewinding.xml"),
            })
        {
            _runner = new RewindingTestRunner(KafkaStreamProviderName, Client.Logger);
            _host = this;
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            _host.StopAllSilos();
        }

        [TestInitialize]
        public void TestInit()
        {
            // Because we don't start a fresh orleans (until orleans will get their sh*t together) it is crucial in this test set
            // That the cache will always be empty, so we wait a bit between tests until it removes everything from the cache
            Task.Delay(TimeSpan.FromSeconds(5.5)).Wait();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceToken()
        {
            Client.Logger.Info("************************ KafkaMultipleStreamsSameNamespace *********************************");
            await _runner.SubscribingWithOldSequenceToken();
        }

        [TestMethod]
        public async Task SubscribingWithTooOldSequenceToken()
        {
            Client.Logger.Info("************************ SubscribingWithTooOldSequenceToken *********************************");
            await _runner.SubscribingWithTooOldSequenceToken();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceTokenForFirstTime()
        {
            Client.Logger.Info("************************ SubscribingWithOldSequenceTokenForFirstTime *********************************");
            await _runner.SubscribingWithTooOldSequenceTokenForFirstTime();            
        }

        [TestMethod]
        public async Task SubscribingMultipleConsumersDifferentTokens()
        {
            Client.Logger.Info("************************ SubscribingMultipleConsumersDifferentTokens *********************************");
            await _runner.SubscribingMultipleConsumersDifferentTokens();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceTokenAfterBatchSending()
        {
            Client.Logger.Info("************************ SubscribingWithOldSequenceTokenAfterBatchSending *********************************");
            await _runner.SubscribingWithOldSequenceTokenAfterBatchSending();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceTokenAfterBatchSendingOnlyOneBatch()
        {
            Client.Logger.Info("************************ SubscribingWithOldSequenceTokenAfterBatchSending *********************************");
            await _runner.SubscribingWithOldSequenceTokenAfterBatchSendingOnlyOneBatch();
        }     
    }
}
