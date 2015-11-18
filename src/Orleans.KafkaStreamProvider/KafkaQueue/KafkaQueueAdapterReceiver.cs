using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KafkaNet.Interfaces;
using KafkaNet.Protocol;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaQueueAdapterReceiver : IQueueAdapterReceiver
    {
        private Task _currentCommitTask;
        private long _lastOffset;
        private readonly IManualConsumer _consumer;
        private readonly IKafkaBatchFactory _factory;
        private readonly Logger _logger;
        private readonly KafkaStreamProviderOptions _options;

        public QueueId Id { get; private set; }

        public long CurrentOffset { get { return _lastOffset; } }

        public KafkaQueueAdapterReceiver(QueueId queueId, IManualConsumer consumer, KafkaStreamProviderOptions options,
            IKafkaBatchFactory factory, Logger logger)
        {
            // input checks
            if (queueId == null) throw new ArgumentNullException("queueId");
            if (consumer == null) throw new ArgumentNullException("consumer");
            if (factory == null) throw new ArgumentNullException("factory");
            if (options == null) throw new ArgumentNullException("options");
            if (logger == null) throw new ArgumentNullException("logger");

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
                    _lastOffset = await Task.Run(() => _consumer.FetchLastOffset());
                    _logger.Verbose("KafkaQueueAdapterReceiver - Initialized with latest offset. Offset is {0}", _lastOffset);
                }
                else
                {
                    _lastOffset = await Task.Run(() => _consumer.FetchOffset(_options.ConsumerGroupName));
                    _logger.Verbose("KafkaQueueAdapterReceiver - Initialized with ConsumerGroupOffset offset. ConsumerGroup is {0} Offset is {1}", _options.ConsumerGroupName, _lastOffset);
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
                _lastOffset = await Task.Run(() => _consumer.FetchLastOffset());
                await Task.Run(() => _consumer.UpdateOrCreateOffset(_options.ConsumerGroupName, _lastOffset));
                _logger.Verbose("KafkaQueueAdapterReceiver - Offset was not found for ConsumerGroup {0}, saved the latest offset for the ConsumerGroup. Offset is {1}", _options.ConsumerGroupName, _lastOffset);
            }
        }

        public async Task<IList<IBatchContainer>> GetQueueMessagesAsync(int maxCount)
        {            
            try
            {            
                var task = _consumer.FetchMessages(maxCount, _lastOffset);

                await Task.Run(() => Task.WaitAny(task, Task.Delay(_options.ReceiveWaitTimeInMs)));
              
                // Checking that the task completed successfully
                if (!task.IsCompleted)
                {
                    _logger.Verbose("KafkaQueueAdapterReceiver - Fetching operation was not completed, tried to fetch {0} messages from offest {1}", maxCount, _lastOffset);
                    return new List<IBatchContainer>();
                }
                if (task.IsFaulted && task.Exception != null)
                {
                    _logger.Info("KafkaQueueAdapterReceiver - Fetching messages from kafka failed, tried to fetch {0} messages from offest {1}", maxCount, _lastOffset);
                    throw task.Exception;
                }
                if (task.Result == null)
                {
                    return new List<IBatchContainer>();
                }

                var messages = task.Result.ToList();
                var batches = messages.Select(m => _factory.FromKafkaMessage(m, m.Meta.Offset)).ToList();

                if (batches.Count > 0)
                {
                    _logger.Verbose("KafkaQueueAdapterReceiver - Pulled {0} messages for queue number {1}", batches.Count, Id.GetNumericId());
                    _lastOffset += batches.Count;                
                }               

                return batches;
            }
            catch (BufferUnderRunException)
            {
                // This case the next message in the queue is too big for us to read, so we skip it
                _logger.Info("KafkaQueueAdapterReceiver - A message in the Kafka queue was too big to pull, skipping over it. offset was {0}", _lastOffset);
                _lastOffset++;

                return new List<IBatchContainer>();
            }
        }

        private async Task CommitOffset(long offsetToCommit)
        {
            var commitTask = Task.Run(() => _consumer.UpdateOrCreateOffset(_options.ConsumerGroupName, offsetToCommit));
            await Task.WhenAny(commitTask, Task.Delay(_options.ReceiveWaitTimeInMs));

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
        }

        public async Task MessagesDeliveredAsync(IList<IBatchContainer> messages)
        {            
            // Finding the higest offset
            if (messages.Any())
            {
                var highestOffset = messages.Max(b => (b.SequenceToken as EventSequenceToken).SequenceNumber);
                await CommitOffset(highestOffset);
            }
        }
    }
}