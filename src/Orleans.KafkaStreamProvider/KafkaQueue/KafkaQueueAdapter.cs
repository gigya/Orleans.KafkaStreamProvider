using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaQueueAdapter : IQueueAdapter
    {
        private readonly HashRingBasedStreamQueueMapper _streamQueueMapper;
        private readonly ProtocolGateway _gateway;
        private readonly IKafkaBatchFactory _batchFactory;
        private readonly Logger _logger;
        private readonly KafkaStreamProviderOptions _options;
        private readonly Producer _producer;

        public bool IsRewindable { get { return false; } }

        public string Name { get; private set; }

        public StreamProviderDirection Direction
        {
            get { return StreamProviderDirection.ReadWrite; }
        }

        public KafkaQueueAdapter(HashRingBasedStreamQueueMapper queueMapper, KafkaStreamProviderOptions options, string providerName, IKafkaBatchFactory batchFactory, Logger logger)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (batchFactory == null) throw new ArgumentNullException("batchFactory");
            if (queueMapper == null) throw new ArgumentNullException("queueMapper");
            if (logger == null) throw new ArgumentNullException("logger");
            if (string.IsNullOrEmpty(providerName)) throw new ArgumentNullException("providerName");

            _options = options;
            _streamQueueMapper = queueMapper;
            Name = providerName;
            _batchFactory = batchFactory;
            _logger = logger;

            // Creating a producer
            KafkaOptions kafkaOptions = new KafkaOptions(_options.ConnectionStrings.ToArray()){Log = new KafkaLogBridge(logger)};
            var broker = new BrokerRouter(kafkaOptions);
            _producer = new Producer(broker) { BatchDelayTime = TimeSpan.FromMilliseconds(_options.TimeToWaitForBatchInMs), BatchSize = _options.ProduceBatchSize };
            _gateway = new ProtocolGateway(kafkaOptions);

            _logger.Info("KafkaQueueAdapter - Created a KafkaQueueAdapter");
        }

        public IQueueAdapterReceiver CreateReceiver(QueueId queueId)
        {
            var partitionId = (int)queueId.GetNumericId();

            _logger.Info("KafkaQueueAdapter - Creating a KafkaQueueAdapterReceiver for partition {0} in topic {1}", partitionId, _options.TopicName);

            var clientName = Environment.MachineName + AppDomain.CurrentDomain.FriendlyName;

            // Creating a receiver
            var manualConsumer = new ManualConsumer(partitionId, _options.TopicName, _gateway, clientName, _options.MaxBytesInMessageSet);

            return new KafkaQueueAdapterReceiver(queueId, manualConsumer, _options, _batchFactory, _logger);
        }

        public async Task QueueMessageBatchAsync<T>(Guid streamGuid, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext)
        {
            var queueId = _streamQueueMapper.GetQueueForStream(streamGuid, streamNamespace);
            
            var partitionId = (int)queueId.GetNumericId();

            _logger.Verbose("KafkaQueueAdapter - For StreamId: {0}, StreamNamespcae:{1} using partition {2}", streamGuid, streamNamespace, partitionId);

            // Creating a message for each event, to support eventually connecting the sequence token with the kafka offset

            var payload = _batchFactory.ToKafkaMessage(streamGuid, streamNamespace, events, requestContext);
            if (payload == null)
            {
                _logger.Info("The batch factory returned a faulty message, the message was not sent");
                return;
            }

            var messageToSend = new List<Message> {payload};            
            var response = await Task.Run(() => _producer.SendMessageAsync(_options.TopicName, messageToSend, (short)_options.AckLevel, partition: partitionId));

            // This is ackLevel != 0 check
            if (response != null)
            {
                var responsesWithError =
                    response.Where(messageResponse => messageResponse.Error != (int) ErrorResponseCode.NoError);

                // Checking all the responses
                foreach (var messageResponse in responsesWithError)
                {                    
                    _logger.Info(
                        "KafkaQueueAdapter - Error sending message through kafka client, the error code is {0}, message offset is {1}",
                        messageResponse.Error, messageResponse.Offset);

                    throw new KafkaApplicationException(
                        "Failed at producing the message with offset {0} for queue {1} in topic {2}", messageResponse.Offset,
                        queueId, _options.TopicName)
                    {
                        ErrorCode = messageResponse.Error,
                        Source = "KafkaStreamProvider"
                    };
                }
            }
        }
    }
}