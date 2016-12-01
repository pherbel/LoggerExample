using Logger.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.File
{
    public class FileLoggerProvider :ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new ConcurrentDictionary<string, FileLogger>();

        private readonly LogLevel _minLogLevel;
        public FileLoggerProvider(LogLevel minLevel)
        {
            _minLogLevel = minLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return CreateLoggerImplementation(categoryName);
        }
        private FileLogger CreateLoggerImplementation(string name)
        {
            return new FileLogger(name, (logLevel) => logLevel >= _minLogLevel);
        }
        public void Dispose()
        {

        }
    }
}
