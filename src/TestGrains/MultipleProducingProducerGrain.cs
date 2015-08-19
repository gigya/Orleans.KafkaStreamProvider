using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using TestGrainInterfaces;

namespace TestGrains
{
    [Serializable]
    public class StreamKey : IStreamIdentity, IEquatable<StreamKey>
    {
        public StreamKey(Guid streamGuid, string streamNamespace)
        {
            Guid = streamGuid;
            Namespace = streamNamespace;
        }

        public Guid Guid { get; private set; }

        public string Namespace { get; private set; }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StreamKey);
        }

        public bool Equals(StreamKey other)
        {
            return other != null && Guid == other.Guid && Namespace == other.Namespace;
        }
    }


    class MultipleProducingProducerGrain : Grain, IMultipleProducingProducerGrain
    {
        private readonly Dictionary<IStreamIdentity,IAsyncStream<int>> _producers = new Dictionary<IStreamIdentity, IAsyncStream<int>>();
        private readonly Dictionary<IStreamIdentity,int> _numProduced = new Dictionary<IStreamIdentity, int>();
        private Dictionary<IStreamIdentity, IDisposable> _producerTimers = new Dictionary<IStreamIdentity, IDisposable>();
        internal Logger _logger;

        public override Task OnActivateAsync()
        {
            _logger = base.GetLogger("SampleStreaming_ProducerGrain " + base.IdentityString);
            _logger.Info("OnActivateAsync");            
            return TaskDone.Done;
        }

        public Task<Orleans.Streams.IStreamIdentity> BecomeProducer(Guid streamId, string streamNamespace, string providerToUse)
        {               
            _logger.Info("BecomeProducer");

            var newStreamKey = new StreamKey(streamId, streamNamespace);

            if (_producers.ContainsKey(newStreamKey))
            {
                _logger.Info("Already a producer");
            }
            else
            {
                IStreamProvider streamProvider = base.GetStreamProvider(providerToUse);
                var newStream = streamProvider.GetStream<int>(streamId, streamNamespace);
                _producers.Add(newStreamKey, newStream);
                _numProduced.Add(newStreamKey, 0);
            }

            return Task.FromResult<IStreamIdentity>(newStreamKey);
        }

        public Task StartPeriodicProducing(Orleans.Streams.IStreamIdentity identity)
        {
            _logger.Info("StartPeriodicProducing");

            if (_producerTimers.ContainsKey(identity))
            {
                _logger.Info("Already producing");
            }
            else
            {
                var timer = base.RegisterTimer(TimerCallback, identity, TimeSpan.Zero, TimeSpan.FromMilliseconds(1));
                _producerTimers.Add(identity, timer);   
            }

            return TaskDone.Done;
        }

        private Task TimerCallback(object state)
        {
            IStreamIdentity key = (IStreamIdentity) state;
            return _producerTimers[key] != null ? Fire(key) : TaskDone.Done;
        }

        private Task Fire(IStreamIdentity identity, [CallerMemberName] string caller = null)
        {
            _numProduced[identity]++;
            _logger.Info("{0} (item={1})", caller, _numProduced[identity]);
            return _producers[identity].OnNextAsync(_numProduced[identity]);
        }

        public Task StopPeriodicProducing(Orleans.Streams.IStreamIdentity identity)
        {
            _logger.Info("StopPeriodicProducing");

            if (!_producerTimers.ContainsKey(identity))
            {
                _logger.Info("Not producing anything");
            }
            else
            {
                _producerTimers[identity].Dispose();
                _producerTimers[identity] = null;
                _producerTimers.Remove(identity);
            }
            return TaskDone.Done;
        }

        public Task<Dictionary<Orleans.Streams.IStreamIdentity, int>> GetNumberProduced()
        {
            return Task.FromResult(_numProduced);
        }

        public Task ClearNumberProduced()
        {
            foreach (var key in _numProduced.Keys)
            {
                _numProduced[key] = 0;
            }

            return TaskDone.Done;
        }

        public Task Produce(IStreamIdentity identifier)
        {
            return Fire(identifier);
        }
    }
}
