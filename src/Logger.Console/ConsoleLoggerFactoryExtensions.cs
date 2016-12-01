using Logger.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Console
{
    public static class ConsoleLoggerFactoryExtensions
    {

        public static ILoggerFactory AddConsole(this ILoggerFactory factory)
        {
            return factory.AddConsole(LogLevel.Info);
        }

        public static ILoggerFactory AddConsole(this ILoggerFactory factory, LogLevel minLevel)
        {
            factory.AddProvider(new ConsoleLoggerProvider(minLevel));
            return factory;
        }

    }
}
