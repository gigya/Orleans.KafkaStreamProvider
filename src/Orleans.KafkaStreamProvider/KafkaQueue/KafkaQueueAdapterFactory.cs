using System;
using System.Threading.Tasks;
using Metrics;
using Orleans.KafkaStreamProvider.KafkaQueue.TimedQueueCache;
using Orleans.Providers;
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
        private KafkaQueueAdapter _adapter;        

        public void Init(IProviderConfiguration config, string providerName, Logger logger, IServiceProvider serviceProvider)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (String.IsNullOrEmpty(providerName)) throw new ArgumentNullException(nameof(providerName));

            // Creating an options object with all the config values
            _options = new KafkaStreamProviderOptions(config);

            if (!_options.UsingExternalMetrics)
            {
                Metric.Config.WithHttpEndpoint($"http://localhost:{_options.MetricsPort}/");
            }

            if (!_options.IncludeMetrics)
            {
                Metric.Context("KafkaStreamProvider").Advanced.CompletelyDisableMetrics();
            }

            _providerName = providerName;
            _streamQueueMapper = new HashRingBasedStreamQueueMapper(_options.NumOfQueues, providerName);
            _logger = logger;
            _adapter = new KafkaQueueAdapter(_streamQueueMapper, _options, providerName, new KafkaBatchFactory(), _logger);
            _adapterCache = new TimedQueueAdapterCache(this, TimeSpan.FromSeconds(_options.CacheTimespanInSeconds), _options.CacheSize, _options.CacheNumOfBuckets, logger);
        }

        public Task<IQueueAdapter> CreateAdapter()
        {
            if (_adapter == null)
            {
                _adapter = new KafkaQueueAdapter(_streamQueueMapper, _options, _providerName,
                    new KafkaBatchFactory(), _logger);
            }

            return Task.FromResult<IQueueAdapter>(_adapter);
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