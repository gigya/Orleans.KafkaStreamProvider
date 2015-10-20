using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
