using System;
using System.Linq;
using System.Threading.Tasks;
using Orleans.Providers;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Orleans.KafkaStreamProvider.KafkaQueue
{
    /// <summary>
    /// Factory class for KafkaQueueAdapter.
    /// </summary>
    public class KafkaQueueAdapterFactory : IQueueAdapterFactory
    {
        private KafkaStreamProviderOptions _options;
        private HashRingBasedStreamQueueMapper _streamQueueMapper;
        private IQueueAdapterCache _adapterCache;
        private string _providerName;
        private Logger _logger;

        public void Init(IProviderConfiguration config, string providerName, Logger logger)
        {
            if (config == null) throw new ArgumentNullException("config");            
            if (logger == null) throw new ArgumentNullException("logger");
            if (String.IsNullOrEmpty(providerName)) throw new ArgumentNullException("providerName");

            // Creating an options object with all the config values
            _options = new KafkaStreamProviderOptions(config);
            
            _providerName = providerName;
            _streamQueueMapper = new HashRingBasedStreamQueueMapper(_options.NumOfQueues, providerName);
            _adapterCache = new SimpleQueueAdapterCache(this, _options.CacheSize, logger);
            _logger = logger;
        }

        public Task<IQueueAdapter> CreateAdapter()
        {
            var adapter = new KafkaQueueAdapter(_streamQueueMapper, _options, _providerName,
                new KafkaBatchFactory(), _logger);
            return Task.FromResult<IQueueAdapter>(adapter);
        }

        /// <summary>
        /// Creates a delivery failure handler for the specified queue.
        /// </summary>
        /// <param name="queueId"></param>
        /// <returns></returns>
        public Task<IStreamFailureHandler> GetDeliveryFailureHandler(QueueId queueId)
        {
            return Task.FromResult<IStreamFailureHandler>(new NoOpStreamDeliveryFailureHandler(false));
        }

        public IQueueAdapterCache GetQueueAdapterCache()
        {
            return _adapterCache;
        }

        public IStreamQueueMapper GetStreamQueueMapper()
        {
            return _streamQueueMapper;
        }
    }
}