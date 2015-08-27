using System;
using System.Collections.Generic;
using KafkaNet.Protocol;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaBatchFactory : IKafkaBatchFactory
    {
        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext)
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, events, requestContext);
        }

        public Orleans.Streams.IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId)
        {
            return KafkaBatchContainer.FromKafkaMessage(kafkaMessage, sequenceId);
        }

        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent, Dictionary<string,object> requestContext )
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, singleEvent, requestContext);
        }
    }
}
