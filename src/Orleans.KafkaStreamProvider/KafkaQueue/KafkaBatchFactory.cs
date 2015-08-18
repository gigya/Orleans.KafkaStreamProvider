using System;
using System.Collections.Generic;
using KafkaNet.Protocol;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaBatchFactory : IKafkaBatchFactory
    {
        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events)
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, events);
        }

        public Orleans.Streams.IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId)
        {
            return KafkaBatchContainer.FromKafkaMessage(kafkaMessage, sequenceId);
        }

        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent)
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, singleEvent);
        }
    }
}
