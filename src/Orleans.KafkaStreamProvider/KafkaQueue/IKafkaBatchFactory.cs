using System;
using System.Collections.Generic;
using KafkaNet.Protocol;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public interface IKafkaBatchFactory
    {
        Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext);

        Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent, Dictionary<string, object> requestContext);

        IBatchContainer FromKafkaMessage(Message kafkaMessage, long sequenceId);
    }
}
