using Logger.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.Console
{
    public class ConsoleLoggerProvider : ILoggerProvider 
    { 
        private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers = new ConcurrentDictionary<string, ConsoleLogger>();


        private readonly LogLevel _minLogLevel;
        public ConsoleLoggerProvider(LogLevel minLevel)
        {
            _minLogLevel = minLevel;
        }


        public ILogger CreateLogger(string name)
        { 
            return _loggers.GetOrAdd(name, CreateLoggerImplementation); 
        } 


        private ConsoleLogger CreateLoggerImplementation(string name)
        { 
            return new ConsoleLogger(name,(logLevel)=> logLevel >= _minLogLevel); 
        } 


         public void Dispose()
         { 
         } 
     } 

}
