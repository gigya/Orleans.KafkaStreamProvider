using System;
using System.Collections.Generic;
using KafkaNet.Protocol;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public interface IKafkaBatchFactory
    {
        Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events);

        Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent);

        IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId);
    }
}
