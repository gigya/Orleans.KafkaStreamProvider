﻿using System;
using System.Collections.Generic;
using System.Linq;
using KafkaNet.Protocol;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Serialization;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    [Serializable]
    public class KafkaBatchContainer : IBatchContainer
    {
        private EventSequenceToken _sequenceToken;
        private readonly List<object> _events;
        private readonly Dictionary<string, object> _requestContext;

        public Dictionary<string, object> BatchRequestContext => _requestContext;

        public Guid StreamGuid { get; }

        public string StreamNamespace { get; }

        public StreamSequenceToken SequenceToken => _sequenceToken;

        public string Timestamp { get; private set; }

        private KafkaBatchContainer(Guid streamId, string streamNamespace, List<object> events, Dictionary<string, object> requestContext)
        {
            if (events == null) throw new ArgumentNullException(nameof(events), "Message contains no events");

            StreamGuid = streamId;
            StreamNamespace = streamNamespace;
            _events = events;
            _requestContext = requestContext;
            Timestamp = DateTime.UtcNow.ToString("O");
        }

        private KafkaBatchContainer(Guid streamId, string streamNamespace, object singleEvent, Dictionary<string, object> requestContext)
        {
            if (singleEvent == null) throw new ArgumentNullException(nameof(singleEvent));

            StreamGuid = streamId;
            StreamNamespace = streamNamespace;
            _events = new List<object>(1){singleEvent};
            _requestContext = requestContext;
            Timestamp = DateTime.UtcNow.ToString("O");
        }
        
        public IEnumerable<Tuple<T, StreamSequenceToken>> GetEvents<T>()
        {
            // Get events of the wanted type
            var typedEvents = _events.OfType<T>();

            // returning the tuple with a unique SequenceToken for each event
            return
                typedEvents.Select(
                    (e, i) => Tuple.Create<T, StreamSequenceToken>(e, _sequenceToken.CreateSequenceTokenForEvent(i)));
        }

        public bool ShouldDeliver(IStreamIdentity stream, object filterData, StreamFilterPredicate shouldReceiveFunc)
        {
            return _events.Any(item => shouldReceiveFunc(stream, filterData, item));
        }

        internal static Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, IEnumerable<T> events, Dictionary<string, object> requestContext, SerializationManager serializationManager)
        {
            KafkaBatchContainer container = new KafkaBatchContainer(streamId, streamNamespace, events.Cast<object>().ToList(), requestContext);
            var rawBytes = serializationManager.SerializeToByteArray(container);
            Message message = new Message(){ Value = rawBytes };

            return message;
        }

        internal static Message ToKafkaMessage<T>(Guid streamId, string streamNamespace, T singleEvent, Dictionary<string, object> requestContext, SerializationManager serializationManager)
        {
            KafkaBatchContainer container = new KafkaBatchContainer(streamId, streamNamespace, singleEvent, requestContext);
            var rawBytes = serializationManager.SerializeToByteArray(container);
            Message message = new Message() { Value = rawBytes };

            return message;
        }

        internal static KafkaBatchContainer FromKafkaMessage(Message message, long sequenceId, SerializationManager serializationManager)
        {
            var kafkaBatch = serializationManager.DeserializeFromByteArray<KafkaBatchContainer>(message.Value);
            kafkaBatch._sequenceToken = new EventSequenceToken(sequenceId);

            return kafkaBatch;
        }

        public bool ImportRequestContext()
        {
            if (_requestContext != null)
            {                
                RequestContext.Import(_requestContext);
                return true;
            }
            return false;
        }
    }
}