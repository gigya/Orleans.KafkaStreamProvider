using System;
using System.Collections.Generic;
using System.Linq;
using Orleans.Providers;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaStreamProviderOptions
    {
        // Consts
        public const char ConnectionStringDelimiter = ';';

        // Names
        private const string ConnectionStringsParam = "ConnectionStrings";
        private const string TopicNameParam = "TopicName";
        private const string ConsumerGroupNameParam = "ConsumerGroupName";
        private const string NumOfQueuesParam = "NumOfQueues";
        private const string CacheSizeParam = "CacheSize";
        private const string CacheTimespanInSecondsParam = "CacheTimespan";
        private const string CacheNumOfBucketsParam = "NumOfCacheBuckets";
        private const string ProduceBatchSizeParam = "ProduceBatchSize";
        private const string TimeToWaitForBatchInMsParam = "TimeToWaitForBatchInMs";
        private const string MaxBytesInMessageSetParam = "MaxBytesInMessageSet";
        private const string AckLevelParam = "AckLevel";
        private const string ReceiveWaitTimeInMsParam = "ReceiveWaitTimeInMs";
        private const string OffsetCommitIntervalParam = "OffsetCommitInterval";
        private const string ShouldInitWithLastOffsetParam = "ShouldInitWithLastOffset";
        private const string MaxMessageSizeInBytesParam = "MaxMessageSize";
        private const string MetricsPortParam = "MetricsPort";
        private const string IncludeMetricsParam = "IncludeMetrics";
        private const string UsingExternalMetricsParam = "UsingExternalMetrics";


        // Default values
        private const int DefaultNumOfQueues = 1;
        private const int DefaultCacheSize = 4096 * 4;
        private const int DefaultCacheTimespanInSeconds = 60;
        private const int DefaultCacheNumOfBucketsParam = 10;
        private const int DefaultProduceBatchSize = 1000;
        private const int DefaultTimeToWaitForBatchInMs = 10;
        private const int DefaultMaxBytesInMessageSet = 4096 * 8;
        private const int DefaultAckLevel = 1;
        private const int DefaultReceiveWaitTimeInMs = 100;
        private const int DefaultOffsetCommitInterval = 1;
        private const bool DefaultShouldInitWithLastOffset = true;
        private const int DefaultMaxMessageSizeInBytes = 4096;
        private const int DefaultMetricsPort = 20490;
        private const bool DefaultIncludeMetrics = true;
        private const bool DefaultUsingExternalMetrics = false;

        // Config values
        private readonly IEnumerable<Uri> _connectionStrings;
        private readonly string _consumerGroupName;
        private readonly string _topicName;

        public IEnumerable<Uri> ConnectionStrings { get { return _connectionStrings; } }
        public int NumOfQueues { get; set; }
        public int CacheSize { get; set; }
        public string ConsumerGroupName { get {return _consumerGroupName;}}
        public string TopicName { get { return _topicName; }}
        public int ProduceBatchSize { get; set; }
        public int TimeToWaitForBatchInMs { get; set; }
        public int MaxBytesInMessageSet { get; set; }
        public int AckLevel { get; set; }
        public int ReceiveWaitTimeInMs { get; set; }
        public int OffsetCommitInterval { get; set; }
        public bool ShouldInitWithLastOffset { get; set; }
        public int MaxMessageSizeInBytes { get; set; }
        public int CacheTimespanInSeconds { get; set; }
        public int CacheNumOfBuckets { get; set; }
        public int MetricsPort { get; set; }
        public bool IncludeMetrics { get; set; }
        public bool UsingExternalMetrics { get; set; }

        public KafkaStreamProviderOptions(IEnumerable<Uri> connectionStrings, string topicName, string consumerGroupName)
        {
            if (string.IsNullOrEmpty(topicName)) throw new ArgumentNullException("topicName");
            if (string.IsNullOrEmpty(consumerGroupName)) throw new ArgumentNullException("consumerGroupName");
            var enumerableConnectionStrings = connectionStrings as IList<Uri> ?? connectionStrings.ToList();
            if (connectionStrings == null || !enumerableConnectionStrings.Any()) throw new ArgumentNullException("connectionStrings");

            _connectionStrings = enumerableConnectionStrings;
            _topicName = topicName;
            _consumerGroupName = consumerGroupName;

            NumOfQueues = DefaultNumOfQueues;
            CacheSize = DefaultCacheSize;
            ProduceBatchSize = DefaultProduceBatchSize;
            TimeToWaitForBatchInMs = DefaultTimeToWaitForBatchInMs;
            MaxBytesInMessageSet = DefaultMaxBytesInMessageSet;
            AckLevel = DefaultAckLevel;
            ReceiveWaitTimeInMs = DefaultReceiveWaitTimeInMs;
            OffsetCommitInterval = DefaultOffsetCommitInterval;
            ShouldInitWithLastOffset = DefaultShouldInitWithLastOffset;
            MaxMessageSizeInBytes = DefaultMaxMessageSizeInBytes;
            CacheTimespanInSeconds = DefaultCacheTimespanInSeconds;
            CacheNumOfBuckets = DefaultCacheNumOfBucketsParam;
            MetricsPort = DefaultMetricsPort;
            IncludeMetrics = DefaultIncludeMetrics;
            UsingExternalMetrics = DefaultUsingExternalMetrics;
        }

        public KafkaStreamProviderOptions(IProviderConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            string rawConnectionStrings = GetRequiredParam(ConnectionStringsParam, config);

            // Parsing the connection strings
            var dividedConnectionStrings = rawConnectionStrings.Split(ConnectionStringDelimiter);

            // Setting the required params
            _connectionStrings = dividedConnectionStrings.Select(dividedConnectionString => new Uri(dividedConnectionString)).ToList();
            _topicName = GetRequiredParam(TopicNameParam, config);
            _consumerGroupName = GetRequiredParam(ConsumerGroupNameParam, config);

            // Setting the optional params
            CacheSize = GetOptionalParamInt(CacheSizeParam, DefaultCacheSize, config);
            NumOfQueues = GetOptionalParamInt(NumOfQueuesParam, DefaultNumOfQueues, config);
            AckLevel = GetOptionalParamInt(AckLevelParam, DefaultAckLevel, config);
            MaxBytesInMessageSet = GetOptionalParamInt(MaxBytesInMessageSetParam, DefaultMaxBytesInMessageSet, config);
            OffsetCommitInterval = GetOptionalParamInt(OffsetCommitIntervalParam, DefaultOffsetCommitInterval, config);
            ProduceBatchSize = GetOptionalParamInt(ProduceBatchSizeParam, DefaultProduceBatchSize, config);
            TimeToWaitForBatchInMs = GetOptionalParamInt(TimeToWaitForBatchInMsParam, DefaultTimeToWaitForBatchInMs, config);
            ReceiveWaitTimeInMs = GetOptionalParamInt(ReceiveWaitTimeInMsParam, DefaultReceiveWaitTimeInMs, config);
            ShouldInitWithLastOffset = GetOptionalParamBool(ShouldInitWithLastOffsetParam, DefaultShouldInitWithLastOffset, config);
            MaxMessageSizeInBytes = GetOptionalParamInt(MaxMessageSizeInBytesParam, DefaultMaxBytesInMessageSet,
                config);
            CacheTimespanInSeconds = GetOptionalParamInt(CacheTimespanInSecondsParam, DefaultCacheTimespanInSeconds,
                config);
            CacheNumOfBuckets = GetOptionalParamInt(CacheNumOfBucketsParam, DefaultCacheNumOfBucketsParam, config);
            MetricsPort = GetOptionalParamInt(MetricsPortParam, DefaultMetricsPort, config);
            IncludeMetrics = GetOptionalParamBool(IncludeMetricsParam, DefaultIncludeMetrics, config);
            UsingExternalMetrics = GetOptionalParamBool(UsingExternalMetricsParam, DefaultUsingExternalMetrics, config);
        }

        private static string GetRequiredParam(string paramName, IProviderConfiguration config)
        {
            string paramValue;

            if (!config.Properties.TryGetValue(paramName, out paramValue)) throw new ArgumentException(String.Format("{0} property not set", paramName));            

            return paramValue;
        }

        private static int GetOptionalParamInt(string paramName, int defaultValue, IProviderConfiguration config)
        {
            string paramValuePreParsed;
            var paramValue = defaultValue;
            if (!config.Properties.TryGetValue(paramName, out paramValuePreParsed)) return paramValue;
            if (!int.TryParse(paramValuePreParsed, out paramValue))
                throw new ArgumentException(String.Format("{0} invalid.  Must be int", paramName));

            return paramValue;
        }

        private static bool GetOptionalParamBool(string paramName, bool defaultValue, IProviderConfiguration config)
        {
            string paramValuePreParsed;
            var paramValue = defaultValue;
            if (!config.Properties.TryGetValue(paramName, out paramValuePreParsed)) return paramValue;
            if (!bool.TryParse(paramValuePreParsed, out paramValue))
                throw new ArgumentException(String.Format("{0} invalid.  Must be boolean", paramName));

            return paramValue;
        }
    }
}
