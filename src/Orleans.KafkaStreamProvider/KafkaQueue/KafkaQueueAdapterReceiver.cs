using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaNet.Interfaces;
using KafkaNet.Protocol;
using Metrics;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaQueueAdapterReceiver : IQueueAdapterReceiver
    {
        private Task _currentCommitTask;
        private readonly IManualConsumer _consumer;
        private readonly IKafkaBatchFactory _factory;
        private readonly Logger _logger;
        private readonly KafkaStreamProviderOptions _options;

        // Metrics
        private static readonly Meter MeterConsumedMessagesPerSecond = Metric.Context("KafkaStreamProvider").Meter("Consumed Messages Per Second", Unit.Events);
        private static readonly Histogram HistogramConsumedMessagesPerFetch = Metric.Context("KafkaStreamProvider").Histogram("Consumed Messages Per Fetch", Unit.Custom("Messages"));
        private static readonly Counter CounterActiveReceivers = Metric.Context("KafkaStreamProvider").Counter("Active Receivers", Unit.Custom("Receivers"));
        private static readonly Timer TimerTimeToGetMessageFromKafka = Metric.Context("KafkaStreamProvider").Timer("Time To Get Message From Kafka", Unit.Custom("Fetches"));
        private static readonly Timer TimerTimeToCommitOffset = Metric.Context("KafkaStreamProvider").Timer("Time To Commit Offset", Unit.Custom("Commits"));

        public QueueId Id { get; }

        public long CurrentOffset { get; private set; }

        public KafkaQueueAdapterReceiver(QueueId queueId, IManualConsumer consumer, KafkaStreamProviderOptions options,
            IKafkaBatchFactory factory, Logger logger)
        {            
            // input checks
            if (queueId == null) throw new ArgumentNullException(nameof(queueId));
            if (consumer == null) throw new ArgumentNullException(nameof(consumer));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _options = options;
            Id = queueId;
            _consumer = consumer;
            _factory = factory;
            _logger = logger;
        }

        public async Task Initialize(TimeSpan timeout)
        {
            bool shouldCreate = false;

            try
            {
                if (_options.ShouldInitWithLastOffset)
                {
                    CurrentOffset = await _consumer.FetchLastOffset();
                    _logger.Verbose("KafkaQueueAdapterReceiver - Initialized with latest offset. Offset is {0}", CurrentOffset);
                }
                else
                {
                    CurrentOffset = await _consumer.FetchOffset(_options.ConsumerGroupName);
                    _logger.Verbose("KafkaQueueAdapterReceiver - Initialized with ConsumerGroupOffset offset. ConsumerGroup is {0} Offset is {1}", _options.ConsumerGroupName, CurrentOffset);
                }                    
            }
            catch (KafkaApplicationException ex)
            {
                // This is the error kafka returns when the consumer group doesn't exist
                if (ex.ErrorCode == (int)ErrorResponseCode.UnknownTopicOrPartition)
                {
                    shouldCreate = true;
                }
                else
                {
                    throw;
                }
            }

            if (shouldCreate)
            {
                CurrentOffset = await _consumer.FetchLastOffset();
                await _consumer.UpdateOrCreateOffset(_options.ConsumerGroupName, CurrentOffset);
                _logger.Verbose("KafkaQueueAdapterReceiver - Offset was not found for ConsumerGroup {0}, saved the latest offset for the ConsumerGroup. Offset is {1}", _options.ConsumerGroupName, CurrentOffset);
            }

            CounterActiveReceivers.Increment();
        }

        public async Task<IList<IBatchContainer>> GetQueueMessagesAsync(int maxCount)
        {            
            try
            {
                Task<IEnumerable<Message>> fetchingTask;
                IList<IBatchContainer> batches = new List<IBatchContainer>();

                using (TimerTimeToGetMessageFromKafka.NewContext(Id.ToString()))
                {
                    fetchingTask = _consumer.FetchMessages(maxCount, CurrentOffset);

                    await Task.WhenAny(fetchingTask, Task.Delay(_options.ReceiveWaitTimeInMs));
                }

                // Checking that the task completed successfully
                if (!fetchingTask.IsCompleted)
                {
                    _logger.Warn((int)KafkaErrorCodes.KafkaStreamProviderBase,
                        "KafkaQueueAdapterReceiver - Fetching operation was not completed, tried to fetch {0} messages from offest {1}",
                        maxCount, CurrentOffset);
                    return batches;
                }
                if (fetchingTask.IsFaulted && fetchingTask.Exception != null)
                {
                    _logger.Info(
                        "KafkaQueueAdapterReceiver - Fetching messages from kafka failed, tried to fetch {0} messages from offest {1}",
                        maxCount, CurrentOffset);
                    throw fetchingTask.Exception;
                }
                if (fetchingTask.Result == null)
                {
                    return batches;
                }
                
                var messages = fetchingTask.Result.ToList();
                batches = messages.Select(m => _factory.FromKafkaMessage(m, m.Meta.Offset)).ToList();

                // No batches, we are done here..
                if (batches.Count <= 0) return batches;

                _logger.Verbose("KafkaQueueAdapterReceiver - Pulled {0} messages for queue number {1}", batches.Count, Id.GetNumericId());
                CurrentOffset += batches.Count;

                // Taking a bit of metrics
                MeterConsumedMessagesPerSecond.Mark(Id.ToString(), 1);
                HistogramConsumedMessagesPerFetch.Update(batches.Count, Id.ToString());

                return batches;
            }
            catch (BufferUnderRunException)
            {
                // This case the next message in the queue is too big for us to read, so we skip it
                _logger.Info("KafkaQueueAdapterReceiver - A message in the Kafka queue was too big to pull, skipping over it. offset was {0}", CurrentOffset);
                CurrentOffset++;

                return new List<IBatchContainer>();
            }
        }

        private async Task CommitOffset(long offsetToCommit)
        {
            Task commitTask;

            using (TimerTimeToCommitOffset.NewContext())
            {
                commitTask = _consumer.UpdateOrCreateOffset(_options.ConsumerGroupName, offsetToCommit);
                await Task.WhenAny(commitTask, Task.Delay(_options.ReceiveWaitTimeInMs));
            }

            if (!commitTask.IsCompleted)
            {
                var innerException = commitTask.IsFaulted
                    ? (Exception) commitTask.Exception
                    : new TimeoutException("Commit operation timed out");

                var newException = new KafkaStreamProviderException("Commit offset operation has failed", innerException);

                _logger.Error((int)KafkaErrorCodes.KafkaApplicationError, String.Format(
                    "KafkaQueueAdapterReceiver - Commit offset operation has failed. ConsumerGroup is {0}, offset is {1}",
                    _options.ConsumerGroupName, offsetToCommit), newException);
                throw new KafkaStreamProviderException();
            }

            _logger.Verbose(
                "KafkaQueueAdapterReceiver - Commited an offset to the ConsumerGroup. ConsumerGroup is {0}, offset is {1}",
                _options.ConsumerGroupName, offsetToCommit);            
        }

        public async Task Shutdown(TimeSpan timeout)
        {
            if (_currentCommitTask != null)
            {
                await Task.WhenAny(_currentCommitTask, Task.Delay(timeout));
                _currentCommitTask = null;
                _logger.Verbose("KafkaQueueAdapterReceiver - The receiver had finished a commit and was shutted down");
            }

            CounterActiveReceivers.Decrement();
        }

        public async Task MessagesDeliveredAsync(IList<IBatchContainer> messages)
        {            
            // Finding the highest offset
            if (messages.Any())
            {
                var highestOffset = messages.Max(b => (b.SequenceToken as EventSequenceToken).SequenceNumber);

                // We increment the highest offest since the offest commit represents the next message to be handled.
                await CommitOffset(highestOffset + 1);
            }
        }
    }
}