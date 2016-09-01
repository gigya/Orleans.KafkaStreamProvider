using System;
using System.Runtime.Serialization;

namespace Orleans.KafkaStreamProvider
{
    [Serializable]
    public class KafkaStreamProviderException : Exception
    {
        public KafkaStreamProviderException()
        {
        }

        public KafkaStreamProviderException(string message) 
            : base(message)
        {
        }

        public KafkaStreamProviderException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected KafkaStreamProviderException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
