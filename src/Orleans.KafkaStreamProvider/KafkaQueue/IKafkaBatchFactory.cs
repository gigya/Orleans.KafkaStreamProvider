using System;
using System.Collections.Generic;
using KafkaNet.Protocol;
using Orleans.Streams;
using Orleans.Serialization;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public interface IKafkaBatchFactory
    {
        IEnumerable<Message> ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext, SerializationManager serializationManager);

        Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent, Dictionary<string, object> requestContext, SerializationManager serializationManager);

        IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId, SerializationManager serializationManager);
    }
}
