using Logger.Abstractions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Abstractions
{

    public class Logger<T> : ILogger<T>
    {
        private readonly ILogger _logger;
        public Logger(ILoggerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }


            _logger = factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T)));
        }


        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }


        void ILogger.Log(LogLevel logLevel,string message)
        {
            _logger.Log(logLevel, message);
        }
    }

}
