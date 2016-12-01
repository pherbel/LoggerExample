using Logger.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.File
{
    public static class FileLoggerFactoryExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory)
        {
            return factory.AddFile(LogLevel.Info);
        }

        public static ILoggerFactory AddFile(this ILoggerFactory factory, LogLevel minLevel)
        {
            factory.AddProvider(new FileLoggerProvider(minLevel));
            return factory;
        }
    }
}
