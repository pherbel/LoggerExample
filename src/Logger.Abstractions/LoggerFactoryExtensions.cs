using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logger.Abstractions.Internal;

namespace Logger.Abstractions
{
    public static class LoggerFactoryExtensions
    {
        public static ILogger<T> CreateLogger<T>(this ILoggerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            return new Logger<T>(factory);
        }

        public static ILogger CreateLogger(this ILoggerFactory factory, Type type)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }


            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }


            return factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(type));
        }

    }
}
