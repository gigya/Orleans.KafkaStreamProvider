using System;
using KafkaNet;
using Orleans.Runtime;

namespace Orleans.KafkaStreamProvider
{
    internal enum KafkaErrorCodes
    {
        KafkaStreamProviderBase = 15000,
        KafkaApplicationInfo = KafkaStreamProviderBase + 1,
        KafkaApplicationWarning = KafkaStreamProviderBase + 2,
        KafkaApplicationError = KafkaStreamProviderBase + 3,
        KafkaApplicationFatalError = KafkaStreamProviderBase + 4,
        KafkaApplicationDebugInfo = KafkaStreamProviderBase + 5
    }

    /// <summary>
    /// This class wraps an Orleans logger for kafka-net to use
    /// </summary>
    internal class KafkaLogBridge : IKafkaLog
    {
        private readonly Logger _orleansLogger;

        public KafkaLogBridge(Logger orleansLogger)
        {
            if (orleansLogger == null) throw new ArgumentNullException("orleansLogger");

            _orleansLogger = orleansLogger;
        }

        public void DebugFormat(string format, params object[] args)
        {
            _orleansLogger.Verbose((int)KafkaErrorCodes.KafkaApplicationDebugInfo, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            _orleansLogger.Error((int)KafkaErrorCodes.KafkaApplicationError, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            var message = string.Format(format, args);
            _orleansLogger.Error((int) KafkaErrorCodes.KafkaApplicationFatalError, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _orleansLogger.Info((int)KafkaErrorCodes.KafkaApplicationInfo, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _orleansLogger.Warn((int)KafkaErrorCodes.KafkaApplicationWarning, format, args);
        }
    }
}
