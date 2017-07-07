using System;
using System.Collections.Generic;
using KafkaNet.Protocol;
using Orleans.Serialization;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaBatchFactory : IKafkaBatchFactory
    {
        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext, SerializationManager serializationManager)
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, events, requestContext, serializationManager);
        }

        public Orleans.Streams.IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId, SerializationManager serializationManager)
        {
            return KafkaBatchContainer.FromKafkaMessage(kafkaMessage, sequenceId, serializationManager);
        }

        public Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent, Dictionary<string,object> requestContext, SerializationManager serializationManager)
        {
            return KafkaBatchContainer.ToKafkaMessage(streamId, streamNamespace, singleEvent, requestContext, serializationManager);
        }
    }
}
