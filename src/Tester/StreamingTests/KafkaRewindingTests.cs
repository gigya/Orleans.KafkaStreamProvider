using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        public KafkaRewindingTests()
            : base( new TestingSiloOptions()
            {
                StartFreshOrleans = true,
                SiloConfigFile = new FileInfo("OrleansConfigurationForRewinding.xml"),
            })
        {
            _runner = new RewindingTestRunner(KafkaStreamProviderName, logger);
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            StopAllSilos();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceToken()
        {
            logger.Info("************************ KafkaMultipleStreamsSameNamespace *********************************");
            await _runner.SubscribingWithOldSequenceToken();
        }

        [TestMethod]
        public async Task SubscribingWithTooOldSequenceToken()
        {
            logger.Info("************************ SubscribingWithTooOldSequenceToken *********************************");
            await _runner.SubscribingWithTooOldSequenceToken();
        }

        [TestMethod]
        public async Task SubscribingWithOldSequenceTokenForFirstTime()
        {
            logger.Info("************************ SubscribingWithOldSequenceTokenForFirstTime *********************************");
            await _runner.SubscribingWithTooOldSequenceTokenForFirstTime();            
        }

        [TestMethod]
        public async Task SubscribingMultipleConsumersDifferentTokens()
        {
            logger.Info("************************ SubscribingMultipleConsumersDifferentTokens *********************************");
            await _runner.SubscribingMultipleConsumersDifferentTokens();
        }
    }
}
