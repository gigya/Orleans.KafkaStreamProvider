using System;
using System.Collections.Generic;
using System.Linq;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaStreamProviderOptions
    {
        // Consts
        public const char ConnectionStringDelimiter = ';';

        // Names
        public const string ConnectionStringsParam = "ConnectionStrings";
        public const string TopicNameParam = "TopicName";
        public const string ConsumerGroupNameParam = "ConsumerGroupName";
        public const string NumOfQueuesParam = "NumOfQueues";
        public const string CacheSizeParam = "CacheSize";
        public const string ProduceBatchSizeParam = "ProduceBatchSize";
        public const string TimeToWaitForBatchInMsParam = "TimeToWaitForBatchInMs";
        public const string MaxBytesInMessageSetParam = "MaxBytesInMessageSet";
        public const string AckLevelParam = "AckLevel";
        public const string ReceiveWaitTimeInMsParam = "ReceiveWaitTimeInMs";
        public const string OffsetCommitIntervalParam = "OffsetCommitInterval";
        public const string MaxMessagesToTakeFromKafkaParam = "MaxMessagesToTakeFromKafka";
        public const string ShouldInitWithLastOffsetParam = "ShouldInitWithLastOffsetParam";

        // Default values
        public const int DefaultNumOfQueues = 4;
        public const int DefaultCacheSize = 4096*4;
        public const int DefaultProduceBatchSize = 1000;
        public const int DefaultTimeToWaitForBatchInMs = 10;
        public const int DefaultMaxBytesInMessageSet = 4096*8;
        public const int DefaultAckLevel = 1;
        public const int DefaultReceiveWaitTimeInMs = 100;
        public const int DefaultOffsetCommitInterval = 1;
        public const int DefaultMaxMessagesToTakeFromKafka = 65536;
        public const bool DefaultShouldInitWithLastOffset = true;

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
        public int MaxMessagesToTakeFromKafka { get; set; }
        public bool ShouldInitWithLastOffset { get; set; }

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
            MaxMessagesToTakeFromKafka = DefaultMaxMessagesToTakeFromKafka;
            ShouldInitWithLastOffset = DefaultShouldInitWithLastOffset;
        }
    }
}
