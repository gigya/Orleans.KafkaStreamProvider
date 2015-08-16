using System;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    public class KafkaMessage
    {
        public byte[] Payload { get; private set; }

        public KafkaMessage(byte[] payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException("payload");
            }

            Payload = payload;
        }
    }
}