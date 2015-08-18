using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans.Providers;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    /// <summary>
    /// Factory class for KafkaQueueAdapter.
    /// </summary>
    public class KafkaQueueAdapterFactory : IQueueAdapterFactory
    {
        private KafkaStreamProviderOptions _options;
        private HashRingBasedStreamQueueMapper _streamQueueMapper;
        private IQueueAdapterCache _adapterCache;
        private string _providerName;
        private Logger _logger;

        public void Init(IProviderConfiguration config, string providerName, Logger logger)
        {
            if (config == null) throw new ArgumentNullException("config");            
            if (logger == null) throw new ArgumentNullException("logger");
            if (String.IsNullOrEmpty(providerName)) throw new ArgumentNullException("providerName");

            string rawConnectionStrings = GetRequiredParam(KafkaStreamProviderOptions.ConnectionStringsParam, config);

            // Parsing the connection strings
            var dividedConnectionStrings = rawConnectionStrings.Split(KafkaStreamProviderOptions.ConnectionStringDelimiter);

            // Getting the required params
            var connectionStrings = dividedConnectionStrings.Select(dividedConnectionString => new Uri(dividedConnectionString)).ToList();            
            string topicName = GetRequiredParam(KafkaStreamProviderOptions.TopicNameParam, config);
            string consumerGroupName = GetRequiredParam(KafkaStreamProviderOptions.ConsumerGroupNameParam, config);

            _options = new KafkaStreamProviderOptions(connectionStrings, topicName, consumerGroupName);

            // Getting the optional params
            _options.CacheSize = GetOptionalParam(KafkaStreamProviderOptions.CacheSizeParam, _options.CacheSize, config);
            _options.NumOfQueues = GetOptionalParam(KafkaStreamProviderOptions.NumOfQueuesParam, _options.NumOfQueues, config);
            _options.AckLevel = GetOptionalParam(KafkaStreamProviderOptions.AckLevelParam, _options.AckLevel, config);
            _options.MaxBytesInMessageSet = GetOptionalParam(KafkaStreamProviderOptions.MaxBytesInMessageSetParam, _options.MaxBytesInMessageSet, config);
            _options.OffsetCommitInterval = GetOptionalParam(KafkaStreamProviderOptions.OffsetCommitIntervalParam, _options.OffsetCommitInterval, config);
            _options.ProduceBatchSize = GetOptionalParam(KafkaStreamProviderOptions.ProduceBatchSizeParam, _options.ProduceBatchSize, config);
            _options.TimeToWaitForBatchInMs = GetOptionalParam(KafkaStreamProviderOptions.TimeToWaitForBatchInMsParam, _options.TimeToWaitForBatchInMs, config);
            _options.ReceiveWaitTimeInMs = GetOptionalParam(KafkaStreamProviderOptions.ReceiveWaitTimeInMsParam, _options.ReceiveWaitTimeInMs, config);
            _options.MaxMessagesToTakeFromKafka = GetOptionalParam(KafkaStreamProviderOptions.MaxMessagesToTakeFromKafkaParam, _options.MaxMessagesToTakeFromKafka, config);
            _options.ShouldInitWithLastOffset = GetOptionalParam(KafkaStreamProviderOptions.ShouldInitWithLastOffsetParam, _options.ShouldInitWithLastOffset, config);

            _providerName = providerName;
            _streamQueueMapper = new HashRingBasedStreamQueueMapper(_options.NumOfQueues, providerName);
            _adapterCache = new SimpleQueueAdapterCache(this, _options.CacheSize, logger);
            _logger = logger;
        }

        private static string GetRequiredParam(string paramName, IProviderConfiguration config)
        {
            string paramValue;

            if (!config.Properties.TryGetValue(paramName, out paramValue))
            {
                throw new ArgumentException(String.Format("{0} property not set", paramName));
            }

            return paramValue;
        }

        private static int GetOptionalParam(string paramName, int defaultValue, IProviderConfiguration config)
        {
            string paramValuePreParsed;
            var paramValue = defaultValue;
            if (!config.Properties.TryGetValue(paramName, out paramValuePreParsed)) return paramValue;
            if (!int.TryParse(paramValuePreParsed, out paramValue))
                throw new ArgumentException(String.Format("{0} invalid.  Must be int", paramName));

            return paramValue;
        }

        private static bool GetOptionalParam(string paramName, bool defaultValue, IProviderConfiguration config)
        {
            string paramValuePreParsed;
            var paramValue = defaultValue;
            if (!config.Properties.TryGetValue(paramName, out paramValuePreParsed)) return paramValue;
            if (!bool.TryParse(paramValuePreParsed, out paramValue))
                throw new ArgumentException(String.Format("{0} invalid.  Must be boolean", paramName));

            return paramValue;
        }

        public Task<IQueueAdapter> CreateAdapter()
        {
            var adapter = new KafkaQueueAdapter(_streamQueueMapper, _options, _providerName,
                new KafkaBatchFactory(), _logger);
            return Task.FromResult<IQueueAdapter>(adapter);
        }

        /// <summary>
        /// Creates a delivery failure handler for the specified queue.
        /// </summary>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public Task<IStreamFailureHandler> GetDeliveryFailureHandler(QueueId queueId)
        {
            return Task.FromResult<IStreamFailureHandler>(new NoOpStreamDeliveryFailureHandler(false));
        }

        public IQueueAdapterCache GetQueueAdapterCache()
        {
            return _adapterCache;
        }

        public IStreamQueueMapper GetStreamQueueMapper()
        {
            return _streamQueueMapper;
        }
    }
}